/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : Vouchers Importer                    *
*  Assembly : FinancialAccounting.BalanceEngine.dll         Pattern   : Data Service                         *
*  Type     : DbVouchersImporterDataService                 License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Data methods used to read and write data for vouchers importation data tables.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Data;

using Empiria.FinancialAccounting.BanobrasIntegration.TransactionSlips;
using Empiria.FinancialAccounting.BanobrasIntegration.VouchersImporter.Adapters;

namespace Empiria.FinancialAccounting.BanobrasIntegration.VouchersImporter {

  /// <summary>Data methods used to read and write data for vouchers importation data tables.</summary>
  static internal class DbVouchersImporterDataService {

    static internal void DeleteTransactionSlips(ImportationSetID importationSetID) {
      var op = DataOperation.Parse("do_deleteTransactionSlips",
                                   importationSetID.IdSistema,
                                   importationSetID.TipoContabilidad,
                                   importationSetID.FechaAfectacion);
      DataWriter.Execute(op);
    }


    static internal List<Encabezado> GetEncabezados(string importationSetUID) {
      string filter = GetVouchersFilter(importationSetUID, true);

      var sql = "SELECT * FROM VW_MC_ENCABEZADOS " +
                "WHERE " + filter;

      var op = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectList<Encabezado>(op);
    }


    static internal List<Movimiento> GetMovimientos(string importationSetUID) {
      string filter = GetVouchersFilter(importationSetUID, false);

      var sql = "SELECT * FROM VW_MC_MOVIMIENTOS " +
                "WHERE " + filter;

      var op = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectList<Movimiento>(op);
    }


    static internal ImportVouchersResult GetEncabezadosTotals() {
      var sql = "SELECT ENC_SISTEMA, ENC_TIPO_CONT, ENC_FECHA_VOL, COUNT(*) AS TOTAL " +
                "FROM VW_MC_ENCABEZADOS " +
                "GROUP BY ENC_SISTEMA, ENC_TIPO_CONT, ENC_FECHA_VOL";

      var op = DataOperation.Parse(sql);

      var view = DataReader.GetDataView(op);

      var list = new List<ImportVouchersTotals>();

      int vouchersCount = 0;

      for (int i = 0; i < view.Count; i++) {
        // vouchersCount++; ToDo Check

        var importationSet = new ImportationSetID((int) view[i]["ENC_SISTEMA"],
                                                  (int) view[i]["ENC_TIPO_CONT"],
                                                  (DateTime) view[i]["ENC_FECHA_VOL"]);

        var totals = new ImportVouchersTotals {
          Description = importationSet.GetImportationSetDescription(),
          UID = importationSet.GetImportationSetUID(),
          VouchersCount = (int) (decimal) view[i]["TOTAL"]
        };

        vouchersCount += totals.VouchersCount;
        list.Add(totals);
      }

      var result = new ImportVouchersResult();

      list.Sort((x, y) => x.Description.CompareTo(y.Description));

      result.VoucherTotals = list.ToFixedList();
      result.VouchersCount = vouchersCount;

      return result;
    }


    static public string GetVouchersFilter(string importationSetUID, bool forEncabezados) {

      ImportationSetID importationSetID = ImportationSetID.ParseFromImportationSetUID(importationSetUID);

      if (forEncabezados) {
        return $"(ENC_SISTEMA = {importationSetID.IdSistema} AND " +
                $"ENC_TIPO_CONT = {importationSetID.TipoContabilidad} AND " +
                $"ENC_FECHA_VOL = {DataCommonMethods.FormatSqlDbDate(importationSetID.FechaAfectacion)})";

      } else {
        return $"(MCO_SISTEMA = {importationSetID.IdSistema} AND " +
                $"MCO_TIPO_CONT = {importationSetID.TipoContabilidad} AND " +
                $"MCO_FECHA_VOL = {DataCommonMethods.FormatSqlDbDate(importationSetID.FechaAfectacion)})";

      }
    }


    static internal long NextIdVolante() {
      return DataCommonMethods.GetNextObjectId("SEC_ID_VOLANTE");
    }


    static internal void StoreEncabezadoAsSlip(Encabezado o) {
      var operation = DataOperation.Parse("do_store_cof_volante",
                                          o.IdSistema, o.TipoContabilidad,
                                          o.FechaAfectacion, o.NumeroVolante);

      DataWriter.Execute(operation);
    }


    static internal void StoreIssues(Encabezado encabezado, ToImportVoucher item, long idVolante) {
      foreach (var issue in item.AllIssues) {
        var operation = DataOperation.Parse("apd_cof_volante_issue",
                                            NextIdVolanteIssue(), idVolante, -1, issue.Description);
        DataWriter.Execute(operation);
      }
      MergeVouchers(encabezado, idVolante, -1);
    }


    static internal void MergeVouchers(Encabezado encabezado, long idVolante, long voucherId) {
      var operation = DataOperation.Parse("do_merge_cof_volante_poliza",
                                           encabezado.IdSistema,
                                           encabezado.TipoContabilidad, encabezado.FechaAfectacion,
                                           encabezado.NumeroVolante, idVolante, voucherId);
      DataWriter.Execute(operation);
    }

    #region Helpers

    static private long NextIdVolanteIssue() {
      return DataCommonMethods.GetNextObjectId("SEC_ID_VOLANTE_ISSUE");
    }


    #endregion Helpers

  }  // class DbVouchersImporterDataService

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.VouchersImporter
