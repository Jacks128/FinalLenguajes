using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalJacks
{
    class Analizador_Sintactico
    {
        int controlToken;
        string traduccion = "";
        Token tokenActual;
        List<Token> listToken;

        public void parsear(List<Token> tokens)
        {

            this.listToken = tokens;
            controlToken = 0;
            tokenActual = listToken.ElementAt(controlToken);
            Inicio();
        }

        public void Inicio() {
            Lista();
        }

        public void Lista() {
            Declaracion(); ListaP();
            Asignacion();ListaP();
            Imprimir();ListaP();
            MostrarDatos(); ListaP();
        }

        public void ListaP() {
            Declaracion(); ListaP();
            Asignacion(); ListaP();
            Imprimir(); ListaP();
            MostrarDatos(); ListaP();

        }

        public void Declaracion() {
            if (tokenActual.GetTipo().Equals("var"))
            {
                emparejar("var");
                emparejar("ID");
                emparejar("igual");
                Expresion();
                emparejar("punto y coma");

            }
        }

        public void Asignacion()
        {
            if (tokenActual.GetTipo().Equals("ID"))
            {
                emparejar("ID");
                emparejar("igual");
                Expresion();
                emparejar("punto y coma");
            }

        }
        public void Imprimir() {
            if (tokenActual.GetTipo().Equals("print"))
            {
                emparejar("print");
                emparejar("parentesis izq");
                Expresion();
                emparejar("parentesis der");
                emparejar("punto y coma");

            }
        }

        public void MostrarDatos() {
            if (tokenActual.GetTipo().Equals("datos"))
            {
                emparejar("datos");
                emparejar("parentesis izq");
                emparejar("parentesis der");
                emparejar("punto y coma");

            }
        }

        public void Expresion() {
            E();
        }

        public void E()
        {
            //E-> T EP
            T();
            EP();
        }
        public void EP()
        {
            if (tokenActual.GetTipo().Equals("signo mas"))
            {
                //EP-> + T EP
                emparejar("signo mas");
                T();
                EP();
            }
            else if (tokenActual.GetTipo().Equals("signo menos"))
            {
                //EP-> - T EP
                emparejar("signo menos");
                T();
                EP();
            }
            else
            {
                // EP-> EPSILON
                // Para esta producción de EP en epsilon (cadena vacía), simplemente no se hace nada.
            }
        }
        public void T()
        {
            // T->F TP
            F();
            TP();
        }
        public void TP()
        {
            if (tokenActual.GetTipo().Equals("signo por"))
            {
                // TP-> * F TP
                emparejar("signo por");
                F();
                TP();
            }
            else if (tokenActual.GetTipo().Equals("signo dividir"))
            {
                // TP-> / F TP
                emparejar("signo dividir");
                F();
                TP();
            }
            else
            {
                // TP-> EPSILON
            }
        }
        public void F()
        {
            if (tokenActual.GetTipo().Equals("parentesis izq"))
            {
                //F->  (E)
                emparejar("parentesis izq");
                E();
                emparejar("parentesis der");
            }
            else
            {
                //F->  NUMERO
                emparejar("entero");
            }
        }
        public void emparejar(String tip)
        {   
            if (!tokenActual.GetTipo().Equals(tip))
            {
                //ERROR si no viene lo que deberia
                Console.WriteLine("Error Sintactico" + tokenActual.getAuxlex());

               // agregarErrorSintax(ErrorSintax.Descripcion.ERROR_SINTACTICO);

            }
            if (!tokenActual.GetTipo().Equals("ULTIMO"))
            {
                controlToken += 1;
                tokenActual = listToken.ElementAt(controlToken);
            }
        }

    }
}
