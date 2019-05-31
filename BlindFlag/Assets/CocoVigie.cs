using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Scenes

{
    public class CocoVigie : MonoBehaviour
    {
        public static Vigie baba;
        public bool activated;
        public bool quit;
        public bool pauseCoco;

        public AudioClip Babord;        //Vigie
        public AudioClip Tribord;
        public AudioClip Back;
        public AudioClip Corsaire;
        public AudioClip Pirates;
        public AudioClip Forward;
        public AudioClip Ship;
        public AudioClip YesCaptain;
        public AudioClip IsleInView;
        public AudioClip Galion;
        public AudioClip Military;

        public AudioClip TutoCoco;        //Coco
        public AudioClip CocoOn;
        public AudioClip Cocococo;
        public AudioClip Enfin;
        public AudioClip EtPeutCauser;
        public AudioClip EtVousAvez;
        public AudioClip HommeEquipage;
        public AudioClip HP;
        public AudioClip IlPeutContenir;
        public AudioClip Matelots;
        public AudioClip OkCaptain;
        public AudioClip PointDef;
        public AudioClip PointSword;
        public AudioClip PointGun;
        public AudioClip PointReput;
        public AudioClip PointResistance;
        public AudioClip PointHP;
        public AudioClip PointDegat;
        public AudioClip VotreNavire;
        public AudioClip VoulezVous;
        public AudioClip VousAvezShip;
        public AudioClip VousAvez;
        public AudioClip XP;

        private AudioSource Audio;

        void Start()
        {
            Audio = GetComponent<AudioSource>();
            
            if (!BlindCaptain_Stat.Tuto["Coco"])
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(TutoCoco));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TutoCoco.length * 1000 + 500));
                
                BlindCaptain_Stat.Tuto["Coco"] = true;
            }

            Recognition.Function OkCoco = Traitement_Call;
            UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(OkCoco, "Coco baba vigie"));

            baba = FindObjectOfType<Vigie>();
            quit = false;
        }


        void Answertraitement(string word)
        {
            Debug.Log(word);
            if (pauseCoco)
            {
                switch (word)
                {
                    //Coco case
                    case "niveau":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.Lvl + " niveaux."));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(2000));
                        break;

                    case "bateau":
                    case "navire":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VotreNavire));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VotreNavire.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Lvl + " niveaux."));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(2000));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Crew + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Matelots));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Matelots.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(IlPeutContenir));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) IlPeutContenir.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Max_Crew + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(HommeEquipage));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) HommeEquipage.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VotreNavire));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VotreNavire.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.HP + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointHP));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) PointHP.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.XP + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(XP));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) XP.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(EtPeutCauser));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) EtPeutCauser.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Damage + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointDegat));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) PointDegat.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Enfin));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Enfin.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Shield + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointResistance));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) PointResistance.length * 1000 + 500));
                        break;


                    case "vie":
                    case "HP":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VotreNavire));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VotreNavire.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.HP + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(HP));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) HP.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(EtVousAvez));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) EtVousAvez.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.HP + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointHP));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) PointHP.length * 1000 + 500));
                        break;

                    case "raiputation":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.Reputation + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointReput));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) PointReput.length * 1000 + 500));
                        break;

                    case "XP":
                    case "experience":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VotreNavire));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VotreNavire.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Lvl + " niveaux et " + BlindShip_Stat.XP));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(3000));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(XP));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) XP.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(EtVousAvez));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) EtVousAvez.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.Lvl + " niveaux et " + BlindCaptain_Stat.XP));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(3000));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(XP));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) XP.length * 1000 + 500));
                        break;

                    case "aipai":
                    case "sabre":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.SwordDamage + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointSword));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) PointSword.length * 1000 + 500));
                        break;

                    case "pistolet":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.GunDamage + ""));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
                        
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointGun));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) PointGun.length * 1000 + 500));

                        break;

                    case "sauver":
                    case "sauvegarder":
                    case "enregistrer":
                        Debug.Log("sauver");
                        break;

                    case "quitter":
                        Application.Quit(1);
                        break;

                    case "non":
                    case "rien":
                    case "merci":
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(OkCaptain));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) OkCaptain.length * 1000 + 500));
                        quit = true;
                        break;
                }
            }
            else
            {
                switch (word)
                {
                    //Vigie case
                    case "Vois":
                        foreach (KeyValuePair<GameObject, string> pair in baba.ObjetsVus)
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() =>
                                Synthesis.synthesis(pair.Value + " en vue " + baba.Direction(pair.Key.gameObject)));
                        }

                        quit = true;
                        break;

                    case "Ennemy":

                        foreach (KeyValuePair<GameObject, string> objetsVu in baba.ObjetsVus)
                        {
                            if (objetsVu.Value == "Ennemy")
                            {
                                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                                    Synthesis.synthesis(objetsVu.Value + " en vue " +
                                                        baba.Direction(objetsVu.Key.gameObject)));
                            }
                        }

                        quit = true;
                        break;

                    case "Ile":
                        Debug.Log("ok captain");
                        foreach (KeyValuePair<GameObject, string> objetsVu in baba.ObjetsVus)
                        {
                            if ((objetsVu.Value == "Port" || objetsVu.Value == "Ile au trésor"))
                            {
                                Debug.Log("ok captain");
                                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                                    Synthesis.synthesis(objetsVu.Value + " en vue " +
                                                        baba.Direction(objetsVu.Key.gameObject)));
                            }
                        }

                        quit = true;
                        break;
                }
            }

            if (!quit)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VoulezVous));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VoulezVous.length * 1000 + 500));
            }
        }

        private void Update()
        {
            if (activated)
            {
                Debug.Log("demande detecté");

                Recognition.Function AnswerCoco = Answertraitement;
                Debug.Log("nouvelle reco");
                
                UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(AnswerCoco,
                    "non rien merci niveau bateau navire HP vie XP raiputation aipai sabre pistolet " +
                    "sauver sauvegarder quitter Ennemy Ile Vois Bateau "));
                activated = false;
                Debug.Log("désactivé");
            }

            else if (quit)
            {
                quit = false;


                Recognition.Function Indication = Traitement_Call;
                UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Indication, "Coco baba vigie"));

                if (pauseCoco)
                {
                    UnityMainThreadDispatcher.Instance().Enqueue((() => Coco.Play()));
                }
            }
        }

        void Traitement_Call(string mot)
        {
            if (mot == "Coco")
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(CocoOn));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) CocoOn.length * 1000 + 500));
                activated = true;
                
                UnityMainThreadDispatcher.Instance().Enqueue((() => Coco.Paused()));
                pauseCoco = true;
            }

            if (mot == "baba" || mot == "vigie")
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YesCaptain));
                UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YesCaptain.length * 1000 + 500));
                Debug.Log("baba");
                activated = true;
            }
        }
    }
}