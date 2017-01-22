using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractDrag : Interactable {


	[SerializeField] LayerMask interactiveMask = -1;
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 oriPoint;
	private Quaternion oriRotation;
	protected GameObject touchObj;

	bool IsTouch = false;

	public override void MouseDown ()
	{
		base.MouseDown ();
		GetComponent<Rigidbody>().isKinematic = true;

		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		InteractManager.Instance.LockInteractable = true;
		oriPoint = transform.position;
		oriRotation = transform.rotation;

		transform.DOLookAt(  - Camera.main.transform.position + 2 * transform.position , 1f );
	}

	public override void MouseStay ()
	{
		base.MouseStay ();

		Ray mouseRay = Camera.main.ScreenPointToRay( Input.mousePosition );

		RaycastHit hit;
		if ( Physics.Raycast (mouseRay, out hit , 100f , interactiveMask.value))
		{
			if ( !IsTouch )
			{
				transform.DOKill();
				transform.DOLookAt( hit.point - hit.normal * 0.02f , 1f );
			}
			IsTouch = true;
			touchObj = hit.collider.gameObject;
			transform.position = hit.point + hit.normal * 0.02f;
		}
		else{
			if ( IsTouch )
			{
				transform.DOKill();
				transform.DOLookAt( - Camera.main.transform.position + 2 * transform.position , 1f );
			}
			IsTouch = false;
			touchObj = null;
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint + offset);
			transform.position = curPosition;
		}
	}

	public override void MouseUp ()
	{
		base.MouseUp ();
		GetComponent<Rigidbody>().isKinematic = false;
		InteractManager.Instance.LockInteractable = false;
		transform.DOKill();
		transform.position = oriPoint;
		transform.rotation = oriRotation;
		touchObj = null;
	}

}
