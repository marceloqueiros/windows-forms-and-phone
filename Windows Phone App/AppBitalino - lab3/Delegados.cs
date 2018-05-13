using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging; //imagens

namespace AppBitalino___lab3
{
    public delegate void MostrarViewSemParametros();
    public delegate void MetodosSemParametros();
    public delegate void MetodosComParametrosDate(DateTime date);
    public delegate void MetodosComInt(int a);
    public delegate void MetodosCom2Int(int a, int b);
    public delegate void MetodosCom6StringsE1Date(string x1, string x2, DateTime D, string x3, string x4, string x5, string x6);
    public delegate void MetodosComDate(DateTime x);
    public delegate void MetodosCom5Strings1Date1image(string id, string nome, DateTime data, string x2, string x4, string x5, BitmapImage img);
    public delegate void MetodosComUser(Utilizador a);
    public delegate void MetodosComString(string s);
    public delegate void MetodosCom2Strings(string s1, string s2);
    public delegate void MetodosComBoolEString(bool x1, bool x2, bool x3, bool x4, bool x5, bool x6, string y);
    public delegate void MetodosCriarRegisto(string idRegisto, string idUtilizador, string tiposE, string frames);
    public delegate void MetodosComIntString(int index, string filtro);
}
