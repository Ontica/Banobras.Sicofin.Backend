/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                Component : CFDI System Integration               *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll      Pattern   : Command payload                       *
*  Type     : CFDIIntegrationCommand                       License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Command payload used to export transactions and balances to the CFDI system.                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters {

  /// <summary>Command payload used to export transactions and balances to the CFDI system.</summary>
  public class CFDIIntegrationCommand {

    #region Fields

    public string AccountNumber {
      get; set;
    } = string.Empty;


    public string SubledgerAccountNumber {
      get; set;
    } = string.Empty;


    public DateTime FromDate {
      get; set;
    } = ExecutionServer.DateMinValue;


    public DateTime ToDate {
      get; set;
    } = ExecutionServer.DateMinValue;


    internal void EnsureValid() {
      Assertion.Require(AccountNumber, nameof(AccountNumber));
      Assertion.Require(FromDate != ExecutionServer.DateMinValue, "FromDate");
      Assertion.Require(ToDate != ExecutionServer.DateMinValue, "ToDate");
    }

    #endregion Fields

  } // class CFDIIntegrationCommand

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters
