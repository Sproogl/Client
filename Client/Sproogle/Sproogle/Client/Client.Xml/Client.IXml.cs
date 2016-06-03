using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp
{
    interface IXml
    {


        /// <summary>
        /// Add ip to file.xml
        /// </summary>
        /// <param name = "lip"> ip for adding a file </param>
        /// <returns>
        /// </returns>
        void setIpToXML(string lip);



        /// <summary>
        /// Add port to file.xml
        /// </summary>
        /// <param name = "lip"> port for adding a file </param>
        /// <returns>
        /// </returns>
        void setPortToXML(int lport);



        /// <summary>
        /// Add id to file.xml
        /// </summary>
        /// <param name = "ID"> id for adding a file </param>
        /// <returns>
        /// </returns>
        void setIdtoXML(uint ID);



        /// <summary>
        /// returned Ip from file.xml
        /// </summary>
        /// <param></param>
        /// <returns>
        /// string ID
        /// </returns>
        string getIpfromXML();



        /// <summary>
        /// returned id from file.xml
        /// </summary>
        /// <returns>
        /// uint id
        /// </returns>
        uint getIdfromXML();



        /// <summary>
        /// returned port from file.xml
        /// </summary>
        /// <returns>
        /// int port
        /// </returns>
        int getPortfromXML();
       
    }
}
