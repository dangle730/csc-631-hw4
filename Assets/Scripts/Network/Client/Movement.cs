using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float x, y;
    public Transform tracker;
    private Vector2 cachedPosition;
    private ConnectionManager cManager;
    private MessageQueue msgQueue;

    void Awake()
    {
        cManager = gameObject.GetComponent<ConnectionManager>();
        msgQueue = gameObject.GetComponent<MessageQueue>();
        msgQueue.AddCallback(Constants.SMSG_MOVEMENT, responseMovement);
    }

    void Start()
    {
        if (tracker) { cachedPosition = transform.position; }
    }

    void Update()
    {
        if (tracker && cachedPosition != new Vector2(tracker.position.x, tracker.position.y))
        {
            x = transform.position.x;
            y = transform.position.y;
            cachedPosition = new Vector2(tracker.position.x, tracker.position.y);
            transform.position = cachedPosition;
            cManager.send(requestMovement(x, y));
        }
    }

    public RequestMovement requestMovement(float x, float y)
    {
        RequestMovement request = new RequestMovement();
        request.send((int)x, (int)y);
        return request;
    }

    public void responseMovement(ExtendedEventArgs eventArgs)
    {
        ResponseMovementEventArgs args = eventArgs as ResponseMovementEventArgs;
        Debug.Log("Moved player to " + args.ToString());
    }
}
