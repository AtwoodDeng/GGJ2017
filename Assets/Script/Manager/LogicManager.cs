using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour {

	static LogicManager s_Instance;
	public static LogicManager Instance{ get { return s_Instance; }}
	public LogicManager(){ s_Instance = this; }

	[SerializeField] NameListSO AgentNameList;
	[SerializeField] NameListSO LocationNameList;
	[SerializeField] GameObject paperPrefab;
	[SerializeField] Transform sendTransform;
	[SerializeField] Transform getTransform;
	[SerializeField] MessageSender msgSender;

	void OnRecieveWaveMessage( LogicArg arg )
	{
		WaveMessage msg = arg.GetMessage("Message" ) as WaveMessage;
		RecieveTask( msg.agent , msg.location, msg.MsgID);
	}

	void OnEnable() {
		M_Event.RegisterEvent(LogicEvents.RecieveWaveMessage, OnRecieveWaveMessage);
	}

	void OnDisable() {
		M_Event.UnregisterEvent(LogicEvents.RecieveWaveMessage, OnRecieveWaveMessage);
	}

	void Start()
	{
		StartCoroutine( TaskGenerate());
	}

	IEnumerator TaskGenerate()
	{
		while( true )
		{
			CreateTask();
			yield return new WaitForSeconds( 30f );
		}
	}

	public void CreateTask()
	{
		GameObject paper = Instantiate( paperPrefab ) as GameObject;
		paper.transform.position = sendTransform.position;

		Task t = new Task();
		Debug.Log(AgentNameList.names.Count);
		t.taskData.Agent = AgentNameList.names[ Random.Range( 0 , AgentNameList.names.Count) ];
		t.taskData.Location = LocationNameList.names[ Random.Range( 0 , LocationNameList.names.Count) ];
		Paper paperCom = paper.GetComponent<Paper>();
		if ( paperCom != null )
			paperCom.Init(t,Paper.Type.Encode,0);
	}

	public void RecieveTask( string agent , string location , int msgID)
	{
		GameObject paper = Instantiate( paperPrefab ) as GameObject;
		paper.transform.position = getTransform.position;

		Task t = new Task();
		t.taskData.Agent = AgentNameList.names[ Random.Range( 0 , AgentNameList.names.Count) ];
		t.taskData.Location = LocationNameList.names[ Random.Range( 0 , LocationNameList.names.Count) ];
		Paper paperCom = paper.GetComponent<Paper>();
		if ( paperCom != null )
			paperCom.Init(t,Paper.Type.Decode,msgID);
	}


}
