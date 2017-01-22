using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour {

	static NetworkManager s_Instance;
	public static NetworkManager Instance{ get { return s_Instance; }}
	public NetworkManager(){ s_Instance = this; }

	NetworkClient client;
	int port = 7777;

	string inputHost = "localhost";

	void InitializeServer()
	{
		NetworkServer.Listen(port);
		NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnect);

	}
	void InitializeClient()
	{
		client = new NetworkClient();
		client.RegisterHandler(MsgType.Connect, OnClientConnect);
		client.Connect(inputHost, port);
	}

	void OnClientConnect(NetworkMessage netMsg)
	{
		Debug.Log(string.Format("Client has connected to server " + netMsg.conn.hostId));
	}

	void OnServerConnect(NetworkMessage netMsg)
	{
		Debug.Log(string.Format("Client has connected to server with connection id: {0}", netMsg.conn.connectionId));
		NetworkServer.SetClientReady(netMsg.conn);
	}

	void OnGUI()
	{
		if ( !NetworkServer.active || client == null )
		{	
			GUILayout.Label("IP:" + Network.player.ipAddress);
			if (GUILayout.Button("Init Server"))
			{
				InitializeServer();
			}
			if (client == null || !client.isConnected)
			{
				inputHost = GUILayout.TextField(inputHost);
				if (GUILayout.Button("Client Connect"))
				{
					InitializeClient();
				}
			}
			
		}
	}

}
