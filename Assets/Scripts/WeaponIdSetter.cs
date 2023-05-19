using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIdSetter : MonoBehaviour
{
    void Awake()
    {
        SetIds();
    }

    public void SetIds()
    {
        for(int i = 0; i < transform.childCount-1; ++i)
        {
            transform.GetChild(i).GetComponent<WeaponManager>().weaponId = i;
        }
    }
}
