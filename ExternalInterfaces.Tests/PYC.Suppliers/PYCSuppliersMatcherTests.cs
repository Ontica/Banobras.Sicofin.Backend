/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : PYC Suppliers Integration                        Component : Test cases                        *
*  Assembly : Banobras.Sicofin.ExternalInterfaces.Tests.dll    Pattern   : Unit tests                        *
*  Type     : PYCSuppliersMatcherTests                         License   : Please read LICENSE.txt file      *
*                                                                                                            *
*  Summary  : PYC - Sicofin suppliers subledger accounts matcher.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.FinancialAccounting.BanobrasIntegration.PYC;

namespace Empiria.FinancialAccounting.Tests.BanobrasIntegration {

  /// <summary>PYC - Sicofin suppliers subledger accounts matcher.</summary>
  public class PYCSuppliersMatcherTests {

    #region Facts

    [Fact]
    public void Should_CleanData() {
      var sut = new SuppliersMatcher();

      sut.CleanData();

      Assert.True(true);
    }


    [Fact]
    public void Should_Match_Suppliers() {
      var sut = new SuppliersMatcher();

      sut.Match();

      Assert.True(true);
    }


    [Fact]
    public void Should_Read_PYC_Suppliers() {
      FixedList<PYCSupplier> sut = SuppliersMatcherData.GetPYCSuppliers();

      Assert.NotEmpty(sut);
    }


    [Fact]
    public void Should_Read_Sicofin_Suppliers_Subledger_Accounts() {
      FixedList<SicofinSupplier> sut = SuppliersMatcherData.GetSicofinSuppliers();

      Assert.NotEmpty(sut);
    }

    #endregion Facts

  }  // class PYCSuppliersMatcherTests

}  // namespace Empiria.FinancialAccounting.Tests.BanobrasIntegration
