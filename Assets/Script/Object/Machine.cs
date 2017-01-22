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
		MessageSender.SetBase( paper.m_task.taskData.Agent , paper.m_task.taskData.Location);
		temPaper = paper;
	}

	public void Reset()
	{
		if ( temPaper != null )
			temPaper.gameObject.SetActive(false);
		MessageSender.Reset();
	}

}
