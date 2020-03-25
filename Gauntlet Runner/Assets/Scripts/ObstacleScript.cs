using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public GameControlScript control; void Update() {
        transform.Translate(0, 0, control.objectSpeed);
    }
}
