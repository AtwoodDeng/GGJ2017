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
		msg.MsgID = CalculateSeconds();
		msg.agent = agentShower.GetWordResult();
		msg.location = locationShower.GetWordResult();
		msg.agentOri = agentShower.GetBase();
		msg.locationOri = agentShower.GetBase();

		NetworkManager.SendWaveMessage( msg );
	}

	public int CalculateSeconds()
	{
		System.DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);//from 1970/1/1 00:00:00 to now
		System.DateTime dtNow = System.DateTime.Now;
		System.TimeSpan result = dtNow.Subtract(dt);
		int seconds = System.Convert.ToInt32(result.TotalSeconds);
		return seconds;
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

	/// <summary>
	/// Gets the button value.
	/// </summary>
	/// <returns>The button value.</returns>
	/// <param name="index"> 0 for agent , 1 for location.</param>
	public float[] GetButtonValue( int index )
	{
		float[] res = new float[1];
		if ( index == 0 )
		{
			res = new float[agentButton.Count];	
			for( int i = 0 ; i < agentButton.Count ; ++ i  )
			{
				res[i] = agentButton[i].GetValue();
			}
		}else if ( index == 1 ) {
			res = new float[locationButton.Count];	
			for( int i = 0 ; i < locationButton.Count ; ++ i  )
			{
				res[i] = locationButton[i].GetValue();
			}
		}
		return res;
	}
}
