using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseComponent : MonoBehaviour
{


    public float mouseSens = 10f;  // Sensibilidad universal del ratón
    public Camera fpsCam;    // El jugador
    float xRot = 0f;         // Para evitar el clamping
    private PlayerComponent move;
    private InputManager IM;
    float sensFactor;       // Porque el mando y el raton no van igual


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        TryGetComponent(out move);
        IM = GameObject.Find("Input Manager").GetComponent<InputManager>();
        if (PlayerPrefs.GetFloat("sens") > 1)
           mouseSens = PlayerPrefs.GetFloat("sens");

    }

    // Update is called once per frame
    void Update()
    {
        if (IM.playerInput.currentControlScheme.ToLower() == "gamepad")
           sensFactor = 8.0f;
        else
            sensFactor = 1.0f;

        float sens = mouseSens * sensFactor;
        // Actualización Ratón
        //float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        
         Vector2 mouse = IM.look.ReadValue<Vector2>();
        float mouseX = mouse.x * sens * Time.deltaTime;
        float mouseY = mouse.y * sens * Time.deltaTime;

        xRot -= mouseY; // Si usas + obtienes eje invertido

        // Evitar que se mueva más de 90º la cámara
        xRot = Mathf.Clamp(xRot, -60f, 60f);

        fpsCam.transform.localEulerAngles = new Vector3(xRot, 0f, move.tilt);
        transform.Rotate(Vector3.up * mouseX);
    }
}
