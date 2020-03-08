using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class HoldItem : MonoBehaviour
{
    //TODO: Use LERP on rotation, reformat
    
    public Transform destinationTransform;
    public Material newMaterial;
    public float dist;

    private float holdDist;

    Camera cam;
    Rigidbody rb;
    private Transform originalParent;
    private bool beingHeld = false;
    Renderer m_Renderer;
    Material temp;
    private float radius = 1f;

    private float tweenDuration = 0.1f;
    private Vector3 pickupStartPosition, pickupEndPosition;
    private float pickupStartTime, pickupEndTime;
    public bool animatePickUp = false;
    private GameObject heldItem;

    private float throwForceXY = 1f;
    private float throwForceZ = 1f;

    private int collision = 0;

    void Start()
    {
        originalParent = transform.parent;
        m_Renderer = GetComponent<MeshRenderer>();
        temp = m_Renderer.material;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerController>().cam;
    }


    void Update()
    {
        dist = Vector3.Distance(cam.transform.position, transform.position);
        TriggerOutline();

        if(beingHeld)
        {
            holdDist = Vector3.Distance(destinationTransform.position, heldItem.transform.position);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (holdDist > 0.2f && !animatePickUp)
            {
                rb.useGravity = true;
                heldItem.transform.parent = originalParent;
                beingHeld = false;
            }
        }


        if (animatePickUp == true)
        { //Animating the travel between both points
            if (Time.time < pickupEndTime)
            {
                float lerp = Mathf.InverseLerp(pickupStartTime, pickupEndTime, Time.time);    // Calculate lerp value
                heldItem.transform.localPosition = Vector3.Lerp(pickupStartPosition, pickupEndPosition, lerp); //Animation call
            }
            else
            {
                //Animation finished
                animatePickUp = false;
            
            }
        }
    }

    public void ItemInteraction(GameObject item)
    { 

        if (!beingHeld && dist <= radius)
        {
            heldItem = item;
            rb = heldItem.GetComponent<Rigidbody>();
            //item.transform.position = destinationTransform.position;
            heldItem.transform.parent = destinationTransform;
            //rb.isKinematic = true;
            rb.useGravity = false;
            rb.detectCollisions = true;
           
            beingHeld = true;
            animatePickUp = true;
            pickupStartTime = Time.time;                         // Store when we start
            pickupEndTime = Time.time + tweenDuration;

            pickupStartPosition = item.transform.localPosition;  // Store where we start
            pickupEndPosition = new Vector3(0, 0, 0);
        }
        else if (beingHeld)
        {
            //rb.isKinematic = false;
            rb.useGravity = true;
            heldItem.transform.parent = originalParent;
            beingHeld = false;
        }  
    }

    public void throwItem(GameObject item, float swipeTime, Vector2 swipeDirection)
    {
        Debug.Log("Throw");
        //rb.isKinematic = false;
        rb.useGravity = true;
        //rb.AddForce(swipeDirection.x * throwForceXY,swipeDirection.y * throwForceXY,throwForceZ/swipeTime);
        rb.AddForce(destinationTransform.forward * 5, ForceMode.Impulse);
        rb.AddForce(destinationTransform.up * 5, ForceMode.Impulse);
        heldItem.transform.parent = originalParent;
        beingHeld = false;
    }

    void TriggerOutline()
    {
        if (dist <= radius && !beingHeld)
        {
            m_Renderer.material = newMaterial;
        }
        else
        {
            m_Renderer.material = temp;
        }
    }
    void OnCollisionEnter()
    {
        if(beingHeld)
        {
            collision++;
            Debug.Log("Collisions:" + collision);
        }
    }
    void OnCollisionExit()
    {
        if (beingHeld)
        {
            //heldItem.transform.localPosition = new Vector3(0, 0, 0);
            animatePickUp = true;
            pickupStartTime = Time.time;                         // Store when we start
            pickupEndTime = Time.time + tweenDuration;

            pickupStartPosition = heldItem.transform.localPosition;  // Store where we start
            pickupEndPosition = new Vector3(0, 0, 0);
        }
    }


}
