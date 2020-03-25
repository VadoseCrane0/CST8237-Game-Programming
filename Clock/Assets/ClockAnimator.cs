using UnityEngine;
using System;
public class ClockAnimator : MonoBehaviour {
    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f,
        secondsToDegrees = 360f / 60f,
        milliToDegrees = 360f / 60000f;
    public Transform hours, minutes, seconds;
    private void Update() {
        DateTime time = DateTime.Now;
        
        //calcimications
        float 
            secondCalc = time.Second * -secondsToDegrees + time.Millisecond * -milliToDegrees,
            minuteCalc = time.Minute * -minutesToDegrees + secondCalc/60,
            hourCalc = time.Hour * -hoursToDegrees + minuteCalc/60;
        //end of calcimications




        hours.localRotation = Quaternion.Euler(0f, 0f, hourCalc);
        minutes.localRotation = Quaternion.Euler(0f, 0f, minuteCalc);
        seconds.localRotation = Quaternion.Euler(0f, 0f, secondCalc);
    }
}