using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartPedestal : MonoBehaviour
{
    // put on all edit pedestals on start to initialize
    void Start()
    {
        EditModeOBJ eo = gameObject.AddComponent<EditModeOBJ>();
        eo.data.pos = gameObject.transform.position;
        eo.data.rot = gameObject.transform.rotation;
        eo.data.scale = gameObject.transform.localScale;
        eo.data.objectType = EditModeOBJ.ObjectType.Pedestal;
        eo.data.OBJname = name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
