using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : NetworkBehaviour {

		public string IpAddress = "127.0.0.1";
		public int Port = 3035;
		public bool IsUseNat = false;//默认在局域网中

		Vector2 _temp;
		string _infoMessage = "";

		void OnGUI()
		{
			switch (Network.peerType)
			{
			case NetworkPeerType.Disconnected:
				ConnectServer();
				break;
			case NetworkPeerType.Server:
				break;
			case NetworkPeerType.Client:
				ConnectClient();
				break;
			case NetworkPeerType.Connecting:
				break;
			default:
				break;
			}
		}

		void ConnectServer()
		{
			if (GUILayout.Button("Connecting Server"))
			{
				NetworkConnectionError networkError = Network.Connect(IpAddress,Port);
				switch (networkError)
				{
				case NetworkConnectionError.NoError:
					break;
				default:
					break;
				}
			}
		}


		void ConnectClient()
		{
			_temp = GUILayout.BeginScrollView(_temp,GUILayout.Width(300),GUILayout.Height(400));

			GUILayout.Box(_infoMessage);

			_infoMessage = GUILayout.TextArea(_infoMessage);

			if (GUILayout.Button("Send"))
			{
				GetComponent<NetworkView>().RPC("ReciveMessage", RPCMode.All, _infoMessage);
			}

			GUILayout.EndScrollView();
		}
	 
		[RPC]
		void rpc_ReciveMessage(string msg,NetworkMessageInfo info)
		{
			_infoMessage = "Send:" +msg;
		}
			
}
