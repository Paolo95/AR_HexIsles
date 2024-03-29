﻿using UnityEngine;

public class Deactivator : MonoBehaviour
{
    public void Deactivate()
    {
        // Deactivate UI after fade out animations
        gameObject.SetActive(false);
        
        // Show sign tutorial on first level load
        if (Manager.Current.CompletedLevels == 0 && Manager.Current.LevelIndex == 1 &&
            gameObject.name == "MainMenuScreen")
        {
            if (Manager.Current.isARLevel)
            {
                Manager.Current.DialogBox.ShowDialog(Config.Current.StartUpDialogAR);
            }
            else
            {
                Manager.Current.DialogBox.ShowDialog(Config.Current.StartUpDialog);
            }
            
        }
            
    }
}
