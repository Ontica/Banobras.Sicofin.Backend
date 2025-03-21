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

      ProcessExactMatchData(600);

      ProcessSubledgerAccountsMatchData(399, 75, false);
      ProcessSubledgerAccountsMatchData(399, 50, true);

      ProcessAccountNamesMatchData(199, 75, false);
      ProcessAccountNamesMatchData(199, 50, true);
    }

    #region Helpers

    private decimal CalculateProximityFactor(string stringA, string stringB, bool tokenBased) {

      if (tokenBased) {
        return EmpiriaStringDistance.JaccardProximityFactor(stringA, stringB) * 100;

      } else {
        return EmpiriaStringDistance.MongeElkanProximityFactor(EmpiriaStringDistance.DistanceAlgorithm.Levenshtein,
                                                               stringA, stringB) * 100;
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


    private SicofinSupplier GetSicofinSupplier(int subledgerAccountId) {
      return sicofinSuppliers.Find(x => x.SubledgerAccountId == subledgerAccountId);
    }



    private FixedList<PYCSupplier> GetUnmatchedPYCSuppliers(int groupOffset) {
      return pycSuppliers.FindAll(x => x.MatchId == -1)
                         .Sort((x, y) => x.KeywordsTags.Length.CompareTo(y.KeywordsTags.Length))
                         .Reverse();
    }



    private FixedList<SicofinSupplier> GetUnmatchedSicofinSuppliers() {
      return sicofinSuppliers.FindAll(x => x.MatchId == -1)
                             .Sort((x, y) => x.KeywordsTags.Length.CompareTo(y.KeywordsTags.Length))
                             .Reverse();
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

        StoreMatch(pycSupplier, sicofinSupplier, groupOffset);
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

        StoreMatch(pycSupplier, sicofinSupplier, groupOffset + proximityFactor);
      }
    }


    private void ProcessAccountNamesMatchData(int groupOffset, int minFactor, bool tokenBased) {

      foreach (SicofinSupplier sicofinSupplier in GetUnmatchedSicofinSuppliers()) {

        TryMatchBestPYCSupplier(sicofinSupplier, groupOffset, minFactor, tokenBased);

      }
    }


    private void StoreMatch(PYCSupplier pycSupplier, SicofinSupplier sicofinSupplier, decimal proximityFactor) {

      pycSupplier.MatchId = sicofinSupplier.SubledgerAccountId;
      sicofinSupplier.MatchId = pycSupplier.AssignedId;
      sicofinSupplier.ProximityFactor = proximityFactor;

      SuppliersMatcherData.Write(pycSupplier);
      SuppliersMatcherData.Write(sicofinSupplier);
    }


    private void TryMatchBestPYCSupplier(SicofinSupplier sicofinSupplier, int groupOffset, int minFactor, bool tokenBased) {

      PYCSupplier pycBestMatch = null;
      decimal pycBestMatchFactor = 0;

      foreach (PYCSupplier pycSupplier in GetUnmatchedPYCSuppliers(groupOffset)) {

        decimal proximityFactor = CalculateProximityFactor(sicofinSupplier.KeywordsTags,
                                                           pycSupplier.KeywordsTags, tokenBased);

        if (proximityFactor < minFactor) {
          continue;
        }

        if (pycBestMatchFactor < proximityFactor) {
          pycBestMatch = pycSupplier;
          pycBestMatchFactor = proximityFactor;
        }

      }  // foreach

      if (pycBestMatch == null) {
        return;
      }

      SicofinSupplier toRematch = TryUnmatchCurrentSuppliersIfNeeded(sicofinSupplier,
                                                                     groupOffset + pycBestMatchFactor);

      StoreMatch(pycBestMatch, sicofinSupplier, groupOffset + pycBestMatchFactor);

      if (toRematch != null) {
        TryMatchBestPYCSupplier(toRematch, groupOffset, minFactor, tokenBased);
      }
    }


    private SicofinSupplier TryUnmatchCurrentSuppliersIfNeeded(SicofinSupplier sicofinSupplier,
                                                               decimal proximityFactor) {
      var pycSupplier = pycSuppliers.Find(x => x.MatchId == sicofinSupplier.SubledgerAccountId);

      if (pycSupplier == null) {
        return null;
      }

      SicofinSupplier sicofinSupplierToUnmatch = GetSicofinSupplier(pycSupplier.MatchId);

      if (sicofinSupplierToUnmatch.ProximityFactor > proximityFactor) {
        return null;
      }

      sicofinSupplier.MatchId = -1;
      sicofinSupplier.ProximityFactor = 0;
      SuppliersMatcherData.Write(sicofinSupplierToUnmatch);

      pycSupplier.MatchId = -1;
      SuppliersMatcherData.Write(pycSupplier);

      return sicofinSupplierToUnmatch;
    }

    #endregion Helpers

  }  // class SuppliersMatcher

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC
