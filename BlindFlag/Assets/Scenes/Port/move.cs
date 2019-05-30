﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.WebCam;

public class move : MonoBehaviour


{
    private string scene; 
    Vector3 mov = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        scene = "";
        Synthesis.synthesis("Ou voulez vous aller Capitaine ? A la taverne ? Au magasin ? ou bien voulez vous repartir ?");
        Recognition.Function F1 = choix;
        Recognition.start_recognition(F1,"taverne magasin partir quitter partons", 20);
    }


    private void choix(string msg)
    {
        switch (msg)
        {
                case "taverne" :
                    scene = "taverne";

                    break;
                case "magasin":
                    scene = "magasin";
                    
                    break;
                case "quitter":
                case "partons":
                case "partir" :
                    Synthesis.synthesis("Voulez vous allez au prochain port ou chercher un trésor ?");
                    Recognition.Function F2 = PouC;
                    Recognition.start_recognition(F2,"trésor port", 20);
                    
                    break;
                    
                    
                       
                    
        }
        
    }

    

    private void PouC(string msg)
    {
        switch (msg)
        {
                case "port":
                    scene = "port";
                    
                    break;
                
                case "trésor":
                    scene = "tresor";
                    
                    break;
                    
                    
        }
    }
    
    
    public float moveSpeed = 20f;
    public float turnSpeed = 50f;
    // Update is called once per frame
    void Update()
    {
        if (scene != "")
        {
            switch (scene)
            {
                    case "taverne" : 
                        
                        SceneManager.LoadScene("taverne");
                        BlindShip_Stat.SceneLoad = 3;
                        SceneManager.UnloadSceneAsync("port");
                        break;
                    case "magasin" :
                        
                        SceneManager.LoadScene("ShipShop");
                        BlindShip_Stat.SceneLoad = 6;
                        SceneManager.UnloadSceneAsync("port");
                        break;
                    case "port" :
                        SceneManager.LoadScene("navi");
                        foreach (GameObject o in SceneManager.GetSceneByName("navi").GetRootGameObjects())
                        {
                            if (o.name == "island")
                            {
                                o.tag = "Le port";
                                break;
                            }
                        
                        }
                        BlindShip_Stat.SceneLoad = 0;
                       
                        SceneManager.UnloadSceneAsync("Port");
                        break;
                    case "tresor" :
                        SceneManager.LoadScene("navi");
                        foreach (GameObject o in SceneManager.GetSceneByName("navi").GetRootGameObjects())
                        {
                            if (o.name == "island")
                            {
                                o.tag = "L'ile au trésor";
                                break;
                            }
                        
                        }
                        BlindShip_Stat.SceneLoad = 0;

                        SceneManager.UnloadSceneAsync("Port");
                        break;
            }
        }
        
        
        
        
        
        
        
        
        if (Input.GetKey(KeyCode.UpArrow)) moveSpeed += 1f; //on augmente la vitesse ou on la baisse
        if (Input.GetKey(KeyCode.DownArrow)) moveSpeed -= 1f;
        if(Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime); //on tourne en f° du temps et non de l'update
        if (Input.GetKey(KeyCode.Space)) moveSpeed += 1f;

        if (moveSpeed < 0f) moveSpeed = 0f;
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); //on avance en f° du temps
        
/*      transform.position += mov;
        if (transform.position.x>45)
        {
            mov.x = -0.1f;
        }

        if (transform.position.x < 0)
        {
            mov.x = 0.1f;
        }
*/
        
        
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ship-Shop")
        {
            
            Debug.Log("PIPIPIPI");

            //SceneManager.LoadScene("chasseautrésor");
        }
    }
}
