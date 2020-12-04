using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_032_SpicyInvaders;

namespace P_032_SpicyInvadersTests
{
    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        public void IsInsideTest()
        {
            // Arrange 
            int sizeX = 5;
            int sizeY = 3;
            int posX = 30;
            int posY = 10;
            Block block;

            // Act 
            block = new Block(sizeX, sizeY, posX, posY);
            block.Initialize();

            bool result = block.IsInside(30, 10);

            // Assert 
            Assert.IsTrue(result, "Erreur dans le calcul");
        }
    }
}
