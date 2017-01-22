using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour {

	static LogicManager s_Instance;
	public static LogicManager Instance{ get { return s_Instance; }}
	public LogicManager(){ s_Instance = this; }



}
