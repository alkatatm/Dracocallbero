using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaFire : MonoBehaviour {

    Rigidbody rb;
	// Use this for initialization
	void Start () {

        rb = gameObject.GetComponentInChildren<Rigidbody>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                rb.AddForce(transform.right * 5050);
            }
        }
    }
}
