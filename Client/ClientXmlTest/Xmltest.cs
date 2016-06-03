using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sp;

namespace ClientXmlTest
{
    [TestClass]
    public class Xmltest
    {
        string filename = "config.xml";
        string ip = "127.0.0.1";
        int port = 1322;
        uint id = 1488;

        [TestMethod]
        public void setIpToXMLTest()
        {
            Client.Xml Xmlunit = new Client.Xml(filename);
            Xmlunit.setIpToXML(ip);
        }


        [TestMethod]
        public void setIdToXMLTest()
        {
            Client.Xml Xmlunit = new Client.Xml(filename);
            Xmlunit.setIdtoXML(id);
        }


        [TestMethod]
        public void sePortToXMLTest()
        {
            Client.Xml Xmlunit = new Client.Xml(filename);
            Xmlunit.setPortToXML(port);
        }


        [TestMethod]
        public void getIpfromXMTest()
        {
            Client.Xml Xmlunit = new Client.Xml(filename);
            string actualIp = Xmlunit.getIpfromXML();
            Assert.AreEqual(ip, actualIp, "incorrect reading IP from " + filename);
        }


        [TestMethod]
        public void getIdToXMLTest()
        {
            uint idactual;
            Client.Xml Xmlunit = new Client.Xml(filename);
            idactual = Xmlunit.getIdfromXML();
            Assert.AreEqual(id, idactual, "incorrect reading ID from "+filename);
        }


        [TestMethod]
        public void getPortToXMLTest()
        {
            int actualPort;
            Client.Xml Xmlunit = new Client.Xml(filename);
            actualPort = Xmlunit.getPortfromXML();
            Assert.AreEqual(port, actualPort, "incorrect reading PORT from " + filename);
        }
    }
}
