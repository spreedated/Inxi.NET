using NUnit.Framework;

namespace InxiFrontend.Tests
{
    [TestFixture]
    public class EqualityModelsTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void BatteryEqualityTests()
        {
            Battery b = new("foo",12, "foo", "foo", "foo", "foo");
            Battery c = new("foo",12, "foo", "foo", "foo", "foo");
            Battery d = new("foo",13, "foo", "foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);
            Assert.That(b.Equals(d), Is.False);

            c = new("fool", 12, "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", 12, "fool", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", 12, "foo", "fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", 12, "foo", "foo", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", 12, "foo", "foo", "foo", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void BiosEqualityTests()
        {
            BIOS b = new("foo", "foo", "foo");
            BIOS c = new("foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void GraphicsEqualityTests()
        {
            Graphics b = new("foo", "foo", "foo", "foo", "foo");
            Graphics c = new("foo", "foo", "foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void HardDriveEqualityTests()
        {
            HardDrive b = new("foo", "foo", "foo", "foo", "foo", "foo", new() { {"foo", new("foo", "foo", "foo", "foo") } });
            HardDrive c = new("foo", "foo", "foo", "foo", "foo", "foo", new() { {"foo", new("foo", "foo", "foo", "foo") } });

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo", "foo", "foo", "foo", new() { {"foo", new("foo", "foo", "foo", "foo") } });
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo", "foo", "foo", "foo", new() { { "foo", new("foo", "foo", "foo", "foo") } });
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo", "foo", "foo", new() { { "foo", new("foo", "foo", "foo", "foo") } });
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "fool", "foo", "foo", new() { { "foo", new("foo", "foo", "foo", "foo") } });
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "fool", "foo", new() { { "foo", new("foo", "foo", "foo", "foo") } });
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "foo", "fool", new() { { "foo", new("foo", "foo", "foo", "foo") } });
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void PartitionEqualityTests()
        {
            Partition b = new("foo", "foo", "foo", "foo");
            Partition c = new("foo", "foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void MachineInfoEqualityTests()
        {
            MachineInfo b = new("foo", "foo", "foo", "foo", "foo", "foo");
            MachineInfo c = new("foo", "foo", "foo", "foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo", "foo", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void NetworkEqualityTests()
        {
            Network b = new("foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo");
            Network c = new("foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo", "foo", "foo", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "fool", "foo", "foo", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "fool", "foo", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "foo", "fool", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "foo", "fool", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "foo", "foo", "fool", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "foo", "foo", "foo", "fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "foo", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void PCMemoryEqualityTests()
        {
            PCMemory b = new("foo", "foo", "foo");
            PCMemory c = new("foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void ProcessorEqualityTests()
        {
            Processor b = new("foo", "foo", "foo", 12, "foo", new string[] { "foo", "foo" }, "foo", 1024, "foo", 14525, "foo");
            Processor c = new("foo", "foo", "foo", 12, "foo", new string[] { "foo", "foo" }, "foo", 1024, "foo", 14525, "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo", 12, "foo", new string[] { "foo", "foo" }, "foo", 1024, "foo", 14525, "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo", 12, "foo", new string[] { "foo", "foo" }, "foo", 1024, "foo", 14525, "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", 12, "foo", new string[] { "foo", "foo" }, "foo", 1024, "foo", 14525, "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", 14, "foo", new string[] { "foo", "foo" }, "foo", 1024, "foo", 14525, "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", 12, "fool", new string[] { "foo", "foo" }, "foo", 1024, "foo", 14525, "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", 12, "foo", new string[] { "foo", "foo" }, "fool", 1024, "foo", 14525, "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", 12, "foo", new string[] { "foo", "foo" }, "foo", 512, "foo", 14525, "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", 12, "foo", new string[] { "foo", "foo" }, "foo", 1024, "fool", 14525, "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", 12, "foo", new string[] { "foo", "foo" }, "foo", 1024, "foo", 14528, "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", 12, "foo", new string[] { "foo", "foo" }, "foo", 1024, "foo", 14525, "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void SoundEqualityTests()
        {
            Sound b = new("foo", "foo", "foo", "foo", "foo");
            Sound c = new("foo", "foo", "foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void SystemInfoEqualityTests()
        {
            SystemInfo b = new("foo", "foo", 64, "foo", "foo", "foo", "foo");
            SystemInfo c = new("foo", "foo", 64, "foo", "foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", 64, "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", 64, "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", 32, "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", 64, "fool", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", 64, "foo", "fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", 64, "foo", "foo", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", 64, "foo", "foo", "foo", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [Test]
        public void WindowsLogicalPartitionEqualityTests()
        {
            WindowsLogicalPartition b = new("foo", "foo", "foo", "foo", "foo");
            WindowsLogicalPartition c = new("foo", "foo", "foo", "foo", "foo");

            Assert.That(b.Equals(c), Is.True);

            c = new("fool", "foo", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "fool", "foo", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "fool", "foo", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "fool", "foo");
            Assert.That(b.Equals(c), Is.False);

            c = new("foo", "foo", "foo", "foo", "fool");
            Assert.That(b.Equals(c), Is.False);
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
