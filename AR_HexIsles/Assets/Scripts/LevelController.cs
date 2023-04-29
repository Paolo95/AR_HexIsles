using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LevelController : MonoBehaviour
{
    [HideInInspector] public static Pose objectLevelPose;
    [SerializeField] private static GameObject mapObject;
    private bool isSceneInitialized = false;

    private void Awake()
    {
        if (Manager.Current.isARLevel)
        {
            mapObject = GameObject.Find("Map");
            isSceneInitialized = false;
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

    public static void SetObjectLevelPose(Pose position)
    {
        objectLevelPose = position;
    }

}
