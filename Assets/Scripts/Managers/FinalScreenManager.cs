using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScreenManager : MonoBehaviour
{
    private SaveStatsComponent SSC;
    void Awake()
    {
        TryGetComponent(out SSC);
    }

    // Update is called once per frame
    void Update()
    {
        SSC.DrawPlayerScore();
        SSC.DrawPlayerTime();
    }
}
