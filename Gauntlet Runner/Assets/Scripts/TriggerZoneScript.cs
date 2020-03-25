using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
}
