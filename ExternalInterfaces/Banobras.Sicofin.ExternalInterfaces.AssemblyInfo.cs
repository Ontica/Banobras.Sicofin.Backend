/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  System   : Sistema de Contabilidad Financiera (SICOFIN)   Module  : Banobras Integration Services         *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.dll        Pattern : Assembly Attributes File              *
*                                                            License : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Este módulo provee servicios para integrar SICOFIN con otros sistemas y fuentes de             *
*             información propias de BANOBRAS.                                                               *
*                                                                                                            *
*             Entre estos servicios se encuentra la recepción de volantes provenientes de sistemas           *
*             transversales o vía templates Excel o archivos de texto.                                       *
*                                                                                                            *
*             Aquí también están los servicios para exportar saldos a otros sistemas y ejecutar              *
*             procesos externos.                                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

/*************************************************************************************************************
* Assembly configuration attributes.                                                                         *
*************************************************************************************************************/
[assembly: AssemblyTrademark("Empiria and Ontica are either trademarks of La Vía Óntica SC or Ontica LLC.")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("Banobras.Sicofin.ExternalInterfaces.Tests")]
