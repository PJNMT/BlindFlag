using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipShop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        launch_shop();
    }

    private static void Launch(Recognition.Function func, string MainSentence, string WordList, int Time)
    {
        Synthesis.synthesis("Votre fortune s'éléve à " + BlindShip_Stat.Money + " lingots d'or.");
        Thread.Sleep(2000);
        Synthesis.synthesis("Que voulez-vous faire ?" + " " + MainSentence);
        Recognition.start_recognition(Time, WordList, func);
    }
    
    private static void launch_shop(int status = 0) // Status initialement à 0
    {
        if (status == 0)
        {
            Synthesis.synthesis("Bonjour captaine !");
            Thread.Sleep(2000);
        }

        Recognition.Function Main = MainShop;
        Launch(Main, "Réparer votre navire, améliorer votre équipement ou votre navire, ou bien partir ?", "réparer améliorer équipement quiter partir", 15);
    }

    private static void MainShop(string speech)
    {
        Synthesis.synthesis("OK captaine !");
        Thread.Sleep(2000);
        
        Debug.Log(speech);
        
        if (speech != "quiter" && speech != "partir")
        {
            switch (speech)
            {
                case "réparer":
                    int cost = (BlindShip_Stat.HP / BlindShip_Stat.Max_HP) * 1000 * BlindShip_Stat.Lvl;
                    if (cost <= BlindShip_Stat.Money) repair(cost);
                    else
                    {
                        Synthesis.synthesis("Désolé captaine, mais vous n'avez pas assez d'argent...");
                        Thread.Sleep(5000);
                    }
                    
                    break;

                case "équipement":
                case "améliorer":
                    UP();
                    break;
            }
        }
        else if (speech == "quiter" || speech == "partir")
        {
            Synthesis.synthesis("Aurevoir captaine, au plaisir de vous revoir dans mon échope !");
            Thread.Sleep(4000);
            UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene("Port"));
        }
        else
        {
            launch_shop(1);
        }
    }
    
    private static void repair(int cost)
    {
        BlindShip_Stat.Money -= cost;
        BlindShip_Stat.HP = BlindShip_Stat.Max_HP;
    }

    private static void UP()
    {
        Recognition.Function Main = _UP_;
        Launch(Main, "Améliorer vos cannon, la capacité de votre calle, vos quartier d'équipage, ou alors, améliorer votre sabre, votre pistolet, ou bien vous voulez ne rien faire ?", "cannon capacité calle quartier équipage sabre épée pistolet fusil rien", 15);
    }
    
    private static void _UP_(string speech)
    {
        Synthesis.synthesis("OK captaine !");
        Thread.Sleep(2000);

        int cost = 0;
        
        if (speech != "rien")
        {
            switch (speech)
            {
                case "canon":
                    if (BlindShip_Stat.Damage != BlindShip_Stat.Max_Damage)
                    {
                        cost = (BlindShip_Stat.Damage / BlindShip_Stat.Max_Damage) * 1000 * BlindShip_Stat.Lvl;
                        if (cost <= BlindShip_Stat.Money)
                        {
                            BlindShip_Stat.Money -= cost;
                            BlindShip_Stat.Damage += 3;
                        }
                        else
                        {
                            Synthesis.synthesis("Désolé captaine, mais vous n'avez pas assez d'argent...");
                            Thread.Sleep(5000);
                        }
                    }
                    else
                    {
                        Synthesis.synthesis("Désolé captaine, mais vos cannon sot déjà amélioré au maximum...");
                        Thread.Sleep(5000);
                    }
                    break;

                case "capacité":
                case "calle":
                    cost = (BlindShip_Stat.Money / BlindShip_Stat.Max_Money) * 1000 * BlindShip_Stat.Lvl;
                    if (cost <= BlindShip_Stat.Money)
                    {
                        BlindShip_Stat.Money -= cost;
                        BlindShip_Stat.Max_Money += 500;
                    }
                    else
                    {
                        Synthesis.synthesis("Désolé captaine, mais vous n'avez pas assez d'argent...");
                        Thread.Sleep(5000);
                    }
                    break;

                case "équipage":
                case "quartier":
                    cost = (BlindShip_Stat.Crew / BlindShip_Stat.Max_Crew) * 1000 * BlindShip_Stat.Lvl;
                    if (cost <= BlindShip_Stat.Money)
                    {
                        BlindShip_Stat.Money -= cost;
                        BlindShip_Stat.Max_Crew += 2;
                    }
                    else
                    {
                        Synthesis.synthesis("Désolé captaine, mais vous n'avez pas assez d'argent...");
                        Thread.Sleep(5000);
                    }
                    break;

                case "pistolet":
                case "fusil":
                    if (BlindCaptain_Stat.GunDamage != BlindCaptain_Stat.Max_GunDamage)
                    {
                        cost = (BlindCaptain_Stat.GunDamage / BlindCaptain_Stat.Max_GunDamage) * 1000 * BlindCaptain_Stat.Lvl;
                        if (cost <= BlindShip_Stat.Money)
                        {
                            BlindShip_Stat.Money -= cost;
                            BlindCaptain_Stat.GunDamage += 10;
                        }
                        else
                        {
                            Synthesis.synthesis("Désolé captaine, mais vous n'avez pas assez d'argent...");
                            Thread.Sleep(5000);
                        }
                    }
                    else
                    {
                        Synthesis.synthesis("Désolé captaine, mais votre pistolet ne peut pas être amélioré...");
                        Thread.Sleep(5000);
                    }
                    break;

                case "sabre":
                case "épée":
                    if (BlindCaptain_Stat.SwordDamage != BlindCaptain_Stat.Max_SwordDamage)
                    {
                        cost = (BlindCaptain_Stat.SwordDamage / BlindCaptain_Stat.Max_SwordDamage) * 1000 * BlindCaptain_Stat.Lvl;
                        if (cost <= BlindShip_Stat.Money)
                        {
                            BlindShip_Stat.Money -= cost;
                            BlindCaptain_Stat.SwordDamage += 5;
                        }
                        else
                        {
                            Synthesis.synthesis("Désolé captaine, mais vous n'avez pas assez d'argent...");
                            Thread.Sleep(5000);
                        }
                    }
                    else
                    {
                        Synthesis.synthesis("Désolé captaine, mais votre sabre ne peut pas être amélioré...");
                        Thread.Sleep(5000);
                    }
                    break;
            }
            
            BlindShip_Stat.XP += cost/(10*BlindShip_Stat.Lvl);
            BlindCaptain_Stat.XP += cost/(10*BlindCaptain_Stat.Lvl);
        }
    }
}
