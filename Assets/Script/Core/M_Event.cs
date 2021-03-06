﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// The list of logic events
/// </summary>
public enum LogicEvents
{
	None=0,
	RecieveWaveMessage = 10,
	SendMessage = 11,

	SelectAgentName = 20,
	SelectLocation =21,

	StickPaper = 30,

	AgentDead = 40,

}

public class M_Event : MonoBehaviour {

	/// <summary>
	/// Event handler. handle the event with basic arg
	/// </summary>
	public delegate void EventHandler(BasicArg arg);

	/// <summary>
	/// an example for the normal event
	/// </summary>
	public static event EventHandler StartApp;
	public static void FireStartApp(BasicArg arg){if ( StartApp != null ) StartApp(arg) ; }


	/// <summary>
	/// Handler specified for dealing with the logic events
	/// </summary>
	public delegate void LogicHandler( LogicArg arg );

	/// <summary>
	/// The list of logic events, we assum that the number of total events is less than 9999
	/// </summary>
//	public static LogicHandler[] logicEvents = new LogicHandler[System.Enum.GetNames (typeof (LogicEvents)).Length];
	public static LogicHandler[] logicEvents = new LogicHandler[9999];

	/// <summary>
	/// A static interface to fire the logic events, without specified type
	/// </summary>
	/// <param name="arg">Argument.</param>
	public static void FireLogicEvent( LogicArg arg )
	{
		if (arg.type != LogicEvents.None) {
			FireLogicEvent (arg.type, arg);
		}
	}
	/// <summary>
	/// a static interface to fire the logic events, with specified type
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="arg">Argument.</param>
	public static void FireLogicEvent( LogicEvents type , LogicArg arg )
	{
		if ( logicEvents[(int)type] != null )
		{
			arg.type = type;
			logicEvents [(int)type] ( arg );
		}

	}

	/// <summary>
	/// Registers the event according to the event type
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="handler">Handler.</param>
	public static void RegisterEvent( LogicEvents type , LogicHandler handler )
	{
		logicEvents [(int)type] += handler;
	}

	/// <summary>
	/// Unregisters the event.
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="handler">Handler.</param>
	public static void UnregisterEvent( LogicEvents type , LogicHandler handler )
	{
		logicEvents [(int)type] -= handler;
	}

	/// <summary>
	/// Register the handler function to all events
	/// </summary>
	/// <param name="handler">Handler.</param>
	public static void RegisterAll( LogicHandler handler )
	{
		for (int i = 0; i < logicEvents.Length; ++i)
			logicEvents [i] += handler;
	}

	/// <summary>
	/// Unregister the handler to all events
	/// </summary>
	/// <param name="handler">Handler.</param>
	public static void UnRegisterAll ( LogicHandler handler )
	{
		for (int i = 0; i < logicEvents.Length; ++i)
			logicEvents [i] -= handler;
	}


	public static string BLOCK = "block";
}

/// <summary>
/// Basic argument class, with a sender 
/// </summary>
public class BasicArg : EventArgs
{
	public BasicArg(object _this){m_sender = _this;}
	object m_sender;
	public object sender{get{return m_sender;}}
}

/// <summary>
/// Message argument, with a dictionary to record the parameters
/// </summary>
public class MsgArg : BasicArg
{
	public MsgArg(object _this):base(_this){}
	Dictionary<string,object> m_dict;
	Dictionary<string,object> dict
	{
		get {
			if ( m_dict == null )
				m_dict = new Dictionary<string, object>();
			return m_dict;
		}
	}
	public void AddMessage(string key, object val)
	{
		dict.Add(key, val);
	}
	public object GetMessage(string key)
	{
		object res;
		dict.TryGetValue(key , out res);
		return res;
	}
	public bool ContainMessage(string key)
	{
		return dict.ContainsKey(key);
	}
}

/// <summary>
/// Logic argument.
/// </summary>
public class LogicArg : MsgArg
{
	public LogicArg(object _this):base(_this){}

	/// <summary>
	/// The type of the arg.
	/// </summary>
	public LogicEvents type;
}