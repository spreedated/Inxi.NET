using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

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

        [Test]
        [Description("Async testrun, must finish within one minute.")]
        public void TestRunOnAsyncTest()
        {
            Stopwatch s = new();
            TimeSpan timeConstraint = new TimeSpan(0, 1, 0);
            CancellationTokenSource ct = new(timeConstraint);
            this.testInstance.RunFinished += (o, s) => { ct.Cancel(); };

            Task.Factory.StartNew(async () =>
            {
                s.Start();
                await this.testInstance.RetrieveInformationAsync();
                s.Stop();
            });

            while (!ct.IsCancellationRequested)
            {
                Thread.Sleep(50);
            }

            s.Stop();
            Assert.That(s.Elapsed, Is.LessThan(timeConstraint));

            var HardwareInfo = this.testInstance.Hardware;

            Assert.Multiple(() =>
            {
                Assert.That(HardwareInfo.CPU, Is.Not.Null);
                Assert.That(HardwareInfo.GPU, Is.Not.Null);
                Assert.That(HardwareInfo.RAM, Is.Not.Null);
                Assert.That(HardwareInfo.HDD, Is.Not.Null);
                Assert.That(HardwareInfo.Sound, Is.Not.Null);
                Assert.That(HardwareInfo.System, Is.Not.Null);
                Assert.That(HardwareInfo.BIOS, Is.Not.Null);
                Assert.That(HardwareInfo.Machine, Is.Not.Null);
            });
        }

        /// <summary>
        /// Tests getting hardware information
        /// </summary>
        [Test]
        public void TestGetHardwareInformation()
        {
            this.testInstance.RetrieveInformation();
            var HardwareInfo = this.testInstance.Hardware;
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.CPU);
                Assert.IsNotNull(HardwareInfo.GPU);
                Assert.IsNotNull(HardwareInfo.RAM);
                Assert.IsNotNull(HardwareInfo.HDD);
                Assert.IsNotNull(HardwareInfo.Sound);
                Assert.IsNotNull(HardwareInfo.System);
                Assert.IsNotNull(HardwareInfo.BIOS);
                Assert.IsNotNull(HardwareInfo.Machine);
            });
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
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.BIOS);
            });
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
            Assert.Multiple(() =>
            {

                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.GPU);
            });
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
            Assert.Multiple(() =>
            {

                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.HDD);
            });
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
            Assert.Multiple(() =>
            {

                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.Machine);
            });
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
            Assert.Multiple(() =>
            {

                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.Network);
            });
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
            Assert.Multiple(() =>
            {

                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.RAM);
            });
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
            Assert.Multiple(() =>
            {

                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.CPU);
            });
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
            Assert.Multiple(() =>
            {

                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.Sound);
            });
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
            Assert.Multiple(() =>
            {

                Assert.IsNotNull(HardwareInfo);
                Assert.IsNotNull(HardwareInfo.System);
            });
        }

    }
}