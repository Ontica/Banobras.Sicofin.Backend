/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                 Component : Vouchers Importer                    *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll       Pattern   : Command payload                      *
*  Type     : InterfazUnicaImporterCommand                  License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Command used to import vouchers from an InterfazUnica data structure.                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.FinancialAccounting.BanobrasIntegration.VouchersImporter.Adapters {

  /// <summary>Command used to import vouchers from an InterfazUnica data structure.</summary>
  public class InterfazUnicaImporterCommand {

    public string ImportationRuleUID {
      get; set;
    } = string.Empty;


    public EncabezadoDto[] ENCABEZADOS {
      get; set;
    } = new EncabezadoDto[0];


  }  // class InterfazUnicaImporterCommand



  /// <summary>
  /// DTO used for remove transaction slips coming from web services using 'Interfaz Única'.</summary>
  public class RemoveFromInterfazUnicaCommand {

    public string ImportationRuleUID {
      get; set;
    } = string.Empty;


    public int ENC_SISTEMA {
      get; set;
    }

    public int ENC_TIPO_CONT {
      get; set;
    }

    public DateTime ENC_FECHA_VOL {
      get; set;
    }

  }


}  // namespace Empiria.FinancialAccounting.BanobrasIntegration.VouchersImporter.Adapters
