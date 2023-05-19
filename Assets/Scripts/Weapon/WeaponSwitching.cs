using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    public int selectedWeapon = 0;
    public PlayerComponent player; // Necesitamos el array de armas
    private InputManager IM;
    public UIManager UIM;

    private static bool canSwitch = true;

    // Start is called before the first frame update
    void Awake()
    {
        SelectWeapon();
        IM = GameObject.Find("Input Manager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSwitch)
        {
            ChangeWeaponWithMouse();
            ChangeWeaponWithGamePad();
            //ChangeWeaponWithKeyboard();
        }
        if (UIManager.pauseState)
        {
            canSwitch = false;
        }
        else
        {
            canSwitch = true;
        }

    }

    public static void SetSwitch (bool b)
    {
        canSwitch = b;
    }
    /*
    private void ChangeWeaponWithKeyboard()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }

        SelectWeapon();
    }
    */

    private void ChangeWeaponWithMouse()
    {
        int prevWeapon = selectedWeapon; // Comprueba si es necesario cambiar

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= player.weaponsPicked.Count - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = player.weaponsPicked.Count - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        if (prevWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }
    
    private void ChangeWeaponWithGamePad()
    {
        int prevWeapon = selectedWeapon;

        if(IM.switchW.WasPressedThisFrame())
        {
            if (selectedWeapon >= player.weaponsPicked.Count - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (prevWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    public void SelectWeapon()
    {
        int i = 0;

        // Busca en todas las armas para ponerla al principio, pero las añade de forma correcta
        foreach(GameObject w in player.allWeapons)
        {
            if( i == selectedWeapon)
            {
                w.SetActive(true);
            }
            else
            {
                w.SetActive(false);
            }
            i++;
        }
    }
}
