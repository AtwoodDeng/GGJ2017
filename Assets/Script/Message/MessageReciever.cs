using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageReciever : MonoBehaviour {
	[SerializeField] List<WaveScrollBar> buttonList = new List<WaveScrollBar>();

	[SerializeField] WordShower shower;

	public void Update()
	{
		for( int i = 0 ; i < 7 && i < buttonList.Count; ++ i )
		{
			shower.SetPosition( i , i);
			shower.SetValue( i , buttonList[i].GetValue());
		}

	}

	void OnRecieveWaveMessage( LogicArg arg )
	{
		WaveMessage msg = arg.GetMessage("Message" ) as WaveMessage;
		shower.SetBase(msg.str);
	}

	void OnEnable() {
		M_Event.RegisterEvent(LogicEvents.RecieveWaveMessage, OnRecieveWaveMessage);
	}

	void OnDisable() {
		M_Event.UnregisterEvent(LogicEvents.RecieveWaveMessage, OnRecieveWaveMessage);
	}
}
