using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Text_NLS_Fix_Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            string cmdStr = string.Join(" ", args);
            if (cmdStr.Length == 0)
            {
                //ヘルプ文
                Console.WriteLine("Text_NLS_Fix_Tool by USh_MK78\r\n" +
                    "使い方\r\n" +
                    "改行コードを修正\r\n" +
                    "Text_NLS_Fix_Tool.exe [InputFile]\r\n" +
                    "\r\n" + 
                    "Sketchupでマテリアルを正確に読み込むために[illum 2]文字列を追加、改行コードを修正する\r\n" +
                    "Text_NLS_Fix_Tool.exe [InputFile] -r2illum\r\n" +
                    "\r\n" +
                    "Blenderで出力したOBJファイルに付属するMTLファイルのパス、およびファイル修飾子を修正します\r\n" +
                    "Text_NLS_Fix_Tool.exe <MTL File> -fix\r\n" +
                    "\r\n" +
                    "Blenderで出力したOBJファイルに付属するMTLファイルのパス、およびファイル修飾子を修正、テクスチャが存在するディレクトリを変更します。\r\n" +
                    "Text_NLS_Fix_Tool.exe <MTL File> -InputDir <InDIR> -NewDir <NDIR>\r\n" +
                    "\r\n" +
                    "例\r\n" +
                    "A/B/C.tgaをA/C.tgaにしたい場合\r\n" +
                    "InputDir=A/B/\r\n" +
                    "NewDir=A/\r\n" +
                    "\r\n");
            }
            try
            {
                if (cmdStr == args[0] + " " + args[1])
                {
                    //Text_NLS_Fix_Tool.exe <Input File>
                    StreamReader sr1 = new StreamReader(args[0]);
                    string sr1str = sr1.ReadToEnd();
                    sr1.Close();

                    string changeformat = sr1str.Replace("\n", "\r\n");
                    System.IO.StreamWriter sw1 = new System.IO.StreamWriter(args[1] + "_fixed.mtl", false);
                    sw1.Write(changeformat);
                    sw1.Close();
                }
                if (cmdStr == args[0] + " " + args[1] + " -r2illum")
                {
                    //Text_NLS_Fix_Tool.exe <Input File> -r2illum
                    StreamReader sr1 = new StreamReader(args[1]);
                    List<string> res = sr1.ReadToEnd().Split(new char[] { '\r', '\n' }).ToList();

                    var fde = res.Select(n => n.Replace("d 1\n", "d 1\r\nillum 2\r\n")).ToList();
                    string replaceillum_and_Fix_NLS = string.Join("\r\n", fde.ToArray());

                    System.IO.StreamWriter sw1 = new System.IO.StreamWriter(args[1] + "_fixed.mtl", false);
                    sw1.Write(replaceillum_and_Fix_NLS);
                    sw1.Close();
                }
                if (cmdStr == args[0] + " " + args[1] + " -fix")
                {
                    //Text_NLS_Fix_Tool.exe <MTL File> -fix
                    StreamReader sr1 = new StreamReader(args[1]);
                    List<string> res = sr1.ReadToEnd().Split(new char[] { '\r', '\n' }).ToList();

                    var fde = res.Select(n => n.Replace(".PNG", ".png")).ToList().Select(t => t.Replace("\\", "/")).ToList();
                    string pathfix = string.Join("\r\n", fde.ToArray());

                    System.IO.StreamWriter sw2 = new System.IO.StreamWriter(args[1] + "_fixFileExt_and_Path.mtl", false);
                    sw2.Write(pathfix);
                    sw2.Close();
                }
                if (cmdStr == args[0] + " " + args[1] + " -InputDir " + args[3] + " -NewDir " + args[5])
                {
                    //Text_NLS_Fix_Tool.exe <MTL File> -InputDir <InDIR> -NewDir <NDIR>
                    StreamReader sr1 = new StreamReader(args[1]);
                    List<string> res = sr1.ReadToEnd().Split(new char[] { '\r', '\n' }).ToList();

                    var fde = res.Select(n => n.Replace(".PNG", ".png")).ToList().Select(t => t.Replace("\\", "/")).ToList().Select(f => f.Replace(args[3], args[5])).ToList();
                    string changePath = string.Join("\r\n", fde.ToArray());

                    System.IO.StreamWriter sw1 = new System.IO.StreamWriter(args[1] + "_fixTexType_and_Path_TexDir.mtl", false);
                    sw1.Write(changePath);
                    sw1.Close();
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.WriteLine("引数が指定されなかったため処理を強制的に終了しました。\r\n引数を指定してこのプログラムを起動してください。");
                Environment.Exit(0);
            }
        }
    }
}
