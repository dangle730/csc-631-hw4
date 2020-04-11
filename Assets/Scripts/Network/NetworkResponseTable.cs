using UnityEngine;

using System;
using System.Collections.Generic;

public class NetworkResponseTable {

	public static Dictionary<short, Type> responseTable { get; set; }
	
	public static void init() {
		responseTable = new Dictionary<short, Type>();
		add(Constants.SMSG_AUTH, "ResponseLogin");//201
		add(Constants.SMSG_PLAYERS, "ResponsePlayers");//203
		add(Constants.SMSG_TEST, "ResponseTest");//204
        add(Constants.SMSG_REGISTER, "ResponseRegister");
        add(Constants.SMSG_MOVEMENT, "ResponseMovement");
        add(Constants.SMSG_PLAYERHIT, "ResponsePlayerHit");
	}
	
	public static void add(short response_id, string name) {
		responseTable.Add(response_id, Type.GetType(name));
	}
	
	public static NetworkResponse get(short response_id) {
		init ();
		NetworkResponse response = null;
		if (responseTable.ContainsKey(response_id)) {
			response = (NetworkResponse) Activator.CreateInstance(responseTable[response_id]);
			response.response_id = response_id;
		} else {
			Debug.Log("Response [" + response_id + "] Not Found");
		}
		
		return response;
	}
}
