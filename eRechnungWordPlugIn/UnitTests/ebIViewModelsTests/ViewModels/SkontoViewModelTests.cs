using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIViewModels.ViewModels;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using ebIViewModelsTests.ViewModels;

namespace ebIViewModels.ViewModels.Tests
{
    [TestFixture]
    public class SkontoViewModelTests : CommonTestSetup
    {
        SkontoViewModel _skonto;
        SkontoViewModels _skontoList;
        [SetUp]
        public void SkontoTestInitialize()
        {
            InvVm.VmInvDate = DateTime.Today;
            InvVm.VmInvDueDate = InvVm.VmInvDate.AddDays(30);
            _skontoList = Cmn.UContainer.Resolve<SkontoViewModels>(new ParameterOverride("invVm", InvVm));
            _skonto = Cmn.UContainer.Resolve<SkontoViewModel>(new ParameterOverride("skontoEntry", _skontoList));
            // _skonto.InvoiceDueDays = 10;
            _skonto.SkontoProzent = 3;
            _skonto.SkontoBasisBetrag = 1000;
            _skonto.SkontoFaelligDate = InvVm.VmInvDate.AddDays(10);
        }
        [Test]
        public void SkontoViewModelTest()
        {
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(_skonto.Results.IsValid);
        }

        [Test]
        public void SkontoProzentBundInvalidTests()
        {
            _skonto.CurrentRuleSet = ebIModels.Models.InvoiceSubtypes.ValidationRuleSet.Government;
            _skonto.SkontoProzent = 0;
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid,"1.");

            _skonto.SkontoProzent = 100;
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid,"2.");
        }
        [Test]
        public void SkontoProzentIndustriesValidTests()
        {
            Cmn.Invoice.InvoiceSubtype.VariantOption = ebIModels.Models.InvoiceSubtypes.ValidationRuleSet.Industries;
            _skontoList.LoadFromInvoice(Cmn.Invoice);
            //_skonto.UpdateFromSkontoListEntry(_skontoList);
            _skonto.SkontoProzent = 0;
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results); 
            Assert.IsTrue(_skonto.Results.IsValid);

            Console.WriteLine("Test = 100");
            _skonto.SkontoProzent = 100;
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(_skonto.Results.IsValid);

            Console.WriteLine("Test > 100");
            _skonto.SkontoProzent = 110;
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid);

            Console.WriteLine("Test > 0");
            _skonto.SkontoProzent = -5;
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid);
        }
        [Test]
        public void SkontoProzentGovernmentValidTests()
        {
            Cmn.Invoice.InvoiceSubtype.VariantOption = ebIModels.Models.InvoiceSubtypes.ValidationRuleSet.Government;
            _skontoList.LoadFromInvoice(Cmn.Invoice);
            //_skonto.UpdateFromSkontoListEntry(_skontoList);
            _skonto.CurrentRuleSet = ebIModels.Models.InvoiceSubtypes.ValidationRuleSet.Government;
            _skonto.SkontoProzent = 0;
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid,"1.");

            Console.WriteLine("Test = 100");
            _skonto.SkontoProzent = 100;
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid);

            Console.WriteLine("Test > 100");
            _skonto.SkontoProzent = 110;
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid,"2.");

            Console.WriteLine("Test > 0");
            _skonto.SkontoProzent = -5;
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid,"3.");
        }

        [Test]
        public void SkontoTageTest()
        {
            _skonto.SkontoFaelligDate = InvVm.VmInvDate.AddDays(-5);
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid);

            _skonto.SkontoFaelligDate = InvVm.VmInvDueDate.AddDays(+5);
            _skonto.IsValidSkonto();
            Cmn.ListResults(_skonto.Results);
            Assert.IsTrue(!_skonto.Results.IsValid);

        }
    }
}
