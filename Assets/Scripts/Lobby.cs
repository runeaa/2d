using UnityEngine;
using System.Collections;

public class Lobby : MonoBehaviour {

	private NetworkManager networkManager;
	private Vector2 scrollPosition = Vector2.zero;

	void Start () {
		 //networkManager = GetComponent<NetworkManager> ();
	}

	void OnGUI() {

		if (GUI.Button (new Rect (20, 20, 180, 20), "Create Game"))
			startGame ();

		if (GUI.Button (new Rect (220, 20, 180, 20), "Refresh"))
			refresh ();

		Rect client = new Rect (20, 60, Screen.width - 40, Screen.height - 100);
		Rect source = new Rect (0, 0, Screen.width - 60, 100 * 30);

		scrollPosition = GUI.BeginScrollView (client,
		                                     scrollPosition,
		                                     source);

		for (int i = 0; i < 100; i++) {
			if (GUI.Button (new Rect (0, 30 * i, Screen.width - 100, 20), "Game_: " + i))
				joinGame (i);
		}

		GUI.EndScrollView ();
 	}

	private void startGame() {
		// TODO: start game
	}

	private void refresh() {
		// TODO: refresh
	}

	private void joinGame(int id) {
		// TODO: join game
	}
}
