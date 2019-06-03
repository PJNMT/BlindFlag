using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Mover_IA : MonoBehaviour
{
    
    private static bool decteted;
    public static int Level;
    public static BoatType type;
    public int Speed;
    public static float TailleMap = 1000f;
    private Transform BlindShip;
    public GameObject target;

    public enum BoatType
    {
        Marines = 0,
        Pirate,
        Marchand, 
        Corsaire
    }
    
    // Start is called before the first frame update
    void Start()
    {
        decteted = false;
        //(BoatType)(Random.Range(0, 3));
        int OurLevel = 10;
        Level = Random.Range(OurLevel-3,OurLevel+6);
        
        if (TailleMap < 200) TailleMap = 1000;
        
        float rx = Random.Range(-1f, 1f);
        float rz = Random.Range(-1f, 1f);
        
        float x = 0f;
        if (rx > 0) x = Random.Range(TailleMap/2, 200f);
        else x = Random.Range(-TailleMap/2, -200f);
        
        float z = 0;
        if (rz > 0) z = Random.Range(TailleMap/2, 200f);
        else z = Random.Range(-TailleMap/2, -200f);
        transform.position = new Vector3 (x, 1f, z);

    }

    // Update is called once per frame
    void Update()
    {
        bool control = true;
        if (Math.Abs(transform.position.x) > TailleMap / 2)
        {
            transform.Rotate(Vector3.up, Speed * Speed* Time.deltaTime);
            control = false;
        }
        if (Math.Abs(transform.position.z) > TailleMap / 2)
        {
            transform.Rotate(Vector3.up, Speed * Speed * Time.deltaTime);
            control = false;
        }

        if (control)
        {
            if (decteted && target != null)
            {
                BlindShip = target.transform;
                if (type == BoatType.Marchand)
                {
                    FleeTarget();
                    //s'enfuit
                }
                else if (type == BoatType.Marines)
                {
                    FaceTarget();
                    //s'approche
                }
                else
                {
                    if (Random.Range(0,1)<=0)
                    {
                        FaceTarget();
                        //bateau s'approche
                    }
                    else
                    {
                        FleeTarget();
                        //bateau s'enfuit
                    }
                }
            
            }
            else
            {
                transform.Rotate(Vector3.up, 50 * Time.deltaTime);
                transform.Translate(Vector3.left * Speed * Time.deltaTime); //on avance en f° du temps
            }
        }
        
        if (Distance(target.gameObject)<30)
        {
            Thread.Sleep(2000);
            LoadScene.Load(LoadScene.Scene.SeaBattle, LoadScene.Scene.Navigation);
        }   
    }
    private double Distance(GameObject O_O)
    {
        return Math.Sqrt(Math.Pow(O_O.transform.position.x - transform.position.x, 2)+
                         Math.Pow(O_O.transform.position.z - transform.position.z, 2));
    }
    private void OnTriggerEnter(Collider other)
    {
        decteted = true;
    }

    private void OnTriggerExit(Collider other)
    {
        decteted = false;
    }
    
    void FaceTarget()
    {
        transform.Translate((transform.position - target.transform.position).normalized * Time.deltaTime*Speed);
    }
    void FleeTarget()
    {
        transform.Translate(-(transform.position - target.transform.position).normalized * Time.deltaTime*Speed);
    }
}
