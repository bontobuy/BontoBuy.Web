using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BontoBuy.Web.Controllers;
using BontoBuy.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BontoBuy.Web.Tests
{
    [TestClass]
    public class TestingTest
    {
        [TestMethod]
        public void RetrieveBrands()
        {
            //Arrange
            var controller = new TestingController();
            List<BrandViewModel> result = null;
            string message = String.Empty;

            //Act
            try
            {
                result = controller.RetrieveBrands();
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            //Assert
            Assert.IsNotNull(result);
        }
    }
}