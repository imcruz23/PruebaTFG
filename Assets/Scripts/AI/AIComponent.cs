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
    public AudioManager AM;

    protected void Awake()
    {
        TryGetComponent(out health);
        if(!player)
            player = GameObject.Find("Character");
        if(!LM)
            LM = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        if(!UIM)
            UIM = GameObject.Find("UI Manager").GetComponent<UIManager>();
        if (!AM)
            AM = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
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
        // Aumenta la vida del jugador
        var p = GameObject.Find("Character");
        if (p) // Comprobamos si el jugador existe
        {
            HealthComponent hc = p.GetComponent<HealthComponent>();
            // Curamos al jugador
            hc.TakeDamage(-5);
            if (hc.life > hc.maxLife)
            {
                hc.life = hc.maxLife;
            }
            // Timers y puntuacion
            if (MusicalNote.hitOnTime)
            {
                // Si es al ritmo, más puntuacion
                UIM.DrawCriticalText();
                LM.AddScore(score * 2);
                UIM.UndrawCriticalText();
            }
            else // Puntuacion estandar
                LM.AddScore(score);
            //print("Mi score es de :" + LM.score);
            // Ponemos el timer para quitar el texto de la racha
            KillstreakManager.killTimer = 2000;
            KillstreakManager.enemiesKilled++;

        }
        
    }


}
