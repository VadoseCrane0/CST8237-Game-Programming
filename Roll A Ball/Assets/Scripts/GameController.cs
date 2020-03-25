using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//GUI
	public Text scoreText;
	public Text winText;
	public Text playerPosition;
	public Text playerVelocity;
	public Text distToClosest;

	//Game State
	private int count;
	private int numPickups = 4;

	//Pickups
	public GameObject[] pickups;

	//Player
	public PlayerController player;

	//LineRenderers
	private LineRenderer lineRenderer;
	private GameObject closest;
	private GameObject aligned;

	//State:
	public enum DebugState {NORMAL, DISTANCE, VISION};
	DebugState state;

	// Use this for initialization
	void Start () {
		count = 0;
		winText.text = "";
		playerPosition.text = "";
		playerVelocity.text = "";
		distToClosest.text = "";
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.enabled = false;
		SetCountText ();
		closest = null;
		aligned = null;
		state = DebugState.NORMAL;
	}

	public void ItemCollected()
	{
		count++;
		SetCountText ();
	}

	void Update()
	{
		bool down = Input.GetKeyDown(KeyCode.Space);
		if (down) 
		{
			if(state == DebugState.VISION) state = DebugState.NORMAL;
			else if(state == DebugState.DISTANCE) state = DebugState.VISION;
			else if(state == DebugState.NORMAL) state = DebugState.DISTANCE;

			if(state != DebugState.NORMAL) lineRenderer.enabled = true;
		}
	}


	// Update is called once per frame
	void LateUpdate () {
		//Position, velocity, etc printed at every cycle. Done in LateUpdate so all positions and velocities are updated.
		UpdateDebugInformation ();
	}


	//Sets the count of items collected and the victory text.
	private void SetCountText()
	{
		scoreText.text = "Score: " + count.ToString ();
		if (count >= numPickups) {
			winText.text = "You Win!";
		}
	}

	private float studyPickups()
	{
		Vector3 playerPos = player.transform.position;
		Vector3 playerVel = new Vector3(player.velocity.x, player.velocity.y, player.velocity.z);
		playerVel.Normalize();

		float minDistance = float.MaxValue;
		float minDotProduct = -10.0f;


		for(int i = 0; i < pickups.Length; ++i)
		{
			GameObject p = pickups[i];	
			if(p.activeSelf)
			{
				p.GetComponent<Renderer>().material.color = Color.white;

				//for the distance
				Vector3 playerToPickup = (p.transform.position - playerPos);
				float dist = playerToPickup.magnitude;
				if(dist < minDistance)
				{
					closest = p;
					minDistance = dist;
				}

				//for the most aligned with the players velocity
				playerToPickup.Normalize();
				float dotProduct = Vector3.Dot(playerVel, playerToPickup);

				if(playerVel.magnitude > 0.0 && dotProduct > minDotProduct)
				{
					minDotProduct = dotProduct;
					aligned = p;
				}
			}

		}
		return minDistance;
	}


	//Updates the debug information.
	private void UpdateDebugInformation()
	{

		float dist = studyPickups ();

		if (state != DebugState.NORMAL) {
			//POSITION
			float xPos = player.transform.position.x;
			float zPos = player.transform.position.z;
			playerPosition.text = "Position: " + xPos.ToString ("0.00") + "," + zPos.ToString ("0.00");

			//VELOCITY, SPEED AND LINERENDERER2
			float xVel = player.velocity.x;
			float zVel = player.velocity.z;
			float speed = player.velocity.magnitude;
			playerVelocity.text = "Velocity: " + xVel.ToString ("0.00") + "," + zVel.ToString ("0.00") + ", Speed: " + speed.ToString ("0.00");
		}

		if (state == DebugState.DISTANCE) {	
			//CALCULATIONS TO CLOSEST PICKUP
			if (closest != null) {
				closest.GetComponent<Renderer>().material.color = Color.blue;
				lineRenderer.SetPosition (0, player.transform.position); 
				lineRenderer.SetPosition (1, closest.transform.position); 
 
				distToClosest.text = "Distance to closest: " + dist.ToString ("0.00");
			} else {
				lineRenderer.enabled = false;
				distToClosest.text = "Distance to closest: N/A";
			}
		
		} else if (state == DebugState.VISION) 
		{
			if(aligned != null)
			{
				aligned.GetComponent<Renderer>().material.color = Color.green;
				lineRenderer.SetPosition (0, player.transform.position); 
				lineRenderer.SetPosition (1, player.transform.position + player.velocity * 1.0f); 
				lineRenderer.SetWidth (0.1f, 0.1f);

				aligned.transform.LookAt(player.transform, Vector3.up);
			}
		}
	}
}