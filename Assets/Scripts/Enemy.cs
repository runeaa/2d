using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int lifeLoss = 1;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision hit){
		if(hit.gameObject.tag == "Player"){
			Debug.Log("Spilleren mister liv");
		}
	}
}
	
