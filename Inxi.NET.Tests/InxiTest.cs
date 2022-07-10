

// Inxi.NET  Copyright (C) 2020-2021  EoflaOE
// 
// This file is part of Inxi.NET
// 
// Inxi.NET is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Inxi.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InxiFrontend.Tests
{

    [TestClass]
    public class InxiTest
    {

        /// <summary>
    /// Tests getting hardware information
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformation()
        {
            var InxiInstance = new Inxi();
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.CPU);
            Assert.IsNotNull(HardwareInfo.GPU);
            Assert.IsNotNull(HardwareInfo.RAM);
            Assert.IsNotNull(HardwareInfo.HDD);
            Assert.IsNotNull(HardwareInfo.Sound);
            Assert.IsNotNull(HardwareInfo.System);
            Assert.IsNotNull(HardwareInfo.BIOS);
            Assert.IsNotNull(HardwareInfo.Machine);
        }

        /// <summary>
    /// Tests getting hardware information selectively (BIOS)
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformationSelectiveBIOS()
        {
            var InxiInstance = new Inxi(InxiHardwareType.BIOS);
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.BIOS);
        }

        /// <summary>
    /// Tests getting hardware information selectively (Graphics)
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformationSelectiveGraphics()
        {
            var InxiInstance = new Inxi(InxiHardwareType.Graphics);
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.GPU);
        }

        /// <summary>
    /// Tests getting hardware information selectively (HardDrive)
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformationSelectiveHardDrive()
        {
            var InxiInstance = new Inxi(InxiHardwareType.HardDrive);
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.HDD);
        }

        /// <summary>
    /// Tests getting hardware information selectively (Machine)
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformationSelectiveMachine()
        {
            var InxiInstance = new Inxi(InxiHardwareType.Machine);
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.Machine);
        }

        /// <summary>
    /// Tests getting hardware information selectively (Network)
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformationSelectiveNetwork()
        {
            var InxiInstance = new Inxi(InxiHardwareType.Network);
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.Network);
        }

        /// <summary>
    /// Tests getting hardware information selectively (PCMemory)
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformationSelectivePCMemory()
        {
            var InxiInstance = new Inxi(InxiHardwareType.PCMemory);
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.RAM);
        }

        /// <summary>
    /// Tests getting hardware information selectively (Processor)
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformationSelectiveProcessor()
        {
            var InxiInstance = new Inxi(InxiHardwareType.Processor);
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.CPU);
        }

        /// <summary>
    /// Tests getting hardware information selectively (Sound)
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformationSelectiveSound()
        {
            var InxiInstance = new Inxi(InxiHardwareType.Sound);
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.Sound);
        }

        /// <summary>
    /// Tests getting hardware information selectively (System)
    /// </summary>
        [TestMethod]
        public void TestGetHardwareInformationSelectiveSystem()
        {
            var InxiInstance = new Inxi(InxiHardwareType.System);
            var HardwareInfo = InxiInstance.Hardware;
            Assert.IsNotNull(InxiInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.System);
        }

    }
}