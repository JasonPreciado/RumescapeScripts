using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    bool rotateL = false, rotateR = false;
    public Material Lit;
    public Material Unlit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotateR && rotateL)
        {
            rotateL = false;
            rotateR = false;
            GameObject.Find("ButtonL/Indicator").GetComponent<MeshRenderer>().material = Unlit;
            GameObject.Find("ButtonR/Indicator").GetComponent<MeshRenderer>().material = Unlit;
        }
        else if (rotateR)
        {
            transform.Rotate(0, 0, 50f * Time.deltaTime);
        }
        else if (rotateL)
        {
            transform.Rotate(0, 0, -50f * Time.deltaTime);
        }
    }
    public void activate(string button)
    {
        if (button == "ButtonL")
        {
            if (rotateL)
            {
                rotateL = false;
                GameObject.Find(button + "/Indicator").GetComponent<MeshRenderer>().material = Unlit;
            }
            else
            {
                rotateL = true;
                GameObject.Find(button + "/Indicator").GetComponent<MeshRenderer>().material = Lit;

            }
        }
        else if (button == "ButtonR")
        {
            if (rotateR)
            {
                rotateR = false;
                GameObject.Find(button + "/Indicator").GetComponent<MeshRenderer>().material = Unlit;
            }
            else
            {
                rotateR = true;
                GameObject.Find(button + "/Indicator").GetComponent<MeshRenderer>().material = Lit;
            }
        }

    }
}
