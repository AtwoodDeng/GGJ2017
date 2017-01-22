using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : Interactable {

	[SerializeField] LogicEvents eventType;
	[SerializeField] string name;
	[SerializeField] UnityEvent eventFunc;

	public override void MouseDown ()
	{
		base.MouseDown ();

		LogicArg arg = new LogicArg(this);
		arg.AddMessage("name" , name );

		M_Event.FireLogicEvent( eventType , arg ); 

		if ( eventFunc != null )
		{
			eventFunc.Invoke();
		}
	}

}
