using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : MonoBehaviour {

    public GameObject[] magicSpell;
    public GameObject fire;
    public Transform firespawn;

    void Start()
    {
    }
    void StartSpell()
    {
        GetComponentInChildren<AudioSource>().Play();
        Instantiate(fire, firespawn.position, firespawn.rotation);
        for (int i = 0; i < magicSpell.Length; i++){
            if (magicSpell[i].GetComponent<ParticleSystem>())
                magicSpell[i].GetComponent<ParticleSystem>().Play();
                
            if (magicSpell[i].GetComponent<Light>())
                magicSpell[i].GetComponent<Light>().enabled = true;
        }
        
    }
    void EndSpell()
    {
        for (int i = 0; i < magicSpell.Length; i++){
            if (magicSpell[i].GetComponent<ParticleSystem>())
                magicSpell[i].GetComponent<ParticleSystem>().Stop();
            if (magicSpell[i].GetComponent<Light>())
                magicSpell[i].GetComponent<Light>().enabled = false;
        }
    }
}
