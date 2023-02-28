using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private PlayerComponent player;
    //[SerializeField] private GameObject guns;
    private void Start()
    {
        if(!player)
            player = GameObject.Find("Character").GetComponent<PlayerComponent>();
    }
    void OnEnable()
    {
        OnEnablePlayerEvents();
    }

    void OnDisable()
    {
        OnDisablePlayerEvents();
    }

    void OnEnablePlayerEvents()
    {
        //WeaponInteractable.weaponPickedInfo += player.WeaponPickedListener;
        //HealthInteractable.healPickedInfo += player.HealPickedListener;
    }

    void OnDisablePlayerEvents()
    {
        //WeaponInteractable.weaponPickedInfo -= player.WeaponPickedListener;
        //HealthInteractable.healPickedInfo -= player.HealPickedListener;
    }
}
