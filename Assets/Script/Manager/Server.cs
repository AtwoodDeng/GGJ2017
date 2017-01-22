using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : NetworkBehaviour {

		public string IpAddress = "127.0.0.1";
		public int Port = 3035;
		public bool IsUseNat=false;//默认在局域网中

		Vector2 _temp;
		string _infoMessage = "";

		void OnGUI()
		{
			switch (Network.peerType)
			{
			case NetworkPeerType.Disconnected://服务端未初始化，客户端不能连接
				InitalGameServer();//初始化服务器
				break;
			case NetworkPeerType.Server://服务端
				ConnectingGameServer();
				break;
			case NetworkPeerType.Client://客户端

				break;
			case NetworkPeerType.Connecting://正在连接到服务端
				break;
			default:
				break;
			}
		}
		/// <summary>
		/// 初始化服务端
		/// </summary>
		void InitalGameServer()
		{
			if (GUILayout.Button("StartServer"))
			{
				NetworkConnectionError networkError = Network.InitializeServer(32,Port, IsUseNat);
				switch (networkError)
				{
				case NetworkConnectionError.NoError:
					break;
				default:
					break;
				}
			}
		}

		void ConnectingGameServer()
		{
			GUILayout.Label("Server is running...");

			if (GUILayout.Button("Disconnect"))
			{
				DisConnectGameServer();
			}


			int clientConnects = Network.connections.Length;
			for (int i = 0; i < clientConnects; i++)
			{
				GUILayout.Label("Client:"+i);
				GUILayout.Label("Client IP："+Network.connections[i].ipAddress);
				GUILayout.Label("Client Port：" + Network.connections[i].port);
				GUILayout.Label("-          -");

			}

			_temp = GUILayout.BeginScrollView(_temp,GUILayout.Width(300),GUILayout.Height(400));
			GUILayout.Box(_infoMessage);

			GUILayout.EndScrollView();
		}

		/// <summary>
		/// 断开服务器
		/// </summary>
		void DisConnectGameServer()
		{
			Network.Disconnect();
		}

		[RPC]
		void rpc_ReciveMessage(string msg,NetworkMessageInfo info)
		{
			_infoMessage = "Recieve：" + msg;
		}
}
