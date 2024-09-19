/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : CFDI System Integration              *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Service provider                     *
*  Type     : CFDIBalancesBuilder                           License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Generates financial account balances for the CFDI system.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Empiria.FinancialAccounting.BalanceEngine;
using Empiria.FinancialAccounting.BalanceEngine.Adapters;
using Empiria.FinancialAccounting.BalanceEngine.UseCases;

using Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters;

namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI {

  /// <summary>Generates financial account balances for the CFDI system.</summary>
  internal class CFDIBalancesBuilder {

    private readonly CFDIIntegrationCommand _command;

    public CFDIBalancesBuilder(CFDIIntegrationCommand command) {
      Assertion.Require(command, nameof(command));

      _command = command;
    }

    internal FixedList<CFDIBalanceDto> Build() {
      FixedList<SaldosPorCuentaEntryDto> balances = GetAccountingBalances();

      balances = balances.FindAll(x => x.ItemType == TrialBalanceItemType.BalanceTotalCurrency);

      return Map(balances);
    }

    #region Helpers

    private FixedList<SaldosPorCuentaEntryDto> GetAccountingBalances() {
      var query = new TrialBalanceQuery {
        TrialBalanceType = TrialBalanceType.SaldosPorCuenta,
        AccountsChartUID = AccountsChart.IFRS.UID,
        BalancesType = BalancesType.AllAccounts,
        ShowCascadeBalances = true,
        WithSubledgerAccount = _command.SubledgerAccountNumber.Length != 0,
        UseCache = false,
        Accounts = new string[] { _command.AccountNumber },
        InitialPeriod = new BalancesPeriod {
          FromDate = _command.FromDate,
          ToDate = _command.ToDate,
        }
      };

      if (_command.SubledgerAccountNumber.Length != 0) {
        query.SubledgerAccounts = new string[] { _command.SubledgerAccountNumber };
      }

      using (var usecases = TrialBalanceUseCases.UseCaseInteractor()) {
        var entries = usecases.BuildTrialBalance(query)
                              .Entries;

        return entries.Select(x => (SaldosPorCuentaEntryDto) x)
                      .ToFixedList();
      }
    }


    private FixedList<CFDIBalanceDto> Map(FixedList<SaldosPorCuentaEntryDto> balances) {
      return balances.Select(x => Map(x)).ToFixedList();
    }

    private CFDIBalanceDto Map(SaldosPorCuentaEntryDto balance) {
      Account account = AccountsChart.IFRS.GetAccount(_command.AccountNumber);

      var dto = new CFDIBalanceDto {
        LedgerNumber = balance.LedgerNumber,
        LedgerName = Ledger.Parse(balance.LedgerUID).Name,
        AccountNumber = account.Number,
        AccountName = account.Name,
        CurrencyCode = balance.CurrencyCode,
        InitialBalance = balance.InitialBalance,
        Debits = balance.Debit,
        Credits = balance.Credit,
        CurrentBalance = balance.CurrentBalanceForBalances,
      };

      if (string.IsNullOrEmpty(_command.SubledgerAccountNumber)) {
        return dto;
      }

      var subledgerAccount = SubledgerAccount.TryParse(AccountsChart.IFRS,
                                                       _command.SubledgerAccountNumber);

      if (subledgerAccount != null) {
        dto.SubledgerAccountNumber = subledgerAccount.Number;
        dto.SubledgerAccountName = subledgerAccount.Name;
      }

      return dto;
    }

    #endregion Helpers

  }  // class CFDIBalancesBuilder

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI
