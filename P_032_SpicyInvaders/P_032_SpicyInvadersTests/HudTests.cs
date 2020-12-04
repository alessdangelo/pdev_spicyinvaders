/*
	ETML
	Date:
	Auteur: Manuel Oro
	Description:
	Modifié le:
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_032_SpicyInvaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_032_SpicyInvadersTests
{
    [TestClass]
    public class HudTests
    {
        [TestMethod]
        public void HudTest()
        {
            // Arrange
            Hud hud;

            // Act 
            hud = new Hud();

            // Assert 
            Assert.IsNotNull(hud, "Erreur dans le calcul");
        }
    }
}
