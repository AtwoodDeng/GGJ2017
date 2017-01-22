using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	static NetworkManager s_Instance;
	public static NetworkManager Instance{ get { return s_Instance; }}
	public NetworkManager(){ s_Instance = this; }


}
