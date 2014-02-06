#pragma strict

function Update () {

	reset();
}

function reset(){
	
	
	if(transform.position.y < -4){
		transform.position.x = 0;
		transform.position.y = 2;
		transform.position.z = 0;
		StartCoroutine(wait());
	}
}

function wait(){
	yield WaitForSeconds(2 * Time.deltaTime);
}