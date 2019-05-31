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
        
        void Start()
        {
            Audio = GetComponent<AudioSource>();
            Launch(true, "réparer améliorer équipement quiter partir");
        }

        private void Launch(bool WDUWTD, string WordList, bool enter = false)
        {
            if (enter)
            {
                Audio.PlayOneShot(Enter);
                Audio.volume = 0.5f;
                Thread.Sleep((int) Enter.length * 1000);
                
                Audio.PlayOneShot(Hello);
                Audio.volume = 1f;
                Thread.Sleep((int) Hello.length * 1000 + 500);
            }
            
            Audio.PlayOneShot(YourGoldRise);
            Thread.Sleep((int) YourGoldRise.length * 1000 + 500);
            
            Synthesis.synthesis(BlindShip_Stat.Money + "");
            Thread.Sleep(1300);
            
            Audio.PlayOneShot(Gold);
            Thread.Sleep((int) Gold.length * 1000 + 500);
            
            Audio.PlayOneShot(WhatDoUWantToDo);
            Thread.Sleep((int) WhatDoUWantToDo.length * 1000 + 500);

            Recognition.Function Func;
            
            Audio.PlayOneShot(WhatDoUWantToDo);
            Thread.Sleep((int) WhatDoUWantToDo.length * 1000 + 500);

            if (WDUWTD)
            {
                Audio.PlayOneShot(WhatDoUWantToDo_1);
                Thread.Sleep((int) WhatDoUWantToDo_1.length * 1000 + 500);
                Func = MainFunc;
            }
            else
            {
                Audio.PlayOneShot(WhatDoUWantToDo_2);
                Thread.Sleep((int) WhatDoUWantToDo_2.length * 1000 + 500);
                Func = SecondFunc;
            }
            
            Recognition.start_recognition(Func, WordList);
        }
        
        private void repair()
        {
            int cost = (BlindShip_Stat.HP / BlindShip_Stat.Max_HP) * 1000 * BlindShip_Stat.Lvl;
            
            if (cost <= BlindShip_Stat.Money)
            {
                BlindShip_Stat.Money -= cost;
                BlindShip_Stat.HP = BlindShip_Stat.Max_HP;
                
                Audio.PlayOneShot(Repaire);
                Thread.Sleep((int) Repaire.length * 1000 + 500);
            }
            else
            {
                Audio.PlayOneShot(YouDontHaveEngougthGold);
                Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500);
            }
        }

        private void Quit()
        {
            Audio.PlayOneShot(GoodBye);
            Thread.Sleep((int) GoodBye.length * 1000 + 500);
            UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene("Port"));
        }

        private void MainFunc(string speech)
        {
            Audio.PlayOneShot(OkCaptaine);
            Thread.Sleep((int) OkCaptaine.length * 1000 + 500);
            
            if (speech != "quiter" && speech != "partir")
            {
                switch (speech)
                {
                    case "réparer":
                        repair();
                        break;

                    case "équipement":
                    case "améliorer":
                        Launch(false, "cannon capacité calle quartier équipage sabre épée pistolet fusil rien");
                        break;
                }
                
                Launch(true, "réparer améliorer équipement quiter partir");
            }
            else Quit();
        }

        private void SecondFunc(string speech)
        {
            Audio.PlayOneShot(OkCaptaine);
            Thread.Sleep((int) OkCaptaine.length * 1000 + 500);
            
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
                                Audio.PlayOneShot(YouDontHaveEngougthGold);
                                Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500);
                            }
                        }
                        else
                        {
                            Audio.PlayOneShot(CannonCantBeUp);
                            Thread.Sleep((int) CannonCantBeUp.length * 1000 + 500);
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
                            Audio.PlayOneShot(YouDontHaveEngougthGold);
                            Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500);
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
                            Audio.PlayOneShot(YouDontHaveEngougthGold);
                            Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500);
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
                                Audio.PlayOneShot(YouDontHaveEngougthGold);
                                Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500);
                            }
                        }
                        else
                        {
                            Audio.PlayOneShot(YourGunCantBeUp);
                            Thread.Sleep((int) YourGunCantBeUp.length * 1000 + 500);
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
                                Audio.PlayOneShot(YouDontHaveEngougthGold);
                                Thread.Sleep((int) YouDontHaveEngougthGold.length * 1000 + 500);
                            }
                        }
                        else
                        {
                            Audio.PlayOneShot(YourSwordCantBeUp);
                            Thread.Sleep((int) YourSwordCantBeUp.length * 1000 + 500);
                        }
                        break;
                }
                
                BlindShip_Stat.XP += cost/(10*BlindShip_Stat.Lvl);
                BlindCaptain_Stat.XP += cost/(10*BlindCaptain_Stat.Lvl);
                
                Launch(true, "réparer améliorer équipement quiter partir");
            }
        }
    }
}