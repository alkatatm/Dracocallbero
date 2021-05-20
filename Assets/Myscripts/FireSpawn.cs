using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

   void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Wall"))
            Destroy(gameObject);
    }
    // Update is called once per frame
	void FixedUpdate () {

        GetComponent<Rigidbody>().AddForce(transform.forward * 25);
        Destroy(gameObject, 3.0f);
	}
}
