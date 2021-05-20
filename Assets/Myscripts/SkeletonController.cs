using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour {

    public float hitCD = 5.0f;
    public float timeStamp;

    // Update is called once per frame
    void FixedUpdate () {

        if (!GameObject.Find("Player").GetComponent<PlayerHealth>().isDead)
        {
            if (timeStamp <= Time.time)
            {
                
                GetComponent<Animator>().SetTrigger("Hit");
                timeStamp = Time.time + hitCD;
            }
        }
    }
}
