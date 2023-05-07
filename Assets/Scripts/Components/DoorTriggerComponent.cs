using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerComponent : MonoBehaviour
{
    Animator doorAnimation;
    Vector3 startPos;
    Vector3 endPos;
    public int up;
    float duration = 2.0f;
    float timeElapsed = 0.0f;
    bool onArea = false;

    private void Awake()
    {
        TryGetComponent(out doorAnimation);
        startPos = transform.position;
    }
    private void Start()
    {
        endPos = new Vector3 (startPos.x, startPos.y + up, startPos.z);
    }


    private void OnTriggerEnter(Collider other)
    {
        print("IN");
        //doorAnimation.Play("LiftDoor");
        //StartCoroutine(LiftDoor());
        UpDoor();
    }
    private void OnTriggerExit(Collider other)
    {
        print("OUT");
        //doorAnimation.Play("DownDoor");
        DownDoor();
    }

    private void UpDoor()
    {
        /*
        timeElapsed += Time.deltaTime;
        float t = Mathf.Clamp01(timeElapsed / duration);
         transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime);
        */

        transform.position = endPos;
    }

    private void DownDoor()
    {
        /*
        timeElapsed = 0f;
        timeElapsed += Time.deltaTime;
        float t = Mathf.Clamp01(timeElapsed / duration);
        transform.position = Vector3.Lerp(endPos, startPos, t);
        */
        transform.position = startPos;
    }
}
