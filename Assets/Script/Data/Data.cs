using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Data : NetworkBehaviour {

	NetworkClient myClient;

	public class MyMsgType {
		public static short Msg = MsgType.Highest + 1;
	};

	public void SendMessage( int id )
	{
		WaveMessage msg = new WaveMessage();

		NetworkServer.SendToAll( MyMsgType.Msg , msg );
	}

	// Create a client and connect to the server port
	public void SetupClient()
	{
		myClient = new NetworkClient();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);
		myClient.RegisterHandler(MyMsgType.Msg, OnMessage);
		myClient.Connect("10.123.101.138", 0);
	}

	public void OnMessage(NetworkMessage netMsg)
	{
		WaveMessage msg = netMsg.ReadMessage<WaveMessage>();
//		Debug.Log("On Message " + msg.id);
	}

	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
	}

}

public class WaveMessage: MessageBase
{
	public int id;
	public string agent;
	public string location;

}

public class SetIDMessage: MessageBase
{
	public int id;
}

[CreateAssetMenu (fileName = "NarrativePlot", menuName = "Wave/NameList", order = 1)]
public class NameListSO : ScriptableObject {
	public List<string> names;
}

[CreateAssetMenu (fileName = "NarrativePlot", menuName = "Wave/NameList", order = 1)]
[System.Serializable]
public class TaskSO : ScriptableObject {
	public string Agent;
	public string Location;
}

public class Task
{
	public TaskSO taskData;

	public Task()
	{
		taskData = new TaskSO();
	}

}