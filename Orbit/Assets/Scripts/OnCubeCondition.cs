using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCubeCondition : MonoBehaviour
{
    [SerializeField] GameObject conditionLight;
    [SerializeField] AudioClip ting;

    public void OnConditionMet()
    {
        conditionLight.SetActive(true);
        AudioSource.PlayClipAtPoint(ting, Camera.main.transform.position, 4);
    }

    public void OnConditionLost()
    {
        conditionLight.SetActive(false);
    }
}
