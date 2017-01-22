using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : InteractDrag {

	[SerializeField] TextMesh agentText;
	[SerializeField] TextMesh locationText;
	public Task m_task;
	public enum Type
	{
		Encode,
		Decode,
	}
	public Type type;

	public void Init( Task task , Type type )
	{
		agentText.text = task.taskData.Agent;
		locationText.text = task.taskData.Location;
		m_task = task;
	}

	public override void MouseUp ()
	{
		bool ifStick = false;
		if ( touchObj != null )
		{
			Stickable stick = touchObj.GetComponent<Stickable>();
			if ( stick != null )
			{
				stick.Stick( gameObject );
				GetComponent<Rigidbody>().isKinematic = true;
				ifStick = true;
				InteractManager.Instance.LockInteractable = false;
			}
		}

		if ( !ifStick )
		{
			base.MouseUp ();
		}
	}
}
