using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalJacks
{
    class Analizador_Lexico
    {
        private int estado;
        private int inicial;
        private static int id;
        private static int columna = 0;
        private static int fila = 1;
        private String auxlex;
        private String error;
        DateTime fecha = DateTime.Now;
        public void Scanear(String Entrada)
        {
            Entrada = Entrada + '#';
            estado = 0;
            id = 0;
            inicial = 100;
            auxlex = "";
            error = "";
            char c;
            for (int i = 0; i <= Entrada.Length - 1; i++)
            {
                c = Entrada.ElementAt(i);
                columna++;
                switch (estado)
                {
                    case 0:
                        if (char.IsLetter(c))//reservada
                        {
                            estado = 1;
                            auxlex += c;
                        }
                        else if (char.IsDigit(c))//entero
                        {
                            estado = 3;
                            auxlex += c;
                        }
                        else if (c == '+')//signo mas
                        {

                            auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_MAS);
                        }
                        else if (c == '\r' || c == '\t' || c == '\b' || Char.IsWhiteSpace(c) || c == '\f')
                        {

                            //estado = 0;
                            fila++;
                            columna = 0;
                        }
                        else if (c == '\n')
                        {
                            columna = 0;
                        }
                        else if (c == '-')//signo menos
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_MENOS);
                        }
                        else if (c == '*')//signo por
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_POR);
                        }
                        else if (c == '/')//signo dividir
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_DIVIDIR);
                        }
                        else if (c == '(')
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.PARENTESIS_IZQ);
                        }
                        else if (c == ')')
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.PARENTESIS_DER);
                        }
                        else if (c == ';')
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.PUNTO_COMA);
                        }
                        else if (c == '=')
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.IGUAL);
                        }


                        else
                        {
                            if (c.CompareTo('#') == 0 && i == Entrada.Length - 1)
                            {
                                Console.WriteLine("Hemos concluido el analisis con exito");
                                estado = 0;
                            }
                            else
                            {
                                Console.WriteLine("Error lexico en " + c);
                                estado = 0;
                                error += c;
                                agregarError(Error.Descripcion.CARACTER_DESCONOCIDO);


                            }
                        }
                        break;

                    case 1: /*Palabra reservada o id*/
                        if (char.IsLetter(c) || c == '_')
                        {
                            estado = 1;
                            auxlex += c;
                        }
                        else if (char.IsDigit(c))
                        {
                            estado = 2;
                            auxlex += c;

                        }
                        else
                        {
                            estado = 0;
                            switch (auxlex)
                            {
                                case "int":
                                    agregarToken(Token.Tipo.RES_PRINT);
                                    break;
                                case "float":
                                    agregarToken(Token.Tipo.RES_DATOS);
                                    break;
                                case "char":
                                    agregarToken(Token.Tipo.VAR);
                                    break;

                                default:
                                    agregarToken(Token.Tipo.IDENTIFICADOR);

                                    break;
                            }
                            i--;

                        }
                        break;
                    case 2:
                        if (char.IsLetterOrDigit(c) || c == '_')
                        {
                            auxlex += c;
                            id = 2;
                            estado = 2;
                            //agregarToken(Token.Tipo.ID);


                        }
                        else
                        {
                            agregarToken(Token.Tipo.IDENTIFICADOR);
                            i--;
                        }
                        break;
                    case 3:
                        if (char.IsDigit(c))
                        {
                            auxlex += c;
                            estado = 3;

                        }
                        
                        else
                        {

                          //  id = 4;
                            agregarToken(Token.Tipo.ENTERO);

                            i--;



                        }
                        break;

                }


            }



        }
        public void agregarToken(Token.Tipo tipo)
        {
            Manejador.Obtenerllamado().getsalidaToken().Add(new Token(auxlex, tipo, fila, columna));
            auxlex = "";
            estado = 0;
        }

        public void agregarError(Error.Descripcion desc)
        {
            Manejador.Obtenerllamado().getsalidaError().Add(new Error(error, desc, fila, columna));
            error = "";
            estado = 0;
        }

        public void TokenHTML() {

            // MessageBox.Show();
            String pagina;
            pagina = "<html>" +
            "<body bgcolor= #F5D0A9>" +
            "<h1 align='center'><U>TABLA DE TOKENS</U></h1></br>" +
             "<h2 align='right'><U>Fecha y hora " + fecha + ": </U></h2></br>" +
            "<table cellpadding='20' border = '1' align='center'>" +
            "<tr>" +
            "<td bgcolor= #FF8000><strong>No." + "</strong></td>" +
            "<td bgcolor= #FF8000><strong>Tipo" + "</strong></td>" +
            "<td bgcolor= #FF8000><strong>Lexema" + "</strong></td>" +
            "<td bgcolor= #FF8000><strong>Columna" + "</strong></td>" +
            "<td bgcolor= #FF8000><strong>Fila" + "</strong></td>" +
         
            "</tr>";
            String cadena = "";
            String t;

            for (int i = 0; i < Manejador.Obtenerllamado().getsalidaToken().Count; i++)
            {
                Token lista = Manejador.Obtenerllamado().getsalidaToken().ElementAt(i);
                t = "";
                t = "<tr>" +
                "<td><strong>" + (i + 1).ToString() +
                "</strong></td>" +
                "<td>" + lista.GetTipo() +
                "</td>" +
                "<td>" + lista.getAuxlex() +
                "</td>" +
                "<td>" + lista.GetColumna() +
                "</td>" +
                "<td>" + lista.GetFila() +
                "</td>" +
                 
                "</tr>";
                cadena = cadena + t;
            }
            pagina = pagina + cadena +
            "</table>" +
            "</body>" +
            "</html>";
            File.WriteAllText("Tokens.html", pagina);
            System.Diagnostics.Process.Start("Tokens.html");
        }
    }
}