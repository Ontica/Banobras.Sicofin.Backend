/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : Balances Exporter                    *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Use case interactor class            *
*  Type     : ExportBalancesUseCases                        License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Use cases used to export balances to other Banobras' systems.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.FinancialAccounting.BalanceEngine;
using Empiria.FinancialAccounting.BalanceEngine.Adapters;
using Empiria.FinancialAccounting.BalanceEngine.UseCases;
using Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.Adapters;
using Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.Data;
using Empiria.Services;

namespace Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.UseCases {

  /// <summary>Use cases used to export balances to other Banobras' systems.</summary>
  public class ExportBalancesUseCases : UseCase {

    #region Constructors and parsers

    protected ExportBalancesUseCases() {
      // no-op
    }

    static public ExportBalancesUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<ExportBalancesUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases


    public FixedList<ExportedBalancesDto> Export(ExportBalancesCommand command) {
      Assertion.Require(command, "command");

      using (var usecases = TrialBalanceUseCases.UseCaseInteractor()) {
        TrialBalanceQuery query = command.MapToTrialBalanceQuery();

        TrialBalanceDto trialBalance = usecases.BuildTrialBalance(query);

        FixedList<ExportedBalancesDto> balances =
                      ExportBalancesMapper.MapToExportedBalances(command, trialBalance);

        if (command.StoreInto != StoreBalancesInto.None) {
          ExportBalancesDataService.StoreBalances(command, balances);
        }

        return balances;
      }
    }


    public TrialBalanceDto GetBalanceForCNBV64Report(ExportBalancesCommand command) {
      Assertion.Require(command, nameof(command));

      using (var usecases = TrialBalanceUseCases.UseCaseInteractor()) {

        TrialBalanceQuery _query = MapToBalanceQueryCNBV64(command);

        return usecases.BuildTrialBalance(_query);
      }
    }


    public TrialBalanceDto GetBalanceForCNBV76Report(ExportBalancesCommand command) {
      Assertion.Require(command, nameof(command));

      using (var usecases = TrialBalanceUseCases.UseCaseInteractor()) {

        TrialBalanceQuery _query = MapToBalanceQueryCNBV76(command);

        return usecases.BuildTrialBalance(_query);
      }
    }

    #endregion Use cases

    #region Helpers

    private TrialBalanceQuery MapToBalanceQueryCNBV64(ExportBalancesCommand command) {
      return new TrialBalanceQuery {
        TrialBalanceType = TrialBalanceType.Balanza,
        AccountsChartUID = AccountsChart.IFRS.UID,
        WithSubledgerAccount = true,
        FromAccount = "1.15",
        ToAccount = "1.15",
        BalancesType = BalancesType.WithCurrentBalanceOrMovements,
        ShowCascadeBalances = false,
        InitialPeriod = {
          FromDate = command.FromDate,
          ToDate = command.ToDate
        }
      };
    }


    private TrialBalanceQuery MapToBalanceQueryCNBV76(ExportBalancesCommand command) {
      return new TrialBalanceQuery {
        TrialBalanceType = TrialBalanceType.Balanza,
        AccountsChartUID = AccountsChart.IFRS.UID,
        FromAccount = "20.01",
        ToAccount = "20.01",
        Ledgers = new string[] { "81816c16-3306-98b0-66bf-a69021e31171" },
        BalancesType = BalancesType.AllAccounts,
        ShowCascadeBalances = false,
        InitialPeriod = {
          FromDate = command.FromDate,
          ToDate = command.ToDate
        }
      };
    }

    #endregion Helpers

  }  // class ExportBalancesUseCases

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.UseCases
