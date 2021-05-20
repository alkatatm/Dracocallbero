using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GameObject.Find("Skele").GetComponent<AudioSource>().volume -= 0.03f;
            if (GameObject.Find("Skele").GetComponent<AudioSource>().volume == 0)
            {
                GameObject.Find("Drag").GetComponent<AudioSource>().volume += 0.01f;
                GameObject.Find("Skele").GetComponent<AudioSource>().Stop();
                if (!GameObject.Find("Drag").GetComponent<AudioSource>().isPlaying)

                    GameObject.Find("Drag").GetComponent<AudioSource>().Play();
            }
        }
    }
}


