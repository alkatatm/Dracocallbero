using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float max_health = 100;
    public float current_health;

    void Start()
    {
        current_health = max_health;
    }
    // Update is called once per frame
    void Update()
    {
        if (current_health <= 0)
        {
            gameObject.tag = "Untagged";
            GetComponent<Animator>().ResetTrigger("Dmg");
            GetComponent<Animator>().SetTrigger("Death");
            GameObject.Find("SkeletonTrigger").SetActive(false);
            Destroy(GameObject.Find("SkeleHp"));
            GetComponent<SkeletonController>().enabled = false;
            GetComponentInChildren<BoxCollider>().enabled = false;
            Destroy(this);
            
        }
    }
}
