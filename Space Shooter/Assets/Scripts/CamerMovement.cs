using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamerMovement : MonoBehaviour
{
    public float speed;
    public Camera cam1;
    public Camera cam2;
    public GameObject player;
    private bool done;
     
    // Start is called before the first frame update
    void Start()
    {
        cam1.enabled = false;
        cam2.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!done) {

            cam2.transform.position = Vector3.Lerp(cam2.transform.position, cam1.transform.position, speed);
            cam2.transform.rotation = Quaternion.Lerp(cam2.transform.rotation, cam1.transform.rotation, speed);
           
            if(cam2.transform.position == cam1.transform.position) {
                cam2.enabled = false;
                cam1.enabled = true;
                done = true;
            }
        }
    }
}
