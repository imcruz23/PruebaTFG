using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthInteractable : Interactable
{

    public float heal;
    //public delegate void healPicked(float l);
    //public static event healPicked healPickedInfo;

    [SerializeField] private PlayerComponent PC;

    private void Start()
    {
        if(!PC)
            PC = GameObject.Find("Character").GetComponent<PlayerComponent>();
    }
    // Update is called once per frame
    void Update() 
    {
        if (!base.inRange)
            return;

        gameObject.SetActive(false);
        //healPickedInfo(heal);
        PC.HealPickedListener(heal);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        PC.audioManager.PlayPlayerHealSound();

    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
