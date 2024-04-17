using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ARFocusCircle : MonoBehaviour
{
    public GameObject object1;

    public GameObject button;

    public GameObject placementIndicator;

    private Image placementIndicatorImage;

    private ARSessionOrigin arOrigin;
     private Pose placementPose;
     private bool placementPoseIsValid = false;

    private bool placementIndicatorEnabled = true;

    void Start()
    {
      //  arOrigin = FindObjectOfType<ARSessionOrigin>();

       // placementIndicatorImage = placementIndicator.GetComponent<Image>();

        //scanText.SetActive(true);
        //placeText.SetActive(false);
    }

    void Update()
    {

        if (placementIndicatorEnabled == true)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }

        //if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //  PlaceObject();
        //}
    }

    public void PlaceObject()
    {

        object1.transform.position = placementPose.position;
        object1.transform.rotation = placementPose.rotation;
        object1.SetActive(true);


        //GameObject[] virtualObjects = new GameObject[] { object1 };

        //for (int i = 0; i < virtualObjects.Length; i++)
        //{
        //    GameObject objectToPlace = Instantiate(virtualObjects[i]);
        //    objectToPlace.SetActive(true);
        //    objectToPlace.transform.position = placementPose.position;
        //    objectToPlace.transform.rotation = placementPose.rotation;


        button.SetActive(false);
        DestroyGameObject();
    }

    void DestroyGameObject()
    {
        Destroy(placementIndicator);
    }

    public void SpawnAllObjects()
    {

        PlaceObject();
    }

    private void UpdatePlacementIndicator()
    {
        if (placementIndicator != null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            if (placementIndicatorImage != null)
                placementIndicatorImage.enabled = true;
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

            Vector3 cameraPosition = Camera.current != null ? Camera.current.transform.position : arOrigin.camera.transform.position;
            Vector3 lookAtCameraPosition = new Vector3(cameraPosition.x, placementPose.position.y, cameraPosition.z);
            placementIndicator.transform.LookAt(lookAtCameraPosition);

            // Optionally, make the placementIndicator remain upright (e.g., prevent rotation around the y-axis)
            Vector3 indicatorEulerAngles = placementIndicator.transform.rotation.eulerAngles;
            placementIndicator.transform.rotation = Quaternion.Euler(0f, indicatorEulerAngles.y, 0f);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        var hits = new List<ARRaycastHit>();

        if (arOrigin.GetComponent<ARRaycastManager>() != null)
        {
            arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneEstimated);
        }

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            if (Camera.current != null) // Check if the Camera.current is not null before accessing it
            {
                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
    }
}