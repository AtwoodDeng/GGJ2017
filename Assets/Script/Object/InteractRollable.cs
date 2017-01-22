using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRollable : Interactable {

	protected Vector3 oriOffset;
	protected Vector3 center;
	[SerializeField] protected float YMax = 180f;
	[SerializeField] protected float value = 0;

	protected Vector3 oriLocalEular;

	void Start()
	{
		oriLocalEular = transform.localEulerAngles;
	}

	public override void MouseDown ()
	{
		base.MouseDown ();
		InteractManager.Instance.LockInteractable = true;

		center = Camera.main.WorldToScreenPoint( transform.position );
		oriOffset = Input.mousePosition - center;
		oriOffset.z = 0;
	}

	public override void MouseStay ()
	{
		base.MouseStay ();

		Vector3 temOff = Input.mousePosition - center;
		temOff.z = 0;

//		float oriAngle = Mathf.Atan2( oriOffset.y , oriOffset.x ) * Mathf.Rad2Deg;
//		float temAngle = Mathf.Atan2( temOff.y , temOff.x ) * Mathf.Rad2Deg;
		float angle = Vector3.Angle( oriOffset , temOff) ;
		if ( Vector3.Dot( Vector3.forward , Vector3.Cross( oriOffset , temOff )) > 0 )
		{
			angle = -angle;
		}
			
		value += angle / YMax / 2f;
		value = Mathf.Clamp( value , -0.5f , 0.5f );

		Vector3 eular = transform.localEulerAngles;
		eular.y = value * YMax;
		transform.localEulerAngles = eular;

		oriOffset = temOff;
	}

	public override void MouseUp ()
	{
		base.MouseUp ();
		InteractManager.Instance.LockInteractable = false;
	}

	virtual public void Reset()
	{
		value = 0;
		transform.localEulerAngles = oriLocalEular;
	}

}
