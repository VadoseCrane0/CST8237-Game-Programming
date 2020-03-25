using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    GameControlScript control;
    public GameObject obstacle, powerup;
    float timeElapsed = 0, spawnCycle = .5f;
    bool spawnPowerup = true;

    void Start() {
        control = GetComponent<GameControlScript>();

    }

    void Update() {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > spawnCycle) {
            GameObject temp;
            if(spawnPowerup) {
                temp = (GameObject)Instantiate(powerup);
                temp.GetComponent<PowerupScript>().control = control;
                Vector3 pos = temp.transform.position;
                temp.transform.position = new Vector3(Random.Range(-3, 4), pos.y, pos.z);
            } else {
                temp = (GameObject)Instantiate(obstacle);
                temp.GetComponent<ObstacleScript>().control = control;
                Vector3 pos = temp.transform.position;
                temp.transform.position = new Vector3(Random.Range(-3, 4), pos.y, pos.z);
            }
            timeElapsed -= spawnCycle;
            spawnPowerup = !spawnPowerup;
        }
    }
}
