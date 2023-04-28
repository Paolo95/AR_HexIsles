using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(requiredComponent: typeof(ARRaycastManager),
    requiredComponent2: typeof(ARPlaneManager))]
public class PlaceLevel : MonoBehaviour
{
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager arPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Pose pose;

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    
    
    
    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;

        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon) && !Manager.Current.isScenePlaced)
        {
            foreach (ARRaycastHit hit in hits)
            {
                pose = hit.pose;
                LevelController.SetObjectLevelPose(pose);
                Manager.Current.setScenePlaced(true);
            }

            foreach (var plane in arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

        }
    }

}

