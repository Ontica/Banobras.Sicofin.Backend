/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : CFDI System Integration              *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Information Holder                   *
*  Type     : CFDITransaction                               License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Holds information about an accounting transaction for use of the CFDI system.                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI {

  /// <summary>Holds information about an accounting transaction for use of the CFDI system.</summary>
  internal class CFDITransaction {

    [DataField("ID_MAYOR")]
    internal Ledger Ledger {
      get; private set;
    }


    [DataField("ID_TRANSACCION")]
    internal long VoucherId {
      get; private set;
    }


    [DataField("NUMERO_TRANSACCION")]
    internal string VoucherNumber {
      get; private set;
    }


    [DataField("CONCEPTO_TRANSACCION")]
    internal string VoucherConcept {
      get; private set;
    }


    [DataField("ID_ELABORADA_POR")]
    internal Participant RecordedBy {
      get; private set;
    }


    [DataField("FECHA_AFECTACION")]
    internal DateTime AccountingDate {
      get; private set;
    }


    [DataField("FECHA_REGISTRO")]
    internal DateTime RecordingDate {
      get; private set;
    }


    [DataField("ID_SECTOR")]
    internal Sector Sector {
      get; private set;
    }


    [DataField("NUMERO_CUENTA_AUXILIAR")]
    internal string SubledgerAccountNumber {
      get; private set;
    }


    [DataField("NOMBRE_CUENTA_AUXILIAR")]
    internal string SubledgerAccountName {
      get; private set;
    }


    [DataField("ID_MONEDA")]
    internal Currency Currency {
      get; private set;
    }

    [DataField("DEBE")]
    internal decimal Debit {
      get; private set;
    }


    [DataField("HABER")]
    internal decimal Credit {
      get; private set;
    }

  }  // class CFDITransaction

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI
