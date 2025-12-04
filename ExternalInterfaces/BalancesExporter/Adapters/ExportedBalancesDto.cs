/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Banobras Integration Services                Component : Balances Exporter                     *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll      Pattern   : Information Holder                    *
*  Type     : ExportedBalancesDto                          License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Data Transfer Object with balance information for other systems (Banobras).                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.Adapters {

  public class ExportedBalancesDto {

    public int Empresa {
      get; internal set;
    }

    public DateTime Fecha {
      get; internal set;
    }

    public int Anio {
      get {
        return Fecha.Year;
      }
    }

    public int Mes {
      get {
        return Fecha.Month;
      }
    }

    public int Dia {
      get {
        return Fecha.Day;
      }
    }

    public string Area {
      get {
        return "AREA";
      }
    }

    public string NumeroMayor {
      get; internal set;
    }

    public string Cuenta {
      get; internal set;
    }

    public int NaturalezaCuenta {
      get; internal set;
    }

    public string Sector {
      get; internal set;
    }

    public string Auxiliar {
      get; internal set;
    }

    public int Moneda {
      get {
        return 1;
      }
    }

    public int MonedaOrigen {
      get; internal set;
    }

    public decimal SaldoAnterior {
      get; internal set;
    }

    public decimal MontoDebito {
      get; internal set;
    }

    public decimal MontoCredito {
      get; internal set;
    }

    public decimal Saldo {
      get; internal set;
    }

    public decimal SaldoPromedio {
      get; internal set;
    }

    public DateTime FechaUltimoMovimiento {
      get; internal set;
    }

    public string CalificaMoneda {
      get {
        return "null";
      }
    }

  }  // class ExportedBalancesDto

} // namespace Empiria.FinancialAccounting.BanobrasIntegration.BalancesExporter.Adapters
