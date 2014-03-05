using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 10.0f;
	public float speed = 1.0f;
	public float jumpSpeed = 2.0f;


	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private SpawnControl SpawnControl;

	bool facingRight = true;

	/*
	private float syncDelay = 0f;
	private float lastSynchronizationTime = 0f;
	private Vector3 syncVelocity = Vector3.zero;

	Vector3 realPosition = Vector3.zero;
	Vector3 realVelocity = Vector3.zero;
	*/
	void Start () {

		if (isPlayer ()) {
			SpawnControl.SpawnPlayer ();
		}
	}

	void Update(){
		Debug.Log ("Kjlere soull");
		if (isPlayer()) {

			float direction = Input.GetAxis("Horizontal");
			rigidbody2D.velocity = new Vector2(direction * maxSpeed, rigidbody2D.velocity.y);

			if (Input.GetButtonDown ("Jump")) {
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpSpeed);
			}

			// endrer spillerverden
			if(direction > 0 && !facingRight ){
				flip ();
			}else if(direction < 0 && facingRight){
				flip ();
			}

		}else {

			//rigidbody2D.isKinematic = true;
			rigidbody2D.velocity = Vector3.zero;

			SyncedMovement();
		}
	}

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
		//transform.position = Vector3.Lerp (transform.position, realPosition, syncDelay ); //+ realVelocity * Time.deltaTime;

	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;

		if (stream.isWriting)
		{
			
			syncPosition = transform.position;
			stream.Serialize(ref syncPosition);

			syncVelocity = rigidbody2D.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);

			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;

			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = transform.position;
		}
	}

	private bool isPlayer() {
		return networkView.isMine;
	}

	//Skifter animasjonsretning når man endrer retning.
	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}