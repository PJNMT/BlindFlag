using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Scenes.ShipShop
{
    public class ShipShop : MonoBehaviour
    {
        public AudioClip WhatDoUWantToDo_1;
        public AudioClip GoodBye;
        public AudioClip Hello;
        public AudioClip Enter;
        public AudioClip Gold;
        public AudioClip WhatDoUWantToDo;
        public AudioClip CannonCantBeUp;
        public AudioClip YourGoldRise;
        public AudioClip YourGunCantBeUp;
        public AudioClip YourSwordCantBeUp;
        public AudioClip YouDontHaveEngougthGold;
        public AudioClip WhatDoUWantToDo_2;
        public AudioClip OkCaptaine;
        public AudioClip Repaire;

        private AudioSource Audio;
        public GameObject Taverne;
        public GameObject Street;
        
        void Start()
        {
            Taverne.GetComponent<AudioSource>().Play();
            Taverne.GetComponent<AudioSource>().loop = true;
            Street.GetComponent<AudioSource>().Play();
            Street.GetComponent<AudioSource>().loop = true;
            Audio = GetComponent<AudioSource>();
            Launch(true, "raiparer amailiorer aiquipement quitter partir", true);
        }

        private void Launch(bool WDUWTD, string WordList, bool enter = false)
        {
            if (enter)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Enter));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Enter.length * 800));
                
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Hello));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Hello.length * 1000 + 500));
            }
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YourGoldRise));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YourGoldRise.length * 1000 + 500));
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Money + ""));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Gold));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Gold.length * 1000 + 500));
            
            Recognition.Function Func;

            if (WDUWTD)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(WhatDoUWantToDo));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) WhatDoUWantToDo.length * 1000 + 500));
                
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(WhatDoUWantToDo_1));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) WhatDoUWantToDo_1.length * 1000 + 500));
                Func = MainFunc;
            }
            else
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(WhatDoUWantToDo_2));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) WhatDoUWantToDo_2.length * 1000 + 500));
                Func = SecondFunc;
            }
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, WordList));
        }
        
        private void repair() 
        {
            int cost = (int) ((BlindShip_Stat.Max_HP / (float) BlindShip_Stat.HP) * 10);
            
            if (cost <= BlindShip_Stat.Money)
            {
                BlindShip_Stat.Money -= cost;
                UnityMainThreadDispatcher.Instance().Enqueue(() => BlindShip_Stat.AddStat(Audio, "HP"));
                
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Repaire));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Repaire.length * 1000 + 500));
            }
            else
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YouDontHaveEngougthGold));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500));
            }
        }

        private void Quit()
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(GoodBye));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) GoodBye.length * 1000 + 500));
            UnityMainThreadDispatcher.Instance().Enqueue(() => LoadScene.Load(LoadScene.Scene.Port, LoadScene.Scene.ShipShop));
        }

        private void MainFunc(string speech)
        {
            if (speech != "quitter" && speech != "partir")
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(OkCaptaine));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) OkCaptaine.length * 1000 + 500));
                
                switch (speech)
                {
                    case "raiparer":
                        repair();
                        Launch(true, "raiparer amailiorer aiquipement quitter partir");
                        break;

                    case "aiquipement":
                    case "amailiorer":
                        Launch(false, "cannon capacitai calle quartier aiquipage sabre aipai pistolet fusil rien");
                        break;
                }
            }
            else Quit();
        }

        private void SecondFunc(string speech)
        {
            int cost = 0;
        
            if (speech != "rien")
            {
                switch (speech)
                {
                    case "canon":
                        if (BlindShip_Stat.Damage < BlindShip_Stat.Max_Damage)
                        {
                            cost = (BlindShip_Stat.Damage / BlindShip_Stat.Max_Damage) * 1000 * BlindShip_Stat.Lvl;
                            if (cost <= BlindShip_Stat.Money)
                            {
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(OkCaptaine));
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) OkCaptaine.length * 1000 + 500));
                                
                                BlindShip_Stat.Money -= cost;
                                UnityMainThreadDispatcher.Instance().Enqueue(() => BlindShip_Stat.Adddamage(Audio, 3));
                            }
                            else
                            {
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YouDontHaveEngougthGold));
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500));
                            }
                        }
                        else
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(CannonCantBeUp));
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) CannonCantBeUp.length * 1000 + 500));
                        }
                        break;

                    case "capacitai":
                    case "calle":
                        cost = (BlindShip_Stat.Money / BlindShip_Stat.Max_Money) * 1000 * BlindShip_Stat.Lvl;
                        if (cost <= BlindShip_Stat.Money)
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(OkCaptaine));
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) OkCaptaine.length * 1000 + 500));
                            
                            BlindShip_Stat.Money -= cost;
                            BlindShip_Stat.Max_Money += 500;
                        }
                        else
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YouDontHaveEngougthGold));
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500));
                        }
                        break;

                    case "aiquipage":
                    case "quartier":
                        cost = (BlindShip_Stat.Crew / BlindShip_Stat.Max_Crew) * 1000 * BlindShip_Stat.Lvl;
                        if (cost <= BlindShip_Stat.Money)
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(OkCaptaine));
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) OkCaptaine.length * 1000 + 500));
                            
                            BlindShip_Stat.Money -= cost;
                            UnityMainThreadDispatcher.Instance().Enqueue(() => BlindShip_Stat.AddCrew(Audio, 2));
                        }
                        else
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YouDontHaveEngougthGold));
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500));
                        }
                        break;

                    case "pistolet":
                    case "fusil":
                        if (BlindCaptain_Stat.GunDamage < BlindCaptain_Stat.Max_GunDamage)
                        {
                            cost = (BlindCaptain_Stat.GunDamage / BlindCaptain_Stat.Max_GunDamage) * 1000 * BlindCaptain_Stat.Lvl;
                            if (cost <= BlindShip_Stat.Money)
                            {
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(OkCaptaine));
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) OkCaptaine.length * 1000 + 500));
                                
                                BlindShip_Stat.Money -= cost;
                                UnityMainThreadDispatcher.Instance().Enqueue(() => BlindCaptain_Stat.AddGundamage(Audio, 10));
                            }
                            else
                            {
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YouDontHaveEngougthGold));
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500));
                            }
                        }
                        else
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YourGunCantBeUp));
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YourGunCantBeUp.length * 1000 + 500));
                        }
                        break;

                    case "sabre":
                    case "aipai":
                        if (BlindCaptain_Stat.SwordDamage < BlindCaptain_Stat.Max_SwordDamage)
                        {
                            cost = (BlindCaptain_Stat.SwordDamage / BlindCaptain_Stat.Max_SwordDamage) * 1000 * BlindCaptain_Stat.Lvl;
                            if (cost <= BlindShip_Stat.Money)
                            {
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(OkCaptaine));
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) OkCaptaine.length * 1000 + 500));
                                
                                BlindShip_Stat.Money -= cost;
                                UnityMainThreadDispatcher.Instance().Enqueue(() => BlindCaptain_Stat.AddSworddamage(Audio, 5));
                            }
                            else
                            {
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YouDontHaveEngougthGold));
                                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500));
                            }
                        }
                        else
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YourSwordCantBeUp));
                            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YourSwordCantBeUp.length * 1000 + 500));
                        }
                        break;
                }
                
                BlindShip_Stat.XP += cost/(10*BlindShip_Stat.Lvl);
                BlindCaptain_Stat.XP += cost/(10*BlindCaptain_Stat.Lvl);
                
                Launch(true, "raiparer amailiorer aiquipement quitter partir");
            }
        }
    }
}