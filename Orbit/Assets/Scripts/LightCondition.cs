using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class LightCondition : MonoBehaviour
{
    enum Condition
    {
        Error, inLight, inDark, inRed, inLightAndDark
    };

    Condition[] edgeCondition = new Condition[4];

    [Header("Conditions")]
    [SerializeField] Condition currentCondition;
    public bool conditionMet = false;

    //Raycast
    LayerMask layer = 1 << 6;
    RaycastHit[][] hits = new RaycastHit[4][];
    Vector3[] edges = new Vector3[4];
    Vector3 direction;
    Color color;

    [Header("References")]
    [SerializeField] GameObject conditionLight;
    [SerializeField] AudioClip ting;
    private bool hasTinged;
    private GameObject sun;

    void Start()
    {
        sun = GameObject.Find("Sun");

        switch (tag)
        {
            case "White":
                color = Color.white;
                break;
            
            case "Blue":
                color = Color.blue;
                break;

            case "Green":
                color = Color.green;
                break;
        }
    }

    public void Update()
    {
        PositionEdges();
        CastRays();
        GetEdgeConditions();
        GetTotalCondition();
    }

    #region - Raycasting -

    void PositionEdges()
    {
        edges[0] = new Vector3(transform.localPosition.x + .8f, transform.position.y, transform.localPosition.z + .8f);
        edges[1] = new Vector3(transform.localPosition.x + .8f, transform.position.y, transform.localPosition.z - .8f);
        edges[2] = new Vector3(transform.localPosition.x - .8f, transform.position.y, transform.localPosition.z + .8f);
        edges[3] = new Vector3(transform.localPosition.x - .8f, transform.position.y, transform.localPosition.z - .8f);
    }

    void CastRays()
    {
        //Cast rays
        for (int i = 0; i < 4; i++)
        {
            hits[i] = Physics.RaycastAll(edges[i], (sun.transform.position - edges[i]).normalized, Vector3.Distance(edges[i], sun.transform.position), layer);
        }
    }

    #endregion

    void GetEdgeConditions()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < hits[i].Length; j++)
            {
                if((hits[i][j].transform.tag) == "Red")
                {
                    if(!YellowBlockCheck(i, 0))
                    {
                        return;
                    }
                }

                Debug.Log(gameObject.name + hits[i][j].transform.name);
            }

            if (hits[i].Length == 0)
            {
                edgeCondition[i] = Condition.inLight;

                //Draw rays
                Debug.DrawRay(edges[i], (sun.transform.position - edges[i]).normalized * Vector3.Distance(edges[i], sun.transform.position), Color.white);
            }
            else
            {
                switch (hits[i][0].transform.tag)
                {
                    case "White":
                    case "Blue":
                    case "Yellow":

                        edgeCondition[i] = Condition.inDark;
                    
                        //Draw ray
                        Debug.DrawRay(edges[i], (sun.transform.position - edges[i]).normalized * Vector3.Distance(edges[i], sun.transform.position), Color.blue);
                        break;

                    case "Red":
                        break;

                    case "Green":
                        edgeCondition[i] = Condition.inLight;
                        //Draw ray
                        Debug.DrawRay(edges[i], (sun.transform.position - edges[i]).normalized * Vector3.Distance(edges[i], sun.transform.position), Color.white);
                        break;

                    default:
                        edgeCondition[i] = Condition.Error;
                        break;

                }
            }
        }
    }

    bool YellowBlockCheck(int i, int redNum)
    {
        for(int j = 0; j < redNum; j++)
        {
            if (hits[i][j].transform.tag == "Yellow")
            {
                return true;
            }
        }

        edgeCondition[i] = Condition.inRed;

        //Draw rays
        Debug.DrawRay(edges[0], (sun.transform.position - edges[0]).normalized * Vector3.Distance(edges[0], sun.transform.position), Color.red);
        Debug.DrawRay(edges[1], (sun.transform.position - edges[1]).normalized * Vector3.Distance(edges[1], sun.transform.position), Color.red);
        Debug.DrawRay(edges[2], (sun.transform.position - edges[2]).normalized * Vector3.Distance(edges[2], sun.transform.position), Color.red);
        Debug.DrawRay(edges[3], (sun.transform.position - edges[3]).normalized * Vector3.Distance(edges[3], sun.transform.position), Color.red);

        return false;
    }

    void GetTotalCondition()
    {
        for (int i = 0; i < 4; i++)
        {
            //Any red
            if (edgeCondition[i] == Condition.inRed)
            {
                currentCondition = Condition.inRed;
                //Draw ray
                Debug.DrawRay(edges[i], (sun.transform.position - edges[i]).normalized * Vector3.Distance(edges[i], sun.transform.position), Color.red);

                conditionMet = false;
                conditionLight.SetActive(false);
                return;
            }
        }
        
        if (Condition.inLight == edgeCondition[0] && edgeCondition[0] == edgeCondition[1] && edgeCondition[1] == edgeCondition[2] && edgeCondition[2] == edgeCondition[3])
        {
            currentCondition = Condition.inLight;
        }
        else if (Condition.inDark == edgeCondition[0] && edgeCondition[0] == edgeCondition[1] && edgeCondition[1] == edgeCondition[2] && edgeCondition[2] == edgeCondition[3])
        {
            currentCondition = Condition.inDark;
        }
        else
        {
            currentCondition = Condition.inLightAndDark;
        }

        switch (tag)
        {
            case "White":
                if(currentCondition == Condition.inLight)
                {
                    conditionMet = true;
                    conditionLight.SetActive(true);
                }
                else
                {
                    conditionMet = false;
                    conditionLight.SetActive(false);
                }
                break;
            
            case "Blue":
                if (currentCondition == Condition.inDark)
                {
                    conditionMet = true;
                    conditionLight.SetActive(true);
                }
                else
                {
                    conditionMet = false;
                    conditionLight.SetActive(false);
                }
                break;

            case "Green":
                conditionMet = true;
                conditionLight.SetActive(true);
                break;
        }

        if(!hasTinged && conditionMet)
        {
            AudioSource.PlayClipAtPoint(ting, Camera.main.transform.position, 4);
            hasTinged = true;
        }
        else if (!conditionMet)
        {
            hasTinged = false;
        }
    }
}
