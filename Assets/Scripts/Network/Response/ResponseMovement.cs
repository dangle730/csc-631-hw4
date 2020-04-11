using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseMovementEventArgs : ExtendedEventArgs
{
    public float x { get; set; }
    public float y { get; set; }

    public ResponseMovementEventArgs() { event_id = Constants.SMSG_MOVEMENT; }

    public override string ToString() { return " X: " + x + " Y: " + y; }
}

public class ResponseMovement : NetworkResponse
{
    private float x, y;

    public ResponseMovement() { }

    public override void parse()
    {
        x = DataReader.ReadInt(dataStream);
        y = DataReader.ReadInt(dataStream);
    }

    public override ExtendedEventArgs process()
    {
        ResponseMovementEventArgs args = new ResponseMovementEventArgs();
        args.x = x;
        args.y = y;
        return args;
    }
}