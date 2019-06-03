using System;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tresor_reco : MonoBehaviour
{
    private (float, float) coor;
    private bool launch;

    private Enigma enigme;

    private AudioSource Audio;
    private AudioSource Coco;

    public AudioClip Find;
    public AudioClip IfAnswer;
    public AudioClip UWin;
    public AudioClip GiveUp;
    public AudioClip WrongAnswer;
    public AudioClip WhatIsYourAnswer;
    public AudioClip Gold;
    
    void Start() 
    {
        Audio = GetComponent<AudioSource>();
        Coco = GameObject.Find("Coco").GetComponent<AudioSource>();
        
        coor.Item1 = Random.Range(2f, 98f);
        coor.Item2 = Random.Range(30f, 98f);
        
        transform.position = new Vector3(coor.Item1, 1f, coor.Item2);

        launch = false;
        
        enigme = new Enigma(BlindCaptain_Stat.NbEnigme);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => LaunchAnswer());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.name == "You" || other.name == "Sphere") && !launch)
        {
            launch = true;
            Coco.Stop();
            Coco.loop = false;
            Coco.volume = 0f;
            FindObjectOfType<cubecontroller>().sedeplacer = false;
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Find));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Find.length * 1000 + 500));

            UnityMainThreadDispatcher.Instance().Enqueue(() => LaunchEnigma());
        }
    }

    private void LaunchEnigma()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(enigme._enigme));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(enigme._enigme.Length * 80));
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(IfAnswer));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) IfAnswer.length * 1000 + 500));

        Recognition.Function Func = Option;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "abandonner abandonne stop arraiter indice aide raipaite raipaiter raientendre ainigme"));
    }

    private void LaunchAnswer()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(WhatIsYourAnswer));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) WhatIsYourAnswer.length * 1000 + 500));
        
        Recognition.Function Func = Answer;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func));
    }

    private void Option(string speech)
    {
        if (speech != "abandonner" && speech != "abandonne" && speech != "stop" && speech != "arraiter")
        {
            switch (speech)
            {
                case "indice":
                case "aide":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(enigme._indice));
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(enigme._indice.Length * 100));
                    break;
                
                case "raipaiter":
                case "raipaite":
                case "raientendre":
                case "ainigme":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => LaunchEnigma());
                    break;
            }
        }
        else END(false);
    }
    
    private bool IsGoodAnswer(string Answer_U, string Answer_E)
    {
        string[] U = Answer_U.Split(' ').Where(e => e != null).ToArray();
        string[] E = Answer_E.Split(' ').Where(e => e != null).ToArray();

        int len = E.Length - 1;
        bool Right = true;

        while (len >= 0 && Right)
        {
            Right &= U.Contains(E[len]);
            len -= 1;
        }

        return Right;
    }
    
    private void Answer(string speech)
    {
        if (IsGoodAnswer(speech, enigme._answer)) UnityMainThreadDispatcher.Instance().Enqueue(() => END(true));
        else
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(WrongAnswer));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) WrongAnswer.length * 1000 + 500));
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => LaunchEnigma());
        }
    }

    private void END(bool win)
    {
        if (win)
        {
            BlindShip_Stat.Money += enigme.gold;
            BlindCaptain_Stat.NbEnigme += 1;
            BlindCaptain_Stat.chasseautresor = false;

            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(UWin));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) UWin.length * 1000 + 500));
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(enigme.gold + ""));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Gold));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Gold.length * 1000 + 700));
        }
        else
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(GiveUp));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) GiveUp.length * 1000 + 500));
        }

        UnityMainThreadDispatcher.Instance().Enqueue(() => LoadScene.Load(LoadScene.Scene.Navigation, LoadScene.Scene.ChasseAuTresor));
    }
}

public class Enigma
{
    private string EnigmePath = "enigme.txt";
    
    public int _number;
    public string _enigme;
    public string _indice;
    public string _answer;
    public int gold;


    public Enigma(int number)
    {
        _number = number;
        
        using (StreamReader MyReader = new StreamReader(EnigmePath))
        {
            string lecture = "";
            int i = 0;

            while (i < _number && !MyReader.EndOfStream)
            {
                lecture = MyReader.ReadLine();
                i += 1;
            }
            
            string[] division = lecture.Split(':').Where(e => e != null).ToArray();

            _enigme = division[1];
            _indice = division[2];
            _answer = division[3];
            gold = int.Parse(division[4]);
        }
    }
}