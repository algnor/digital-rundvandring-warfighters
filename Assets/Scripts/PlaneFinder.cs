using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Class for finding plane info
/// </summary>
public class PlaneFinder : MonoBehaviour
{
    // The text component to write our findings too
    [SerializeField]
    TextMeshProUGUI planeInfoText;

    // Subsystem for casting raycasts in AR
    [SerializeField]
    ARRaycastManager raycastManager;

    // Subsystem for managing created planes
    [SerializeField]
    ARPlaneManager planeManager;

    // Update is executed every frame
    // The logic here casts a ray to find out which plane we are looking at
    private void Update()
    {
        // List for storing the objects that the ray intersects with
        List<ARRaycastHit> hits = new();

        // Casts a ray and returns true if something (TrackableType.PlaneWithinBounds in this case) has been hit
        if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinBounds))
        {
            // Check for making sure the object is of correct type
            // Also checks the first item in the hits list as that should be the first object that intersects with the raycast
            if (hits[0].hitType == TrackableType.PlaneWithinBounds)
            {
                // We need to access the planemanager to find out more information about the plane
                var plane = planeManager.GetPlane(hits[0].trackableId);

                // Sets planeInfoText with plane size
                planeInfoText.text = "x: " + plane.size.x.ToString("F2") + " y: " + plane.size.y.ToString("F2");

                // A common use case could be to make sure the plane is large enough before beginning the AR experience.
                // This example makes sure the plane is atleast 2x2 meters
                //if (((ARPlane)hits[0].trackable).size.x > 2 && ((ARPlane)hits[0].trackable).size.y > 2)
                //{
                //    eg StartGame();
                //}
            }

        }
        else
        {
            // Set planeInfoText.text to empty if we are not currently looking at a valid plane
            planeInfoText.text = "";
        }
    }
}
