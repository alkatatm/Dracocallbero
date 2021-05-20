using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgCal : MonoBehaviour {

    
    void OnTriggerEnter(Collider other)
    {
        if ((GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_swing_right") ||
            GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_swing_left")))
        {
            if (other.tag.Equals("Enemy"))
            {
                other.GetComponent<Animator>().SetTrigger("Dmg");
                other.GetComponent<EnemyHealth>().current_health -= 15;
                GameObject.Find("SkeleHpBar").transform.Translate(75, 0, 0);
                GetComponent<AudioSource>().clip = GameObject.Find("Player").GetComponent<AudioLoad>().clipList[1];
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().Play();
                else
                    GetComponent<AudioSource>().Stop();
            }
        }

        if (GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_shield_blow"))
        {
            if (other.tag.Equals("Enemy"))
            {
                other.GetComponent<Animator>().SetTrigger("Dmg");
                other.GetComponent<EnemyHealth>().current_health -= 50;
                GameObject.Find("SkeleHpBar").transform.Translate(250, 0, 0);
                GetComponent<AudioSource>().clip = GameObject.Find("Player").GetComponent<AudioLoad>().clipList[1];
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().Play();
                else
                    GetComponent<AudioSource>().Stop();
            }
        }
    }
}
