using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{

     public int weaponId;

    [Header("Camera")]
    public Camera cam;

    [Header("Projectile Settings")]
    public Transform spawnPoint;
    public GameObject projectile;
    //public float shootForce = 1500f;
    public float shootRate = 0.5f; // Parametro para controlar el tiempo entre disparos
    private float shootTime; // CD de disparos real
    public float range = 1000f; // Poco rango = escopeta, mucho rango = franco
    public float weaponDamage = 10f;
    public bool allowHoldButton; // Modo de disparo con hold
    public float spread; // Dispersion de bala

    [Header("Bullets")]
    int bullets;
    public int bulletsInCharger; // Balas de cada cargador
    public float timeToReload; // Tiempo de recarga
    public int bulletsPerTap = 1; // Modo de escopeta
    int bulletsShot; // Balas disparadas para controlar las rafagas
    public int burstBullets; // Para hacer rafagas
    //bool canShoot; // Comprueba las situaciones en las que puedes disparar

    [Header("UI")]
    public TMP_Text bulletText; // Balas actuales, balas de cargador y modo de disparo (automatico o rafagas)
    public GameObject impactEffect; // Como las balas dentro del juego no existen realmente, necesitamos un efecto de impacto en la zona del raycast
    bool shootingInput; // Para comprobar si se ha pulsado el input de disparo (sirve tmb para cambiarlo segun el tipo de disparo)
    bool reloading; // Para evitar recargar y disparar a la vez

    bool burstMode; // Controla que esté en modo burst
    bool burstModeInput; // Es el input para cambiar de modo

    bool isPicked;

    public float backAmount;

    // Para que exista recoil
    float prevShootTime;
    float currentSpread;
    float prevSpread;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private InputManager IM;

    private Animator animator;

    [SerializeField] private UIManager UIM;

    [SerializeField] private WeaponSwitching WS;

    [SerializeField] private SongManager rythmMechanic;

    void Awake()
    {
        if(!audioManager)
            audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        if(!IM)
            IM = GameObject.Find("Input Manager").GetComponent<InputManager>();
        if(!UIM)
            UIM = GameObject.Find("UI Manager").GetComponent<UIManager>();
        TryGetComponent(out animator);
    }

    private void Start()
    {
        bullets = bulletsInCharger;
        //canShoot = true;
    }
    void Update()
    {

        DetectWeaponInput();
       
        bulletText.text = bullets + " / " + bulletsInCharger + " " + DetectWeaponMode(); // Para poder mostrar la munición

        if (IM.reload.WasReleasedThisFrame() && bullets < bulletsInCharger && !reloading)
        {
            ReloadWeapon();
        }

        if (burstModeInput && burstBullets > 0)
        {
            burstMode = !burstMode;
        }

        if (shootingInput && Time.time > shootTime && bullets > 0 && !reloading)
        {
            bulletsShot = burstBullets;
            Shoot();
        }

        if (bullets <= 0)
            UIM.DrawReloadText();
        else
            UIM.UndrawReloadText();

    }

    private string DetectWeaponMode()
    {
        if (!burstMode)
            return "Aut";
        else
            return "Burst / " + burstBullets;

    }

    /* Controla si puedes mantener pulsado o no
    Ademas, integra el modo de burst */
    private void DetectWeaponInput()
    {
        if (!allowHoldButton) shootingInput = IM.fire.WasPressedThisFrame();
        else shootingInput = IM.fire.IsPressed();
        burstModeInput = IM.change.WasReleasedThisFrame();
    }

    void SimulateRecoil()
    {
        if (allowHoldButton && (Mathf.Abs(shootTime - prevShootTime) < 0.1f))
        {

            currentSpread = prevSpread * 1.17f;
        }
        else
        {
            currentSpread = spread;
        }

    }

    // Este método comprueba, mediante Raycast, que la bala que sale del spawnPoint colisione con algo
    void Shoot()
    {
        WeaponSwitching.SetSwitch(false);

        // Variable temporal para controlar el daño real del arma, si da en ritmo, el arma tiene mas daño
        float weaponDamage_ = weaponDamage;

        /*
        // Comprobamos si se le da justo a tiempo
        if(rythmMechanic.PressedOnTime())
        {
            weaponDamage_ *= 2;
        }
        */
        //canShoot = false;
        RaycastHit hit;

        animator.SetTrigger("shooting");
        InstantiateProjectile();
        audioManager.PlayShootingSound(weaponId);

        // Esto evita la layer 14 a la hora de hacer el raycast
        var layerMask = ~(1 << 14);

        for (int i = 0; i < bulletsPerTap; ++i)
        {
            //SimulateRecoil();

            float allSpread = Random.Range(-spread, spread);
            Vector3 direction = cam.transform.forward + new Vector3(allSpread, allSpread, 0);

            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.05f, transform.localPosition.z - 0.1f);

            //Vector3 direction = cam.transform.forward + new Vector3(currentSpread, currentSpread, 0); // Esta linea es para calcular recoil

            // Si el raycast ha dado hit en algo
            if (Physics.Raycast(cam.transform.position, direction, out hit, range, layerMask))
            {
                InstantiateImpact(hit);
                // Comprobamos si es un enemigo
                HealthComponent hc = hit.transform.GetComponent<HealthComponent>();

                if (hc != null)
                {
                    hc.TakeDamage(weaponDamage_);
                    audioManager.PlayEnemyDamageSound();
                }
            }
        }

        bullets--;
        bulletsShot--;

        if (bullets < 0)
            bullets = 0;

        // Esto se usa para simular recoil
        /*
        prevSpread = currentSpread;
        prevShootTime = shootTime + shootRate;
        */

        shootTime = Time.time + shootRate; // Aplicamos tiempo de cooldown
        //print(prevShootTime - shootTime);
        // Aplica la ráfaga si existe o el numero es mayor que 0
        if (bulletsShot > 0 && burstMode)
            Invoke("Shoot", (shootRate/burstBullets));

        // Evitamos que no se pueda cambiar
        Invoke(nameof(ActivateSW), 1.0f);
    }

    // Esta funcion no recarga realmente, solo cambia el estado a reloading para no poder hacer nada mas
    private void ReloadWeapon()
    {
        WeaponSwitching.SetSwitch(false);
        audioManager.PlayReloadingSound(weaponId);
        reloading = true;
        animator.SetBool("reloading", true);
        Invoke(nameof(ReloadFinished), timeToReload);
    }

    // Recarga real
    private void ReloadFinished()
    {
        bullets = bulletsInCharger;
        reloading = false;
        animator.SetBool("reloading", false);
        Invoke(nameof(ActivateSW), 0.3f);
    }

    // Activar el cambio de arma
    private void ActivateSW()
    {
        WeaponSwitching.SetSwitch(true);
    }

    // Como solo queremos un muzzle flash en el disparo, activamos el efecto y no lo instanciamos para asi no destruirlo
    private void InstantiateProjectile()
    {
        projectile.transform.position = spawnPoint.position;
        projectile.transform.forward = cam.transform.forward;
        projectile.gameObject.SetActive(true);
        Invoke(nameof(DisableProjectile), 0.03f);
    }

    private void DisableProjectile()
    {
        projectile.gameObject.SetActive(false);
    }

    // Esto se podria hacer con una pool de impactos para evitar crearlos
    private void InstantiateImpact(RaycastHit hit)
    {
        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.identity);
        impactGO.gameObject.SetActive(true);
        Destroy(impactGO, 0.3f);
    }

    private void DisableImpact()
    {
        impactEffect.SetActive(false);
    }
}
