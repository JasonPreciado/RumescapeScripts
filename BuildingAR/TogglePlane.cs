using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class TogglePlane : MonoBehaviour {
    public ARPlaneManager planeManager;
    public ARPointCloudManager pointCloudManager;
    
    
    public void OnValueChanged(bool isOn){
        VisualizePlanes(isOn);
        VisualizePoints(isOn);
        // planeManager.enabled = isOn;
        // m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;
        // Console.WriteLine(m_ARPlaneManager.enabled);
        // string planeDetectionMessage = "";
        // if (m_ARPlaneManager.enabled){
        //     planeDetectionMessage = "Disable Plane Detection and Hide Existing";
        //     SetAllPlanesActive(true);
        // }
        // else{
        //     planeDetectionMessage = "Enable Plane Detection and Show Existing";
        //     SetAllPlanesActive(false);
        // }
    }

    // void SetAllPlanesActive(bool value)
    // {
    //     foreach (var plane in m_ARPlaneManager.trackables)
    //         plane.gameObject.SetActive(value);
    // }
    void VisualizePlanes(bool active) {
        planeManager.enabled = active;
        foreach (ARPlane plane in planeManager.trackables){
            plane.gameObject.SetActive(active);
        } 
    }
    void VisualizePoints(bool active) {
        pointCloudManager.enabled = active;
        foreach (ARPointCloud point in pointCloudManager.trackables){
            point.gameObject.SetActive(active);
        } 
    }
  

}
