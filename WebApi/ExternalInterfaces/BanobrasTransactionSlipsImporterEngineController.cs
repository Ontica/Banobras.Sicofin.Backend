﻿/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                Component : Voucher importation                   *
*  Assembly : Banobras.Sicofin.WebApi.dll                  Pattern   : Command Controller                    *
*  Type     : BanobrasDbVoucherImporterEngineController    License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Controls the transaction slips candidates importation process to acounting vouchers.           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.FinancialAccounting.BanobrasIntegration.VouchersImporter.Adapters;
using Empiria.FinancialAccounting.BanobrasIntegration.VouchersImporter.UseCases;

namespace Empiria.FinancialAccounting.WebApi.BanobrasIntegration {

  /// <summary>Imports transaction slips candidates to acounting vouchers.</summary>
  public class BanobrasTransactionSlipsImporterEngineController : WebApiController {

    #region Database importer

    [HttpGet]
    [Route("v2/financial-accounting/vouchers/database-importer/status")]
    [Route("v2/financial-accounting/vouchers/import-from-database/status")]
    public SingleObjectModel GetVoucherImporterEngineStatus() {

      using (var service = DbVoucherImporterEngine.ServiceInteractor()) {
        ImportVouchersResult result = service.Status();

        return new SingleObjectModel(base.Request, result);
      }
    }


    [HttpPost]
    [Route("v2/financial-accounting/vouchers/database-importer/start")]
    [Route("v2/financial-accounting/vouchers/import-from-database")]
    public SingleObjectModel StartVoucherImporterEngine([FromBody] ImportVouchersCommand command) {

      base.RequireBody(command);

      using (var service = DbVoucherImporterEngine.ServiceInteractor()) {
        ImportVouchersResult result = service.Start(command);

        return new SingleObjectModel(base.Request, result);
      }
    }


    [HttpPost]
    [Route("v2/financial-accounting/vouchers/database-importer/stop")]
    [Route("v2/financial-accounting/vouchers/import-from-database/stop")]
    public SingleObjectModel StopVoucherImporterEngine() {

      using (var service = DbVoucherImporterEngine.ServiceInteractor()) {
        ImportVouchersResult result = service.Stop();

        return new SingleObjectModel(base.Request, result);
      }
    }

    #endregion Database importer

  }  // class BanobrasDbVoucherImporterEngineController

}  // namespace Empiria.FinancialAccounting.WebApi.BanobrasIntegration
