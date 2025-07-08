/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Transaction Slips                             Component : Use cases                            *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Use case interactor class            *
*  Type     : TransactionSlipUseCases                       License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Use cases used for retrieve information about Banobras operating systems' transaction slips.   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Services;

using Empiria.FinancialAccounting.BanobrasIntegration.TransactionSlips.Adapters;
using Empiria.FinancialAccounting.BanobrasIntegration.VouchersImporter;

namespace Empiria.FinancialAccounting.BanobrasIntegration.TransactionSlips.UseCases {

  /// <summary>Use cases used for retrieve information about Banobras operating
  /// systems' transaction slips.</summary>
  public class TransactionSlipUseCases : UseCase {

    #region Constructors and parsers

    protected TransactionSlipUseCases() {
      // no-op
    }

    static public TransactionSlipUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<TransactionSlipUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public void DeleteTransactionSlips(string importationSetUID) {
      Assertion.Require(importationSetUID, nameof(importationSetUID));

      var importationSetID = ImportationSetID.ParseFromImportationSetUID(importationSetUID);

      DbVouchersImporterDataService.DeleteTransactionSlips(importationSetID);

      EmpiriaLog.Info($"Se eliminó el conjunto de volantes " +
                      $"descrito por {importationSetUID}.");

    }

    public TransactionSlipDto GetTransactionSlip(string transactionSlipUID) {
      Assertion.Require(transactionSlipUID, nameof(transactionSlipUID));

      TransactionSlip slips = TransactionSlip.Parse(transactionSlipUID);

      return TransactionSlipMapper.Map(slips);
    }


    public FixedList<TransactionSlipDto> GetTransactionSlipsList(TransactionSlipsQuery query) {
      Assertion.Require(query, nameof(query));

      FixedList<TransactionSlip> slips = TransactionSlipSearcher.Search(query);

      return TransactionSlipMapper.Map(slips, true);
    }


    public FixedList<TransactionSlipDescriptorDto> SearchTransactionSlips(TransactionSlipsQuery query) {
      Assertion.Require(query, nameof(query));

      FixedList<TransactionSlip> slips = TransactionSlipSearcher.Search(query);

      return TransactionSlipMapper.MapToDescriptors(slips);
    }


    #endregion Use cases

  }  // class TransactionSlipUseCases

}  // Empiria.FinancialAccounting.BanobrasIntegration.VouchersImporter.UseCases
