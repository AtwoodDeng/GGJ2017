using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTest : MonoBehaviour {

		
	NetworkClient client;
	short msgID = 888;

	void InitializeClient()
	{
		client = new NetworkClient();
		client.RegisterHandler(MsgType.Connect, OnClientConnect);
		client.RegisterHandler(msgID, RecieveMsg);

		client.Connect(inputHost, 7777);
	}

	void InitializeServer()
	{
		NetworkServer.Listen(7777);

		NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnect);
		NetworkServer.RegisterHandler(msgID, RecieveServerMsg);
		
	}

	void OnServerConnect(NetworkMessage netMsg)
	{
		Debug.Log(string.Format("Client has connected to server with connection id: {0}", netMsg.conn.connectionId));

		NetworkServer.SetClientReady(netMsg.conn);
	}

	void OnClientConnect(NetworkMessage netMsg)
	{
		Debug.Log(string.Format("Client has connected to server"));
	}

	void SendWaveMsg()
	{
		WaveMessage msg = new WaveMessage();
		msg.str = sendMsg;
		client.Send( msgID , msg);
		Debug.Log("Send " + sendMsg);
	}

	public void RecieveServerMsg( NetworkMessage netMsg )
	{
		Debug.Log("Get msg in server ");
		WaveMessage msg = netMsg.ReadMessage<WaveMessage>();
		NetworkServer.SendToAll(msgID,msg);
	}

	public void RecieveMsg(NetworkMessage netMsg)
	{
		WaveMessage msg = netMsg.ReadMessage<WaveMessage>();
		if ( msg.str != sendMsg )
			recieveMsg = msg.str;
		//		Debug.Log("On Message " + msg.id);
	}

	string inputHost = "localhost";
	string sendMsg = "write something";
	string recieveMsg = "what I get";

	void OnGUI()
	{
		GUILayout.Label("IP:"+Network.player.ipAddress);
		if (!NetworkServer.active)
		{
			if (GUILayout.Button("Init Server"))
			{
				InitializeServer();
			}
		}

		if (client == null || !client.isConnected)
		{
			if (GUILayout.Button("Client Connect"))
			{
				InitializeClient();
			}
			inputHost = GUILayout.TextField(inputHost);
		}
		if ( client != null && client.isConnected)
		{
			sendMsg = GUILayout.TextField(sendMsg);
			if ( GUILayout.Button("Send"))
			{
				SendWaveMsg();	
			}
		}

		GUILayout.Label(recieveMsg);
	}
}

