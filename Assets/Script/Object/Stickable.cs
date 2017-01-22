using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickable : MonoBehaviour {

	virtual public void Stick( GameObject obj )
	{
		obj.transform.parent = transform;
	}
}
