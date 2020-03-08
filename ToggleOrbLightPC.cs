using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOrbLightPC : MonoBehaviour
{
    //Switch between two materials, one for the Orb being lit, another for it being unlit.
    [SerializeField]
    private Material Lit;
    [SerializeField]
    private Material Unlit;
    [SerializeField]
    private Material UnlitOL;
    GameObject Orb;
    [SerializeField]
    private Camera cam;
    Renderer m_Renderer;
    public float radius = 0.5f;
    public float dist;

    public bool itemLit;
    


    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

 
    void Update()
    {

        dist = Vector3.Distance(cam.transform.position, transform.position);

        //Look for touch screen input
       //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        if (Input.GetMouseButtonDown (0))
        {
            //Use raycast for based on what the camera is facing and where you touched(mousePoistion works for some reason)
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit, 1) && dist <= radius)
            {
                //If object is an Orb
                if (hit.transform.tag == "Orb")
                {
                    Orb = hit.collider.gameObject;
                    if (Orb.GetComponent<MeshRenderer>().sharedMaterial == Unlit)
                    {
                        Orb.GetComponent<MeshRenderer>().material = Lit;
                        itemLit = true;
                    }
                    else
                    {
                        Orb.GetComponent<MeshRenderer>().material = Unlit;
                        itemLit = false;
                    }
                }
            }
            if (!itemLit)
                TriggerOutline();
        }
    }

    void TriggerOutline()
    {
        if (dist <= radius)
        {
            m_Renderer.material = UnlitOL;
        }
        else
        {
            m_Renderer.material = Unlit;
        }
    }

}
