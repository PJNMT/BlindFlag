using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Coco_Vigie : MonoBehaviour
{public AudioClip Babord; //Vigie
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
    public AudioClip EnVue;

    public AudioClip TutoCoco;
    public AudioClip TutoCoco1;
    public AudioClip TutoCoco2;//Coco
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

    public static AudioSource Audio;

    public static Vigie baba;
    
    public AudioClip DirectionToAudio(Vigie.direction D)
    {
        switch (D)
        {
            case Vigie.direction.Derriere:
                return Back;
            case Vigie.direction.ABabord:
                return Babord;
            case Vigie.direction.ATribord:
                return Tribord;
            default:
                return Forward;
        }
    }

    public AudioClip TagToAudio(string tag)
    {
        switch (tag)
        {
            case "Ile":
            case "Ile au trésor":
            case "Port":
                return IsleInView;
            default:
                return Ship;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        baba = FindObjectOfType<Vigie>();

        if (!BlindCaptain_Stat.Tuto["Coco"])
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(TutoCoco));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TutoCoco.length * 1000 + 500));
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(TutoCoco1));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TutoCoco1.length * 1000 + 500));
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(TutoCoco2));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TutoCoco2.length * 1000 + 500));

            BlindCaptain_Stat.Tuto["Coco"] = true;
        }

        Launch();
    }

    public void Choice(string speech)
    {
        if (speech == "coco") LaunchCoco();
        else LaunchVigie();
    }

    public void Launch()
    {
        Recognition.Function Func = Choice;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "coco baba vigie"));
    }

    public void LaunchCoco()
    {
        Coco.Paused();
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Cocococo));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Cocococo.length * 1000 + 500));
        Recognition.Function Func = OKCoco;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "niveau bateau navire vie HP raiputation XP expairience aipai sabre pistolet sauvegarder  sauver enregistrer quitter non rien merci"));
    }

    public void LaunchVigie()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(YesCaptain));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) YesCaptain.length * 1000 + 500));
        
        Recognition.Function Func = Baba;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "vois ennemie ile stop rien"));
    }

    public void Baba(string speech)
    {
        switch (speech)
        {
            case "vois":
                foreach (KeyValuePair<GameObject, string> pair in baba.ObjetsVus)
                {
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(TagToAudio(pair.Value)));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TagToAudio(pair.Value).length * 1000 + 500));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(DirectionToAudio(baba.Direction(pair.Key.gameObject))));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) DirectionToAudio(baba.Direction(pair.Key.gameObject)).length * 1000 + 500));
                }
                break;

            case "ennemie":
                foreach (KeyValuePair<GameObject, string> objetsVu in baba.ObjetsVus)
                {
                    if (objetsVu.Value == "Ennemy")
                    {
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(TagToAudio(objetsVu.Value)));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TagToAudio(objetsVu.Value).length * 1000 + 500));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(DirectionToAudio(baba.Direction(objetsVu.Key.gameObject))));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) DirectionToAudio(baba.Direction(objetsVu.Key.gameObject)).length * 1000 + 500));
                    }
                }
                break;

            case "ile":
                Debug.Log("ok captain");
                foreach (KeyValuePair<GameObject, string> objetsVu in baba.ObjetsVus)
                {
                    if ((objetsVu.Value == "Port" || objetsVu.Value == "Ile au trésor"))
                    {
                        Debug.Log("ok captain");
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(TagToAudio(objetsVu.Value)));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) TagToAudio(objetsVu.Value).length * 1000 + 500));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(DirectionToAudio(baba.Direction(objetsVu.Key.gameObject))));
                        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) DirectionToAudio(baba.Direction(objetsVu.Key.gameObject)).length * 1000 + 500));
                    }
                }
                break;
        }
        
        Launch();
    }

    public void OKCoco(string speech)
    {
        if (speech != "rien" && speech != "non" && speech != "merci" && speech != "quitter")
        {
            switch (speech)
            {
                case "niveau":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.Lvl + " niveaux."));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(2000));
                    break;

                case "bateau":
                case "navire":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VotreNavire));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) VotreNavire.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Lvl + " niveaux."));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(2000));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Crew + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Matelots));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) Matelots.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(IlPeutContenir));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) IlPeutContenir.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Max_Crew + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(HommeEquipage));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) HommeEquipage.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VotreNavire));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) VotreNavire.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.HP + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointHP));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) PointHP.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.XP + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(XP));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) XP.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(EtPeutCauser));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) EtPeutCauser.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Damage + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointDegat));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) PointDegat.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Enfin));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Enfin.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.Shield + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointResistance));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) PointResistance.length * 1000 + 500));
                    break;


                case "vie":
                case "HP":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VotreNavire));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) VotreNavire.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindShip_Stat.HP + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(HP));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) HP.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(EtVousAvez));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) EtVousAvez.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.HP + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointHP));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) PointHP.length * 1000 + 500));
                    break;

                case "raiputation":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.Reputation + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointReput));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) PointReput.length * 1000 + 500));
                    break;

                case "XP":
                case "expairience":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VotreNavire));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) VotreNavire.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                        Synthesis.synthesis(BlindShip_Stat.Lvl + " niveaux et " + BlindShip_Stat.XP));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(3000));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(XP));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) XP.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(EtVousAvez));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) EtVousAvez.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                        Synthesis.synthesis(BlindCaptain_Stat.Lvl + " niveaux et " + BlindCaptain_Stat.XP));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(3000));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(XP));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) XP.length * 1000 + 500));
                    break;

                case "aipai":
                case "sabre":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.SwordDamage + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointSword));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) PointSword.length * 1000 + 500));
                    break;

                case "pistolet":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VousAvez));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) VousAvez.length * 1000 + 500));

                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Synthesis.synthesis(BlindCaptain_Stat.GunDamage + ""));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));

                    UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(PointGun));
                    UnityMainThreadDispatcher.Instance()
                        .Enqueue(() => Thread.Sleep((int) PointGun.length * 1000 + 500));

                    break;

                case "sauver":
                case "sauvegarder":
                case "enregistrer":
                    Save.SaveGame();
                    break;
            }

            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(VoulezVous));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) VoulezVous.length * 1000 + 500));
        }
        else
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(OkCaptain));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) OkCaptain.length * 1000 + 500));
            Coco.Play();
            Launch(); 
        }
    }
}