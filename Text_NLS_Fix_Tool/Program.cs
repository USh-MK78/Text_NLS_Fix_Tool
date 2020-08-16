﻿using System;
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
                    "Text_NLS_Fix_Tool [InputFile]\r\n" +
                    "Sketchupでマテリアルを正確に読み込むために[illum 2]文字列を追加、改行コードを修正する\r\n" +
                    "Text_NLS_Fix_Tool [InputFile] -r2illum");
            }
            try
            {
                if (cmdStr == args[0])
                {
                    //Text_NLS_Fix_Tool.exe <Input File>
                    StreamReader sr1 = new StreamReader(args[0]);
                    string sr1str = sr1.ReadToEnd();
                    sr1.Close();

                    string changeformat = sr1str.Replace("\n", "\r\n");
                    System.IO.StreamWriter sw1 = new System.IO.StreamWriter(args[0] + "_fixed.mtl", false);
                    sw1.Write(changeformat);
                    sw1.Close();
                }
                if(cmdStr == args[0] + " -r2illum")
                {
                    //Text_NLS_Fix_Tool.exe <Input File> -r2illum
                    StreamReader sr1 = new StreamReader(args[0]);
                    string sr1str = sr1.ReadToEnd();
                    sr1.Close();

                    string replaceillum_and_Fix_NLS = sr1str.Replace("d 1\n", "d 1\r\nillum 2\r\n");
                    System.IO.StreamWriter sw1 = new System.IO.StreamWriter(args[0] + "_fixed.mtl", false);
                    sw1.Write(replaceillum_and_Fix_NLS);
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
