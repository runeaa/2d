using UnityEngine;
using System.Collections;

public class SpawnControl : MonoBehaviour {

	public GameObject playerPrefab;

	void Start () {
	
	}

	void Update () {
	
	}

	public void SpawnPlayer()
	{
		Debug.Log ("Spawner spilleren");
		Network.Instantiate(playerPrefab, new Vector3(1f, 0.5f, 10f), Quaternion.identity, 0);
	}
}