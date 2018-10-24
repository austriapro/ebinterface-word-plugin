using System;
using NUnit.Framework;
using ebIViewModels.ViewModels;
using Microsoft.Practices.Unity;

namespace ebIViewModelsTests.ViewModels
{
    [TestFixture]
    public class UpdateInvDateTests : CommonTestSetup
    {
        [Test]
        public void UpdateInvDateAndNrTests()
        {

            var updView = Cmn.UContainer.Resolve<UpdateInvoiceViewModel>(new ParameterOverride("invoice",Cmn.Invoice));
            updView.Validate();
            Assert.IsTrue(updView.Results.IsValid);
        }
    }
}
