using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver, restart;
    private int score;
    private int rainbowPicker = 0;
    private Color[] colors = { Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow, Color.red};
    public float colorDuration;
    private float time = 0;
    private int colorIndex=0;

    void Start() {

        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update() {
        if(restart) {
            if(Input.GetKeyDown(KeyCode.S)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if(gameOver) {
            if(colorIndex >= 6) {
                gameOverText.color = Color.Lerp(colors[6], colors[0], time);
            }else {
                gameOverText.color = Color.Lerp(colors[colorIndex], colors[colorIndex + 1], time);
            }
            if(time < 1) {
                time += Time.deltaTime / colorDuration;
            } else {
                time = 0;
            }
            if(colorIndex >= 6) {
                colorIndex = 0;
            }else if(gameOverText.color == colors[colorIndex + 1]) {
                colorIndex++;
            }
        }
        rainbowPicker++;
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        while(true) {
            for(int i = 0; i < hazardCount; i++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if(gameOver) {
                restartText.text = "Press 'S' to Restart";
                restart = true;
            }
        }
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver() {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }
}

