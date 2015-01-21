using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace ClubPeliculas
{
    class Program
    {
        public static bool validarFecha(string fecha) {
            bool bandera = false;
            DateTime result;
            if (DateTime.TryParse(fecha, out result))
            { bandera = true; }
            return bandera;
        }
        static void Main(string[] args)
        {
            /*proyecto del primer parcial 
             * tema:Club de peliculas
             * Nombre: Tipanguano Muyolema Xavier 
             * Curso: 8/52
            */
            #region declaraciones

            string op, letras = @"[a-zA-ZñÑ\s]", numeros = @"[0-9]";
            string nameCliente = @"([A-ZÑÁÉÍÓÚ]-?[a-zñáíéóú]+\s\b)";
            int ope;
            //variables del cliente
            string opc, cedu, nombre, apellido;
            //variables de la pelicula
            string añoPubñicacionPelicula, tituloPelicula, genero,codPelicula;
            //variables prestamo
            string fechNueva, nuevoPlazo;
            
            clientes cl = new clientes();
            //datos del xml cliente
            string nameXml = "clientes";
            string nameNodoXml = "cliente";
            cl.datosXml(nameXml, nameNodoXml);
            //datos del xml pelicula
            peliculas pelicula = new peliculas();
            string peliculaXmlraiz = "peliculas";
            string PeliculaXmlnodo = "pelicula";
            pelicula.peliculasDato(peliculaXmlraiz, PeliculaXmlnodo);
            //datos del xml pelicula
            string prestamoXmlraiz = "prestamos";
            string prestamoXmlnodo = "prestamo";
            prestamo pres = new prestamo();
            pres.prestamoDto(prestamoXmlraiz, prestamoXmlnodo);
            pres.XmlCliente(nameXml, nameNodoXml);
            pres.Xmlpelicula(peliculaXmlraiz,PeliculaXmlnodo);
            #endregion
            do
            {
                Console.WriteLine("\t\tMenu\n1)Gestionar Clientes");
                Console.WriteLine("2)Gestionar Peliculas");
                Console.WriteLine("3)Gestionar Prestamos");
                Console.WriteLine("4)Inventario");  
                Console.WriteLine("0)Salir");            
                Console.Write("\nElija una opcion:  ");
                op = Console.ReadLine();
                if(clientes.comprobar(op,numeros)){
                    if (clientes.convertir(op))
                    {
                        ope=int.Parse(op);
                        if (ope == 1)
                        {
                            #region gestionCliente
                            cl.crearXml();
                            int opec;
                            do{
                            Console.Clear();
                            Console.WriteLine("\t\tGestion de Clientes");
                            Console.WriteLine("1)Ingresar\n2)Modificar\n3)Eliminar\n4)Listar");
                            Console.WriteLine("0)Retroceder\n");
                            Console.Write("Elija una opcion: ");
                            opc=Console.ReadLine();
                            if (clientes.comprobar(opc, numeros))
                            {
                                if (clientes.convertir(opc))
                                {
                                    opec = int.Parse(opc);
                                    if (opec == 1)
                                    {
                                        #region nuevoCliente
                                        do
                                        {
                                            Console.Clear();
                                            Console.WriteLine("\t\tIngreso de Clientes");
                                            Console.Write("Ingrese Cedula: ");
                                            cedu = Console.ReadLine();
                                                if(clientes.comprobar(cedu,numeros)){
                                                    if (cedu.Length == 10)
                                                    {
                                                        if (cl.existeCliente(cedu)) {
                                                            Console.Write("Cedula ya existe...");
                                                        }
                                                        else
                                                        {
                                                            Console.Write(" Ok");
                                                            break;
                                                        }
                                                    }
                                                    else { Console.WriteLine("cedula debe tener 10 digitos"); }
                                                }
                                                else { Console.WriteLine("No se aceptan letras "); 
                                                }
                                           Console.ReadKey();
                                        }while(true);
                                        do{
                                        Console.WriteLine("\nIngrese Nombres: ");
                                        nombre = Console.ReadLine();
                                        if (clientes.comprobar(nombre, nameCliente))
                                        {
                                            if (nombre.Length <4)
                                            {
                                                Console.Write("No es un nombre");
                                            }
                                            else if (nombre.Length > 25) {
                                                Console.WriteLine("A superado el maximo de caracteres");
                                            }
                                            else
                                            {
                                                Console.Write(" Ok");
                                                break;
                                            }
                                        }
                                        else { Console.Write("No se aceptan nombres digite correctamente"); }
                                        }while(true);
                                        do{
                                        Console.Write("\nIngrese Apellidos: ");
                                        apellido = Console.ReadLine();
                                        if (clientes.comprobar(apellido, nameCliente))
                                        {
                                            if (apellido.Length < 4)
                                            {
                                                Console.Write("No es un apellido");
                                            }
                                            else if (apellido.Length > 25)
                                            {
                                                Console.WriteLine("A superado el maximo de caracteres");
                                            }
                                            else
                                            {
                                                Console.Write(" Ok");
                                                break;
                                            }
                                        }
                                        else { Console.WriteLine("No se aceptan numeros "); }
                                        }while(true);
                                    //se validan cada dato de entrada y se los envia a la clase clientes   
                                        cl.datosCliente(cedu, nombre, apellido);
                                        cl.InsertarXml();
    #endregion
                                    }
                                    else if (opec == 2)
                                    {
                                        #region modificarCliente
                                        do
                                        {
                                            Console.Clear();
                                            Console.WriteLine("\t\tModificacion de Clientes");
                                            Console.Write("Buscar Cedula: ");
                                            cedu = Console.ReadLine();
                                            if (clientes.comprobar(cedu, numeros))
                                            {
                                                if (cedu.Length == 10)
                                                {
                                                   if(cl.existeCliente(cedu)){
                                                       break;
                                                   }
                                                   else{
                                                       Console.Write("La cedula no existe");
                                                   }
                                                }
                                                else { Console.WriteLine("cedula debe tener 10 digitos"); }
                                            }
                                            else { Console.WriteLine("No se aceptan letras "); }
                                            Console.ReadKey(); 
                                        } while (true);
                                        cl.BuscarCliente(cedu);
                                        do
                                        {
                                            Console.WriteLine("\nIngrese nuevos nombres: ");
                                            nombre = Console.ReadLine();
                                            if (clientes.comprobar(nombre, nameCliente))
                                            {
                                                if (nombre.Length < 4)
                                                {
                                                    Console.Write("No es un nombre");
                                                }
                                                else if (nombre.Length > 25)
                                                {
                                                    Console.WriteLine("A superado el maximo de caracteres");
                                                }
                                                else
                                                {
                                                    Console.Write(" Ok");
                                                    break;
                                                }
                                            }
                                            else { Console.Write("No se aceptan numeros "); }
                                        } while (true);
                                        do
                                        {
                                            Console.Write("\nIngrese nuevos apellidos: ");
                                            apellido = Console.ReadLine();
                                            if (clientes.comprobar(apellido, nameCliente))
                                            {
                                                if (apellido.Length < 4)
                                                {
                                                    Console.Write("No es un apellido");
                                                }
                                                else if (apellido.Length > 25)
                                                {
                                                    Console.WriteLine("A superado el maximo de caracteres");
                                                }
                                                else
                                                {
                                                    Console.Write(" Ok");
                                                    break;
                                                }
                                            }
                                            else { Console.WriteLine("No se aceptan numeros "); }
                                        } while (true);
                                        //invocacion de los metodos
                                        cl.datosCliente(cedu,nombre,apellido);
                                        cl.ModificarDatosXml();
                                        Console.WriteLine("\n\n\t\tValores Actuales");
                                        cl.presentarCliente();
    #endregion
                                    }
                                    else if (opec == 3)
                                    {
                                        #region eliminarCliente
                                        do
                                        {
                                            Console.Clear();
                                            Console.WriteLine("\t\tEliminacion de Clientes");
                                            Console.Write("Buscar Cedula: ");
                                            cedu = Console.ReadLine();
                                            if (clientes.comprobar(cedu, numeros))
                                            {
                                                if (cedu.Length == 10)
                                                {
                                                    if (cl.existeCliente(cedu))
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.Write("La cedula no existe");
                                                    }
                                                }
                                                else { Console.WriteLine("cedula debe tener 10 digitos"); }
                                            }
                                            else { Console.WriteLine("No se aceptan letras "); }
                                            Console.ReadKey();
                                        } while (true);
                                        cl.BuscarCliente(cedu);
                                        Console.WriteLine("los datos borrados");
                                        cl.EliminarDatosXml(cedu);
                                        #endregion
                                    }   
                                    else if (opec == 4)
                                    {
                                        #region listarClientes
                                        Console.Clear();
                                        Console.WriteLine("\t\tLista de Clientes");
                                        cl.listaXml(2);
                                        #endregion
                                    }
                                    else if (opec > 4 || opec < 0) { Console.WriteLine("Opcion Inexistente"); }
                                    else { break; }
                                }
                                else { Console.WriteLine("La opcion no existe"); }
                            }
                            else
                            {
                                Console.WriteLine("No se aceptan caracteres...");
                            }
                            Console.ReadKey();
                            }while(true);
                            #endregion
                        }
                        else if (ope == 2)
                        {
                            #region gestionPelicula
                            string opcPelicula;
                            string codigoP;
                            byte opcionMovie;
                        
                           pelicula.crearXml();
                            do{
                                Console.Clear();
                                Console.WriteLine("\t\tGestion de Peliculas");
                                Console.WriteLine("1)Ingresar\n2)Modificar\n3)Eliminar\n4)Listar");
                                Console.WriteLine("0)Retroceder");
                                Console.Write("\nElija una opcion: ");
                                opcPelicula = Console.ReadLine();
                                if(clientes.comprobar(opcPelicula,numeros)){
                                    if(peliculas.validar(opcPelicula)){
                                        opcionMovie=byte.Parse(opcPelicula);
                                        if(opcionMovie==1)
                                        {
                                            #region nuevaPelicula
                                            Console.Clear();
                                            Console.WriteLine("\t\tIngreso de peliculas");
                                            Console.WriteLine("Codigo:"+pelicula.generarCodigo());
                                            do{
                                                Console.WriteLine("Ingrese titulo de la pelicula: ");
                                                tituloPelicula = Console.ReadLine();
                                                if (clientes.comprobar(tituloPelicula, letras)) {
                                                    Console.Write("Ok");
                                                    break;
                                                }
                                                else {
                                                    Console.Write("Caracteres no validos...");
                                                }
                                            }while(true);
                                            Console.Write("Ingrese año de publicacion:");
                                            añoPubñicacionPelicula=Console.ReadLine();
                                            do{
                                                Console.Write("Ingrese genero: ");
                                                genero = Console.ReadLine();
                                                if(clientes.comprobar(genero,letras)){
                                                    Console.Write("Ok");
                                                    break;
                                                }
                                                else{
                                                    Console.Write("Caracteres no soportados...");
                                                }
                                            }while(true);
                                            pelicula.datosPelicula(tituloPelicula,añoPubñicacionPelicula,genero);
                                            pelicula.InsertarNodoXml();
                                            #endregion
                                        }
                                        else if(opcionMovie==2)
                                        {
                                            #region modificarPelicula
                                            do
                                            {
                                                Console.Clear();
                                                Console.WriteLine("\t\tModificacion de peliculas");
                                                Console.Write("Ingrese codigo de la pelicula: ");
                                                codigoP = Console.ReadLine();
                                                if (clientes.comprobar(codigoP, numeros))
                                                {
                                                    if (codigoP.Length == 5)
                                                    {
                                                        if (pelicula.existePelicula(codigoP))
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.Write("La pelicula no existe");
                                                        }
                                                    }
                                                    else { Console.WriteLine("codigo debe tener 5 digitos"); }
                                                }
                                                else { Console.WriteLine("No se aceptan letras "); }
                                                Console.ReadKey();
                                            } while (true);
                                            pelicula.buscarPelicula(codigoP);
                                            do
                                            {
                                                Console.WriteLine("\nIngrese nuevo titulo: ");
                                                tituloPelicula = Console.ReadLine();
                                                if (clientes.comprobar(tituloPelicula, letras))
                                                {
                                                    if (tituloPelicula.Length < 2)
                                                    {
                                                        Console.Write("No es un titulo");
                                                    }
                                                    else if (tituloPelicula.Length > 30)
                                                    {
                                                        Console.WriteLine("A superado el maximo de caracteres");
                                                    }
                                                    else
                                                    {
                                                        Console.Write(" Ok");
                                                        break;
                                                    }
                                                }
                                                else { Console.Write("No se aceptan numeros "); }
                                            } while (true);
                                        
                                            Console.Write("Ingrese año de publicacion:");
                                            añoPubñicacionPelicula=Console.ReadLine();
                                            do{
                                                Console.Write("Ingrese genero: ");
                                                genero = Console.ReadLine();
                                                if(clientes.comprobar(genero,letras)){
                                                    Console.Write("Ok");
                                                    break;
                                                }
                                                else{
                                                    Console.Write("Caracteres no soportados...");
                                                }
                                            }while(true);
                                            //invocacion de los metodos
                                            pelicula.datosPelicula(tituloPelicula,añoPubñicacionPelicula,genero);
                                            pelicula.ModificarPeliculaXml(codigoP);
                                            #endregion
                                        }
                                        else if(opcionMovie==3)
                                        {
                                            #region eliminarPelicula
                                            do
                                            {
                                                Console.Clear();
                                                Console.WriteLine("\t\tEliminacion de peliculas");
                                                Console.Write("Buscar codigo pelicula: ");
                                                codigoP = Console.ReadLine();
                                                if (clientes.comprobar(codigoP, numeros))
                                                {
                                                    if (codigoP.Length == 5)
                                                    {
                                                        if (pelicula.existePelicula(codigoP))
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.Write("La pelicula no existe");
                                                        }
                                                    }
                                                    else { Console.WriteLine("codigo debe tener 5 digitos"); }
                                                }
                                                else { Console.WriteLine("No se aceptan letras "); }
                                                Console.ReadKey();
                                            } while (true);
                                            pelicula.buscarPelicula(codigoP);
                                            pelicula.EliminarPeliculaXml(codigoP);

                                            #endregion 
                                        }
                                        else if(opcionMovie==4)
                                        {
                                            #region listarPelicula
                                            Console.Clear();
                                            Console.WriteLine("\t\t\tLISTADO DE PELICULAS");
                                            pelicula.listarPeliculas(2);
                                            #endregion
                                        }
                                        else if(opcionMovie<0 || opcionMovie>4){
                                            Console.WriteLine("Opcion no existe...");
                                        }
                                        else{
                                            break;
                                        }
                                    }
                                    else{
                                        Console.WriteLine("no es un numero valido");
                                    }
                                }
                                else{
                                    Console.WriteLine("no se aceptan letras...");
                                }
                                Console.ReadKey();
                            }
                            while(true);
                            #endregion
                        }
                        else if (ope == 3)
                        {
                            #region gestionPrestamo
                            int plazo;
                            string opcPrestamo;
                            byte opcionPres;
                            pres.crearXml();
                            do{
                        
                                Console.Clear();
                                Console.WriteLine("\t\tGestion de Prestamos");
                                Console.WriteLine("1) Nuevo\n2) Modificar\n3) Listar\n4) Eliminar");
                                Console.WriteLine("0) Retroceder\n");
                                Console.Write("Eliga una opcion: ");
                                opcPrestamo = Console.ReadLine();
                                if (clientes.comprobar(opcPrestamo, numeros))
                                {
                                   if (peliculas.validar(opcPrestamo))
                                   {
                                       opcionPres = byte.Parse(opcPrestamo);
                                       if (opcionPres == 1)
                                       {
                                           #region nuevoPrestamo
                                           Console.WriteLine(pres.nodoXmlPrestamo);
                                           do
                                           {
                                               Console.Clear();
                                               Console.WriteLine("\t\tRegistro de prestamos");
                                               Console.WriteLine("Registro Numero:  " + pres.generarCodigo());
                                               Console.Write("Ingrese cedula: ");
                                               cedu = Console.ReadLine();
                                               if (clientes.comprobar(cedu, numeros))
                                               {
                                                   if (cedu.Length == 10)
                                                   {
                                                       if (cl.existeCliente(cedu))
                                                       {
                                                           break;
                                                       }
                                                       else
                                                       {
                                                           Console.WriteLine("Cedula no existe");
                                                       }
                                                   }
                                                   else { Console.WriteLine("cedula debe tener 10 digitos"); }
                                               }
                                               else
                                               {
                                                   Console.WriteLine("No se aceptan letras ");
                                               }
                                               Console.ReadKey();
                                           } while (true);
                                           pres.xmlNodoCliente = nameNodoXml;
                                           pres.docXmlCliente = nameXml;
                                           pres.BuscarCliente(cedu);
                                           do
                                           {
                                               Console.Write("Ingrese titulo de la pelicula: ");
                                               tituloPelicula = Console.ReadLine().ToUpper();
                                               if (clientes.comprobar(tituloPelicula, letras))
                                               {
                                                   if (pres.PeliculaExiste(tituloPelicula))
                                                   {
                                                       if (pres.PeliculaOcupada(tituloPelicula))
                                                       {
                                                          break;
                                                       }
                                                       else
                                                       {
                                                           Console.WriteLine("Pelicula esta ocupada");
                                                        }
                                                   }
                                                   else {
                                                       Console.WriteLine("Pelicula no existe");
                                                   }
                                               }
                                               else
                                               {
                                                   Console.WriteLine("Caracteres no validos...");
                                               }
                                           } while (true);
                                           do
                                           {
                                               Console.Write("\nIngrese dias de alquiler: ");
                                               nuevoPlazo = Console.ReadLine();
                                               if (clientes.comprobar(nuevoPlazo, numeros))
                                               {
                                                   plazo = int.Parse(nuevoPlazo);
                                                   if (plazo > 1 && plazo < 31)
                                                   {
                                                       break;
                                                   }
                                                   else
                                                   {
                                                       Console.WriteLine("ingrese plazo entre 2 y 30");
                                                   }
                                               }
                                               else { Console.WriteLine("No se aceptan caracteres "); }
                                           } while (true);
                                           //buscarPelicula toma los valores segun el titulo 
                                           pres.buscarPelicula(tituloPelicula);
                                           pres.ModificarPelicula(tituloPelicula,"OCUPADO");
                                           pres.presentarPelicula();
                                           pres.presentarClienteP();
                                           pres.InsertarNodoXml(plazo,pres.fechaCaducidad(plazo));
                                           Console.ReadKey();
                                           #endregion
                                       }
                                       else if (opcionPres == 2) {
                                           #region modificarPrestamo
                                           do
                                           {
                                               Console.Clear();
                                               Console.WriteLine("\t\tModificacion de Prestamos");
                                               Console.Write("Ingrese codigo pelicula: ");
                                               codPelicula = Console.ReadLine();
                                               if (clientes.comprobar(codPelicula, numeros))
                                               {
                                                   if (codPelicula.Length == 5)
                                                   {
                                                       if (pres.buscarPrestamoXPelicula(codPelicula))
                                                       {
                                                           break;
                                                       }
                                                       else
                                                       {
                                                           Console.Write("codigo no existe en la lista de prestamos");
                                                       }
                                                   }
                                                   else { Console.WriteLine("codigo debe tener 5 digitos"); }
                                               }
                                               else { Console.WriteLine("No se aceptan caracteres "); }
                                               Console.ReadKey();
                                           } while (true);
                                           do
                                           {
                                               Console.WriteLine("\nIngrese nueva fecha: ");
                                               fechNueva = Console.ReadLine();
                                               if (validarFecha(fechNueva))
                                               {
                                                   break;
                                               }
                                               else { Console.Write("fecha no valida "); }
                                           } while (true);
                                           do
                                           {
                                               Console.Write("\nNuevo tiempo de caducidad: ");
                                               nuevoPlazo = Console.ReadLine();
                                               if (clientes.comprobar(nuevoPlazo, numeros))
                                               {
                                                   plazo = int.Parse(nuevoPlazo);
                                                   if (plazo > 1 && plazo < 31)
                                                   {
                                                       break;     
                                                   }
                                                   else {
                                                       Console.WriteLine("ingrese plazo entre 2 y 30");
                                                   }
                                               }
                                               else { Console.WriteLine("No se aceptan caracteres "); }
                                           } while (true);
                                            pres.modificarPrestamoXPelicula(codPelicula, fechNueva, plazo);
                                            #endregion
                                   
                                       }
                                       else if (opcionPres == 3) {
                                           #region listarPrestamo
                                           string lisOp;
                                           int listOpcion;
                                           do
                                           {  
                                               Console.Clear();
                                               Console.WriteLine("\t\tListado de clientes");
                                               Console.WriteLine("1)Por cliente\n2)Por Fecha posterior\n3)Todos");
                                               Console.WriteLine("0)Retroceder");
                                               Console.Write("\n\nIngrese una opcion: ");
                                               lisOp = Console.ReadLine();
                                               if(clientes.convertir(lisOp)){
                                                   listOpcion = int.Parse(lisOp);
                                                   if(listOpcion==1)
                                                   {
                                                       do{
                                                       Console.Clear();
                                                       Console.WriteLine("Buscar Cedula: ");
                                                        cedu = Console.ReadLine();
                                                        if (clientes.comprobar(cedu, numeros))
                                                        {
                                                            if (cedu.Length == 10)
                                                            {
                                                               if(pres.buscarPrestamoXPelicula(cedu)){
                                                                   break;
                                                               }
                                                               else{
                                                                   Console.Write("La cedula no existe en prestamos");
                                                               }
                                                            }
                                                            else { Console.WriteLine("cedula debe tener 10 digitos"); }
                                                        }
                                                        else { Console.WriteLine("No se aceptan letras "); }
                                                        Console.ReadKey(); 
                                                    } while (true);
                                                    pres.listarPrestamoXCodPel(cedu);
                                                   }
                                                   else if (listOpcion == 2) {
                                                       {
                                                           do
                                                           {
                                                               Console.Clear();
                                                               Console.Write("Buscar por fecha: ");
                                                               fechNueva = Console.ReadLine();
                                                               if (validarFecha(fechNueva))
                                                               {
                                                                   if (pres.buscarPrestamoXPelicula(fechNueva))
                                                                   {
                                                                       break;
                                                                   }
                                                                   else {
                                                                       Console.WriteLine("Fecha no existe");
                                                                   }
                                                                }
                                                               else { Console.WriteLine("No se aceptan carecteres "); }
                                                               Console.ReadKey();
                                                           } while (true);
                                                           pres.listarPrestamoXCodPel(fechNueva);
                                                       }
                                                   }
                                                   else if (listOpcion == 3) {
                                                       Console.Clear();
                                                       Console.WriteLine("-----------------Lista de Prestamos---------------------");
                                                       pres.listarPrestamo();
                                                   }
                                                   else if (listOpcion == 0) {
                                                       break;
                                                   }
                                                   else{
                                                       Console.WriteLine("Opcion no existe...");
                                                   }
                                               }
                                               else{
                                                   Console.WriteLine("no se aceptan letras");
                                               }
                                               Console.ReadKey();
                                           }while(true);
                                           #endregion 

                                       }
                                       else if (opcionPres == 4) {
                                           do
                                           {
                                              Console.Clear();
                                              Console.WriteLine("Eliminacion de Prestamos");
                                              Console.Write("Ingrese codigo de la pelicula: ");
                                              codPelicula = Console.ReadLine().ToUpper();
                                              if (clientes.convertir(codPelicula))
                                               {
                                                   if (pres.buscarPrestamoXPelicula(codPelicula))
                                                   {
                                                       break;
                                                   }
                                                   else
                                                   {
                                                       Console.WriteLine("Pelicula no existe en prestamos");
                                                   }
                                               }
                                               else
                                               {
                                                   Console.WriteLine("Caracteres no validos...");
                                               }
                                              Console.ReadKey();
                                           } while (true);
                                           pres.eliminarPrestamo(codPelicula);
                                           pres.ModificarPelicula(codPelicula,"DISPONIBLE");

                                       }
                                       else if (opcionPres < 0 || opcionPres > 4)
                                       {
                                           Console.WriteLine("Opcion no existe...");
                                       }
                                       else
                                       {
                                           break;
                                       }
                                   }
                                   else
                                   {
                                       Console.WriteLine("no es un numero valido");
                                   }
                                }
                                else
                                {
                                   Console.WriteLine("no se aceptan letras...");
                                }
                                Console.ReadKey();
                                }
                               while (true);
                            #endregion
                        }
                        else if (ope == 4)
                        {
                            #region Inventario
                            Console.Clear();
                            Console.WriteLine("\t\t\tINVENTARIO GLOBAL");
                            Console.WriteLine("-----------------Lista de Clientes---------------");
                            cl.listaXml(3);
                            Console.WriteLine("\n\n\n\n-----------------Lista de Peliculas---------------");
                            pelicula.listarPeliculas(18);
                            Console.WriteLine("\n\n\n-----------------Lista de Prestamos---------------");
                            pres.listarPrestamo();

                            #endregion
                        }
                        else if (ope < 0 || ope > 4) { Console.WriteLine("La opcion "+ope+" no es valida"); }
                        else { Console.Write("\n\nSaliendo del programa\n\nPress any key to continue..."); break; }
                    }
                    else { Console.WriteLine("Eliga una opcion valida"); }
                }
                else
                {
                    Console.Write("no se aceptan caracteres...");
                }
                Console.ReadKey();
                Console.Clear();
            }while(true);
            Console.ReadKey();
        }
    }
}
