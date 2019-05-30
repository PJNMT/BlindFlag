using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bruitsbateau : MonoBehaviour
{
    private GameObject player;
    public AudioClip plancher;
    public AudioClip grincement;
    private bool do_grinc = true;
    private bool do_plancher = true;
    private Vector3 lastplay = new Vector3(0, 0, 0);

    IEnumerator soundmouettes()
    {
        GetComponent<AudioSource>().PlayOneShot(grincement);
        yield return new WaitForSeconds(30f);
        do_grinc = true;
    }

    IEnumerator soundplancher()
    {
        GetComponent<AudioSource>().PlayOneShot(plancher);
        yield return new WaitForSeconds(3f);
        do_plancher = true;
    }

    void Start()
    {
        player = GameObject.Find("You");
    }

    void Update()
    {
        if (do_grinc)
        {
            do_grinc = false;
            StartCoroutine(soundmouettes());
        }

        if (!(lastplay == player.transform.position) && do_plancher)
        {
            do_plancher = false;
            StartCoroutine(soundplancher());
            lastplay = player.transform.position;
        }
    }
}
