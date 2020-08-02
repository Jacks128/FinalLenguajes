using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalJacks
{
    public partial class Form1 : Form
    { Analizador_Lexico analizadorl;
        Manejador token;
        public Form1()
        {
            InitializeComponent();
           
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)

        {
            String entry = rtbTexto.Text;
analizadorl /*lex*/ = new Analizador_Lexico();

            analizadorl.Scanear(entry);
            
            analizadorl.TokenHTML();

            Manejador.Obtenerllamado().getsalidaToken().Add(new Token("ULTIMO", Token.Tipo.ULTIMO, 0, 0));
        }
    }
}
