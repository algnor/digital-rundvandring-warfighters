using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class PlacementScript : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject Skrapan;
   
    private Pose placementPose;
    private ARRaycastManager raycastManager;
    private bool PlacementPoseIsValid = false;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void FixedUpdate()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        if (PlacementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        
        raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.FeaturePoint);

        PlacementPoseIsValid = hits.Count > 0;
        if (PlacementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
    public void Place()
    {
       if (hits.Count > 0)
       {
           GameObject.Instantiate(Skrapan, placementPose.position, Quaternion.Euler(0, 0, 270));

       }
        

    }
}
