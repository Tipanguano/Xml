using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace ClubPeliculas
{
    class clientes
    {
        prestamo pr = new prestamo();
        string ced, nom, ape, docXml, xmlNodo;
        string ruta;
        XmlDocument miXML = new XmlDocument();

        public static bool comprobar(string operacion, string numero)
        {
            Regex r = new Regex(numero);
            if (r.IsMatch(operacion) == true)
            { return true; }
            else { return false; }
        }

        public static bool convertir(string letra)
        {
            int a;
            if (int.TryParse(letra, out a))
            { return true; }
            else { return false; }
        }

        public void datosCliente(string cedula, string nombre, string apellido)
        {
            ced = cedula;
            nom = nombre;
            ape = apellido;
        }

        public void datosXml(string nameXml, string nodoXml)
        {
            this.docXml = nameXml;
            this.xmlNodo = nodoXml;
            this.ruta = @"D:\xavier\pack de proyectos\Visual Studio\Soluciones\proyectoClub\" + docXml + ".xml";
        }

        public void presentarCliente()
        {
            Console.WriteLine("Cedula:" + ced);
            Console.WriteLine("nombre:" + nom);
            Console.WriteLine("Cedula:" + ape);
        }

        public XmlNode crearNodo(string nodoCliente, string cedula, string nombre, string apellido)
        {
            XmlElement cliente = miXML.CreateElement(nodoCliente);
            XmlAttribute dni = miXML.CreateAttribute("dni");
            dni.Value = cedula;
            cliente.SetAttributeNode(dni);

            XmlElement nom = miXML.CreateElement("Nombres".ToLower());
            nom.InnerText = nombre;
            cliente.AppendChild(nom);
            XmlElement ape = miXML.CreateElement("Apellidos".ToLower());
            ape.InnerText = apellido;
            cliente.AppendChild(ape);

            return cliente;
        }

        public void InsertarXml()
        {
            miXML.Load(ruta);
            XmlNode cliente = this.crearNodo(xmlNodo, ced, nom, ape);
            XmlNode raiz = miXML.DocumentElement;
            raiz.InsertAfter(cliente, raiz.LastChild);
            miXML.Save(ruta);
        }

        public void crearXml()
        {
            if (File.Exists(ruta))
            {
                Console.WriteLine("Xml ya existe..\n" + ruta);
            }
            else
            {
                
                XmlDeclaration version = miXML.CreateXmlDeclaration("1.0", "utf-8", "no");
                XmlComment info = miXML.CreateComment("Listado de " + docXml);

                XmlElement raiz = miXML.CreateElement(docXml);
                miXML.AppendChild(raiz);
                miXML.InsertBefore(version, raiz);
                miXML.InsertBefore(info, raiz);
                miXML.Save(ruta);
            }
        }

        public void leerXml()
        {
            XDocument doc = XDocument.Load(ruta);
            Console.WriteLine(doc.ToString());
            Console.ReadKey();
            var clientes = from cli in doc.Descendants("clientes") select cli;
            Console.WriteLine("Cedula\t\tNombres\t\tApellidos");
            foreach (XElement etiquetas in clientes.Elements("cliente"))
            {
                Console.Write(etiquetas.Element("dni").Value);
                Console.Write(" " + etiquetas.Element("nombres").Value);
                Console.Write("  " + etiquetas.Element("apellidos").Value + "\n");
            }

        }

        public void listaXml(int top)
        {
            miXML.Load(ruta);
            XmlNodeList clientes = miXML.GetElementsByTagName(docXml);
            XmlNodeList lista =
            ((XmlElement)clientes[0]).GetElementsByTagName(xmlNodo);
            Console.SetCursorPosition(1, top++);
            Console.WriteLine("CEDULA\t \tNOMBRE \t\tAPELLIDO");
            foreach (XmlElement etiquetas in lista)
            {
                int i = 0;
                string dni = etiquetas.GetAttribute("dni");
                XmlNodeList nombre = etiquetas.GetElementsByTagName("nombres");
                XmlNodeList ape = etiquetas.GetElementsByTagName("apellidos");
                Console.SetCursorPosition(1, top);
                Console.Write(dni );
                Console.SetCursorPosition(15, top);
                Console.Write(nombre[i].InnerText);
                Console.SetCursorPosition(32, top++);
                Console.Write(ape[i++].InnerText + "\n");
            }
        }

        public void BuscarCliente(string dni)
        {
            miXML.Load(ruta);
            XmlNodeList clientes = miXML.GetElementsByTagName(docXml);
            XmlNodeList lista =
            ((XmlElement)clientes[0]).GetElementsByTagName("cliente");
            int top = 5;
            Console.WriteLine("\nCedula\t\tNombre\t\tApellido");
            foreach (XmlElement etiquetas in lista)
            {
                int i = 0;
                string cedul = etiquetas.GetAttribute("dni");
                
                if (cedul.Equals(dni))
                {
                    XmlNodeList nombre = etiquetas.GetElementsByTagName("nombres");
                    XmlNodeList ape = etiquetas.GetElementsByTagName("apellidos");
                    Console.SetCursorPosition(0, top);
                    Console.Write(cedul);
                    Console.SetCursorPosition(13, top);
                    Console.Write(nombre[i].InnerText);
                    Console.SetCursorPosition(31, top++);
                    Console.Write(ape[i].InnerText + "\n");
                    pr.cedula = cedul;
                    pr.apellido = ape[i].InnerText;
                    pr.nombre = nombre[i].InnerText;
                    i++;
                    }

            }

        }
        public bool existeCliente(string dni)
        {
            bool msj = false;
            miXML.Load(ruta);
            XmlNodeList clientes = miXML.GetElementsByTagName(docXml);
            XmlNodeList lista =
            ((XmlElement)clientes[0]).GetElementsByTagName("cliente");
            foreach (XmlElement etiquetas in lista)
            {
                string cedul = etiquetas.GetAttribute("dni");
                if (cedul.Equals(dni))
                {
                    msj = true;
                }

            }
            return msj;
        }

        public void ModificarDatosXml()
        {
            XmlNodeList nodeList = miXML.SelectNodes("//" + xmlNodo);
            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes["dni"].Value.Equals(ced))
                {
                    node.FirstChild.InnerText = nom;
                    node.LastChild.InnerText = ape;
                }

            }
            miXML.Save(ruta);
        }
        public void EliminarDatosXml(string cedula)
        {
            miXML.Load(ruta);
            XmlNode node = miXML.SelectSingleNode("//"+xmlNodo+"[@dni='"+cedula+"']");
            node.ParentNode.RemoveChild(node);
            miXML.Save(ruta);
        }
 
    }
    
}
