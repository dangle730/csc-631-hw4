using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestPlayerHit : NetworkRequest
{
    public RequestPlayerHit() { request_id = Constants.CMSG_PLAYERHIT; }

    public void send(int health)
    {
        packet = new GamePacket(request_id);
        packet.addInt32(health);
    }
}
