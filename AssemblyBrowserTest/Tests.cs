using System;
using System.Reflection;
using AssemblyBrowser;
using AssemblyBrowser.Enums;
using AssemblyBrowserTest.ClassForTest;
using NUnit.Framework;

namespace AssemblyBrowserTest
{
    [TestFixture]
    public class Tests
    {
        private const string Path = "D:\\AssemblyBrowserWPF\\TestAssembly.dll";
        private const int ExpectedNamespaceCount = 2;
        private const int ExpectedPropertyCount = 2;
        private const int ExpectedParameterCount = 1;
        private const int ExpectedFieldCount = 4;
        private const int ExpectedMethodCount = 11;
        private readonly BrowserOfAssembly _browserOfAssembly = new BrowserOfAssembly();

        [Test]
        public void Browse_Of_Object_Should_Return_Eleven_Methods()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(typeof(object));
            Assert.AreEqual(ExpectedMethodCount, browsingResult.Namespaces[0].AssemblyTypes[0].Methods.Count);
        }

        [Test]
        public void Browse_Of_Class_A_Should_Return_Abstract_Type_And_Abstract_Property()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(typeof(A));
            Assert.IsTrue(browsingResult.Namespaces[0].AssemblyTypes[0].IsAbstract);
            Assert.IsTrue(browsingResult.Namespaces[0].AssemblyTypes[0].Properties[0].IsAbstract);
        }

        [Test]
        public void Browse_Of_Class_B_Should_Return_Four_Fields()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(typeof(B));
            Assert.AreEqual(ExpectedFieldCount, browsingResult.Namespaces[0].AssemblyTypes[0].Fields.Count);
        }

        [Test]
        public void Browse_Of_Class_B_Should_Return_Two_Properties()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(typeof(B));
            Assert.AreEqual(ExpectedPropertyCount, browsingResult.Namespaces[0].AssemblyTypes[0].Properties.Count);
        }

        [Test]
        public void Browse_Of_AsmBrowserAssembly_Should_Return_Two_Namespace()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(Path);
            Assert.AreEqual(ExpectedNamespaceCount, browsingResult.Namespaces.Count);
        }

        [Test]
        public void Browse_Of_Class_C_Should_Return_One_Method_With_One_Out_Parameter()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(typeof(C));
            Assert.AreEqual(ExpectedParameterCount,
                browsingResult.Namespaces[0].AssemblyTypes[0].Methods[0].Parameters.Count);
            Assert.AreEqual(ParameterType.Out,
                browsingResult.Namespaces[0].AssemblyTypes[0].Methods[0].Parameters[0].PassingType);
        }
    }
}