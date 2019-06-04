using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    private static string SavePath = "Save/BlindFlag_Save.bin";

    private static string unary_encode(int n)
    {
        string msg_encode = "";

        for (int i = n; i > 0; i--)
        {
            msg_encode += "0";
        }

        msg_encode += "1";

        return msg_encode;
    }

    private static string gamma_encode(int n)
    {
        if (n > 0)
        {
            string n_bin = "";
            int u;

            if (n == 0)
            {
                n_bin = "0";
            }
            else
            {
                while (n != 0)
                {
                    n_bin = n % 2 + n_bin;
                    n /= 2;
                }
            }

            u = n_bin.Length - 1;

            return unary_encode(u) + n_bin.Remove(0, 1);
        }
        else if (n == 0) return "0";
        else
        {
            throw new Exception("n < 0");
        }
    }

    private static int gamma_decode(string msg)
    {
        int n = 0;
        int len = msg.Length;

        if (msg == "1")
        {
            n = 1;
        }
        else if (len < 3)
        {
            throw new Exception("Not Gamma encode");
        }
        else
        {
            int a = 0;

            while (msg[a] != '1')
            {
                a += 1;
            }

            if (a >= len - 1)
            {
                throw new Exception("Not Gamma encode");
            }
            else
            {
                int i = 0;
                int u;

                int pow = 1;

                string n_bin = "";

                while (msg[i] == '0')
                {
                    i += 1;
                }

                u = i;

                n_bin = msg.Remove(0, len - 2 - u);

                for (int j = n_bin.Length - 1; j > 0; j--)
                {
                    n += Int32.Parse(n_bin[j] + "") * pow;
                    pow *= 2;
                }
            }
        }

        return n;
    }

    public static void SaveGame()
    {
        using (StreamWriter MyWriter = new StreamWriter(SavePath))
        {
            MyWriter.WriteLine(gamma_encode(BlindShip_Stat.Lvl));
            MyWriter.WriteLine(gamma_encode(BlindShip_Stat.Money));
            MyWriter.WriteLine(gamma_encode(BlindShip_Stat.HP));
            MyWriter.WriteLine(gamma_encode(BlindShip_Stat.XP));
            MyWriter.WriteLine(gamma_encode(BlindShip_Stat.Damage));
            MyWriter.WriteLine(gamma_encode(BlindShip_Stat.Crew));
            MyWriter.WriteLine(gamma_encode(BlindShip_Stat.Shield));

            MyWriter.WriteLine(gamma_encode(BlindCaptain_Stat.Lvl));
            MyWriter.WriteLine(gamma_encode(BlindCaptain_Stat.HP));
            MyWriter.WriteLine(gamma_encode(BlindCaptain_Stat.XP));
            MyWriter.WriteLine(gamma_encode(BlindCaptain_Stat.GunDamage));
            MyWriter.WriteLine(gamma_encode(BlindCaptain_Stat.SwordDamage));
            MyWriter.WriteLine(gamma_encode(BlindCaptain_Stat.Reputation));
            MyWriter.WriteLine(gamma_encode(BlindCaptain_Stat.NbEnigme));
            MyWriter.WriteLine(gamma_encode(BlindCaptain_Stat.chasseautresor ? 1 : 0));

            MyWriter.WriteLine((gamma_encode(BlindShip_Stat.SceneLoad)));

            foreach (var k in BlindCaptain_Stat.Tuto)
            {
                MyWriter.WriteLine((k.Value == false) ? 0 : 1);
            }
        }
    }

    public static void LoadGame()
    {
        if (IsThereASave())
        {
            using (StreamReader MyReader = new StreamReader(SavePath))
            {
                BlindShip_Stat.Lvl = gamma_decode(MyReader.ReadLine());
                BlindShip_Stat.SetStat();
                BlindShip_Stat.Money = gamma_decode(MyReader.ReadLine());
                BlindShip_Stat.HP = gamma_decode(MyReader.ReadLine());
                BlindShip_Stat.XP = gamma_decode(MyReader.ReadLine());
                BlindShip_Stat.Damage = gamma_decode(MyReader.ReadLine());
                BlindShip_Stat.Crew = gamma_decode(MyReader.ReadLine());
                BlindShip_Stat.Shield = gamma_decode(MyReader.ReadLine());

                BlindCaptain_Stat.Lvl = gamma_decode(MyReader.ReadLine());
                BlindCaptain_Stat.SetStat();
                BlindCaptain_Stat.HP = gamma_decode(MyReader.ReadLine());
                BlindCaptain_Stat.XP = gamma_decode(MyReader.ReadLine());
                BlindCaptain_Stat.GunDamage = gamma_decode(MyReader.ReadLine());
                BlindCaptain_Stat.SwordDamage = gamma_decode(MyReader.ReadLine());
                BlindCaptain_Stat.Reputation = gamma_decode(MyReader.ReadLine());
                BlindCaptain_Stat.NbEnigme = gamma_decode(MyReader.ReadLine());
                BlindCaptain_Stat.chasseautresor = gamma_decode(MyReader.ReadLine()) != 0;

                int SceneLoad = gamma_decode(MyReader.ReadLine());

                BlindShip_Stat.SceneLoad =
                    (SceneLoad == 2 || SceneLoad == 3 || SceneLoad == 4) ? (int) LoadScene.Scene.Port : (int) LoadScene.Scene.Navigation;

                string[] S = MyReader.ReadToEnd().Split(' ', '\t', '\r', '\n');
                
                BlindCaptain_Stat.Tuto = new Dictionary<string, bool>()
                {
                    {"SeaBattle", (S[0] == "0" ? false : true)},
                    {"ChasseAuTresor", (S[1] == "0" ? false : true)},
                    {"Combat", (S[2] == "0" ? false : true)},
                    {"Coco", (S[3] == "0" ? false : true)},
                };
                
                SceneManager.LoadScene(BlindShip_Stat.SceneLoad);
            }
        }
    }

    public void LoadGamenotStatic()
    {
        LoadGame();
    }

    public static bool IsThereASave()
    {
        try
        {
            using (StreamReader MyReader = new StreamReader(SavePath))
            {
                List<string> save = MyReader.ReadToEnd().Split(' ', '\t', '\r', '\n').Where(e => e != "").ToList();
                return save.Count == 19;
            }
        }
        catch
        {
            return false;
        }
    }

    public static void DeleteSave()
    {
        if (IsThereASave())
        {
            File.Delete(SavePath);
        }
    }
}
