using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using TMPro;

public class PlacementScript : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject Skrapan;
    public GameObject[] Skrapor;
    public int YearIndex = 0;
    public TMP_Text yearText;
    
   
    private Pose placementPose;
    private bool PlacementPoseIsValid = false;
    private GameObject currentSkrapan;
    private GameObject oldSkrapan;

    private ARRaycastManager raycastManager;
    private ARSessionOrigin ARSessionOrigin;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject[] switchButtons;

    private bool SkrapanHasBeenPlaced = false;
    private bool SkrapanHasBeenChanged = false;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        ARSessionOrigin = FindObjectOfType<ARSessionOrigin>();
        switchButtons = GameObject.FindGameObjectsWithTag("switchBTN");
        foreach (GameObject button in switchButtons)
        {
            button.SetActive(false);
        }
    }

    void FixedUpdate()
    {   
        if(!SkrapanHasBeenPlaced)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }

        switch (YearIndex)
        {
            case 0:
                {
                    yearText.SetText("År 1900");
                    break;
                }
            case 1:
                {
                    yearText.SetText("År 1981");
                    break;
                }
            case 2:
                {
                    yearText.SetText("År 1985");
                    break;
                }
            case 3:
                {
                    yearText.SetText("År 2022");
                    break;
                }
            case 4:
                {
                    yearText.SetText("År 2100");
                    break;
                }
        }

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
           GameObject.Instantiate(Skrapor[0], placementPose.position, Quaternion.Euler(0, 0, 0), ARSessionOrigin.transform);
           SkrapanHasBeenPlaced = true;

           foreach(GameObject button in switchButtons)
           {
              button.SetActive(true);
           }

           var PlaceElements = GameObject.FindGameObjectsWithTag("placeBTN");
           foreach(GameObject element in PlaceElements)
           {
              element.SetActive(false);
           }

            placementIndicator.SetActive(false);

       }

    }

    public void ChangeSkrapan(int numberInput)
    {
        currentSkrapan = GameObject.FindGameObjectWithTag("building");
        YearIndex += numberInput;

        Debug.Log(YearIndex);
        if (!SkrapanHasBeenChanged)
        {
            Destroy(GameObject.FindGameObjectWithTag("building"));
        }
        if (YearIndex > Skrapor.Length - 1) YearIndex = 0;
        if (YearIndex == -1) YearIndex = Skrapor.Length - 1;
        oldSkrapan = currentSkrapan;
        Instantiate(Skrapor[YearIndex], currentSkrapan.transform.position, currentSkrapan.transform.rotation, ARSessionOrigin.transform);
        Destroy(oldSkrapan);
        
        SkrapanHasBeenChanged = true;

        
    }
}
