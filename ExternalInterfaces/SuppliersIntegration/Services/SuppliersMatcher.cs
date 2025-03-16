/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : PYC Suppliers Integration            *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Service provider                     *
*  Type     : SuppliersMatcher                              License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Service used to match PYC suppliers with their corresponding subledger accounts in SICOFIN.    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC {

  /// <summary>Service used to match PYC suppliers with their corresponding
  /// subledger accounts in SICOFIN.</summary>
  public class SuppliersMatcher {

    private FixedList<PYCSupplier> pycSuppliers;
    private FixedList<SicofinSupplier> sicofinSuppliers;


    public void CleanData() {
      LoadData();
      CleanPYCData();
      CleanSicofinData();
    }


    public void Match() {
      LoadData();
      ProcessExactMatchData(400);
      ProcessSubledgerAccountsMatchData(300, 75, false);
      ProcessSubledgerAccountsMatchData(300, 50, true);
      ProcessAccountNamesMatchData(100, 75, false);
      ProcessAccountNamesMatchData(100, 50, true);
    }

    #region Helpers

    private decimal CalculateProximityFactor(string stringA, string stringB, bool tokenBased) {
      if (tokenBased) {
        return TokenBasedDistance.Jaccard(stringA, stringB) * 100;
      } else {
        return EmpiriaStringDistance.DamerauLevenshteinProximityFactor(stringA, stringB) * 100;
      }
    }


    private void CleanPYCData() {
      foreach (PYCSupplier supplier in pycSuppliers) {
        supplier.CleanName = EmpiriaStringDistance.PrepareForDistance(supplier.Name.Replace(".", string.Empty));
        supplier.KeywordsTags = EmpiriaStringDistance.KeywordsForDistance(supplier.CleanName);
        supplier.MatchId = -1;

        SuppliersMatcherData.Write(supplier);
      }
    }


    private void CleanSicofinData() {

      foreach (SicofinSupplier supplier in sicofinSuppliers) {
        supplier.CleanName = EmpiriaStringDistance.PrepareForDistance(supplier.Name.Replace(".", string.Empty));
        supplier.KeywordsTags = EmpiriaStringDistance.KeywordsForDistance(supplier.CleanName);
        supplier.MatchId = -1;
        supplier.ProximityFactor = 0;

        SuppliersMatcherData.Write(supplier);
      }
    }


    private FixedList<PYCSupplier> GetUnmatchedPYCSuppliers() {
      return pycSuppliers.FindAll(x => x.MatchId == -1);
    }


    private FixedList<SicofinSupplier> GetUnmatchedSicofinSuppliers() {
      return sicofinSuppliers.FindAll(x => x.MatchId == -1);
    }


    private void LoadData() {
      pycSuppliers = SuppliersMatcherData.GetPYCSuppliers();
      sicofinSuppliers = SuppliersMatcherData.GetSicofinSuppliers();
    }


    private void ProcessExactMatchData(int groupOffset) {

      foreach (SicofinSupplier sicofinSupplier in GetUnmatchedSicofinSuppliers()) {
        PYCSupplier pycSupplier = pycSuppliers.Find(x => x.SubledgerAccountNo == sicofinSupplier.SubledgerAccountNo &&
                                                         x.KeywordsTags == sicofinSupplier.KeywordsTags);
        if (pycSupplier == null) {
          continue;
        }

        pycSupplier.MatchId = sicofinSupplier.SubledgerAccountId;

        sicofinSupplier.MatchId = pycSupplier.AssignedId;
        sicofinSupplier.ProximityFactor = groupOffset;

        SuppliersMatcherData.Write(pycSupplier);
        SuppliersMatcherData.Write(sicofinSupplier);
      }
    }


    private void ProcessSubledgerAccountsMatchData(int groupOffset, int minFactor, bool tokenBased) {

      foreach (SicofinSupplier sicofinSupplier in GetUnmatchedSicofinSuppliers()) {

        PYCSupplier pycSupplier = pycSuppliers.Find(x => x.SubledgerAccountNo == sicofinSupplier.SubledgerAccountNo);

        if (pycSupplier == null) {
          continue;
        }

        decimal proximityFactor = CalculateProximityFactor(sicofinSupplier.KeywordsTags,
                                                           pycSupplier.KeywordsTags, tokenBased);


        if (proximityFactor < minFactor) {
          continue;
        }

        pycSupplier.MatchId = sicofinSupplier.SubledgerAccountId;
        sicofinSupplier.MatchId = pycSupplier.AssignedId;
        sicofinSupplier.ProximityFactor = groupOffset + proximityFactor;

        SuppliersMatcherData.Write(pycSupplier);
        SuppliersMatcherData.Write(sicofinSupplier);
      }
    }


    private void ProcessAccountNamesMatchData(int groupOffset, int minFactor, bool tokenBased) {

      foreach (SicofinSupplier sicofinSupplier in GetUnmatchedSicofinSuppliers()) {

        PYCSupplier bestMatch = null;

        foreach (PYCSupplier pycSupplier in GetUnmatchedPYCSuppliers()) {

          decimal proximityFactor = CalculateProximityFactor(sicofinSupplier.KeywordsTags,
                                                             pycSupplier.KeywordsTags, tokenBased);

          if (proximityFactor < minFactor) {
            continue;
          }

          if (bestMatch == null || bestMatch.ProximityFactor < proximityFactor) {
            bestMatch = pycSupplier;
            bestMatch.ProximityFactor = proximityFactor;
          }

        }  // foreach

        if (bestMatch == null) {
          continue;
        }

        bestMatch.MatchId = sicofinSupplier.SubledgerAccountId;
        sicofinSupplier.MatchId = bestMatch.AssignedId;
        sicofinSupplier.ProximityFactor = groupOffset + bestMatch.ProximityFactor;

        SuppliersMatcherData.Write(bestMatch);
        SuppliersMatcherData.Write(sicofinSupplier);

      }  // foreach
    }

    #endregion Helpers

  }  // class SuppliersMatcher

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC
