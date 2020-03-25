using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public GameControlScript control; void Update() {
        transform.Translate(0, 0, control.objectSpeed);
    }
}
