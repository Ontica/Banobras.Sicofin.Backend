/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                Component : Web Api                               *
*  Assembly : Banobras.Sicofin.WebApi.dll                  Pattern   : Query Controller                      *
*  Type     : BanobrasExportBalancesController             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Query web API used to return balances used by other systems (Banobras).                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.Storage;
using Empiria.WebApi;

using Empiria.FinancialAccounting.BalanceEngine.Adapters;
using Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.Adapters;
using Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.UseCases;
using Empiria.FinancialAccounting.Reporting.Balances;

namespace Empiria.FinancialAccounting.WebApi.BanobrasIntegration {

  /// <summary>Query web API used to return balances used by other systems (Banobras).</summary>
  public class BanobrasExportBalancesController : WebApiController {

    #region Web Apis

    [HttpPost]   // ToDo: AllowAnonymous Removed
    [Route("v2/financial-accounting/integration/export-balances")]
    public SingleObjectModel ExportBalances([FromBody] ExportBalancesCommand command) {

      base.RequireBody(command);

      command.AccountsChartId = AccountsChart.IFRS.Id;

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        usecases.Export(command);

        return new SingleObjectModel(this.Request, "La exportación de saldos se realizó con éxito.");
      }
    }


    [HttpPost]      // ToDo: Remove AllowAnonymous
    [Route("v2/financial-accounting/integration/balances-by-day")]
    public CollectionModel ExportBalancesByDay([FromBody] BanobrasExportBalancesCommand banobrasCommand) {

      base.RequireBody(banobrasCommand);

      ExportBalancesCommand command = banobrasCommand.ConvertToExportBalancesCommandByDay();

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        FixedList<ExportedBalancesDto> balancesDto = usecases.Export(command);

        return new CollectionModel(this.Request, balancesDto);
      }
    }


    [HttpPost]  // // ToDo: Remove AllowAnonymous
    [Route("v2/financial-accounting/integration/balances-by-month")]
    public CollectionModel ExportBalancesByMonth([FromBody] BanobrasExportBalancesCommand banobrasCommand) {

      base.RequireBody(banobrasCommand);

      ExportBalancesCommand command = banobrasCommand.ConvertToExportBalancesCommandByMonth();

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        FixedList<ExportedBalancesDto> balancesDto = usecases.Export(command);

        return new CollectionModel(this.Request, balancesDto);
      }
    }


    [HttpPost]  // // ToDo: Remove AllowAnonymous
    [Route("v2/financial-accounting/integration/rerdo/balances-for-cnbv64")]
    public CollectionModel GetBalanceForCNBV64([FromBody] ExportBalancesCommand command) {

      base.RequireBody(command);

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        TrialBalanceDto trialBalance = usecases.GetBalanceForCNBV64Report(command);

        var balances = trialBalance.Entries.Select(x => (BalanzaTradicionalEntryDto) x);

        return new CollectionModel(this.Request, new FixedList<BalanzaTradicionalEntryDto>(balances));
      }
    }


    [HttpPost]  // // ToDo: Remove AllowAnonymous
    [Route("v2/financial-accounting/integration/rerdo/balances-for-cnbv64")]
    public SingleObjectModel ExportBalanceForCNBV64ToExcel([FromBody] ExportBalancesCommand command) {

      base.RequireBody(command);

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        TrialBalanceDto trialBalance = usecases.GetBalanceForCNBV64Report(command);

        var excelExporter = new BalancesExcelExporterService();

        FileDto excelFileDto = excelExporter.Export(trialBalance);

        return new SingleObjectModel(this.Request, excelFileDto);
      }
    }


    [HttpPost]  // // ToDo: Remove AllowAnonymous
    [Route("v2/financial-accounting/integration/rerdo/balances-for-cnbv76")]
    public CollectionModel GetBalanceForCNBV76([FromBody] ExportBalancesCommand command) {

      base.RequireBody(command);

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        TrialBalanceDto trialBalance = usecases.GetBalanceForCNBV76Report(command);

        var balances = trialBalance.Entries.Select(x => (BalanzaTradicionalEntryDto) x);

        return new CollectionModel(this.Request, new FixedList<BalanzaTradicionalEntryDto>(balances));
      }
    }


    [HttpPost]  // // ToDo: Remove AllowAnonymous
    [Route("v2/financial-accounting/integration/rerdo/balances-for-cnbv64")]
    public SingleObjectModel ExportBalanceForCNBV76ToExcel([FromBody] ExportBalancesCommand command) {

      base.RequireBody(command);

      using (var usecases = ExportBalancesUseCases.UseCaseInteractor()) {

        TrialBalanceDto trialBalance = usecases.GetBalanceForCNBV76Report(command);

        var excelExporter = new BalancesExcelExporterService();

        FileDto excelFileDto = excelExporter.Export(trialBalance);

        return new SingleObjectModel(this.Request, excelFileDto);
      }
    }

    #endregion Web Apis

  } // class BanobrasExportBalancesController

} // namespace Empiria.FinancialAccounting.WebApi.BanobrasIntegration
