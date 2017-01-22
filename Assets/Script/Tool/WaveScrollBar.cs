using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveScrollBar : MonoBehaviour {

	[SerializeField]Scrollbar bar;

	void Start()
	{
		if ( bar == null )
			bar = GetComponent<Scrollbar>();
	}

	public float GetValue()
	{
		return bar.value;
	}
}
