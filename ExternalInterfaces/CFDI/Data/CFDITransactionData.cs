/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                Component : CFDI System Integration               *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll      Pattern   : Data services                         *
*  Type     : CFDITransactionData                          License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides data services for accounting transactions with the CFDI system.                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Data;

namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Data {

  /// <summary>Provides data services for accounting transactions with the CFDI system.</summary>
  static internal class CFDITransactionData {

    #region Methods

    static internal FixedList<CFDITransaction> GetTransactions(string filter) {
      var sql = "SELECT * FROM vw_cof_movimiento " +
                $"WHERE {filter} " +
                "ORDER BY FECHA_AFECTACION, NUMERO_TRANSACCION";

      var op = DataOperation.Parse(sql);

      return DataReader.GetPlainObjectFixedList<CFDITransaction>(op);
    }

    #endregion Methods

  }  // class CFDITransactionData

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Data
