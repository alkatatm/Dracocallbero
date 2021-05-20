using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(0);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            SceneManager.LoadScene(1);
    }
}
