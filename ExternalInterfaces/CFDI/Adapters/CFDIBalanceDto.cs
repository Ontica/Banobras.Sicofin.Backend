/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : CFDI System Integration              *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Data Transfer Object                 *
*  Type     : CFDIBalanceDto                                License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : DTO used to return account balances to the CFDI system.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters {

  /// <summary>DTO used to return account balances to the CFDI system.</summary>
  public class CFDIBalanceDto {

    public string LedgerNumber {
      get; internal set;
    }


    public string LedgerName {
      get; internal set;
    }


    public string CurrencyCode {
      get; internal set;
    }


    public decimal InitialBalance {
      get; internal set;
    }


    public decimal Debits {
      get; internal set;
    }


    public decimal Credits {
      get; internal set;
    }


    public decimal CurrentBalance {
      get; internal set;
    }

  }  // class CFDIBalanceDto

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters
