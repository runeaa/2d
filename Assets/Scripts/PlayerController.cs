using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 10.0f;
	public float speed = 1.0f;
	public float jumpSpeed = 2.0f;

	//private Vector2 walk = Vector2.zero;
	//public Vector2 movementVector = Vector2.zero;
	private bool grounded = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (networkView.isMine)
		{
			float move = Input.GetAxis("Horizontal");
			
			rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		}
	}

	void Update(){

		if(grounded){
			Debug.Log ("LOL");
		}

		if (Input.GetButtonDown ("Jump")) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
		}
	}

	void onCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag("this is your tags name")){ //collision.gameObject.tag == "Ground"
			Debug.Log ("Collision");	
		}
		Debug.Log (collision.ToString());
		grounded = true;
		Debug.Log ("Collision");
	}

	void onCollisionExit(){
		grounded = false;
		Debug.Log ("Collision");
	}
}