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


    public TrialBalanceDto GetBalanceForCNBV64Report(TrialBalanceQuery query) {
      Assertion.Require(query, nameof(query));

      using (var usecases = TrialBalanceUseCases.UseCaseInteractor()) {

        return usecases.BuildTrialBalance(query);
      }
    }


    public TrialBalanceDto GetBalanceForCNBV76Report(TrialBalanceQuery query) {
      Assertion.Require(query, nameof(query));

      using (var usecases = TrialBalanceUseCases.UseCaseInteractor()) {

        return usecases.BuildTrialBalance(query);
      }
    }

    #endregion Use cases

  }  // class ExportBalancesUseCases

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.UseCases
