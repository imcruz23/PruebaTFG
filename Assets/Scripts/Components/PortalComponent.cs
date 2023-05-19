using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalComponent : MonoBehaviour
{
    // Para poder guardar los tiempos
    public SaveStatsComponent stats;
    public Animator anim;

    private void Awake()
    {
        TryGetComponent(out stats);
        //TryGetComponent(out anim);
    }
    /*
    private void OnBecameVisible()
    {
        anim.SetBool("active", true);
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("LevelCompleted");
            stats.SavePlayerStats();
        }
    }

}
