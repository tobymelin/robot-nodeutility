﻿using NUnit.Framework;
using RobotMovementTool;
using System;

namespace RobotMovementToolTest
{
    public class CoordValidationTests
    {
        Helpers helpers;
        double precision = 1e-6;

        [SetUp]
        public void Setup()
        {
            helpers = new Helpers();
        }

        [TestCaseSource("ValidCoordCases")]
        public void ValidXCoordTest(string coordInput, double[] coordList)
        {
            Assert.That(helpers.ValidateSingleCoord(coordInput),
                Is.EqualTo(coordList[0]).Within(precision));
        }
        [TestCaseSource("ValidCoordCases")]
        public void ValidYCoordTest(string coordInput, double[] coordList)
        {
            if (coordList.Length >= 2)
                Assert.That(helpers.ValidateSingleCoord(coordInput, 1),
                    Is.EqualTo(coordList[1]).Within(precision));
        }
        [TestCaseSource("ValidCoordCases")]
        public void ValidZCoordTest(string coordInput, double[] coordList)
        {
            if (coordList.Length >= 3)
                Assert.That(helpers.ValidateSingleCoord(coordInput, 2),
                    Is.EqualTo(coordList[2]).Within(precision));
        }
        static object[] ValidCoordCases =
        {
            new object[] { "123,456,789", new double[] { 123, 456, 789 } },
            new object[] { "-23.5, 12.3, 87.2", new double[] {-23.5, 12.3, 87.2 } },
            new object[] { "798213, -2512, -5782", new double[] {798213, -2512, -5782} },
            new object[] { " 1, 2, 5    ", new double[] {1, 2, 5} },
            new object[] { "1,\t5,-4", new double[] {1, 5, -4} },
            new object[] { "9,\t53", new double[] {9, 53} },
            new object[] { "   -58.23   \t  ", new double[] {-58.23} }
        };

        [TestCaseSource("InvalidCoordCases")]
        public void InvalidCoordTest(string coordInput, int coordIdx)
        {
            Assert.Throws<ArgumentException>(delegate { helpers.ValidateSingleCoord(coordInput, coordIdx); });
        }
        static object[] InvalidCoordCases =
        {
            new object[] { "123,456foo,789", 1 },
            new object[] { "-23.5, 12#.3, 87!.2", 2 },
            new object[] { "798bar213     , -2512, -5782", 0 },
            new object[] { " 0472, -123  ", 2 }
        };
    }
}
