using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float v_movement;
    float h_movement;
    float r_movement;
    float r_speed = 250f;
    float speed = 3.0f;
    Animator anim;
    Rigidbody rb;
    public Transform GameCamera;

    void Start () {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        
        v_movement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        h_movement = Input.GetAxis("Horizontal") * (speed) * Time.deltaTime;
        

        anim.SetBool("Backward", false);
        anim.SetBool("Forward", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
        anim.SetBool("Sprint", false);
        anim.SetBool("Jump", false);
        

        if (v_movement < 0)
        {
            anim.SetBool("Backward", true);
            transform.Translate(0, 0, v_movement);
        }


        if (v_movement > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, v_movement * (speed - 1));
                anim.SetBool("Sprint", true);
                GetComponent<AudioSource>().clip = GetComponent<AudioLoad>().clipList[0];
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().Play();

            }
            else
            {
                GetComponent<AudioSource>().clip = GetComponent<AudioLoad>().clipList[2];
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().Play();
                
            }
            anim.SetBool("Forward", true);
            transform.Translate(0, 0, v_movement);
        }
        else
            GetComponent<AudioSource>().Stop();
        if (h_movement > 0 || h_movement < 0)
        {
            transform.Translate(h_movement, 0, 0);
            if (h_movement < 0)
                anim.SetBool("Left", true);
            anim.SetBool("Right", true);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0, 250, 0));
            anim.SetBool("Jump",true);
        }
        Quaternion cam_Rotate = GameCamera.transform.rotation;
        cam_Rotate.x = 0;
        cam_Rotate.z = 0;

        transform.rotation = cam_Rotate;

    }

}
