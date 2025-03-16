/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : PYC Suppliers Integration            *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Information Holder                   *
*  Type     : PYCSupplier                                   License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Holds a PYC supplier account.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC {

  /// <summary>Holds a PYC supplier account.</summary>
  public class PYCSupplier {

    [DataField("PRV_ID", ConvertFrom = typeof(decimal))]
    public int AssignedId {
      get; internal set;
    }


    [DataField("PRV_DESCRIPCION")]
    public string Name {
      get; internal set;
    }


    [DataField("NUEVO_NOMBRE")]
    public string CleanName {
      get; internal set;
    }


    [DataField("PRV_RFC")]
    public string TaxCode {
      get; internal set;
    }


    [DataField("PRV_AUXILIAR")]
    public string SubledgerAccountNo {
      get; internal set;
    }


    [DataField("ULTIMO_MOVIMIENTO")]
    public DateTime LastUpdate {
      get; internal set;
    }


    [DataField("KEYWORDS_TAGS")]
    public string KeywordsTags {
      get; internal set;
    }


    [DataField("MATCH_ID", ConvertFrom = typeof(decimal))]
    public int MatchId {
      get; internal set;
    }

  }  // class PYCSupplier

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC
