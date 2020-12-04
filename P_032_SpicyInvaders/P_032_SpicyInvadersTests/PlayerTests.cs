using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_032_SpicyInvaders;

namespace P_032_SpicyInvadersTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void MoveTest()
        {
            // Arrange 
            int posX = 30;
            int posY = 10;
            int resExpected = 31;
            Player player;

            // Act 
            player = new Player(posX, posY, 3);
            player.Move(1);
            int result = posX + 1;

            // Assert 
            Assert.AreEqual(resExpected, result, "Erreur dans le calcul");
        }

        [TestMethod]
        public void InvicibilityTest()
        {
            // Arrange 
            int posX = 30;
            int posY = 30;
            DateTime resExpected = DateTime.Now.AddSeconds(3.0);
            Player player;

            // Act 
            player = new Player(posX, posY, 3);
            DateTime result = new DateTime();
            
            player.Invicibility();
            result = player.TempInvicibility;

            // Assert 
            Assert.AreEqual(resExpected.Second, result.Second, "Erreur dans le calcul");
        }
    }
}
