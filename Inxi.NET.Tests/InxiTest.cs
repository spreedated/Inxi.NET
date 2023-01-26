
// Inxi.NET  Copyright (C) 2020-2021  Aptivi
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

using NUnit.Framework;

namespace InxiFrontend.Tests
{

    [TestFixture]
    public class InxiTest
    {
        private Inxi testInstance = null;

        [SetUp]
        public void SetUp()
        {
            this.testInstance = new();
        }

        /// <summary>
        /// Tests getting hardware information
        /// </summary>
        [Test]
        public void TestGetHardwareInformation()
        {
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
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
        [Test]
        public void TestGetHardwareInformationSelectiveBIOS()
        {
            this.testInstance.SelectedHardwareTypes = InxiHardwareType.BIOS;
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.BIOS);
        }

        /// <summary>
        /// Tests getting hardware information selectively (Graphics)
        /// </summary>
        [Test]
        public void TestGetHardwareInformationSelectiveGraphics()
        {
            this.testInstance.SelectedHardwareTypes = InxiHardwareType.Graphics;
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.GPU);
        }

        /// <summary>
        /// Tests getting hardware information selectively (HardDrive)
        /// </summary>
        [Test]
        public void TestGetHardwareInformationSelectiveHardDrive()
        {
            this.testInstance.SelectedHardwareTypes = InxiHardwareType.HardDrive;
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.HDD);
        }

        /// <summary>
        /// Tests getting hardware information selectively (Machine)
        /// </summary>
        [Test]
        public void TestGetHardwareInformationSelectiveMachine()
        {
            this.testInstance.SelectedHardwareTypes = InxiHardwareType.Machine;
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.Machine);
        }

        /// <summary>
        /// Tests getting hardware information selectively (Network)
        /// </summary>
        [Test]
        public void TestGetHardwareInformationSelectiveNetwork()
        {
            this.testInstance.SelectedHardwareTypes = InxiHardwareType.Network;
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.Network);
        }

        /// <summary>
        /// Tests getting hardware information selectively (PCMemory)
        /// </summary>
        [Test]
        public void TestGetHardwareInformationSelectivePCMemory()
        {
            this.testInstance.SelectedHardwareTypes = InxiHardwareType.PCMemory;
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.RAM);
        }

        /// <summary>
        /// Tests getting hardware information selectively (Processor)
        /// </summary>
        [Test]
        public void TestGetHardwareInformationSelectiveProcessor()
        {
            this.testInstance.SelectedHardwareTypes = InxiHardwareType.Processor;
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.CPU);
        }

        /// <summary>
        /// Tests getting hardware information selectively (Sound)
        /// </summary>
        [Test]
        public void TestGetHardwareInformationSelectiveSound()
        {
            this.testInstance.SelectedHardwareTypes = InxiHardwareType.Sound;
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.Sound);
        }

        /// <summary>
        /// Tests getting hardware information selectively (System)
        /// </summary>
        [Test]
        public void TestGetHardwareInformationSelectiveSystem()
        {
            this.testInstance.SelectedHardwareTypes = InxiHardwareType.System;
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.IsNotNull(this.testInstance);
            Assert.IsNotNull(HardwareInfo);
            Assert.IsNotNull(HardwareInfo.System);
        }

    }
}