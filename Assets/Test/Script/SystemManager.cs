using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour {

	[SerializeField] Scrollbar scrollBarOne;

	[SerializeField] Scrollbar scrollBarTwo;

	[SerializeField] Scrollbar scrollBarThree;

	[SerializeField] Image image;

	void Update()
	{
		image.color = new Color( scrollBarOne.value , scrollBarTwo.value , scrollBarThree.value );
		image.material.SetFloat( "_ValueOne" , scrollBarOne.value);
		image.material.SetFloat( "_ValueTwo" , scrollBarTwo.value);
		image.material.SetFloat( "_ValueThree" , scrollBarThree.value);
	}
}
