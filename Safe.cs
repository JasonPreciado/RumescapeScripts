using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    private Animation anim;
    public Material indicatorOff;
    public Material indicatorOn;
    private GameObject indicator;
    private bool isOpen = false;

    private int[] safeValues = new int[3];

    public Material[] safeNumbers = new Material[9];
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        indicator = GameObject.Find("Safe Door/Lock Indicator");
        safeNumbers[0] = (Material)Resources.Load("One", typeof(Material));
        safeNumbers[1] = (Material)Resources.Load("Two", typeof(Material));
        safeNumbers[2] = (Material)Resources.Load("Three", typeof(Material));
        safeNumbers[3] = (Material)Resources.Load("Four", typeof(Material));
        safeNumbers[4] = (Material)Resources.Load("Five", typeof(Material));
        safeNumbers[5] = (Material)Resources.Load("Six", typeof(Material));
        safeNumbers[6] = (Material)Resources.Load("Seven", typeof(Material));
        safeNumbers[7] = (Material)Resources.Load("Eight", typeof(Material));
        safeNumbers[8] = (Material)Resources.Load("Nine", typeof(Material));

        safeValues[0] = 7;
        safeValues[1] = 7;
        safeValues[2] = 7;


    }

    // Update is called once per frame
    void Update()
    {
        if(safeValues[0] == 3 && safeValues[1] == 0 && safeValues[2] == 5 && !isOpen)
        {
            safeOpen();
            isOpen = true;
        }
    }

    public void changeLockNumber(string name)
    {
        if(name == "Lock Number 1")
        {
            safeValues[0]++;
            safeValues[0] = safeValues[0] % 9;
            GameObject.Find(name).GetComponent<MeshRenderer>().material = safeNumbers[safeValues[0]];
        }
        else if(name == "Lock Number 2")
        {
            safeValues[1]++;
            safeValues[1] = safeValues[1] % 9;
            GameObject.Find(name).GetComponent<MeshRenderer>().material = safeNumbers[safeValues[1]];
        }
        else if(name == "Lock Number 3")
        {
            safeValues[2]++;
            safeValues[2] = safeValues[2] % 9;
            GameObject.Find(name).GetComponent<MeshRenderer>().material = safeNumbers[safeValues[2]];
        }
    }

    void safeOpen()
    {
        indicator.GetComponent<MeshRenderer>().material = indicatorOn;
        anim.Play("Open");
    }
}
