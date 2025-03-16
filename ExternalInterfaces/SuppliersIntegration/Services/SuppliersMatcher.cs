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

    #region Helpers

    private void CleanPYCData() {
      foreach (PYCSupplier supplier in pycSuppliers) {
        supplier.CleanName = EmpiriaStringDistance.PrepareForDistance(supplier.Name);
        supplier.KeywordsTags = EmpiriaStringDistance.KeywordsForDistance(supplier.CleanName);
        supplier.MatchId = -1;

        SuppliersMatcherData.Write(supplier);
      }
    }


    private void CleanSicofinData() {

      foreach (SicofinSupplier supplier in sicofinSuppliers) {
        supplier.CleanName = EmpiriaStringDistance.PrepareForDistance(supplier.Name);
        supplier.KeywordsTags = EmpiriaStringDistance.KeywordsForDistance(supplier.CleanName);
        supplier.MatchId = -1;
        supplier.ProximityFactor = 0;

        SuppliersMatcherData.Write(supplier);
      }
    }


    private void LoadData() {
      pycSuppliers = SuppliersMatcherData.GetPYCSuppliers();
      sicofinSuppliers = SuppliersMatcherData.GetSicofinSuppliers();
    }

    #endregion Helpers

  }  // class SuppliersMatcher

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC
