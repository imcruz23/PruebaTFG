using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairChanger : MonoBehaviour
{
    public Image _crosshair;
    public Camera fpsCam;
    public PlayerComponent player;
    // Update is called once per frame

    private void Awake()
    {
        _crosshair.material.color = Color.white;
    }
    void Update()
    {
        _crosshair.material.color = Color.white;

        // Hacemos el raycast

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)); // El rayo.Apunta desde el mouse
        if (Physics.Raycast(ray, out hit, (int) player.playerRange))
        {
            var selected_ = hit.transform;

            if(selected_.gameObject.layer == (int) Layers.Enemy)
            {
                _crosshair.material.color = Color.red;
            }
        }
    }
}
