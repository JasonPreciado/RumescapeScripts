using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeOBJ : MonoBehaviour
{
    //for storing the info of what prefab each obj has
    public enum ObjectType { RealPig, RealCoin1, Pedestal, Room };
    [Serializable]
    public struct Data
    {
        //store obj info
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 scale;
        public ObjectType objectType;
        public string OBJname;
        public Component Anim;
        public string AnimN;
    }
    public Data data;
}
