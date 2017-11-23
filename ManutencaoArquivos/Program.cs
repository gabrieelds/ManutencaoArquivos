using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManutencaoArquivos {
    class Program {
        static void Main(string[] args) {
            Directory dir = new Directory();
            dir.path = @"E:\Trucker\";
            dir.bkpPath = @"C:\Corebuilder\Aplica\_log_BKP\";
            dir.subDir = System.IO.Directory.GetDirectories(dir.path, "*"); 

            //A pasta de bkp PRECISA se chamar _log_BKP, não to afim de pensar numa forma dinâmica de pegar a última pasta informada na bkpPath
            foreach (string p in dir.subDir) {
                if ((p.Split('\\').LastOrDefault() != "_log_BKP") && (p.Split('\\').LastOrDefault() != "Temp")) {
                    dir.dayPath = dir.bkpPath + p.Split('\\').LastOrDefault() + @"\" + (DateTime.Today.ToString("yyyy-MM-dd"));
                    dir.createDir(dir.dayPath);
                    dir.moveFiles(p, dir.dayPath);
                    dir.cleanTemp(p);
                    dir.cleanFolder(p, "*.txt");
                    dir.cleanFolder(p, "*.xml");
                    dir.cleanFolder(p, "*.sql");
                    dir.cleanFolder(p, "*.dbf");
                    dir.cleanFolder(p, "*.dbt");
                    dir.cleanFolder(p, "*.zip");
                    dir.cleanFolder(p, "*.log");
                    dir.cleanFolder(p, "*.csv");
                    /*teste do git*/
                }
            }
        }
    }
}