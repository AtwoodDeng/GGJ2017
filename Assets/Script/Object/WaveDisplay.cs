using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDisplay : MonoBehaviour {


	[SerializeField] MessageSender sender;
	[SerializeField] LineWave lineWave;

	public bool showAgent;
	public bool showLocation;

	void Update()
	{
		
		if ( showAgent )
		{
			float[] buttonValue = sender.GetButtonValue( 0 );
			for( int i = 0 ; i < buttonValue.Length && i < lineWave.curvePower.Length; ++ i )
				lineWave.curvePower[i] = buttonValue[i] + 0.2f;
		}
		else if ( showLocation )
		{
			float[] buttonValue = sender.GetButtonValue( 1 );
			for( int i = 0 ; i < buttonValue.Length && i < lineWave.curvePower.Length; ++ i )
				lineWave.curvePower[i] = buttonValue[i] + 0.2f;
		}
	}
}
