/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : CFDI System Integration              *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Service provider                     *
*  Type     : CFDITransactionBuilder                        License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Builds a list of accounting transactions for the use of the CFDI system.                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

using Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters;
using Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Data;
using Empiria.FinancialAccounting.Vouchers.Adapters;

namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI {

  /// <summary>Builds a list of accounting transactions for the use of the CFDI system.</summary>
  internal class CFDITransactionBuilder {

    private CFDIIntegrationCommand _command;

    public CFDITransactionBuilder(CFDIIntegrationCommand command) {
      Assertion.Require(command, nameof(command));

      _command = command;
    }

    internal FixedList<CFDITransaction> Build() {
      string filter = BuildFilter();

      return CFDITransactionData.GetTransactions(filter);
    }

    private string BuildFilter() {
      string datesFilter = BuildAccountingDateRangeFilter(_command.FromDate, _command.ToDate);
      string accountFilter = BuildAccountFilter(_command.AccountNumber);
      string subledgerAccountFilter = BuildSubledgerAccountFilter(_command.SubledgerAccountNumber);

      var filter = new Filter(datesFilter);

      filter.AppendAnd(accountFilter);
      filter.AppendAnd(subledgerAccountFilter);

      return filter.ToString();
    }


    static private string BuildAccountingDateRangeFilter(DateTime fromDate, DateTime toDate) {
      return $"{DataCommonMethods.FormatSqlDbDate(fromDate)} <= FECHA_AFECTACION AND " +
             $"FECHA_AFECTACION < {DataCommonMethods.FormatSqlDbDate(toDate.Date.AddDays(1))}";
    }


    static private string BuildAccountFilter(string accountNumber) {
      return $"NUMERO_CUENTA_ESTANDAR = '{accountNumber}'";
    }

    static private string BuildSubledgerAccountFilter(string subledgerAccountNumber) {
      if (subledgerAccountNumber.Length == 0) {
        return string.Empty;
      }

      return $"NUMERO_CUENTA_AUXILIAR = '{subledgerAccountNumber}'";
    }

  }  // class CFDITransactionBuilder

} // namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI
