/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                Component : Balances Exporter                     *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll      Pattern   : Mapper class                          *
*  Type     : ExportBalancesMapper                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Maps trial balances entries to ExportedBalancesDto structures.                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using Empiria.FinancialAccounting.BalanceEngine;
using Empiria.FinancialAccounting.BalanceEngine.Adapters;

namespace Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.Adapters {

  static public class ExportBalancesMapper {

    static public FixedList<ExportedBalancesDto> MapToExportedBalances(ExportBalancesCommand command,
                                                                       TrialBalanceDto trialBalance) {
      FixedList<BalanzaTradicionalEntryDto> entries = GetEntriesToBeExported(trialBalance);

      return new FixedList<ExportedBalancesDto>(entries.Select(x => MapTrialBalanceEntry(command, x)));
    }


    static private FixedList<BalanzaTradicionalEntryDto> GetEntriesToBeExported(TrialBalanceDto trialBalance) {
      var list = trialBalance.Entries.FindAll(x => x.ItemType == TrialBalanceItemType.Entry ||
                                                   x.ItemType == TrialBalanceItemType.Summary)
                                     .Select(x => (BalanzaTradicionalEntryDto) x)
                                     .Distinct();

      return list.ToFixedList();
    }


    static private ExportedBalancesDto MapTrialBalanceEntry(ExportBalancesCommand command,
                                                            BalanzaTradicionalEntryDto entry) {
      var account = StandardAccount.Parse(entry.StandardAccountId);
      var subledgerAccount = SubledgerAccount.Parse(entry.SubledgerAccountId);

      return new ExportedBalancesDto {
        Empresa = command.AccountsChartId,
        Fecha = command.ToDate,
        NumeroMayor = command.BreakdownLedgers ? Ledger.Parse(entry.LedgerUID).Id.ToString() : "CONSOLIDADO",
        Cuenta = account.Number,
        NaturalezaCuenta = account.DebtorCreditor == DebtorCreditorType.Deudora ? 1 : -1,
        Sector = entry.SectorCode,
        Auxiliar = subledgerAccount.IsEmptyInstance ? "0" : subledgerAccount.Number,
        MonedaOrigen = Currency.Parse(entry.CurrencyCode).Id,
        SaldoAnterior = entry.InitialBalance,
        MontoDebito = entry.Debit,
        MontoCredito = entry.Credit,
        Saldo = entry.CurrentBalanceForBalances,
        SaldoPromedio = Math.Round((decimal) entry.AverageBalance, 2),
        FechaUltimoMovimiento = entry.LastChangeDate,
      };
    }

  }  // class ExportBalancesMapper

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.Adapters
