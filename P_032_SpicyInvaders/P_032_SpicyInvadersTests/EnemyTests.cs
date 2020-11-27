/*
	ETML
	Date:
	Auteur: Manuel Oro
	Description:
	Modifié le:
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_032_SpicyInvaders;

namespace P_032_SpicyInvadersTests
{
    [TestClass]
    public class EnemyTests
    {
        [TestMethod]
        public void MoveTest()
        {
            // Arrange 
            int posX = 30;
            int posY = 10;
            int resExpected = 11;

            // Act 
            Enemy enemy = new Enemy(posX, posY);
            enemy.Move(new int[2] { 0, 1 });
            int result = posY + 1;

            // Assert 
            Assert.AreEqual(resExpected, result, "Erreur dans le calcul");
        }
    }
}
