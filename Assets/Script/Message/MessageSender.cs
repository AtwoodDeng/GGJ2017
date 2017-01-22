using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSender : MonoBehaviour {

	[SerializeField] List<WaveScrollBar> buttonList = new List<WaveScrollBar>();

	[SerializeField] WordShower shower;

	public void Update()
	{
		for( int i = 0 ; i < 7  && i < buttonList.Count; ++ i )
		{
			shower.SetPosition( i , i );
			shower.SetValue( i , buttonList[i].GetValue());
		}


	}


	public void SendWaveMessage()
	{
		WaveMessage msg = new WaveMessage();
		msg.str = shower.GetWordResult();

		NetworkManager.SendWaveMessage( msg );
	}
}
