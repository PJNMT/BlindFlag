using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Mover_IA : MonoBehaviour
{
    
    private static bool decteted = false;
    public static int Level;
    public static BoatType type;
    private static int Speed;
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
        target = null;
        //(BoatType)(Random.Range(0, 3));
        int OurLevel = 10;
        Level = Random.Range(OurLevel-3,OurLevel+6);
        Speed = 50;
        
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
        
        if (decteted)
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
        }
        transform.Translate(Vector3.left * Speed * Time.deltaTime); //on avance en f° du temps

        if (Distance(target)<30)
        {
            SceneManager.LoadScene("seabattle");
            BlindShip_Stat.SceneLoad = 1;
            SceneManager.UnloadSceneAsync("navi");
        }
        
        
         
    }
    private double Distance(GameObject O_O)
    {
        return Math.Sqrt(Math.Pow(O_O.transform.position.x - transform.position.x, 2)+
                         Math.Pow(O_O.transform.position.z - transform.position.z, 2));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ship")
        {
            target = other.gameObject;
        }

        decteted = true;
    }

    private void OnTriggerExit(Collider other)
    {
        decteted = false;
    }
    
    void FaceTarget()
    {
        Vector3 direction = (BlindShip.position - transform.position).normalized;
        Quaternion Rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * 100f);
    }
    void FleeTarget()
    {
        Vector3 direction = (BlindShip.position - transform.position).normalized;
        Quaternion Rotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * 100f);
    }
}
