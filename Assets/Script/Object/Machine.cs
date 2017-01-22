using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Stickable {
	[SerializeField] MessageSender MessageSender;
	Paper temPaper;

	public override void Stick (GameObject obj)
	{
		base.Stick (obj);

		Paper paper = obj.GetComponent<Paper>();
		if ( paper != null )
		{
			MessageSender.SetBase( paper.Agent , paper.Location);
			temPaper = paper;

			LogicArg arg = new LogicArg(this);
			arg.AddMessage("paper" , paper );
			M_Event.FireLogicEvent( LogicEvents.StickPaper , arg );
		}
	}

	public void Reset()
	{
		if ( temPaper != null )
			temPaper.gameObject.SetActive(false);
		MessageSender.Reset();
	}

}
