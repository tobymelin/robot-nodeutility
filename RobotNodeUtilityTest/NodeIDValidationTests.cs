/* Robot Node Utility
 * Copyright (C) 2020  Tobias Melin
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using NUnit.Framework;
using RobotNodeUtility;
using System;

namespace RobotNodeUtilityTest
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