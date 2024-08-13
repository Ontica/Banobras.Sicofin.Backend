/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : CFDI System Integration              *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Use case interactor class            *
*  Type     : CFDIIntegrationUseCases                       License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Use cases used to export accounting transactions and balances to the CFDI system.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;

using Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters;

namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.UseCases {

  /// <summary>Use cases used to export accounting transactions and balances to the CFDI system.</summary>
  public class CFDIIntegrationUseCases : UseCase {

    #region Constructors and parsers

    protected CFDIIntegrationUseCases() {
      // no-op
    }

    static public CFDIIntegrationUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<CFDIIntegrationUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<CFDIBalanceDto> GetBalances(CFDIIntegrationCommand command) {
      Assertion.Require(command, nameof(command));

      command.EnsureValid();

      var builder = new CFDIBalancesBuilder(command);

      return builder.Build();
    }


    public FixedList<CFDITransactionDto> GetTransactions(CFDIIntegrationCommand command) {
      Assertion.Require(command, nameof(command));

      command.EnsureValid();

      var builder = new CFDITransactionBuilder(command);

      FixedList<CFDITransaction> transactions = builder.Build();

      return CFDITransactionMapper.Map(transactions);
    }

    #endregion Use cases

  }  // class CFDIIntegrationUseCases

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.CFDI.UseCases
