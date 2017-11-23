using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManutencaoArquivos {
    class Directory {
        public string[] subDir { get; set; }
        public string path { get; set; }
        public string bkpPath { get; set; }
        public string dayPath { get; set; }
        string fileBkp { get; set; }
        string[] fileOriginal = { "vpws.log", "long_qry.dbf", "long_qry.dbt", "gpftrap.log" };

        //Cria o diretório da semana para backup
        public void createDir(string bkpPath) {
            if (!System.IO.Directory.Exists(bkpPath)) {
                System.IO.Directory.CreateDirectory(bkpPath);
            }
        }
        /*teste do git*/
        //Move os arquivos do fileOriginal e log para o novo diretório
        public void moveFiles(string path, string bkpPath){
            foreach(string s in this.fileOriginal){
                this.fileBkp = bkpPath + @"\" + s;
                if(File.Exists(path + @"\" + s)){
                    try {
                        System.IO.File.Move(path + @"\" + s, this.fileBkp);
                    } catch (IOException e) {
                        Console.WriteLine("Erro '{0}'", e);
                    }
                }
            }
            if (System.IO.Directory.Exists(path + @"\log\")) {
                try {
                    System.IO.Directory.Move(path + @"\log\", bkpPath + @"\log\");
                    System.IO.Directory.CreateDirectory(path + @"\log\");    
                } catch (IOException e) {
                    Console.WriteLine("Erro '{0}'", e);
                }       
            }
        }

        //Limpa os arquivos da temp
        public void cleanTemp(string path) {
            if (System.IO.Directory.Exists(path + @"\temp\")) {
                try {
                    System.IO.Directory.Delete(path + @"\temp\", true);
                    System.IO.Directory.CreateDirectory(path + @"\temp\");  
                } catch (IOException e) {
                    Console.WriteLine("Erro '{0}'", e);
                }
            }
        }

        //Limpa extensões específicas de um diretório
        public void cleanFolder( string path, string ext) {
            if (System.IO.Directory.Exists(path)){
                foreach (string f in System.IO.Directory.GetFiles(path, ext)) {
                    if(!IsFileLocked(f)){
                        File.Delete(f);
                    }
                }
            }
        }

        public bool IsFileLocked(string filePath) {
            try {
                using (File.Open(filePath, FileMode.Open)) { }
            } catch (IOException e) {
                var errorCode = System.Runtime.InteropServices.Marshal.GetHRForException(e) & ((1 << 16) - 1);
                return errorCode == 32 || errorCode == 33;
            }
            return false;
        }

    }
}
//Cara, o próximo que ler esse código, saiba que estou com preguiça de fazer qualquer outra coisa. Gabriel - 28/07/16 17:45