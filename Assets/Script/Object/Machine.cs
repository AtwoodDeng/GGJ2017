using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Stickable {
	[SerializeField] MessageSender MessageSender;

	public override void Stick (GameObject obj)
	{
		base.Stick (obj);

		Paper paper = obj.GetComponent<Paper>();
		MessageSender.SetBase( paper.m_task.taskData.Agent , paper.m_task.taskData.Location);

	}
}
