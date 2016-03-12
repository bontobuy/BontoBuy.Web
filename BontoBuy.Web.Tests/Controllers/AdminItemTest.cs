using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BontoBuy.Web.Controllers;
using BontoBuy.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BontoBuy.Web.Tests.Controllers
{
    [TestClass]
    public class AdminItemTest
    {
        [TestMethod]
        public void Retrieve()
        {
            var list = new List<AdminRetrieveItemViewModel>();
            var adminRepoMock = new Mock<IAdminItemRepo>();
            adminRepoMock.Setup(m => m.Retrieve()).Returns(list);
            var controller = new AdminItemController(adminRepoMock.Object);

            var result = controller.Retrieve();

            Assert.IsNotNull(result);
        }
    }
}