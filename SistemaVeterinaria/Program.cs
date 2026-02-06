using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion;

namespace SistemaVeterinaria
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Le decimos que inicie el LOGIN que está en la otra capa
            Application.Run(new FrmLogin());
            // PRUEBA COMENTARIO
        }
    }
}