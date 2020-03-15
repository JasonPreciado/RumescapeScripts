using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceBuildingEdit : MonoBehaviour
{
    [SerializeField]
    public GameObject Building;
    [SerializeField]
    public GameObject RealLevel;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private GameObject PlayerCube;
    [SerializeField]
    private GameObject buildUI;
    [SerializeField]
    private Button redoButton;
    [SerializeField]
    private Button dismissButton;
    private bool menuActive;
    public bool BuildingPlaced;
    private bool planesDisabled;
    private bool pointsDisabled;
    public bool placementOver;
    private Vector3 CenterPos;
    private Vector3 targetPosition;
    private Quaternion targetAngle;
    private int buildingnumber;
    private Vector2 touchPosition = default;
    private Transform oTransform;
    [SerializeField]
    private ARSessionOrigin m_SessionOrigin;
    //private ARRaycastManager arRaycastManager;
    [SerializeField]
    public Vector3 finalBuildingPos;
    public Quaternion finalBuildingRot;


    void Start()
    {
        buildUI.SetActive(false);
        menuActive = false;
        placementOver = false;
        BuildingPlaced = false;
        buildingnumber = 0;
        //arRaycastManager = GetComponent<ARRaycastManager>();
        redoButton.onClick.AddListener(redoPlacement);
        dismissButton.onClick.AddListener(placementOK);
    }
    public void redoPlacement()
    {
            //set building false
            Building.SetActive(false);
            BuildingPlaced = false;
            //set planes and points active
            EnablePlanes();
            EnablePoints();
           //set menu false
            buildUI.SetActive(false);
            menuActive = false;
        
    }
    private void placementOK()
    {

        //set menu false
        buildUI.SetActive(false);
        //Destroy(buildUI);
        menuActive = false;
        //save data for future use
        finalBuildingPos = Building.transform.position;
        finalBuildingRot = Building.transform.rotation;
        //put real level to same place
        RealLevel.transform.position = Building.transform.position;
        RealLevel.transform.rotation = Building.transform.rotation;
        RealLevel.SetActive(false);
        //update all obj pos/rot for re-saving if edits not made
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            obj.data.pos = GameObject.Find(obj.data.OBJname).transform.position;
            obj.data.rot = GameObject.Find(obj.data.OBJname).transform.rotation;

        }

        placementOver = true;     

    }

    private void PlaceBuilding()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (m_SessionOrigin.GetComponent<ARRaycastManager>().Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            if (BuildingPlaced != true)
            {
                var hitPose = hits[0].pose;
                // set hit position to know y values plane
                targetPosition = new Vector3(hitPose.position.x, hitPose.position.y, hitPose.position.z);
                //set variables for camera position for rotation
                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                //for position
                var cameraPos = Camera.current.transform.position;
                //set position to 5 units from camera with y value being plane
                CenterPos = cameraPos + cameraBearing * 5;
                CenterPos.y = hitPose.position.y;
                //set rotation to look at camera
                Quaternion targetRotation = Quaternion.LookRotation(cameraBearing);
                //set building pos
                Building.transform.position = CenterPos;
                //set rot to object looking at camera orientated to the right
                Building.transform.rotation = Quaternion.LookRotation(targetRotation * Vector3.right, Vector3.up);
                //disable planes and point clouds
                DisablePlanes();
                DisablePoints();
                //set building active and ask if placement ok
                Building.SetActive(true);    
                BuildingPlaced = true;
                buildUI.SetActive(true);
            }
        }
    }
    void DisablePlanes()
    {
        ARPlaneManager planeManager = m_SessionOrigin.GetComponent<ARPlaneManager>();
        List<ARPlane> allPlanes = new List<ARPlane>();
        planeManager.enabled = false;
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        planesDisabled = true;
    }
    void DisablePoints()
    {
        ARPointCloudManager pointManager = m_SessionOrigin.GetComponent<ARPointCloudManager>();
        List<ARPointCloud> allPoints = new List<ARPointCloud>();
        pointManager.enabled = false;
        foreach (var point in pointManager.trackables)
        {
            point.gameObject.SetActive(false);
        }
        pointsDisabled = true;
    }
    void EnablePlanes()
    {
        ARPlaneManager planeManager = m_SessionOrigin.GetComponent<ARPlaneManager>();
        List<ARPlane> allPlanes = new List<ARPlane>();
        planeManager.enabled = true;
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
        planesDisabled = false;
    }
    void EnablePoints()
    {
        ARPointCloudManager pointManager = m_SessionOrigin.GetComponent<ARPointCloudManager>();
        List<ARPointCloud> allPoints = new List<ARPointCloud>();
        pointManager.enabled = true;
        foreach (var point in pointManager.trackables)
        {
            point.gameObject.SetActive(true);
        }
        pointsDisabled = false;
    }
    void Update()
    {
        if (placementOver != false)
            return;
        if (menuActive != false)
            return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                PlaceBuilding();
            }
        }
    }
}
