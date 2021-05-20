using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTrigger : MonoBehaviour {

    public Transform target;
    public GameObject hp;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            hp.SetActive(true);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GameObject.FindGameObjectWithTag("Enemy").transform.LookAt(target);
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<SkeletonController>().enabled = true;
            
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

