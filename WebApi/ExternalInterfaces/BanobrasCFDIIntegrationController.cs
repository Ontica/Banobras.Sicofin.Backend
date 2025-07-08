/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                Component : Web Api                               *
*  Assembly : Banobras.Sicofin.WebApi.dll                  Pattern   : Query Controller                      *
*  Type     : BanobrasCFDIIntegrationController            License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Query web API used to return accounts balances and transactions to the CFDI System.            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.WebApi;

using Empiria.FinancialAccounting.BanobrasIntegration.CFDI.Adapters;
using Empiria.FinancialAccounting.BanobrasIntegration.CFDI.UseCases;

namespace Empiria.FinancialAccounting.WebApi.BanobrasIntegration {

  /// <summary>Query web API used to return accounts balances and transactions to the CFDI System.</summary>
  public class BanobrasCFDIIntegrationController : WebApiController {

    #region Web Apis

    [HttpPost, AllowAnonymous]  // ToDo: AllowAnonymous Removed
    [Route("v2/financial-accounting/integration/cfdi-balances")]
    public CollectionModel CFDIBalances([FromBody] CFDIIntegrationCommand command) {

      base.RequireBody(command);

      using (var usecases = CFDIIntegrationUseCases.UseCaseInteractor()) {

        FixedList<CFDIBalanceDto> balancesDto = usecases.GetBalances(command);

        return new CollectionModel(this.Request, balancesDto);
      }
    }


    [HttpPost, AllowAnonymous]   // ToDo: AllowAnonymous Removed
    [Route("v2/financial-accounting/integration/cfdi-transactions")]
    public CollectionModel CFDITransactions([FromBody] CFDIIntegrationCommand command) {

      base.RequireBody(command);

      using (var usecases = CFDIIntegrationUseCases.UseCaseInteractor()) {

        FixedList<CFDITransactionDto> balancesDto = usecases.GetTransactions(command);

        return new CollectionModel(this.Request, balancesDto);
      }
    }


    #endregion Web Apis

  } // class BanobrasCFDIIntegrationController

} // namespace Empiria.FinancialAccounting.WebApi.BanobrasIntegration
