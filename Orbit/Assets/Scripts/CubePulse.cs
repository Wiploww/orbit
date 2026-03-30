using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePulse : MonoBehaviour
{
    Material pulseMaterial;
    float pulseTime;
    //Color circleColor;

    enum PulseColor
    {
        None = 0,
        Blue,
        White,
        Green
    };

    //[SerializeField] PulseColor pulseColor;

    private void Start()
    {
        pulseMaterial = GetComponent<Renderer>().material;

        /*
        switch(pulseColor)
        {
            case PulseColor.Blue:
                circleColor = new Color(0.374675542f, 0, 77.8742905f, 1f);
                break;
            case PulseColor.White:
                circleColor = new Color(7.90669966f, 7.90669966f, 7.90669966f, 1f); 
                break;
            case PulseColor.Green:
                circleColor = new Color(0.113275774f, 11.1817608f, 0f, 1f);
                break;
            default:
                Debug.Log("Please select a cube color.");
                break;
        }

        pulseMaterial.SetColor("_circleColor", circleColor);
        */
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
            pulseTime += .01f;
            pulseMaterial.SetFloat("_circleScale", pulseTime);
            yield return new WaitForSecondsRealtime(.005f);
        }
        yield return null;
    }
}
