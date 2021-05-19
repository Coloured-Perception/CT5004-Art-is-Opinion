using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isEyeTrackerUsed : MonoBehaviour
{
    public bool isEyeTracker;

    private void Update()
    {
        if (PlayerPrefs.HasKey("isEyeTracker"))
        {
            isEyeTracker = PlayerPrefs.GetInt("isEyeTracker") == 1 ? true : false;
        }
        else
        {
            PlayerPrefs.SetInt("isEyeTracker", 0);
            isEyeTracker = false;
        }
    }
}
