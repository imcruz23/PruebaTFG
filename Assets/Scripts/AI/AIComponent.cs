using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIComponent : MonoBehaviour
{
    public float timeBetweenAttacks, sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private HealthComponent health;
    public int score;
    public int damage;
    [SerializeField] protected GameObject player;
    [SerializeField] protected LevelManager LM;
    [SerializeField] protected UIManager UIM;

    protected void Awake()
    {
        TryGetComponent(out health);
        if(!player)
            player = GameObject.Find("Character");
        if(!LM)
            LM = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        if(!UIM)
            UIM = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }
    // Mirando al rival
    protected void Update()
    {
        transform.LookAt(player.transform);
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
    }

    public virtual void Attack()
    {
    }

    protected void OnDestroy()
    {
        LM.score += score;
        //print("Mi score es de :" + LM.score);
        KillstreakManager.killTimer = 2000;
        KillstreakManager.enemiesKilled++;
    }
}
