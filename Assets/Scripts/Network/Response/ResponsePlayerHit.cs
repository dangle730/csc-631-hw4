using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsePlayerHitEventArgs : ExtendedEventArgs
{
    public int health { get; set; }

    public ResponsePlayerHitEventArgs() { event_id = Constants.SMSG_PLAYERHIT; }
}

public class ResponsePlayerHit : NetworkResponse
{
    private int health;

    public ResponsePlayerHit() { }

    public override void parse() { health = DataReader.ReadInt(dataStream); }

    public override ExtendedEventArgs process()
    {
        ResponsePlayerHitEventArgs args = new ResponsePlayerHitEventArgs();
        args.health = health;
        return args;
    }
}