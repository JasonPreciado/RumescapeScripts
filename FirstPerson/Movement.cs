using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement: MonoBehaviour
{
    public float speed;
    private float crouchDist = 0.5f;
    private bool isCrouching = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            Crouch();

        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }
    void Crouch()
    {
        if (isCrouching)
        {
            transform.Translate(0, crouchDist, 0);
            isCrouching = false;
            speed += 0.5f;
        }
        else
        {
            transform.Translate(0, -crouchDist, 0);
            isCrouching = true;
            speed -= 0.5f;
        }
    }
}
