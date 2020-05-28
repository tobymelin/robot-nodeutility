using NUnit.Framework;
using RobotMovementTool;
using System;

namespace RobotMovementToolTest
{
    public class NodeIDValidationTests
    {
        Helpers helpers;

        [SetUp]
        public void Setup()
        {
            helpers = new Helpers();
        }

        [TestCaseSource("ValidNodeIDCases")]
        public void ValidNodeIDTest(string nodeInput, int nodeID)
        {
            Assert.AreEqual(helpers.ValidateNodeID(nodeInput), nodeID);
        }
        static object[] ValidNodeIDCases =
        {
            new object[] { "123", 123 },
            new object[] { "128937", 128937 },
            new object[] { "798213", 798213 },
            new object[] { " 478    ", 478 },
            new object[] { "1", 1 }
        };
        
        [TestCaseSource("InvalidNodeIDCases")]
        public void InvalidNodeIDTest(string nodeInput)
        {
            Assert.Throws<ArgumentException>(delegate { helpers.ValidateNodeID(nodeInput); });
        }
        static object[] InvalidNodeIDCases =
        {
            new object[] { "12@3#" },
            new object[] { "foo" },
            new object[] { "28hwa" },
            new object[] { "1 test" }
        };
    }
}