using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour {
//	bool m_inInteractiveRange = false;

	public virtual bool IsInteractable()
	{
		return true;
	}

	public virtual Vector3 GetInteractCenter()
	{
		return transform.position;
	}

	public virtual void MouseDown()
	{
		
	}

	public virtual void MouseStay()
	{
	}

	public virtual void MouseUp()
	{
	}


//	 void OnTriggerEnter( Collider col )
//	{
//		if (col.gameObject.tag == "Player") {
//			OnPlayerEnter ();
//		}
//	}
//
//	 void OnTriggerExit( Collider col )
//	{
//		if (col.gameObject.tag == "Player") {
//			OnPlayerExit ();
//		}
//	}
//
//	virtual protected void OnPlayerEnter()
//	{
//		m_inInteractiveRange = true;
//	}
//
//	virtual protected void OnPlayerExit()
//	{
//		m_inInteractiveRange = false;
//	}

	public virtual void OnFocus()
	{
	}

	public virtual void OnOutOfFocus()
	{
		
	}
}
