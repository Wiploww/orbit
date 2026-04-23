using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTutorial : MonoBehaviour
{
    [SerializeField] Animator animator;

    void OnMouseDown()
    {
        animator.SetTrigger("hasClicked");
    }
}
