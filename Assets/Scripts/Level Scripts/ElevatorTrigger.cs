using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    public Elevator platform;
    private bool isColliding;
    
    private void OnTriggerEnter2D(Collider2D other) {
        isColliding = true;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.R) && isColliding) {
            platform.nextPlatform();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        isColliding = false;
    }
}
