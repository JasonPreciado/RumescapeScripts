using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    private Inventory inventory;
    Camera cam;
    public float radius = 0.7f;
    Renderer m_Renderer;
    Material temp;
    public Material newMaterial;
    public float dist;
    public Item item;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("canvas").GetComponent<Inventory>();
        m_Renderer = GetComponent<MeshRenderer>();
        temp = m_Renderer.material;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerController>().cam;
    }
    void Update()
    {
        dist = Vector3.Distance(cam.transform.position, transform.position);
        TriggerOutline();
    }

    public void PickUp(GameObject gameobject)
    {
        Destroy(gameobject);
        inventory.AddItem(item);
    }

    void TriggerOutline()
    {
        if (dist <= radius)
        {
            m_Renderer.material = newMaterial;
        }
        else
        {
            m_Renderer.material = temp;
        }
    }
}
