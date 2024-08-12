/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : CFDI System Integration              *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Mapper                               *
*  Type     : CFDITransactionMapper                         License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Maps CFDITransaction to CFDITransactionDto data structures.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters {

  /// <summary>Maps CFDITransaction to CFDITransactionDto data structures.</summary>
  static internal class CFDITransactionMapper {

    static internal FixedList<CFDITransactionDto> Map(FixedList<CFDITransaction> transactions) {
      return transactions.Select(x => Map(x))
                         .ToFixedList();
    }


    static private CFDITransactionDto Map(CFDITransaction transaction) {
      return new CFDITransactionDto {
        LedgerNumber = transaction.Ledger.Number,
        LedgerName = transaction.Ledger.Name,
        VoucherId = transaction.VoucherId,
        VoucherNumber = transaction.VoucherNumber,
        VoucherConcept = transaction.VoucherConcept,
        RecordedBy = transaction.RecordedBy.Name,
        AccountingDate = transaction.AccountingDate,
        RecordingDate = transaction.RecordingDate,
        SectorCode = transaction.Sector.Code,
        SubledgerAccountNumber = transaction.SubledgerAccountNumber,
        SubledgerAccountName = transaction.SubledgerAccountName,
        CurrencyCode = transaction.Currency.Code,
        Debit = transaction.Debit,
        Credit = transaction.Credit,
      };
    }

  } // class CFDITransactionMapper

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters
