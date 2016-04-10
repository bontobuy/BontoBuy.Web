using BontoBuy.Web.Controllers;
using BontoBuy.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BontoBuy.Web.Tests
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void RetrieveBrand()
        {
            //Arrange
            var myController = new TestController();
            List<BrandViewModel> result = null;
            string message = String.Empty;

            //Act
            try
            {
                result = myController.RetrieveBrand();
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