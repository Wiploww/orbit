using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePulse : MonoBehaviour
{
    Material pulseMaterial;
    float pulseTime;
    Color pulseColor;

    //[SerializeField] PulseColor pulseColor;

    private void Start()
    {
        pulseMaterial = GetComponent<Renderer>().material;

        switch(transform.parent.tag)
        {
            case "Blue":
                pulseColor = new Color(0.374675542f, 0, 77.8742905f, 1f);
                break;
            case "White":
                pulseColor = new Color(7.90669966f, 7.90669966f, 7.90669966f, 1f); 
                break;
            case "Green":
                pulseColor = new Color(0.113275774f, 11.1817608f, 0f, 1f);
                break;
            default:
                Debug.Log("Please select a cube color.");
                break;
        }

        pulseMaterial.SetColor("_circleColor", pulseColor);
    }

    public void ActivatePulse()
    {
        pulseTime = 0;
        StartCoroutine(ScalePulse());
    }

    IEnumerator ScalePulse()
    {
        while (pulseTime < 1)
        {
            pulseTime += Time.deltaTime;
            pulseMaterial.SetFloat("_circleScale", pulseTime);
            yield return new WaitForSecondsRealtime(.005f);
        }
        yield return null;
    }
}
