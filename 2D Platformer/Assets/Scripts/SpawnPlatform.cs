using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public int maxPlatforms = 20;

    public GameObject platform;

    private float horizontalMin;
    private float horizontalMax;
    private float verticalMin;
    private float verticalMax;

    private Vector2 originPosition;

    void Start() {
        verticalMax = Random.Range(6, 20);
        verticalMin = -verticalMax;
        horizontalMax = Random.Range(1, 20);
        horizontalMin = Random.Range(1, 20);
        originPosition = transform.position;
        Spawn();
    }

    void Spawn() {
        for(int i = 0; i < maxPlatforms; i++) {
            Vector2 randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax));
            Instantiate(platform, randomPosition, Quaternion.identity);
            originPosition = randomPosition;
        }
    }
}
