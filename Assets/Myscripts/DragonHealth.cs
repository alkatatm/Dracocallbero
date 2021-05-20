using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHealth : MonoBehaviour {

    public int max_health = 1000;
    public int current_health;

    // Use this for initialization
    void Start () {
        current_health = max_health;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Arrow"))
        {
            current_health -= 250;
            GameObject.Find("DragHpBar").transform.Translate(125, 0, 0);
            GetComponent<Animator>().SetTrigger("flyGotHit");
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if (current_health <= 0)
        {
            GetComponent<Animator>().ResetTrigger("flyGotHit");
            GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>().Play();
            GameObject.Find("Drag").GetComponent<AudioSource>().Stop();
            GetComponentInChildren<AudioSource>().Play();
            GameObject.Find("DragonTrigger").SetActive(false);
            Destroy(GameObject.Find("DragHp"));
            Destroy(GetComponent<Dragonfire>());
            GetComponent<FireBreath>().enabled = false;
            GetComponent<Animator>().SetLayerWeight(1, 0);
            GetComponent<Animator>().SetTrigger("flyEndDie");
            Destroy(this);
        }
            
    }
}
