using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
    //Elevator Variables
    public Vector3[] points;
    public int point_number = 0;
    private Vector3 current_target;
    public float tolerance;
    public float speed;
    public float delay_time;
    private float delay_start;
    public bool automatic;

    //Client Variables
    // public float y;
    // public Transform track;
    // private Transform cachedTransform;
    // private Vector3 cachedPosition;
    // // private ConnectionManager cManager;
    // private GameObject mainObject;
    // private MessageQueue msgQueue;

    void Awake () {
        // mainObject = GameObject.Find ("Elevator");
        // cManager = mainObject.GetComponent<ConnectionManager> ();
        // msgQueue = mainObject.GetComponent<MessageQueue> ();
    }

    void Start () {
        if (points.Length > 0) {
            current_target = points[0];
        }
        tolerance = speed * Time.deltaTime;
        // msgQueue.AddCallback (Constants.SMSG_PLATFORM, responsePlatform);
    }

    void Update () {
        if (transform.position != current_target) {
            movePlatform ();
            // if (track && cachedPosition != track.position) {
            //     y = transform.position.y;
            //     cachedPosition = track.position;
            //     transform.position = cachedPosition;
            //     // cManager.send(requestPlatform(y));
            // }
        } else {
            updateTarget ();
            // if (track && cachedPosition != track.position) {
            //     y = transform.position.y;
            //     cachedPosition = track.position;
            //     transform.position = cachedPosition;
            //     //cManager.send(requestPlatform(y));
            // }
        }

    }

    void movePlatform () {
        Vector3 targetPos = current_target - transform.position;
        transform.position += (targetPos / targetPos.magnitude) * speed * Time.deltaTime;

        if (targetPos.magnitude < tolerance) {
            transform.position = current_target;
            delay_start = Time.time;
        }
    }

    void updateTarget () {
        if (automatic) {
            if (Time.time - delay_start > delay_time) {
                nextPlatform ();
            }
        }
    }

    public void nextPlatform () {
        point_number++;
        if (point_number >= points.Length) {
            point_number = 0;
        }
        current_target = points[point_number];
    }

    private void OnTriggerEnter2D (Collider2D other) {
        other.transform.parent = transform;
    }

    private void OnTriggerExit2D (Collider2D other) {
        other.transform.parent = null;
    }

    // public RequestPlatform requestPlatform (float y) {
    //     RequestPlatform request = new RequestPlatform();
    //     request.send((int)y);
    //     return request;
    // }

    // public void responsePlatform (ExtendedEventArgs eventArgs) {
    //     ResponsePlatformEventArgs args = eventArgs as ResponsePlatformEventArgs;
    //     Debug.Log("Elevator position changed.");
    // }
}