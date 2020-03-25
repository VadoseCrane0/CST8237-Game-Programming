using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlScript : MonoBehaviour
{
    public float objectSpeed = -.3f;
    float minSpeed = -.15f, maxSpeed = -.3f , timeRemaining = 10, timeExtension = 1.5f, totalTimeElapsed = 0;
    public GroundScript ground, wall1, wall2;
    bool isGameOver = false;

    void Update() {
        if(isGameOver)
            return;
        totalTimeElapsed += Time.deltaTime;
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
            isGameOver = true;
    }

    public void SlowWorldDown() {
        CancelInvoke("SpeedWorldUp");
        objectSpeed = minSpeed;
        ground.SlowDown();
        wall1.SlowDown();
        wall2.SlowDown();
        Invoke("SpeedWorldUp", 1);
    }

    void SpeedWorldUp() {
        objectSpeed = maxSpeed;
        ground.SpeedUp();
        wall1.SpeedUp();
        wall2.SpeedUp();
    }

    public void PowerupCollected() {
        timeRemaining += timeExtension;
    }

    void OnGUI() {
        if(!isGameOver) {
            GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 50), "Time Remaining");
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height - 80, 20, 40), ((int)timeRemaining).ToString());
        } else {
            GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 100, 120, 50), "Game Over");
            GUI.Label(new Rect(Screen.width / 2 - 55, Screen.height / 2 - 80, 90, 40), "Total Time: " + (int)totalTimeElapsed);
        }
    }
}
