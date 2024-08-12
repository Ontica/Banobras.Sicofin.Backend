/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                Component : CFDI System Integration               *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll      Pattern   : Data Transfer Object                  *
*  Type     : CFDITransactionDto                           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Data Transfer Object with accounting transaction information for the CFDI system.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters {

  /// <summary>Data Transfer Object with accounting transaction information for the CFDI system.</summary>
  public class CFDITransactionDto {

    public string LedgerNumber {
      get; internal set;
    }


    public string LedgerName {
      get; internal set;
    }


    public long VoucherId {
      get; internal set;
    }


    public string VoucherNumber {
      get; internal set;
    }


    public string VoucherConcept {
      get; internal set;
    }

    public string RecordedBy {
      get; internal set;
    }


    public DateTime AccountingDate {
      get; internal set;
    }


    public DateTime RecordingDate {
      get; internal set;
    }


    public string SectorCode {
      get; internal set;
    }


    public string SubledgerAccountNumber {
      get; internal set;
    }


    public string SubledgerAccountName {
      get; internal set;
    }


    public string CurrencyCode {
      get; internal set;
    }


    public decimal Debit {
      get; internal set;
    }


    public decimal Credit {
      get; internal set;
    }

  }  // class CFDITransactionDto

} // namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters
