using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    float speed = .5f; void Update() {
        float offset = Time.time * speed;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, -offset);
    }
    public void SlowDown() {
        speed = speed / 2;
    }
    public void SpeedUp() {
        speed = speed * 2;
    }
}
