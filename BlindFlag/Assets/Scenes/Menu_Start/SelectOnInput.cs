using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;
    
    public AudioClip Continue;
    public AudioClip NewGame;
    public AudioClip Options;
    public AudioClip Quit;
    public AudioClip Back;

    public AudioSource Audio;

    private bool buttonSelected;

    // Use this for initialization
    void Start ()
    {
        Audio = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update () 
    {
        if (Input.GetAxisRaw ("Vertical") != 0 && buttonSelected == false) 
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;

            /*string name = selectedObject.name;

            if (name == "ContinuButton")
            {
                Audio.PlayOneShot(Continue);
            }
            else if (name == "StartButton")
            {
                Audio.PlayOneShot(NewGame);
            }
            else if (name == "Options")
            {
                Audio.PlayOneShot(Options);
            }
            else if (name == "QuitButton")
            {
                
                Audio.PlayOneShot(Quit);
            }
            else if (name == "BackButton")
            {
                Audio.PlayOneShot(Back);
            }*/
            
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}