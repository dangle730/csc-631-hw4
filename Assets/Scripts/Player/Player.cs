using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;
    private ConnectionManager cManager;
    private MessageQueue msgQueue;

    void Awake()
    {
        health = 100;
        cManager = gameObject.GetComponent<ConnectionManager>();
        msgQueue = gameObject.GetComponent<MessageQueue>();
        msgQueue.AddCallback(Constants.SMSG_PLAYERHIT, responsePlayerHit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerHit(int damage)
    {
        health -= damage;
        Debug.Log("health : " + health);
        cManager.send(requestPlayerHit(health));
    }

    public RequestPlayerHit requestPlayerHit(int health)
    {
        RequestPlayerHit request = new RequestPlayerHit();
        request.send(health);
        return request;
    }

    public void responsePlayerHit(ExtendedEventArgs eventArgs)
    {
        ResponsePlayerHitEventArgs args = eventArgs as ResponsePlayerHitEventArgs;
        Debug.Log("Player health: " + args.ToString());
    }
}
