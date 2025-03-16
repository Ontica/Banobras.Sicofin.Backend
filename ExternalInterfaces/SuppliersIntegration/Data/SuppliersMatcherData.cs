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


    static internal void Write(PYCSupplier supplier) {
      var sql = "UPDATE Z_PROVEEDORES_UTILIZADOS " +
               $"SET NUEVO_NOMBRE = '{supplier.CleanName}', " +
               $"KEYWORDS_TAGS = '{supplier.KeywordsTags}', " +
               $"MATCH_ID = {supplier.MatchId} " +
               $"WHERE PRV_ID = {supplier.AssignedId}";

      var op = DataOperation.Parse(sql);

      DataWriter.Execute(op);
    }


    static internal void Write(SicofinSupplier supplier) {
      var sql = "UPDATE Z_AUXILIARES_UTILIZADOS " +
                $"SET NUEVO_NOMBRE = '{supplier.CleanName}', " +
                $"KEYWORDS_TAGS = '{supplier.KeywordsTags}', " +
                $"MATCH_ID = {supplier.MatchId}, " +
                $"PROXIMITY_FACTOR = {supplier.ProximityFactor} " +
                $"WHERE ID_CUENTA_AUXILIAR = {supplier.SubledgerAccountId}";

      var op = DataOperation.Parse(sql);

      DataWriter.Execute(op);
    }

  }  // class SuppliersMatcherData

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC
