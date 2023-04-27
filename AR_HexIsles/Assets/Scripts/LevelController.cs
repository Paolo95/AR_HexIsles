using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LevelController : SingletonMonoBehaviour<LevelController>
{
    [HideInInspector] public Pose objectLevelPose;
    [SerializeField] private GameObject mapObject;
    private bool isSceneInitialized = false;

    private void Awake()
    {
        if (Manager.Current.isARLevel)
        {
            isSceneInitialized = false;
        }
        
        if (objectLevelPose.position == Vector3.zero && !isSceneInitialized && Manager.Current.isScenePlaced && Manager.Current.isARLevel)
        {
           mapObject.SetActive(false);
           objectLevelPose = PlaceLevel.Current.GetCurrentPose();
        }
        
    }

    private void Update()
    {
        if (Manager.Current.isARLevel && !Manager.Current.isScenePlaced)
        {
            mapObject.SetActive(false);
            
        }else if (Manager.Current.isARLevel && Manager.Current.isScenePlaced && !isSceneInitialized)
        {
            mapObject.transform.position = objectLevelPose.position;
            mapObject.SetActive(true);
            isSceneInitialized = true;
        }
    }

    public void SetObjectLevelPose(Pose position)
    {
        objectLevelPose = position;
    }

    public void SetMapActive()
    {
        mapObject.SetActive(true);
    }
    
    public void SetMapInactive()
    {
        mapObject.SetActive(false);
    }
}
