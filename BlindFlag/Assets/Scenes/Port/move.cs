using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour


{
    private string scene;
    private int repere;
    public int dist = 200;
    Vector3 mov = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        repere = 0;
        this.GetComponentInChildren<AudioSource>().mute = true;
        moveSpeed = 0f;
        scene = "";
        Recognition.Function F1 = choix;
        Recognition.start_recognition(F1,"taverne magasin "/* partir quitter partons*/, 0);
    }


    private void choix(string msg)
    {
        switch (msg)
        {
             case "taverne" :
                 if (scene == "")
                 {
                     scene = "taverne";                 
                     Debug.Log(scene);
                 }

    
                 break;
                case "magasin":
                    if (scene == "")
                    {
                        scene = "magasin";
                        Debug.Log(scene);
                    }

                    break;
             case "quitter":
             case "partons":
             case "partir" :
                 if (scene == "")
                 {
                     Synthesis.synthesis("Voulez vous allez au prochain port ou chercher un trésor ?");
                     Recognition.Function F2 = PouC;
                     Recognition.start_recognition(F2, "traisor port", 0);
                 }

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


    public float moveSpeed;

    public float turnSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        if (scene != "")
        {
            switch (scene)
            {
                 case "taverne" :
                     moveSpeed = 1;
                     scene = "0";
                     repere = Time.frameCount;
                    /*LoadScene.Load(LoadScene.Scene.Taverne, LoadScene.Scene.Port);*/
                     break;
                 case "magasin" :
                     moveSpeed = 5;
                     scene = "1";             
                     repere = Time.frameCount;   
                     /*LoadScene.Load(LoadScene.Scene.ShipShop, LoadScene.Scene.Port);*/
                     break;
                 case "port":
                     LoadScene.Load(LoadScene.Scene.Navigation, LoadScene.Scene.Port);
                     foreach (GameObject o in SceneManager.GetSceneByName("navi").GetRootGameObjects())
                     {
                        if (o.name == "island")
                        {
                            o.tag = "Le port";
                            break;
                        }
                     }
                     break;
                 case "tresor":
                     LoadScene.Load(LoadScene.Scene.Navigation, LoadScene.Scene.Port);
                     foreach (GameObject o in SceneManager.GetSceneByName("navi").GetRootGameObjects())
                     {
                        if (o.name == "island")
                        {
                            o.tag = "L'ile au trésor";
                            break;
                        }
                     }
                     break;
            }
        }


        if (scene == "0")
        {   
            int a = repere - Time.frameCount;
            if (a < dist || ( a> dist+20&&a<2*dist)||a>2*dist+20)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                GetComponentInChildren<AudioSource>().mute = false;
            }
            else 
            {
                Debug.Log("Caca");
                transform.Rotate(Vector3.left,45*Time.deltaTime);
                GetComponentInChildren<AudioSource>().mute = true;
            }
            /*else
            {
                transform.Rotate(Vector3.left,45*Time.deltaTime);
            }*/
        }

        if (scene == "1")
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            GetComponentInChildren<AudioSource>().mute = false;

        }
        


        // if (Input.GetKey(KeyCode.UpArrow)) moveSpeed += 1f; //on augmente la vitesse ou on la baisse
        // if (Input.GetKey(KeyCode.DownArrow)) moveSpeed -= 1f;
       /* if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("bite");
            this.GetComponentInChildren<AudioSource>().mute = false;
        }

        if (Input.GetKey(KeyCode.RightArrow)) this.GetComponentInChildren<AudioSource>().mute = true;

        //transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime); //on tourne en f° du temps et non de l'update
        // if (Input.GetKey(KeyCode.Space)) moveSpeed += 1f;

        if (moveSpeed < 0f) moveSpeed = 0f;*/

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

}