using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : SingletonMonoBehaviour<LevelController>
{
    public Pose objectLevelPose;
    [SerializeField] private GameObject mapObject;
    private void Update()
    {
        if (Manager.Current.isARLevel && !Manager.Current.isScenePlaced)
        {
            mapObject.SetActive(false);
            
        }else if (Manager.Current.isARLevel && Manager.Current.isScenePlaced)
        {
            mapObject.SetActive(true);
            mapObject.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            mapObject.transform.position = objectLevelPose.position;
        }
        else
        {
            mapObject.SetActive(true);
        }
    }

    public void SetObjectLevelPose(Pose position)
    {
        objectLevelPose = position;
    }

    public Pose GetObjectLevelPose()
    {
        return objectLevelPose;
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
