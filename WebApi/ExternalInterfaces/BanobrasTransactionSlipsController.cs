/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Transaction Slips                            Component : Web Api                               *
*  Assembly : Banobras.Sicofin.WebApi.dll                  Pattern   : Query Controller                      *
*  Type     : BanobrasTransactionSlipsController           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Quey web API used to retrive transaction slips (volantes) sent from Banobras' systems.         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Storage;
using Empiria.WebApi;

using Empiria.FinancialAccounting.BanobrasIntegration.TransactionSlips.UseCases;
using Empiria.FinancialAccounting.BanobrasIntegration.TransactionSlips.Adapters;

using Empiria.FinancialAccounting.BanobrasIntegration.Reporting;

namespace Empiria.FinancialAccounting.WebApi.BanobrasIntegration {

  /// <summary>Quey web API used to retrive transaction slips (volantes) sent from Banobras' systems.</summary>
  public class BanobrasTransactionSlipsController : WebApiController {

    #region Query web api

    [HttpDelete]
    [Route("v2/financial-accounting/transaction-slips/delete-from-database/{importationSetUID}")]
    public NoDataModel DeleteTransactionSlips([FromUri] string importationSetUID) {

      using (var usecases = TransactionSlipUseCases.UseCaseInteractor()) {

        usecases.DeleteTransactionSlips(importationSetUID);

        return new NoDataModel(base.Request);
      }
    }


    [HttpPost]
    [Route("v2/financial-accounting/transaction-slips/export/{exportationType}")]
    [Route("v2/financial-accounting/transaction-slips/export/{exportationType}/excel")]
    public SingleObjectModel ExportTransactionSlips([FromBody] TransactionSlipsQuery query,
                                                    [FromUri] string exportationType) {

      base.RequireBody(query);

      using (var usecases = TransactionSlipUseCases.UseCaseInteractor()) {
        FixedList<TransactionSlipDto> slips = usecases.GetTransactionSlipsList(query);

        var excelExporter = new ExcelExporterService();

        FileReportDto excelFileDto = excelExporter.Export(slips, exportationType);

        return new SingleObjectModel(base.Request, excelFileDto);
      }
    }


    [HttpGet]
    [Route("v2/financial-accounting/transaction-slips/{transactionSlipUID}")]
    public SingleObjectModel GetTransactionSlip([FromUri] string transactionSlipUID) {

      using (var usecases = TransactionSlipUseCases.UseCaseInteractor()) {
        TransactionSlipDto slip = usecases.GetTransactionSlip(transactionSlipUID);

        return new SingleObjectModel(base.Request, slip);
      }
    }


    [HttpPost]
    [Route("v2/financial-accounting/transaction-slips")]
    public CollectionModel SearchTransactionSlips([FromBody] TransactionSlipsQuery query) {

      base.RequireBody(query);

      using (var usecases = TransactionSlipUseCases.UseCaseInteractor()) {

        FixedList<TransactionSlipDescriptorDto> slips = usecases.SearchTransactionSlips(query);

        return new CollectionModel(base.Request, slips);
      }
    }

    #endregion Query web api

  }  // class BanobrasTransactionSlipsController

}  // namespace Empiria.FinancialAccounting.WebApi.BanobrasIntegration
