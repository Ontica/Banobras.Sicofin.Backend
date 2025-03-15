/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : PYC Suppliers Integration            *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Data Service                         *
*  Type     : SuppliersMatcherData                          License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Data services for PYC-SICOFIN suppliers matching.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Data;

namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC {

  /// <summary>Data services for PYC-SICOFIN suppliers matching.</summary>
  static internal class SuppliersMatcherData {

    static internal FixedList<PYCSupplier> GetPYCSuppliers() {
      var sql = "SELECT * FROM Z_PROVEEDORES_UTILIZADOS";

      var op = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<PYCSupplier>(op);
    }


    static internal FixedList<SicofinSupplier> GetSicofinSuppliers() {
      var sql = "SELECT * FROM Z_AUXILIARES_UTILIZADOS";

      var op = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<SicofinSupplier>(op);
    }

  }  // class SuppliersMatcherData

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC
