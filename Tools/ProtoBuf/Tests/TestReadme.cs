using System;
using System.IO;
using Personal;
using NUnit.Framework;

namespace Test
{
    [TestFixture()]
    public class TestReadme
    {
        /// <summary>
        /// Example found in the README file
        /// </summary>
        [Test()]
        public void Run()
        {
            MemoryStream stream = new MemoryStream();

            Person person = new Person();
            person.Name = "George";
            Person.Serialize(stream, person);

            stream.Seek(0, SeekOrigin.Begin);

            Person person2 = Person.Deserialize(stream);
            Assert.AreEqual(person.Name, person2.Name);
        }
    }
}

