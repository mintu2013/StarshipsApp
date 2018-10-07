using System;
using Starwars;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class StarshipsTests
    {
        [TestMethod]
        public void CalculateResupplyFrequency()
        {
            StarshipManager.Distance = "1000000";
            string resupply = (Convert.ToInt32(StarshipManager.Distance) / (105 * 120)).ToString();

            Starship milleniumFalcon = new Starship()
            {
                Consumables = "5 days",
                MGLT = "105",
                Name = "TIE Advanced x1",
            };
            Assert.AreEqual(resupply, StarshipManager.CalculateNumberofResupply(milleniumFalcon));
        }

        [TestMethod]
        public void ValidInputAlphaNumericFalse()
        {
            StarshipManager.Distance = "@78h28-732L";
            Assert.IsFalse(Helpers.ValidateInput(StarshipManager.Distance));
        }

        [TestMethod]
        public void ValidInputNumericNegativeReturnsFalse()
        {
            StarshipManager.Distance = "-25986";
            Assert.IsFalse(Helpers.ValidateInput(StarshipManager.Distance));
        }


        [TestMethod]
        public void ValidInputNumericPositiveWithinRangeReturnsTrue()
        {
            StarshipManager.Distance = "523658";
            Assert.IsTrue(Helpers.ValidateInput(StarshipManager.Distance));
        }

    }
}
