using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Starting : MonoBehaviour
{

    public VideoPlayer videovague;
    public RawImage rawimage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        videovague.Prepare();
        WaitForSeconds waitforscds = new WaitForSeconds(1f);
        while (!videovague.isPrepared)
        {
            yield return waitforscds;
            break;
        }
        rawimage.texture = videovague.texture;
        videovague.Play();

    }

}
