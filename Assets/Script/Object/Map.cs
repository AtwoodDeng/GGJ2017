using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Map : InteractDrag {

	public enum State
	{
		Down,
		InHand,
	}
	public State m_state;

	[SerializeField] Transform finalTrans;
	[SerializeField] Transform oriTrans;



	public override void MouseDown ()
	{
		if ( m_state == State.Down )
		{
			
		}else if ( m_state == State.InHand )
		{
			base.MouseDown ();
		}
	}

	public override void MouseStay ()
	{
		if ( m_state == State.InHand )
		{
			base.MouseStay ();
		}
	}

	public override void MouseUp ()
	{
		if ( m_state == State.Down ) {
			m_state = State.InHand;
			transform.DOKill();
			transform.DOMove( finalTrans.position , 1f );
			transform.DORotate( finalTrans.rotation.eulerAngles , 1f );

		}else if ( m_state == State.InHand ) {

			InteractManager.Instance.LockInteractable = false;
			touchObj = null;
			GetComponent<Rigidbody>().isKinematic = true;
			
		}
	}

	public override void RightMouseUp ()
	{
		base.RightMouseUp ();
		Reset();
	}

	public void Reset()
	{
		transform.DOKill();
		transform.DOMove( oriTrans.position , 1f );
		transform.DORotate( oriTrans.rotation.eulerAngles , 1f );
		m_state = State.Down;

	}

}
