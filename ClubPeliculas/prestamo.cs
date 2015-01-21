using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ClubPeliculas
{
    class prestamo
    {
        XmlDocument XMLprestamo = new XmlDocument();
        XmlDocument XMLCliente = new XmlDocument();
        XmlDocument XMLPelicula = new XmlDocument();
        public string rutaPrestamo, docXmlPrestamo, nodoXmlPrestamo;

        public string nombre;
        public string apellido;
        public string cedula;

        public string codigo;
        public string titulo;
        public string fecha;
        public string genero;
        public string estado;
        public string idPrestamo;
        public string fechaSist { get; set; }
        public string caducidad { get; set; }
        public string docXmlCliente, xmlNodoCliente, rutaCliente;
        public string docXmlPeicula, nodoPeliculaXml, rutaPelicula;
      
        public void datosCliente(string cedula, string nombre, string apellido)
         {
             this.cedula = cedula;
             this.nombre = nombre;
             this.apellido = apellido;
         }
        
        public void prestamoDto(string raiz, string nodo)
        {
            this.docXmlPrestamo = raiz;
            this.nodoXmlPrestamo = nodo;
            this.rutaPrestamo = @"D:\xavier\pack de proyectos\Visual Studio\Soluciones\proyectoClub\" + docXmlPrestamo + ".xml";
        }

        public void XmlCliente(string raizCliente, string nodoCliente)
        {
            this.docXmlCliente = raizCliente;
            this.xmlNodoCliente = nodoCliente;
            this.rutaCliente = @"D:\xavier\pack de proyectos\Visual Studio\Soluciones\proyectoClub\" + docXmlCliente + ".xml";
        }

        public void Xmlpelicula(string peliculaXmlraiz, string PeliculaXmlnodo)
        {
            this.docXmlPeicula = peliculaXmlraiz;
            this.nodoPeliculaXml = PeliculaXmlnodo;
            this.rutaPelicula = @"D:\xavier\pack de proyectos\Visual Studio\Soluciones\proyectoClub\" + docXmlPeicula + ".xml";
        }

        public void crearXml()
        {
            if (File.Exists(rutaPrestamo))
            {
                Console.WriteLine("Xml ya existe..\n" + rutaPrestamo);
            }
            else
            {
                XmlDeclaration version = XMLprestamo.CreateXmlDeclaration("1.0", "utf-8", "no");
                XmlComment info = XMLprestamo.CreateComment("Listado de " + docXmlPrestamo);

                XmlElement raiz = XMLprestamo.CreateElement(docXmlPrestamo);
                XMLprestamo.AppendChild(raiz);
                XMLprestamo.InsertBefore(version, raiz);
                XMLprestamo.InsertBefore(info, raiz);
                XMLprestamo.Save(rutaPrestamo);
            }
        }

        public void presentarClienteP()
        {
            Console.WriteLine("\n\nCedula: " + cedula);
            Console.WriteLine("Nombre: " + nombre);
            Console.WriteLine("Apellido: " + apellido);
        }

        public void presentarPelicula()
        {   
            Console.WriteLine("\nCodigo: " + codigo);
            Console.WriteLine("Titulo: " + titulo);
            Console.WriteLine("Fecha: " + fecha);
            Console.WriteLine("Genero: " + genero);
            Console.WriteLine("Estado: " + estado);
        }

        public void BuscarCliente(string dni)
        {
            XMLCliente.Load(rutaCliente);
            XmlNodeList clientes = XMLCliente.GetElementsByTagName(docXmlCliente);
            XmlNodeList lista =
            ((XmlElement)clientes[0]).GetElementsByTagName("cliente");
            foreach (XmlElement etiquetas in lista)
            {
                int i = 0;
                string cedul = etiquetas.GetAttribute("dni");
                if (cedul.Equals(dni))
                {
                    XmlNodeList nombre = etiquetas.GetElementsByTagName("nombres");
                    XmlNodeList ape = etiquetas.GetElementsByTagName("apellidos");
                    this.cedula = cedul;
                    this.nombre = nombre[i].InnerText;
                    this.apellido= ape[i].InnerText;
                    i++;
                }
            }
        }

        public void buscarPelicula(string tituloPe)
        {
            XMLPelicula.Load(rutaPelicula);
            XmlNodeList clientes = XMLPelicula.GetElementsByTagName(docXmlPeicula);
            XmlNodeList lista =((XmlElement)clientes[0]).GetElementsByTagName(nodoPeliculaXml);
            foreach (XmlElement etiquetas in lista)
            {
                int i = 0;
                XmlNodeList nTittle = etiquetas.GetElementsByTagName("titulo");
                XmlNodeList ndate = etiquetas.GetElementsByTagName("añopublicacion");
                XmlNodeList ngenero = etiquetas.GetElementsByTagName("genero");
                XmlNodeList nestado = etiquetas.GetElementsByTagName("estado");
                if (nTittle[i].InnerText.Equals(tituloPe))
                {
                    this.codigo = etiquetas.GetAttribute("codigo");
                    this.titulo = nTittle[i].InnerText;
                    this.fecha = ndate[i].InnerText;
                    this.genero = ngenero[i].InnerText;
                    this.estado = nestado[i].InnerText;
                 }
                i++;
            }
        }

        public void ModificarPelicula(string valor,string newEstado)
        {
            XMLPelicula.Load(rutaPelicula);
            XmlNodeList peli = XMLPelicula.GetElementsByTagName(docXmlPeicula);
            XmlNodeList lista = ((XmlElement)peli[0]).GetElementsByTagName(nodoPeliculaXml);
            int i = 0;
            foreach (XmlElement etiquetas in lista)
            {
                XmlNodeList nCodigo = etiquetas.SelectNodes("//@codigo");
                XmlNodeList nTittle = etiquetas.SelectNodes("//titulo");
                if (nTittle[i].InnerText.Equals(valor))
                {
                    etiquetas.SelectSingleNode("estado").InnerText = newEstado;
                }
                else if (nCodigo[i].InnerText.Equals(valor))
                {
                    etiquetas.SelectSingleNode("estado").InnerText = newEstado;
                }
                i++;
            }
            XMLPelicula.Save(rutaPelicula);
        }
        
        public bool PeliculaOcupada(string tituloPe)
        {
            bool msj = false;
            XMLPelicula.Load(rutaPelicula);
            XmlNodeList clientes = XMLPelicula.GetElementsByTagName(docXmlPeicula);
            XmlNodeList lista = ((XmlElement)clientes[0]).GetElementsByTagName(nodoPeliculaXml);
            foreach (XmlElement etiquetas in lista)
            {
                int i = 0;
                XmlNodeList nTittle = etiquetas.GetElementsByTagName("titulo");
                XmlNodeList ndate = etiquetas.GetElementsByTagName("añopublicacion");
                XmlNodeList ngenero = etiquetas.GetElementsByTagName("genero");
                XmlNodeList nestado = etiquetas.GetElementsByTagName("estado");
                if (nTittle[i].InnerText.Equals(tituloPe))
                {
                    if (nestado[i].InnerText.Equals("DISPONIBLE"))
                    {
                        msj = true;
                    }
                }
                i++;
            }
            return msj;
        }
        
        public bool PeliculaExiste(string tituloPe)
        {
            bool msj = false;
            XMLPelicula.Load(rutaPelicula);
            XmlNodeList clientes = XMLPelicula.GetElementsByTagName(docXmlPeicula);
            XmlNodeList lista = ((XmlElement)clientes[0]).GetElementsByTagName(nodoPeliculaXml);
            foreach (XmlElement etiquetas in lista)
            {
                int i = 0;
                XmlNodeList nTittle = etiquetas.GetElementsByTagName("titulo");
                XmlNodeList ndate = etiquetas.GetElementsByTagName("añopublicacion");
                XmlNodeList ngenero = etiquetas.GetElementsByTagName("genero");
                XmlNodeList nestado = etiquetas.GetElementsByTagName("estado");
                if (nTittle[i].InnerText.Equals(tituloPe))
                {
                    msj = true;
                }
                i++;
            }
            return msj;
        }
      
        public string fechaCaducidad(int dias) {
            string fechCaducidad = DateTime.Now.AddDays(dias).ToString("d");
            return fechCaducidad;
        }

        public void InsertarNodoXml(int nuevoPlazo, string fehaCaducar)
        {
            int caducar = nuevoPlazo;
            string fechaSis = Convert.ToString(DateTime.Now.ToString("d"));
            XMLprestamo.Load(rutaPrestamo);
            XmlNode prestamo = this.crearNodo(nodoXmlPrestamo, generarCodigo(), fechaSis, caducar,fehaCaducar);
            
            XmlNode raiz = XMLprestamo.DocumentElement;
            raiz.InsertAfter(prestamo, raiz.LastChild);
            XMLprestamo.Save(rutaPrestamo);
        }

        public XmlNode crearNodo(string nodoPr, string codigoPr, string fechaPr, int caducidad, string fehaCaducar)
        
        {
            XmlElement prestamo = XMLprestamo.CreateElement(nodoPr);
            XmlAttribute codigoPrestamo = XMLprestamo.CreateAttribute("id");
            codigoPrestamo.Value = Convert.ToString(codigoPr);
            prestamo.SetAttributeNode(codigoPrestamo);
            XmlElement cliente = XMLprestamo.CreateElement("cliente".ToLower());
            XmlAttribute idCliente = XMLprestamo.CreateAttribute("cedula");
            idCliente.Value = this.cedula;
            cliente.SetAttributeNode(idCliente);
            XmlElement nomb = XMLprestamo.CreateElement("nombres".ToLower());
            nomb.InnerText = nombre;
            cliente.AppendChild(nomb);
            XmlElement apellid = XMLprestamo.CreateElement("apellidos".ToLower());
            apellid.InnerText = apellido;
            cliente.AppendChild(apellid);
            prestamo.AppendChild(cliente);
            XmlElement pelicula = XMLprestamo.CreateElement("pelicula".ToLower());
            XmlAttribute codigoPel = XMLprestamo.CreateAttribute("codigo");
            codigoPel.Value = this.codigo;
            pelicula.SetAttributeNode(codigoPel);

            XmlElement tittles = XMLprestamo.CreateElement("titulo".ToLower());
            tittles.InnerText = titulo;
            pelicula.AppendChild(tittles);

            XmlElement generos = XMLprestamo.CreateElement("genero".ToLower());
            generos.InnerText = genero;
            pelicula.AppendChild(generos);
            prestamo.AppendChild(pelicula);
            /////////
            XmlElement fechaPres = XMLprestamo.CreateElement("fechaPrestada");
            fechaPres.InnerText = fechaPr;
            prestamo.AppendChild(fechaPres);

            XmlElement nFechaCaducar = XMLprestamo.CreateElement("fechaCaducar");
            nFechaCaducar.InnerText = fehaCaducar;
            prestamo.AppendChild(nFechaCaducar);

            XmlElement caducidadPr = XMLprestamo.CreateElement("caducidad".ToLower());
            caducidadPr.InnerText = caducidad.ToString();
            prestamo.AppendChild(caducidadPr);
            
            return prestamo;
        }

        public void listarPrestamo()
        {
            XMLprestamo.Load(rutaPrestamo);
            XmlNodeList prestamo = XMLprestamo.GetElementsByTagName(docXmlPrestamo);
            XmlNodeList lista =((XmlElement)prestamo[0]).GetElementsByTagName(nodoXmlPrestamo);
            
            foreach (XmlElement etiquetas in lista)
            {
                int i = 0;
                string nId = etiquetas.GetAttribute("id");
                XmlNodeList nCliente = etiquetas.GetElementsByTagName("cliente");
                XmlNodeList nPelicula = etiquetas.GetElementsByTagName("pelicula");
                XmlNodeList nFecha = etiquetas.GetElementsByTagName("fechaPrestada");
                XmlNodeList nFechaCad = etiquetas.GetElementsByTagName("fechaCaducar");
                XmlNodeList nCaducidad = etiquetas.GetElementsByTagName("caducidad");

                Console.WriteLine("-------------------Prestamo # "+ nId.PadRight(20, '-'));
                    foreach (XmlElement tagClientes in nCliente)
                    {
                        int posC = 0;
                        string dni = tagClientes.GetAttribute("cedula");
                        XmlNodeList cNombre = tagClientes.GetElementsByTagName("nombres");
                        XmlNodeList cApe = tagClientes.GetElementsByTagName("apellidos");
                        Console.WriteLine("Cedula: " + dni);
                        Console.WriteLine("Nombre: " + cNombre[posC].InnerText);
                        Console.WriteLine("Apellido: " + cApe[posC++].InnerText);
                    }
                    foreach (XmlElement tagPelicula in nPelicula)
                    {
                        int posPe = 0;
                        string tagCodigo = tagPelicula.GetAttribute("codigo");
                        XmlNodeList pTitulo = tagPelicula.GetElementsByTagName("titulo");
                        XmlNodeList pGenero = tagPelicula.GetElementsByTagName("genero");
                        Console.WriteLine("Codigo: " + tagCodigo);
                        Console.WriteLine("Titulo: " + pTitulo[posPe].InnerText);
                        Console.WriteLine("Genero: " + pGenero[posPe++].InnerText);
                    }
                Console.WriteLine("Fecha prestada: " + nFecha[i].InnerText);
                Console.WriteLine("Fecha a Receptar: " + nFechaCad[i].InnerText);
                Console.WriteLine("Dias prestados: " + nCaducidad[i++].InnerText+"\n");
            }
        }

        public bool existePrestamoId(string codPrestamo)
        {
            bool msj = false;
            XMLprestamo.Load(rutaPrestamo);
            XmlNodeList prestamo = XMLprestamo.GetElementsByTagName(docXmlPrestamo);
            XmlNodeList lista = ((XmlElement)prestamo[0]).GetElementsByTagName(nodoXmlPrestamo);
            foreach (XmlElement etiquetas in lista)
            {
                string codi = etiquetas.GetAttribute("id");
                if (codi.Equals(codPrestamo))
                {
                    msj = true;
                }
            }
            return msj;
        }

        public string generarCodigo()
        {
            XMLprestamo.Load(rutaPrestamo);
            int id = 1;
            do
            {
                if (existePrestamoId(id.ToString()))
                     { id++;}
                else { break; }
            } while (true);
            string codigoPres = id.ToString();

            return codigoPres;
        }

        public bool buscarPrestamoXPelicula(string valorBuscar)
        {
            bool msj=false;
            XMLprestamo.Load(rutaPrestamo);
            XmlNodeList prestamo = XMLprestamo.GetElementsByTagName(docXmlPrestamo);
            XmlNodeList lista = ((XmlElement)prestamo[0]).GetElementsByTagName(nodoXmlPrestamo);
            int i = 0;
            foreach (XmlElement etiquetas in lista)
            {
                XmlNodeList noPelicula = etiquetas.SelectNodes("//@codigo");
                XmlNodeList nIdPrestamp = etiquetas.SelectNodes("//@id");
                XmlNodeList nCliente = etiquetas.SelectNodes("//@cedula");
                XmlNodeList nName = etiquetas.SelectNodes("//nombres");
                XmlNodeList nApell = etiquetas.SelectNodes("//apellidos");
                XmlNodeList nPTitulo = etiquetas.SelectNodes("//titulo");
                XmlNodeList nPGenero = etiquetas.SelectNodes("//genero");
                XmlNodeList nFecha = etiquetas.SelectNodes("//fechaPrestada");
                XmlNodeList nFechaCad = etiquetas.SelectNodes("//fechaCaducar");
                XmlNodeList nCaducidad = etiquetas.SelectNodes("//caducidad");
                if (noPelicula[i].InnerText.Equals(valorBuscar))
                {
                    msj = true;
                }
                else if (nCliente[i].InnerText.Equals(valorBuscar))
                {
                    msj = true;
                }
                else if (nFecha[i].InnerText.Equals(valorBuscar))
                {
                    msj = true;
                }
                i++;
            }
            return msj;
        }

        public void listarPrestamoXCodPel(string valor)
        {
            XMLprestamo.Load(rutaPrestamo);
            XmlNodeList prestamo = XMLprestamo.GetElementsByTagName(docXmlPrestamo);
            XmlNodeList lista = ((XmlElement)prestamo[0]).GetElementsByTagName(nodoXmlPrestamo);
            int i = 0;
            foreach (XmlElement etiquetas in lista)
            {
                XmlNodeList noPelicula=etiquetas.SelectNodes("//@codigo");
                XmlNodeList nIdPrestamp = etiquetas.SelectNodes("//@id");
                XmlNodeList nCliente = etiquetas.SelectNodes("//@cedula");
                XmlNodeList nName = etiquetas.SelectNodes("//nombres");
                XmlNodeList nApell = etiquetas.SelectNodes("//apellidos");
                XmlNodeList nPTitulo = etiquetas.SelectNodes("//titulo");
                XmlNodeList nPGenero = etiquetas.SelectNodes("//genero");
                XmlNodeList nFecha = etiquetas.SelectNodes("//fechaPrestada");
                XmlNodeList nFechaCad = etiquetas.SelectNodes("//fechaCaducar");
                XmlNodeList nCaducidad = etiquetas.SelectNodes("//caducidad");
                if (noPelicula[i].InnerText.Equals(valor))
                {
                    Console.WriteLine("Prestamo # " + nIdPrestamp[i].InnerText);
                    Console.WriteLine("Cedula: " + nCliente[i].InnerText);
                    Console.WriteLine("Nombre: " + nName[i].InnerText);
                    Console.WriteLine("Apellido: " + nApell[i].InnerText);
                    Console.WriteLine("Codigo: " + noPelicula[i].InnerText);
                    Console.WriteLine("Titulo: " + nPTitulo[i].InnerText);
                    Console.WriteLine("Genero: " + nPGenero[i].InnerText);
                    Console.WriteLine("Fecha prestada: " + nFecha[i].InnerText);
                    Console.WriteLine("Fecha a Receptar: " + nFechaCad[i].InnerText);
                    Console.WriteLine("Dias prestados: " + nCaducidad[i].InnerText + "\n");
                }
                else if (nCliente[i].InnerText.Equals(valor))
                {
                    Console.WriteLine("Prestamo # " + nIdPrestamp[i].InnerText);
                    Console.WriteLine("Cedula: " + nCliente[i].InnerText);
                    Console.WriteLine("Nombre: " + nName[i].InnerText);
                    Console.WriteLine("Apellido: " + nApell[i].InnerText);
                    Console.WriteLine("Codigo: " + noPelicula[i].InnerText);
                    Console.WriteLine("Titulo: " + nPTitulo[i].InnerText);
                    Console.WriteLine("Genero: " + nPGenero[i].InnerText);
                    Console.WriteLine("Fecha prestada: " + nFecha[i].InnerText);
                    Console.WriteLine("Fecha a Receptar: " + nFechaCad[i].InnerText);
                    Console.WriteLine("Dias prestados: " + nCaducidad[i].InnerText + "\n");
                }
                else if (nFecha[i].InnerText.Equals(valor))
                {
                    Console.WriteLine("Prestamo # " + nIdPrestamp[i].InnerText);
                    Console.WriteLine("Cedula: " + nCliente[i].InnerText);
                    Console.WriteLine("Nombre: " + nName[i].InnerText);
                    Console.WriteLine("Apellido: " + nApell[i].InnerText);
                    Console.WriteLine("Codigo: " + noPelicula[i].InnerText);
                    Console.WriteLine("Titulo: " + nPTitulo[i].InnerText);
                    Console.WriteLine("Genero: " + nPGenero[i].InnerText);
                    Console.WriteLine("Fecha prestada: " + nFecha[i].InnerText);
                    Console.WriteLine("Fecha a Receptar: " + nFechaCad[i].InnerText);
                    Console.WriteLine("Dias prestados: " + nCaducidad[i].InnerText + "\n");
                }
                i++;
            }
        }

        public void modificarPrestamoXPelicula(string IdPeliculaP,string newFecha,int newPlazo)
        {
            DateTime newCaducidad=DateTime.Parse(newFecha);
            XMLprestamo.Load(rutaPrestamo);
            XmlNodeList prestamo = XMLprestamo.GetElementsByTagName(docXmlPrestamo);
            XmlNodeList lista = ((XmlElement)prestamo[0]).GetElementsByTagName(nodoXmlPrestamo);
            int i = 0;
            foreach (XmlElement etiquetas in lista)
            {
                XmlNodeList noPelicula = etiquetas.SelectNodes("//@codigo");
                XmlNodeList nIdPrestamp = etiquetas.SelectNodes("//@id");
                XmlNodeList nCliente = etiquetas.SelectNodes("//@cedula");
                XmlNodeList nName = etiquetas.SelectNodes("//nombres");
                XmlNodeList nApell = etiquetas.SelectNodes("//apellidos");
                XmlNodeList nPTitulo = etiquetas.SelectNodes("//titulo");
                XmlNodeList nPGenero = etiquetas.SelectNodes("//genero");
                XmlNodeList nFecha = etiquetas.SelectNodes("//fechaPrestada");
                XmlNodeList nFechaCad = etiquetas.SelectNodes("//fechaCaducar");
                XmlNodeList nCaducidad = etiquetas.SelectNodes("//caducidad");
                if (noPelicula[i].InnerText.Equals(IdPeliculaP))
                {
                    nFecha[i].InnerText = newFecha;
                    nFechaCad[i].InnerText=newCaducidad.AddDays(newPlazo).ToString("d");
                    nCaducidad[i].InnerText=newPlazo.ToString();
                }
                i++;
            }
            XMLprestamo.Save(rutaPrestamo);   
        }
       
        public void eliminarPrestamo(string cod)
        {
            XMLprestamo.Load(rutaPrestamo);
            XmlNodeList prestamo = XMLprestamo.GetElementsByTagName(docXmlPrestamo);
            XmlNodeList lista = ((XmlElement)prestamo[0]).GetElementsByTagName(nodoXmlPrestamo);
            int i = 0;
            foreach (XmlElement etiquetas in lista)
            {
                XmlNodeList noPelicula = etiquetas.SelectNodes("//@codigo");
                XmlNodeList nIdPrestamp = etiquetas.SelectNodes("//@id");
                if (noPelicula[i].InnerText.Equals(cod))
                {
                    this.idPrestamo = nIdPrestamp[i].InnerText;
                }
                i++;
            }
            XmlNode node = XMLprestamo.SelectSingleNode("//" + nodoXmlPrestamo + "[@id='" + idPrestamo + "']");
            node.ParentNode.RemoveChild(node);
            Console.WriteLine("prestamo borrado");
            XMLprestamo.Save(rutaPrestamo);
        }

    }
}
