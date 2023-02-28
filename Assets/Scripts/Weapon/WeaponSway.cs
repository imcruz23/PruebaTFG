using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script sirve para balancear el arma y darle un toque más realista
public class WeaponSway : MonoBehaviour
{
    //Movimiento
    [Header("Position")]
    public float swayAmount;
    public float maxSwayAmount;
    public float smoothAmount;

    [Header("Rotation")]
    public float rotationAmount;
    public float maxRotationAmount;
    public float smoothRotation;

    [Space]
    public bool rotationX = true;
    public bool rotationY = true;
    public bool rotationZ = true;

    // Private
    private Vector3 initialPosition;
    private Quaternion initialRotation; // Rotacion
    private float mouseX, mouseY;

    private InputManager IM;

    void Awake()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        IM = GameObject.Find("Input Manager").GetComponent<InputManager>();
    }

    void Update()
    {
        CalculateSway();
        MoveSway();
        //TiltSway();
    }

    private void CalculateSway()
    {
        var lookAt = IM.look.ReadValue<Vector2>();
        mouseX = -lookAt.x * swayAmount;
        mouseY = lookAt.y * swayAmount;

    }

    private void MoveSway()
    {
        
        float moveX = Mathf.Clamp(mouseX, -maxSwayAmount, maxSwayAmount);
        float moveY = Mathf.Clamp(mouseY, -maxSwayAmount, maxSwayAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, smoothAmount * Time.deltaTime);
    }

    
    private void TiltSway()
    {

        float tiltY = Mathf.Clamp(mouseX, -maxRotationAmount, maxRotationAmount);
        float tiltX = Mathf.Clamp(mouseY, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(
            new Vector3(
                rotationX ? -tiltX : 0f, 
                rotationY ? tiltY : 0f, 
                rotationZ ? tiltY : 0f
                ));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation * initialRotation, Time.deltaTime * smoothRotation);
    }
}
