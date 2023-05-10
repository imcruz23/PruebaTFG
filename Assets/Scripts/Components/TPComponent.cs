using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPComponent : MonoBehaviour
{
    public GameObject model;

    private void OnDestroy()
    {
        model.SetActive(true);
    }
}
