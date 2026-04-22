/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : PYC Suppliers Integration                        Component : Test cases                        *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.Tests.dll    Pattern   : Unit tests                        *
*  Type     : PYCSuppliersMatcherTests                         License   : Please read LICENSE.txt file      *
*                                                                                                            *
*  Summary  : PYC - Sicofin suppliers subledger accounts matcher.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.FinancialAccounting.BalanceEngine;
using Empiria.FinancialAccounting.BalanceEngine.Adapters;
using Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.Adapters;
using Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.UseCases;
using Empiria.FinancialAccounting.Reporting.Balances;
using Empiria.Services;
using Empiria.Storage;
using Xunit;

namespace Empiria.FinancialAccounting.Tests.BalancesExporter {
  
  /// <summary></summary>
  public class BalancesExporterTests {


    [Fact]
    public void Should_Get_BalanceForCNBV64Report() {

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        ExportBalancesCommand command = new ExportBalancesCommand {
          FromDate = new System.DateTime(2026,1,1),
          ToDate = new System.DateTime(2026, 1, 31)
        };

        TrialBalanceDto trialBalance = usecases.GetBalanceForCNBV64Report(command);

        var sut = trialBalance.Entries.Select(x => (BalanzaTradicionalEntryDto) x);

        Assert.NotEmpty(sut);
      }
    }


    [Fact]
    public void Should_Can_Export_BalanceForCNBV64Report() {

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        ExportBalancesCommand command = new ExportBalancesCommand {
          FromDate = new System.DateTime(2026, 1, 1),
          ToDate = new System.DateTime(2026, 1, 31)
        };

        TrialBalanceDto trialBalance = usecases.GetBalanceForCNBV64Report(command);

        var excelExporter = new BalancesExcelExporterService();

        FileDto excelFileDto = excelExporter.Export(trialBalance);

        Assert.True(true);
      }
    }


    [Fact]
    public void Should_Can_Export_BalanceForCNBV76Report() {

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        ExportBalancesCommand command = new ExportBalancesCommand {
          FromDate = new System.DateTime(2026, 1, 1),
          ToDate = new System.DateTime(2026, 1, 31)
        };

        TrialBalanceDto trialBalance = usecases.GetBalanceForCNBV76Report(command);

        var excelExporter = new BalancesExcelExporterService();

        FileDto excelFileDto = excelExporter.Export(trialBalance);

        Assert.True(true);
      }
    }


  } // class BalancesExporterTests

} // namespace Empiria.FinancialAccounting.Tests.BalancesExporter
