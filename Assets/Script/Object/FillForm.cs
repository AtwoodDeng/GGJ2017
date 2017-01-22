using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FillForm : MonoBehaviour {

	[SerializeField] TextMesh agentText;
	[SerializeField] TextMesh locationText;

	[SerializeField] Transform finalTrans;
	[SerializeField] Transform oriTrans;

	Paper m_paper;

	void Start()
	{
		Reset();
	}

	void OnEnable()
	{
		M_Event.RegisterEvent(LogicEvents.SelectAgentName , OnSelectAgentName);
		M_Event.RegisterEvent(LogicEvents.SelectLocation , OnSelectLocation);
		M_Event.RegisterEvent(LogicEvents.StickPaper , OnStickPaper);
	}

	void OnDisable()
	{
		M_Event.UnregisterEvent(LogicEvents.SelectAgentName , OnSelectAgentName);
		M_Event.UnregisterEvent(LogicEvents.SelectLocation , OnSelectLocation);
		M_Event.UnregisterEvent(LogicEvents.StickPaper , OnStickPaper);
	}

	void OnSelectAgentName( LogicArg arg )
	{
		string name = (string)arg.GetMessage("name");
		agentText.text = name;

	}
	void OnSelectLocation( LogicArg arg )
	{
		string name = (string)arg.GetMessage("name");
		locationText.text = name;
	}

	void OnStickPaper( LogicArg arg  )
	{
		m_paper = arg.GetMessage("paper") as Paper;
		if ( m_paper.type == Paper.Type.Decode )
		{
			Reset();
			transform.DOKill();
			transform.DOMove( finalTrans.position , 1f );
			transform.DORotate( finalTrans.rotation.eulerAngles , 1f );
		}
	}

	void Reset()
	{
		agentText.text = "*******";
		locationText.text = "*******";

		transform.DOKill();
		transform.DOMove( oriTrans.position , 0f );
		transform.DORotate( oriTrans.rotation.eulerAngles , 0f );
	}

	public void OnConfirm()
	{

		transform.DOKill();
		transform.DOMove( oriTrans.position , 1f );
		transform.DORotate( oriTrans.rotation.eulerAngles , 1f );

		DetectMessage msg = new DetectMessage();
		msg.MsgID = m_paper.m_ID;
		msg.agent = agentText.text;
		msg.location = locationText.text;
		NetworkManager.Instance.DetectMessageClient( msg );
	}
}
