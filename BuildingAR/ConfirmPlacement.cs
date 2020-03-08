using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ConfirmPlacement : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public ARPointCloudManager pointCloudManager;
    public PlaceBuilding ObjScript;
    public void OnValueChanged()
    {
        ObjScript = (PlaceBuilding)GameObject.Find("ContentParent").GetComponent(typeof(PlaceBuilding));
        ObjScript.enabled = false;
        SetAllPlanesDeactive(false);
        SetAllPointsDeactive(false);
    }

    void SetAllPlanesDeactive(bool off)
    {
        planeManager.enabled = off;
        foreach (ARPlane plane in planeManager.trackables){
            plane.gameObject.SetActive(off);
        } 
    }
    void SetAllPointsDeactive(bool off)
    {   
        pointCloudManager.enabled = off;
        foreach (ARPointCloud point in pointCloudManager.trackables){
            point.gameObject.SetActive(off);
        } 
    }
}
