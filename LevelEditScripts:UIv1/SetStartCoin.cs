using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartCoin : MonoBehaviour
{
    // put on all edit coins on start to initialize
    void Start()
    {
        EditModeOBJ eo = gameObject.AddComponent<EditModeOBJ>();
        eo.data.pos = gameObject.transform.position;
        eo.data.rot = gameObject.transform.rotation;
        eo.data.scale = gameObject.transform.localScale;
        eo.data.objectType = EditModeOBJ.ObjectType.RealCoin1;
        eo.data.OBJname = name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
