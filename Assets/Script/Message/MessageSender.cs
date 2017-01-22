using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSender : MonoBehaviour {

	[SerializeField] Transform agentTrans;
	[SerializeField] List<WaveScrollBar> agentButton = new List<WaveScrollBar>();
	[SerializeField] WordShower agentShower;
	[SerializeField] Transform locationTrans;
	[SerializeField] List<WaveScrollBar> locationButton = new List<WaveScrollBar>();
	[SerializeField] WordShower locationShower;

	public void Awake()
	{
		agentButton.AddRange( agentTrans.GetComponentsInChildren<WaveScrollBar>());
		if ( agentShower == null )
			agentShower = agentTrans.GetComponent<WordShower>();
		locationButton.AddRange( locationTrans.GetComponentsInChildren<WaveScrollBar>());
		if ( locationShower == null )
			locationShower = locationTrans.GetComponent<WordShower>();
	}

	public void Update()
	{
		for( int i = 0 ; i < WordShower.WORD_LENGTH && i < agentButton.Count; ++ i )
		{
			agentShower.SetPosition( i , i );
			agentShower.SetValue( i , agentButton[i].GetValue());
		}

		for( int i = 0 ; i < WordShower.WORD_LENGTH && i < locationButton.Count; ++ i )
		{
			locationShower.SetPosition( i , i );
			locationShower.SetValue( i , locationButton[i].GetValue());
		}
	}

	public void SendWaveMessage()
	{
		WaveMessage msg = new WaveMessage();
		msg.agent = agentShower.GetWordResult();
		msg.location = locationShower.GetWordResult();

		NetworkManager.SendWaveMessage( msg );
	}

	public void SetBase( string agent , string location )
	{
		agentShower.SetBase( agent );
		locationShower.SetBase( location);
	}

	public void Reset()
	{
		for( int i = 0 ; i < WordShower.WORD_LENGTH && i < agentButton.Count; ++ i )
		{
			agentButton[i].Reset();
		}
		agentShower.Reset();

		for( int i = 0 ; i < WordShower.WORD_LENGTH && i < locationButton.Count; ++ i )
		{
			locationButton[i].Reset();
		}
		locationShower.Reset();
		
	}
}
