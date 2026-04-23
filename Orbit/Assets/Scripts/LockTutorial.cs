using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTutorial : MonoBehaviour
{
    [SerializeField] Animator anim;

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            anim.SetBool("hasLocked", true);
            gameObject.GetComponent<LockTutorial>().enabled = false;
        }
    }
}
