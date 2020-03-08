using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Camera cam;
    private Vector2 touchPosition = default;
    GameObject item;
    GameObject Orb;
    string name;

    private Vector2 startTouchPos, endTouchPos, swipeDist;
    private float startTime, endTime, swipeTime;

    private bool isSwipe = false;
    private bool isTouch = false;

    private bool isHoldingItem = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
            startTime = Time.time;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;
            endTime = Time.time;
            swipeTime = endTime - startTime;
            swipeDist = endTouchPos - startTouchPos;

            if (swipeDist.y / Screen.height >= 0.25 && swipeTime < 1)
            {
                isSwipe = true;
            }
            else if (swipeTime < 0.25)
            {
                isTouch = true;
            }
        }


        if (isSwipe) //isSwipe
        {
            if (isHoldingItem)
            {
                item.GetComponent<HoldItem>().throwItem(item, swipeTime, swipeDist);
                isHoldingItem = false;
            }
            isSwipe = false;
        }//if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        else if(isTouch) //isTouch
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (isHoldingItem)
            {
                item.GetComponent<HoldItem>().ItemInteraction(item);
                isHoldingItem = false;
            }
            else if (Physics.Raycast(ray, out hit, 5))
            {
                //Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (hit.transform.tag == "Orb")
                {
                    Orb = hit.collider.gameObject;
                    Orb.GetComponent<ToggleOrb>().LightOrb(Orb);
                }
                else if (hit.transform.tag == "button")
                {
                    GameObject control = GameObject.Find("RotatingMaze");
                    name = hit.collider.gameObject.name;

                    control.GetComponent<Rotate>().activate(name);

                }
                else if (hit.transform.tag == "LockNum")
                {
                    Debug.Log("HIT");
                    GameObject safe = GameObject.Find("Safe");
                    name = hit.collider.gameObject.name;

                    safe.GetComponent<Safe>().changeLockNumber(name);

                }
                else if (hit.collider.gameObject.GetComponent<HoldItem>() != null)
                {
                    item = hit.collider.gameObject;
                    item.GetComponent<HoldItem>().ItemInteraction(item);
                    isHoldingItem = true;
                }
                else
                {
                    item = hit.collider.gameObject;
                    //Debug.Log("got here" + item.name);
                    if (item != null)
                        item.GetComponent<ItemPickup>().PickUp(item);
                }
            }
            isTouch = false;
        }
    }
}

