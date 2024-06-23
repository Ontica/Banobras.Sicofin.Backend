/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Reporting Services                            Component : Service Layer                        *
*  Assembly : FinancialAccounting.Reporting.dll             Pattern   : Service provider                     *
*  Type     : ExcelExporter                                 License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Main service to export accounting information to Microsoft Excel.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Office;
using Empiria.Storage;

using Empiria.FinancialAccounting.BanobrasIntegration.TransactionSlip.Exporters;
using Empiria.FinancialAccounting.BanobrasIntegration.TransactionSlips.Adapters;

namespace Empiria.FinancialAccounting.BanobrasIntegration.Reporting {

  /// <summary>Main service to export accounting information to Microsoft Excel.</summary>
  public class ExcelExporterService {


    public FileDto Export(FixedList<TransactionSlipDto> transactionSlips,
                          string exportationType) {

      Assertion.Require(transactionSlips, "transactionSlips");
      Assertion.Require(exportationType, "exportationType");

      string templateUID;

      if (exportationType == "slips") {
        templateUID = $"TransactionSlipsTemplate";
      } else if (exportationType == "issues") {
        templateUID = $"TransactionSlipsIssuesTemplate";
      } else {
        throw Assertion.EnsureNoReachThisCode($"Invalid exportation type '{exportationType}'.");
      }

      var templateConfig = FileTemplateConfig.Parse(templateUID);

      var exporter = new TransactionSlipExporter(templateConfig);

      ExcelFile excelFile;

      if (exportationType == "slips") {
        excelFile = exporter.CreateExcelFile(transactionSlips);

      } else if (exportationType == "issues") {
        excelFile = exporter.CreateIsuesExcelFile(transactionSlips);

      } else {
        throw Assertion.EnsureNoReachThisCode($"Invalid exportation type '{exportationType}'.");
      }

      return excelFile.ToFileDto();

    }

  }  // class ExcelExporter

} // namespace Empiria.FinancialAccounting.Reporting
