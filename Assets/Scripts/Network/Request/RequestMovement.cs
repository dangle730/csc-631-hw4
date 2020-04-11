using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestMovement : NetworkRequest
{
    public RequestMovement() { request_id = Constants.CMSG_MOVEMENT; }

    public void send(float x, float y)
    {
        packet = new GamePacket(request_id);
        packet.addInt32((int)x);
        packet.addInt32((int)y);
    }
}
