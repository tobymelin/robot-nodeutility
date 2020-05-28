using NUnit.Framework;
using RobotMovementTool;

namespace RobotMovementToolTest
{
    public class MovementTests
    {
        [Test]
        public void VctrDefaultsTest()
        {
            Vctr vctr0 = new Vctr();
            Assert.IsTrue(vctr0.Equals(0, 0, 0));
        }

        /*
         * Test absolute movement of vectors
         */
        [Test]
        public void VctrMovementAbsoluteTest1()
        {
            Vctr vector = new Vctr();

            vector.Move("X", 0.5, false);
            vector.Move("Y", -2.3, false);
            vector.Move("Z", -9.7, false);

            Assert.IsTrue(vector.Equals(0.5, -2.3, -9.7));
        }
        [Test]
        public void VctrMovementAbsoluteTest2()
        {
            Vctr vector = new Vctr(0, 23.5, -2.3);

            vector.Move("X", 0.5, false);
            vector.Move("Y", -2.3, false);
            vector.Move("Z", -9.7, false);

            Assert.IsTrue(vector.Equals(0.5, -2.3, -9.7));
        }

        [Test]
        public void VctrMovementRelativeTest1()
        {
            Vctr vector = new Vctr();

            vector.Move("X", 1.2);
            vector.Move("Y", -13.5);
            vector.Move("Z", 1.3);

            Assert.IsTrue(vector.Equals(1.2, -13.5, 1.3));
        }
        [Test]
        public void VctrMovementRelativeTest2()
        {
            Vctr vector = new Vctr(0, 23.5, -2.3);

            vector.Move("X", 1.2);
            vector.Move("Y", -13.5);
            vector.Move("Z", 1.3);

            Assert.IsTrue(vector.Equals(1.2, 10.0, -1.0));
        }
    }
}