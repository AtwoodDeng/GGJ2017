using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour {

	static LogicManager s_Instance;
	public static LogicManager Instance{ get { return s_Instance; }}
	public LogicManager(){ s_Instance = this; }

	[SerializeField] InputField inputField;
	[SerializeField] Text outField;

	public class MyMsgType {
		public static short Msg = MsgType.Highest + 1;
	};

	NetworkClient myClient;

	public void OnHost()
	{
	}

	public void OnClient()
	{
	}

	public void OnSend()
	{
		WaveMessage msg = new WaveMessage();
		msg.str = inputField.text;

		if ( myClient == null )
		{
			myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			myClient.RegisterHandler(MyMsgType.Msg, OnMessage);
			myClient.Connect("127.0.0.1", 4444);
		}

		NetworkServer.SendToAll( MyMsgType.Msg , msg );
	}

	public void OnMessage(NetworkMessage netMsg)
	{
		WaveMessage msg = netMsg.ReadMessage<WaveMessage>();
		outField.text = msg.str;
	}

	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
	}

	public void OnGUI()
	{
		GUILayout.Label( "My IP is " + Network.player.ipAddress + " " + Network.player.port );
		GUILayout.Label("Is server" + Network.isServer);
	}

}
