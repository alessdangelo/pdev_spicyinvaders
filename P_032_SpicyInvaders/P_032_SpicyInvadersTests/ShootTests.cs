using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_032_SpicyInvaders;

namespace P_032_SpicyInvadersTests
{
    [TestClass]
    public class ShootTests
    {
        [TestMethod]
        public void MoveTest()
        {
            // Arrange 
            int posX = 30;
            int posY = 10;
            int resExpected = 11;
            Shoot shoot;

            // Act 
            shoot = new Shoot(posX, posY, 0);
            shoot.Move();
            int result = posY + 1;

            // Assert 
            Assert.AreEqual(resExpected, result, "Erreur dans le calcul");
        }
    }
}
