using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalJacks
{
    class Manejador
    {
        List<Token> salidaToken;
        List<Error> salidaError;
        private static Manejador llamado;

        public static Manejador Obtenerllamado()
        {
            if (llamado == null)
            {
                llamado = new Manejador();
            }
            return llamado;
        }
        public Manejador()
        {
            salidaToken = new List<Token>();

            salidaError = new List<Error>();

        }

        public List<Token> getsalidaToken()
        {
            return salidaToken;
        }


        public List<Error> getsalidaError()
        {
            return salidaError;
        }

    }
}

