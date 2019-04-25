using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.WebCam;

public class move : MonoBehaviour


{
    
    Vector3 mov = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Synthesis.synthesis("Ou voulez vous aller Capitaine ? A la taverne ? Au magasin ? ou bien voulez vous repartir ?");
        Recognition.Function F1 = choix;
        Recognition.start_recognition(20,"taverne magasin quitter",F1);
    }


    private void choix(string msg)
    {
        switch (msg)
        {
                case "taverne" :
                    SceneManager.LoadScene("taverne");
                    SceneManager.UnloadSceneAsync("port");
                    break;
                case "magasin":
                    SceneManager.LoadScene("ShipShop");
                    SceneManager.UnloadSceneAsync("port");
                    break;
                case "quitter":
                    Synthesis.synthesis("Voulez vous allez au prochain port ou chercher un trésor ?");
                    Recognition.Function F2 = PouC;
                    Recognition.start_recognition(20,"trésor port",F2);
                    break;
                    
                    
                    
                    
        }
        
    }

    

    private void PouC(string msg)
    {
        switch (msg)
        {
                case "port":
                    SceneManager.LoadScene("navi");
                    foreach (GameObject o in SceneManager.GetSceneByName("navi").GetRootGameObjects())
                    {
                        if (o.name == "island")
                        {
                            o.tag = "Le port";
                            break;
                        }
                        
                    }
                    
                    SceneManager.UnloadSceneAsync("Port");
                    break;
                
                case "trésor":
                    SceneManager.LoadScene("navi");
                    foreach (GameObject o in SceneManager.GetSceneByName("navi").GetRootGameObjects())
                    {
                        if (o.name == "island")
                        {
                            o.tag = "L'ile au trésor";
                            break;
                        }
                        
                    }

                    SceneManager.UnloadSceneAsync("Port");
                    break;
                    
                    
        }
    }
    
    
    public float moveSpeed = 20f;
    public float turnSpeed = 50f;
    // Update is called once per frame
    void Update()
    {
        
        
        
        
        
        
        
        
        
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
