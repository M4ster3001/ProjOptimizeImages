using System;
using System.Collections.Generic;
using System.Text;

namespace ImageTools
{
    public static class Session
    {
        private static Int32 _qtdeImagens ;
        private static Int32 _qtdeProcessImagens;
        private static string _TypeCompression;
        private static string _path_destino;
        private static string _path_origem;

        public static int QtdeImagens { get => _qtdeImagens; set => _qtdeImagens = value; }
        public static int QtdeProcessImagens { get => _qtdeProcessImagens; set => _qtdeProcessImagens = value; }
        public static string Path_origem { get => _path_origem; set => _path_origem = value; }
        public static string Path_destino { get => _path_destino; set => _path_destino = value; }
        public static string TypeCompression { get => _TypeCompression; set => _TypeCompression = value; }
    }
}
