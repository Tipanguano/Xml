using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace ClubPeliculas
{
    class peliculas
    {
        string cod, titulo,fechPublicacion, genero, docXmlPeicula, nodoPeliculaXml;
        string rutapelicula;
        XmlDocument miXMLPelicula = new XmlDocument();
        string estado;
        public void peliculasDato(string peliculaXmlraiz, string PeliculaXmlnodo)
        {
            this.docXmlPeicula = peliculaXmlraiz;
            this.nodoPeliculaXml = PeliculaXmlnodo;
            this.rutapelicula = @"D:\xavier\pack de proyectos\Visual Studio\Soluciones\proyectoClub\"+docXmlPeicula+".xml";
        }

        public static bool validar(string opcion){
            byte numero;
            if (byte.TryParse(opcion, out numero))
            { return true; }
            else { return false; }
        }

        public void datosPelicula(string tituloPelicula, string añoPublicacion, string generoPelicula)
        {
            this.titulo = tituloPelicula.ToUpper();
            this.fechPublicacion = añoPublicacion.ToUpper();
            this.genero = generoPelicula.ToUpper();
            this.estado = "disponible".ToUpper();
        }

        public void crearXml()
        {
            if (File.Exists(rutapelicula))
            {
                Console.WriteLine("Xml ya existe..\n" + rutapelicula);
            }
            else
            {
                XmlDeclaration version = miXMLPelicula.CreateXmlDeclaration("1.0", "utf-8", "no");
                XmlComment info = miXMLPelicula.CreateComment("Listado de " + docXmlPeicula);

                XmlElement raiz = miXMLPelicula.CreateElement(docXmlPeicula);
                miXMLPelicula.AppendChild(raiz);
                miXMLPelicula.InsertBefore(version, raiz);
                miXMLPelicula.InsertBefore(info, raiz);
                miXMLPelicula.Save(rutapelicula);
            }
        }

        public void InsertarNodoXml()
        {
            miXMLPelicula.Load(rutapelicula);
            XmlNode pelicula = this.crearNodo(nodoPeliculaXml,generarCodigo(), titulo, fechPublicacion, genero, estado);
            XmlNode raiz = miXMLPelicula.DocumentElement;
            raiz.InsertAfter(pelicula, raiz.LastChild);
            miXMLPelicula.Save(rutapelicula);
        }

        public XmlNode crearNodo(string nodoPeliculaXml, string codigo, string titulo, string fechPublicacion, string genero, string estadoP)
        {
            XmlElement pelicula = miXMLPelicula.CreateElement(nodoPeliculaXml);
            XmlAttribute codigoP = miXMLPelicula.CreateAttribute("codigo");
            codigoP.Value = codigo;
            pelicula.SetAttributeNode(codigoP);
            XmlElement tittle = miXMLPelicula.CreateElement("titulo".ToLower());
            tittle.InnerText = titulo;
            pelicula.AppendChild(tittle);
            XmlElement datePublic = miXMLPelicula.CreateElement("añoPublicacion".ToLower());
            datePublic.InnerText = fechPublicacion;
            pelicula.AppendChild(datePublic);
            XmlElement genre = miXMLPelicula.CreateElement("genero".ToLower());
            genre.InnerText = genero;
            pelicula.AppendChild(genre);
            XmlElement estado = miXMLPelicula.CreateElement("estado".ToLower());
            estado.InnerText = estadoP;
            pelicula.AppendChild(estado);
            return pelicula;
        }

        public string generarCodigo() {
            miXMLPelicula.Load(rutapelicula);
            int id = 1;
                do{
                    if (existePelicula(id.ToString().PadLeft(5, '0')))
                    {
                        id++;
                    }
                    else { break; }
                }while(true);
             cod = id.ToString().PadLeft(5, '0');
                
            return cod;
        }

        public void listarPeliculas(int top)
        {
            miXMLPelicula.Load(rutapelicula);
            XmlNodeList peliculas = miXMLPelicula.GetElementsByTagName(docXmlPeicula);
            XmlNodeList lista =
            ((XmlElement)peliculas[0]).GetElementsByTagName(nodoPeliculaXml);
            Console.SetCursorPosition(1, top++);
            Console.WriteLine("id\tTitulo\t\t\t\tPublicacion\tGenero\t\tEstado ");
            foreach (XmlElement etiquetas in lista)
            {
                int i = 0;
                string id = etiquetas.GetAttribute("codigo");
                XmlNodeList titulos = etiquetas.GetElementsByTagName("titulo");
                XmlNodeList fechas = etiquetas.GetElementsByTagName("añopublicacion");
                XmlNodeList generos = etiquetas.GetElementsByTagName("genero");
                XmlNodeList estados = etiquetas.GetElementsByTagName("estado");
                Console.SetCursorPosition(1, top);
                Console.WriteLine(id);
                Console.SetCursorPosition(8, top);
                Console.WriteLine(titulos[i].InnerText);
                Console.SetCursorPosition(42, top);
                Console.Write(fechas[i].InnerText);
                Console.SetCursorPosition(55, top);
                Console.Write(generos[i].InnerText);
                Console.SetCursorPosition(67, top);
                Console.Write(estados[i++].InnerText+"\n");
                top++;
            }
        }

        public bool existePelicula(string codigoP)
        {
            bool msj = false;
            miXMLPelicula.Load(rutapelicula);
            XmlNodeList peliculas = miXMLPelicula.GetElementsByTagName(docXmlPeicula);
            XmlNodeList lista =
            ((XmlElement)peliculas[0]).GetElementsByTagName(nodoPeliculaXml);
            foreach (XmlElement etiquetas in lista)
            {
                string codi = etiquetas.GetAttribute("codigo");
                if (codi.Equals(codigoP))
                {
                    msj = true;
                }
            }
            return msj;
        }

        public void buscarPelicula(string codigoP)
        {
            miXMLPelicula .Load(rutapelicula);
            XmlNodeList clientes = miXMLPelicula.GetElementsByTagName(docXmlPeicula);
            XmlNodeList lista =
            ((XmlElement)clientes[0]).GetElementsByTagName(nodoPeliculaXml);
            int top = 3;
            Console.WriteLine("id\tTitulo\t\t\tPublicacion\tGenero\t\tEstado ");
            foreach (XmlElement etiquetas in lista)
            {
                int i = 0;
                string id = etiquetas.GetAttribute("codigo");
                if (id.Equals(codigoP))
                {
                    XmlNodeList tittle = etiquetas.GetElementsByTagName("titulo");
                    XmlNodeList date = etiquetas.GetElementsByTagName("añopublicacion");
                    XmlNodeList genero = etiquetas.GetElementsByTagName("genero");
                    XmlNodeList estado = etiquetas.GetElementsByTagName("estado");
                    Console.WriteLine(id + "\t" + tittle[i].InnerText);
                    Console.SetCursorPosition(34, top);
                    Console.Write(date[i].InnerText);
                    Console.SetCursorPosition(49, top);
                    Console.Write(genero[i].InnerText);
                    Console.SetCursorPosition(64, top);
                    Console.Write(estado[i++].InnerText + "\n");
                    top++;
                }
            }

        }

        public void ModificarPeliculaXml(string codigoPelicula)
        {
            XmlNodeList peli = miXMLPelicula.GetElementsByTagName(docXmlPeicula);
            XmlNodeList lista = ((XmlElement)peli[0]).GetElementsByTagName(nodoPeliculaXml);
            foreach (XmlElement etiquetas in lista)
            {
                string id = etiquetas.GetAttribute("codigo");
                if (id.Equals(codigoPelicula))
                {
                    etiquetas.SelectSingleNode("titulo").InnerText = titulo;
                    etiquetas.SelectSingleNode("añopublicacion").InnerText = fechPublicacion;
                    etiquetas.SelectSingleNode("genero").InnerText = genero;
                }         
            }    
            miXMLPelicula.Save(rutapelicula);
        }
        
        public void EliminarPeliculaXml(string cod)
        {
            miXMLPelicula.Load(rutapelicula);
            XmlNode node = miXMLPelicula.SelectSingleNode("//" + nodoPeliculaXml + "[@codigo='" + cod + "']");
            node.ParentNode.RemoveChild(node);
            miXMLPelicula.Save(rutapelicula);
            Console.WriteLine("datos eliminados");
        }


    }
}
