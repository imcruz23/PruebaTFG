using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPComponent : MonoBehaviour
{
    public GameObject model;

    private void OnDestroy()
    {
        if (model)
        model.SetActive(true);

        //Instantiate(model, new Vector3(-416.88f, -7.33f, -573.41f), Quaternion.identity);
        //model.SetActive(true);
    }
}
