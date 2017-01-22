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

	static short RECIEVE_MSG = 200;
	static short SEND_MSG = 201;
	static short SETUP_ID = 250;


	string inputHost = "localhost";

	#region Server
	void InitializeServer()
	{
		NetworkServer.Listen(port);
		NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnect);
		NetworkServer.RegisterHandler(SEND_MSG, OnSendMessage);
	}

	void OnServerConnect(NetworkMessage netMsg)
	{
		Debug.Log(string.Format("Client has connected to server with connection id: {0}", netMsg.conn.connectionId));
		NetworkServer.SetClientReady(netMsg.conn);
		SetIDMessage msg = new SetIDMessage();
		msg.id = netMsg.conn.connectionId;
		NetworkServer.SendToClient(netMsg.conn.connectionId, SETUP_ID , msg);
	}

	void OnSendMessage( NetworkMessage netMsg )
	{
		WaveMessage msg = netMsg.ReadMessage<WaveMessage>();
		NetworkServer.SendToAll( RECIEVE_MSG , msg);
	}
	#endregion

	#region Client
	int clientID = -1;
	void InitializeClient()
	{
		client = new NetworkClient();
		client.RegisterHandler(MsgType.Connect, OnClientConnect);
		client.RegisterHandler( SETUP_ID, OnSetUpID);
		client.RegisterHandler( RECIEVE_MSG , OnRecieveMessage);
		client.Connect(inputHost, port);
	}

	public void OnSetUpID(NetworkMessage netMsg)
	{
		SetIDMessage msg = netMsg.ReadMessage<SetIDMessage>();
		if ( clientID < 0 )
			clientID = msg.id;
	}

	void OnClientConnect(NetworkMessage netMsg)
	{
//		Debug.Log(string.Format("Client has connected to server {0} , and my id is {1}",  netMsg.conn.hostId , netMsg.conn.connectionId ));
	}


	public void OnRecieveMessage( NetworkMessage netMsg)
	{
		WaveMessage msg = netMsg.ReadMessage<WaveMessage>();
		Debug.Log(string.Format("Recieve Message id {0} str {1}" , msg.id , msg.str));
		if ( msg.id != clientID ) {
			LogicArg arg = new LogicArg(this);
			arg.AddMessage("Message" , msg );
			M_Event.FireLogicEvent( LogicEvents.RecieveWaveMessage, arg );
		}
	}

	static public void SendWaveMessage( WaveMessage msg )
	{
		NetworkManager.Instance.SendWaveMessageClient( msg );
	}

	public void SendWaveMessageClient( WaveMessage msg )
	{
		msg.id = clientID;
		Instance.client.Send(SEND_MSG , msg );
		Debug.Log("Send Message" + msg.str);
	}

	#endregion

	void OnGUI()
	{
		if ( client == null )
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
			
		}else{
			GUILayout.Label("ClientID " + clientID);
		}
	}

}
