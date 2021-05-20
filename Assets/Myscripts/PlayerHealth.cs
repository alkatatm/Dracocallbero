using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float max_health = 100;
    public float current_health;
    public bool isDead = false;

    void Start()
    {
        current_health = max_health;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Fire"))
        {
            if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_shield_block"))
            {
                current_health -= 1f;
                GameObject.Find("HpBar").transform.Translate(-3, 0, 0);
            }
            else
            {
                current_health -= 5f;
                GameObject.Find("HpBar").transform.Translate(-15, 0, 0);
                if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_taking_hit"))
                    GetComponentInChildren<Animator>().SetTrigger("Hit");
            } 
        }

        if (other.tag.Equals("EnemySword"))
        {
            if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_shield_block"))
            {
                current_health -= 1f;
                GameObject.Find("HpBar").transform.Translate(-3, 0, 0);
            }
            else
            {

                current_health -= 5f;
                GameObject.Find("HpBar").transform.Translate(-15, 0, 0);
                if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_taking_hit"))
                    GetComponentInChildren<Animator>().SetTrigger("Hit");
            }

        }
        if (other.tag.Equals("Death"))
        {
            current_health = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate () {

        if (current_health <= 0)
        {
            GetComponentInChildren<Animator>().Play("hero_War_Dying");
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseAimCamera>().enabled = false;
            this.enabled = false;
            isDead = true;
        }
	}
}
