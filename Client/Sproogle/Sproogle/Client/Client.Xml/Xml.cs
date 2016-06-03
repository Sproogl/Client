using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace sp
{
    public partial class Client
    {
        public class Xml : IXml
        {
            string fileName;

            public Xml(string filename)
            {
                fileName = filename;
            }

            public void setIdtoXML(uint ID)
            {

                XDocument docin = XDocument.Load(fileName);
                XElement port = docin.Root.Element("port");
                XElement ip = docin.Root.Element("ip");


                XDocument doc = new XDocument(new XElement("person",
                                                       new XElement("id", ID),
                                                       ip, port));
                doc.Save(fileName);

            }


            public uint getIdfromXML()
            {


                uint id = 12345;

                XDocument docin = XDocument.Load(fileName);

                XElement element = docin.Root.Element("id");
                try
                {

                    id = Convert.ToUInt32(element.Value);

                }

                catch (NullReferenceException e)
                {
                    return 0;
                }

                return id;
            }

            public void setPortToXML(int lport)
            {

                XDocument docin = XDocument.Load(fileName);
                XElement id = docin.Root.Element("id");
                XElement ip = docin.Root.Element("ip");


                XDocument doc = new XDocument(new XElement("person",
                                                            id,
                                                            ip,
                                                            new XElement("port", lport)));
                doc.Save(fileName);



            }

            public void setIpToXML(string lip)
            {

                XDocument docin = XDocument.Load(fileName);
                XElement id = docin.Root.Element("id");
                XElement port = docin.Root.Element("port");

                XDocument doc = new XDocument(new XElement("person",
                                                            id,
                                                            new XElement("ip", lip),
                                                            port));
                doc.Save(fileName);



            }


            public string getIpfromXML()
            {

                string ip;

                XDocument docin = XDocument.Load(fileName);

                XElement element = docin.Root.Element("ip");
                try
                {

                    ip = element.Value;

                }

                catch (NullReferenceException e)
                {
                    return null;
                }

                return ip;
            }
            public int getPortfromXML()
            {


                int port = 12345;

                XDocument docin = XDocument.Load(fileName);

                XElement element = docin.Root.Element("port");
                try
                {

                    port = Convert.ToInt32(element.Value);

                }

                catch (NullReferenceException e)
                {
                    return 0;
                }

                return port;
            }
        }
    }
}
