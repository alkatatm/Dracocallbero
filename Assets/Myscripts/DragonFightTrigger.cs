using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFightTrigger : MonoBehaviour {

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
            foreach(Transform child in GameObject.Find("FireSound").transform)
            {
                child.gameObject.SetActive(true);
            }
            if (GameObject.FindGameObjectWithTag("Dragon").gameObject.transform.position.y < 300)
            {
                GameObject.FindGameObjectWithTag("Dragon").transform.Translate(0, 0.3f, 0);

            }
            else
            {
                GameObject.FindGameObjectWithTag("Dragon").transform.LookAt(target);
                GameObject.FindGameObjectWithTag("Dragon").GetComponent<Dragonfire>().enabled = true;
                GameObject.FindGameObjectWithTag("Dragon").GetComponent<FireBreath>().enabled = true;
            }
        }
    }
}
