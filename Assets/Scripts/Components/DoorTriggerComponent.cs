using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerComponent : MonoBehaviour
{
    Animator doorAnimation;

    private void Awake()
    {
        TryGetComponent(out doorAnimation);
    }
    private void OnTriggerEnter(Collider other)
    {
        print("IN");
        doorAnimation.Play("LiftDoor");
    }
    private void OnTriggerExit(Collider other)
    {
        print("OUT");
        doorAnimation.Play("DownDoor");
    }
}
