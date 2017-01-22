using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Handle : InteractRollable {

	public UnityEvent SendFunc;

	public override void MouseStay ()
	{

		Vector3 temOff = Input.mousePosition - center;
		temOff.z = 0;

		//		float oriAngle = Mathf.Atan2( oriOffset.y , oriOffset.x ) * Mathf.Rad2Deg;
		//		float temAngle = Mathf.Atan2( temOff.y , temOff.x ) * Mathf.Rad2Deg;
		float angle = Vector3.Angle( oriOffset , temOff) ;
		if ( Vector3.Dot( Vector3.forward , Vector3.Cross( oriOffset , temOff )) > 0 )
		{
			angle = -angle;
		}

		value -= angle / YMax / 2f;
		value = Mathf.Clamp( value , -0.5f , 0.5f );

		Vector3 eular = transform.localEulerAngles;
		eular.x = value * YMax;
		transform.localEulerAngles = eular;

		oriOffset = temOff;
	}

	public override void MouseUp ()
	{
		base.MouseUp ();

	}

}
