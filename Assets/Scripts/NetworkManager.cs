using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "Mohff tutorial game";
	private const string gameName = "Bathrom";
	private HostData[] hostList;
	private SpawnControl spawnControl;

	void Start () {
		spawnControl = GetComponent<SpawnControl> ();
	}

	void Update () {
	
	}

	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				startServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						joinServer(hostList[i]);
				}
			}
		}
	}

	private void startServer()
	{
		Network.InitializeServer(4, 25001, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
		spawnControl.SpawnPlayer();
	}

	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void joinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		spawnControl.SpawnPlayer();
	}


	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("This SERVER OR CLIENT has disconnected from a server");
	}
	
	void OnFailedToConnect(NetworkConnectionError error){
		Debug.Log("Could not connect to server: "+ error);
	}

	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from: " + player.ipAddress +":" + player.port);
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Player disconnected from: " + player.ipAddress+":" + player.port);
	}
	
	
	void OnFailedToConnectToMasterServer(NetworkConnectionError info){
		Debug.Log("Could not connect to master server: "+ info);
	}
	
	void OnNetworkInstantiate (NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}
}
