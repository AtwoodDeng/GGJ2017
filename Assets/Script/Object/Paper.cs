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

	public override void MouseDown ()
	{
		base.MouseDown ();
//		gameObject.layer = LayerMask.NameToLayer("Focus");
//		foreach(Transform trans in GetComponentsInChildren<Transform>())
//			trans.gameObject.layer = LayerMask.NameToLayer("Focus");
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
				ifStick = true;

				GetComponent<Rigidbody>().isKinematic = true;
				InteractManager.Instance.LockInteractable = false;
				touchObj = null;
			}
		}

		if ( !ifStick )
		{
			base.MouseUp ();
		}
//		gameObject.layer = LayerMask.NameToLayer("Paper");
//		foreach(Transform trans in GetComponentsInChildren<Transform>())
//			trans.gameObject.layer = LayerMask.NameToLayer("Paper");
	}
}
