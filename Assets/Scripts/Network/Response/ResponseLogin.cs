using UnityEngine;

using System;

public class ResponseLoginEventArgs : ExtendedEventArgs
{		
	public short status { get; set; }
	public int user_id { get; set; }
	public string username { get; set; }
	
	public ResponseLoginEventArgs() {
		event_id = Constants.SMSG_AUTH;
	}
}

public class ResponseLogin : NetworkResponse
{	
	private short status;
	private int user_id;
	private string username;

	public ResponseLogin() { }
	
	public override void parse()
	{
		status = DataReader.ReadShort(dataStream);
		if (status == 0)
		{
			user_id = DataReader.ReadInt(dataStream);
			username = DataReader.ReadString(dataStream);
		}
	}
	
	public override ExtendedEventArgs process()
	{
		ResponseLoginEventArgs args = null;
		if (status == 0)
		{
			args = new ResponseLoginEventArgs();
			args.status = status;
			args.user_id = user_id;
			args.username = username;
		}

		return args;
	}
}