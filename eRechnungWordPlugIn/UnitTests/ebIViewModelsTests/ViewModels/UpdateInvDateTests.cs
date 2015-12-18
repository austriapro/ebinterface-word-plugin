using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ebIViewModels.ViewModels;
using Microsoft.Practices.Unity;

namespace ebIViewModelsTests.ViewModels
{
    [TestClass]
    public class UpdateInvDateTests : CommonTestSetup
    {
        [TestMethod]
        public void UpdateInvDateAndNrTests()
        {

            var updView = Cmn.UContainer.Resolve<UpdateInvoiceViewModel>(new ParameterOverride("invoice",Cmn.Invoice));
            updView.Validate();
            Assert.IsTrue(updView.Results.IsValid);
        }
    }
}
