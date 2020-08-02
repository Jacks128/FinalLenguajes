using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalJacks
{
    class Error
    {
        private String error;
        private Descripcion descripcion;
        private int fila;
        private int columna;

        public enum Descripcion
        {
            CARACTER_DESCONOCIDO
        }

        public Error(String error, Descripcion desc, int fila, int columna)
        {
            this.error = error;
            this.descripcion = desc;
            this.fila = fila;
            this.columna = columna;
        }

        public String getError()
        {
            return error;
        }

        public int Getfila()
        {
            return fila;
        }

        public int Getcolumna()
        {
            return columna;
        }

        public String Getdescripcion()
        {
            switch (descripcion)
            {
                case Descripcion.CARACTER_DESCONOCIDO:
                    return "Caracter no perteneciente al lenguaje";
                default:
                    return "Desconocido";
            }
        }
    }
}
