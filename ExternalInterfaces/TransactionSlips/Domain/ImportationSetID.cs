/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Transaction Slips                             Component : Domain types                         *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Information holder                   *
*  Type     : ImportationSetID                              License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Describes an ID for set of imported transaction slips.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.FinancialAccounting.Vouchers;

namespace Empiria.FinancialAccounting.BanobrasIntegration.TransactionSlips {

  /// <summary>Describes an ID for set of imported transaction slips.</summary>
  sealed public class ImportationSetID {

    private ImportationSetID() {
      // no-op
    }

    public ImportationSetID(int idSistema, int tipoContabilidad, DateTime fechaAfectacion) {
      this.IdSistema = idSistema;
      this.TipoContabilidad = tipoContabilidad;
      this.FechaAfectacion = fechaAfectacion;
    }


    static public ImportationSetID ParseFromImportationSetUID(string importationSetUID) {
      var importationSetID = new ImportationSetID();

      string[] parts = importationSetUID.Split('|');

      importationSetID.IdSistema = int.Parse(parts[0]);
      importationSetID.TipoContabilidad = int.Parse(parts[1]);

      string[] fechaAfectacionParts = parts[2].Split('-');

      importationSetID.FechaAfectacion = new DateTime(int.Parse(fechaAfectacionParts[0]),
                                                      int.Parse(fechaAfectacionParts[1]),
                                                      int.Parse(fechaAfectacionParts[2]));
      return importationSetID;
    }


    #region Properties

    internal int IdSistema {
      get; set;
    }

    internal int TipoContabilidad {
      get; set;
    }

    internal DateTime FechaAfectacion {
      get; set;
    }

    #endregion Properties

    #region Methods

    public string GetImportationSetDescription() {
      var system = TransactionalSystem.Get(x => x.SourceSystemId == IdSistema);

      Assertion.Require(system, $"No se ha definido un sistema transversal con identificador {IdSistema}.");

      return $"{system.Name}, {FechaAfectacion.ToString("yyyy/MM/dd")}, Tipo Cont. {TipoContabilidad}";
    }


    public string GetImportationSetUID() {
      return $"{IdSistema}|{TipoContabilidad}|{FechaAfectacion.ToString("yyyy-MM-dd")}";
    }

    #endregion Methods

  }  // class ImportationSetID

}  // Empiria.FinancialAccounting.BanobrasIntegration.TransactionSlips
