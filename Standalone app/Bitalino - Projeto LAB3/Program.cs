using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Bitalino___Projeto_LAB3
{
    static class Program
    {
       
        public static ViewMenu V_Menu { get; private set; }
        public static ViewCriarRegisto V_CriarRegisto{ get;private set; }
        public static ViewListarRegistos V_ListarRegistos { get; private set; }
        public static ViewCriarUtilizador V_CriarUtilizador { get; private set; }
        public static ViewListarUtilizadores V_ListarUtilizadores { get; private set; }
        public static ViewVisualizarRegisto V_VisualizarRegisto { get; private set; }
        public static ViewVisualizarUtilizador V_VisualizarUtilizador { get; private set; }
        public static ViewEditarUtilizador V_EditarUtilizador { get; private set; }
        public static ModelBit Model { get; private set; }
        public static ControllerBit Controller { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string a, b, c, d, e, f;
            //Image g = ;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Model = new ModelBit();
            V_Menu = new ViewMenu();
            V_CriarRegisto = new ViewCriarRegisto();
            V_ListarRegistos = new ViewListarRegistos();
            V_CriarUtilizador = new ViewCriarUtilizador();
            V_ListarUtilizadores = new ViewListarUtilizadores();
            V_VisualizarRegisto = new ViewVisualizarRegisto();
            V_VisualizarUtilizador = new ViewVisualizarUtilizador();
            V_EditarUtilizador = new ViewEditarUtilizador();
            Controller = new ControllerBit();

            Application.Run(V_Menu);
        }
    }
}