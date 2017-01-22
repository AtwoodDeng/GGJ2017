using UnityEngine;
using System.Collections;

public class InteractManager : MBehavior{

	static private InteractManager m_Instance;
	static public InteractManager Instance{
		get {
			if (m_Instance == null)
				m_Instance = FindObjectOfType<InteractManager> ();
			return m_Instance;
		}
	}

	[SerializeField] LayerMask interactiveMask = -1;
	public Vector3 TouchPoint;
	public bool LockInteractable = false;

	public Interactable s_Interactable;
	Interactable tem_Interactable
	{
		get{ return s_Interactable; }
		set{
			if (s_Interactable != value) {
				if (s_Interactable != null)
					s_Interactable.OnOutOfFocus ();
				s_Interactable = value;
				if (s_Interactable != null)
					s_Interactable.OnFocus ();
			}
		}
	}
	public Interactable TempInteractable
	{
		get { return tem_Interactable; }
	}

	/// <summary>
	/// return the focuse point in screen position
	/// </summary>
	/// <returns>The point.</returns>
	static public Vector2 FocusPoint
	{
		get{
			if ( Application.platform == RuntimePlatform.WindowsPlayer || 
				Application.platform == RuntimePlatform.OSXPlayer || 
				Application.platform == RuntimePlatform.LinuxPlayer )
				return Input.mousePosition;
			return Input.mousePosition;
		}
	}

	protected override void MAwake ()
	{
		base.MAwake ();

	}

	protected override void MUpdate ()
	{
		base.MUpdate ();
		UpdateTemInteractable ();
		UpdateTemInteract();
	}

	void UpdateTemInteract()
	{
		if ( tem_Interactable != null )
		{
			if ( Input.GetMouseButtonDown(0))
			{
				tem_Interactable.MouseDown();	
			}
			else if ( Input.GetMouseButton( 0 ))
			{
				tem_Interactable.MouseStay();
			}
			else if ( Input.GetMouseButtonUp(0))
			{
				tem_Interactable.MouseUp();
			}
		}
	}

	void UpdateTemInteractable()
	{
		if ( !LockInteractable )
		{
			RaycastHit[] hits;
	//		Ray mainCharacterRay = new Ray (MainCharacter.Instance.transform.position, MainCharacter.Instance.transform.forward);
	//		Ray mainCharacterRay = new Ray( Camera.main.transform.position , Camera.main.transform.forward);
			Ray mainCharacterRay = Camera.main.ScreenPointToRay(FocusPoint);
			hits = Physics.RaycastAll (mainCharacterRay, 100f , interactiveMask.value);

			Interactable target = null;
			foreach( RaycastHit hit in hits )
			{
				target = hit.collider.GetComponent<Interactable> ();
				if (target != null) {
					if (target.IsInteractable() )
					{
						TouchPoint = hit.point;
						break;
					}
					else
						target = null;
				}

			}

			tem_Interactable = target;
		}
	}

}
