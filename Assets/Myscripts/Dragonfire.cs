using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragonfire : MonoBehaviour
{
    Animator anim;
    public float fireCD = 5.0f;
    public float timeStamp;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameObject.Find("Player").GetComponent<PlayerHealth>().isDead)
        {
            StartCoroutine(Cooldown());
            if (timeStamp <= Time.time)
            {
                anim.SetLayerWeight(1, 0.5f);
                anim.SetTrigger("headFire1");
                StartCoroutine(Fire());
                timeStamp = Time.time + fireCD;
            }
        }
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5.0f);
    }
}
