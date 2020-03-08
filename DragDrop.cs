using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    //Switch between two materials, one for the Orb being lit, another for it being unlit.
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private GameObject DragObject;
    private Vector2 touchPosition = default;
    private bool onTouchHold;
    private Vector3 CenterPos;
    [SerializeField]
    private float offsetForward;
    [SerializeField]
    private float offsetVertical;
    Rigidbody rb;

    void Start()
    {
        onTouchHold = false;
    }


    void Update()
    {
        //Look for touch screen input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                //Use raycast for based on what the camera is facing and where you touched(mousePoistion works for some reason)
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                  //If object is a draggable
                    if (hit.transform == DragObject.transform)
                    {
                        onTouchHold = true;
                        rb = DragObject.GetComponent<Rigidbody>();
                       
                    }
                }
            }
            if( touch.phase == TouchPhase.Ended)
            {
                onTouchHold = false;
                rb.isKinematic = false;
                //DragObject.Rigidbody.useGravity = false;
            }
        }

        if (onTouchHold == true)
        {
            rb.isKinematic = true;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            var cameraPos = Camera.current.transform.position;
            CenterPos = cameraPos + cameraBearing * offsetForward;
            CenterPos.y = CenterPos.y - offsetVertical;
            Quaternion targetRotation = Quaternion.LookRotation(cameraBearing);
            DragObject.transform.position = CenterPos;
            DragObject.transform.rotation = Quaternion.LookRotation(targetRotation * Vector3.right, Vector3.up);

        }
    }
}
