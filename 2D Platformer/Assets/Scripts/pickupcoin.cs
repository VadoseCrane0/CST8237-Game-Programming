using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pickupcoin : MonoBehaviour {

    private GameController gameController;


    void OnTriggerEnter2D(Collider2D other) {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController == null) {
            Debug.Log("Cannot find 'GameController' script");
        }
        if(other.gameObject.CompareTag("Player")) {
            Destroy(this.gameObject);
            Debug.Log("Coin was pickedup");
            gameController.AddScore(1);
        }

    }
}
