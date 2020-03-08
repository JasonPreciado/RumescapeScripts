using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    public float fallDistance = 10f;
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -fallDistance)
        {
            transform.position = originalPos;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
