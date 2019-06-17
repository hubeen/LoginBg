using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

namespace LoginBg
{
    class Program
    {
        static bool RegestryConfig()
        {
           bool flag = false;
           if ( Environment.Is64BitOperatingSystem == true)
           {
                object kval = new object();
                const string Path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background";
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                key = key.OpenSubKey(Path, true);

                if(key != null)
                {
                    kval = key.GetValue("OEMBackground");

                    if (kval != null)
                    {
                        if(kval.ToString().Equals(1))
                        {
                            key.SetValue("OEMBackground", 1);
                            key.Close();
                            flag = true;
                        }
                        else
                        {
                            key.SetValue("OEMBackground", 1);
                            key.Close();
                            flag = true;
                        }
                    }
                    else
                    {
                        key.SetValue("OEMBackground", 1);
                        key.Close();
                        flag = true;
                    }
                }
                return flag;
           }
           else
           {
                object kval = new object();
                const string Path = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background";
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                key = key.OpenSubKey(Path, true);

                if (key != null)
                {
                    kval = key.GetValue("OEMBackground");
                    if (kval.ToString().Equals(1))
                    {
                        key.SetValue("OEMBackground", 1);
                        key.Close();
                        flag = true;
                    }
                    else
                    {
                        key.SetValue("OEMBackground", 1);
                        key.Close();
                        flag = true;
                    }
                }
                else
                {
                    key.SetValue("OEMBackground", 1);
                    key.Close();
                    flag = true;
                }
            }
            return flag;

        }

        static void FolderConfig()
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Windows\System32\oobe\info\backgrounds");
            if(di.Exists == false)
            {
                Console.WriteLine(di.Exists);
                di.Create();
            }
            else
            {
                Console.WriteLine(di.Exists);
            }

        }

        static string GetfileSize(double bytesize)
        {
            string size = "0 Bytes";
            if (bytesize >= 1073741824.0)
                size = String.Format("{0:##.##}", bytesize / 1073741824.0) + " GB";
            else if (bytesize >= 1048576.0)
                size = String.Format("{0:##.##}", bytesize / 1048576.0) + " MB";
            else if (bytesize >= 1024.0)
                size = String.Format("{0:##.##}", bytesize / 1024.0) + " KB";
            else if (bytesize > 0 && bytesize < 1024.0)
                size = bytesize.ToString() + " Bytes";

            return size;
        }


        static void Main(string[] args)
        {
           int argc = args.Count();
           if (argc == 1)
           {
                if (RegestryConfig() != true)
                {
                    Console.WriteLine("오 세상에 당신의 컴퓨터는 이 세계의 것이 맞습니까?");
                }
                else
                {
                    FolderConfig();

                    FileInfo fi = new FileInfo(args[0]);
                    if(fi.Length <= 262144)
                    {
                        File.Copy(args[0], @"C:\Windows\System32\oobe\info\backgrounds\backgroundDefault.jpg", true);
                        Console.WriteLine("Login Background Image Change Success");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[!] You are JPEG File size is Greater than 256kb !");
                        Console.ResetColor();
                    }

                    //File.Copy(args[0], @"C:\Windows\System32\oobe\info\backgrounds\backgroundDefault.jpg",true);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Usage : [JPEG File(256kb)]");
                Console.ResetColor();
            }
           
        }
    }
}
