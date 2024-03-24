using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    private DateTime startTime;

    private void OnEnable()
    {
        startTime = DateTime.Now;
    }

    private void Update()
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan timeDifference = currentTime - startTime;
        TimerText.text = timeDifference.ToString(@"m\:ss\:ff");
    }
}
