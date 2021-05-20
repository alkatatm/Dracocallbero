using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    

	void FixedUpdate () {

        GetComponent<PlayerMovement>().enabled = true;

        if (Input.GetKeyDown(KeyCode.LeftControl) &&
            !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_shield_block"))
        {
            GetComponentInChildren<Animator>().SetTrigger("Block");
        }

        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) &&
            (!GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_swing_right") &&
            !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_swing_left")))
        {
            GetComponentInChildren<Animator>().SetTrigger("Attack");
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) &&
            (!GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_run_swing_right") &&
            !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_run_swing_left")))
        {
            GetComponentInChildren<Animator>().SetTrigger("RunAttack");
        }

        if (Input.GetMouseButtonDown(1) &&
            !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_shield_blow"))
        {
            GetComponentInChildren<Animator>().SetTrigger("ShieldAttack");
        }
        


        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_shield_block"))
            GetComponent<PlayerMovement>().enabled = false;
        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_taking_hit"))
            GetComponent<PlayerMovement>().enabled = false;
        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_swing_left"))
            GetComponent<PlayerMovement>().enabled = false;
        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_swing_right"))
            GetComponent<PlayerMovement>().enabled = false;
        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hero_War_shield_blow"))
            GetComponent<PlayerMovement>().enabled = false;

    }
}
