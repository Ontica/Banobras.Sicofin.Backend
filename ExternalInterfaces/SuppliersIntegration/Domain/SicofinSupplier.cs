/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : PYC Suppliers Integration            *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Information Holder                   *
*  Type     : SicofinSupplier                               License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Holds a supplier's assigned subledger account.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC {

  /// <summary>Holds a supplier's assigned subledger account.</summary>
  public class SicofinSupplier {

    [DataField("ID_MAYOR", ConvertFrom = typeof(decimal))]
    public int LedgerId {
      get; internal set;
    }


    [DataField("ID_CUENTA_AUXILIAR", ConvertFrom = typeof(decimal))]
    public int SubledgerAccountId {
      get; internal set;
    }


    [DataField("NUMERO_CUENTA_AUXILIAR")]
    public string SubledgerAccountNo {
      get; internal set;
    }


    [DataField("NOMBRE_CUENTA_AUXILIAR")]
    public string Name {
      get; internal set;
    }


    [DataField("ULTIMO_MOVIMIENTO")]
    public DateTime LastUpdate {
      get; internal set;
    }


    [DataField("NUEVO_NOMBRE")]
    public string CleanName {
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


    [DataField("PROXIMITY_FACTOR")]
    public decimal ProximityFactor {
      get; internal set;
    }

  }  // class SicofinSupplier

}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.PYC
