using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Vector3 lastPosition;
	public Vector3 velocity;

	private GameController game;


	void Start()
	{
		//Copy the current position as the last one for initialization.
		lastPosition = new Vector3 (transform.position.x, transform.position.y , transform.position.z);

		GameObject gObj = GameObject.FindGameObjectWithTag ("GameController");
		game = gObj.GetComponent<GameController>();
	} 

	void Update()
	{
		//Update the velocity taking into account the current and the previous position.
		velocity = (transform.position - lastPosition) / Time.deltaTime;
		lastPosition = transform.position;
	}

	void FixedUpdate()
	{
		//Apply force according to the user input.
		float horAxis = Input.GetAxis ("Horizontal");
		float verAxis = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (horAxis, 0.0f, verAxis);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}


	void OnTriggerEnter(Collider other)
	{
		//If colliding with a pickup object, deactivate it.
		if (other.gameObject.tag == "PickUp") 
		{
			other.gameObject.SetActive(false);

			//Update the game about this collection
			game.ItemCollected();
		}
	}

}


