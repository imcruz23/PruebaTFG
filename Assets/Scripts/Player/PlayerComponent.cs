using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [HideInInspector] public Vector3 playerMov;
    private Vector3 input;
    private Vector3 yVelocity;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private InputManager IM;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private LevelManager LM;

    [Header("Camera")]
    private Camera fpsCam;
    public float specialFov;
    public float cameraChangeTime;
    public float wallRunTilt;
    public float tilt;
    float normalFov;

    [Header("Movement")]
    private CharacterController controller;
    public float normalGravity;
    float gravity;
    public float wallrunSpeed;
    public float runSpeed;
    public float airSpeed;

    float speed;

    [Header("Collisions")]
    public Transform groundCheck; // Empty que tiene el jugador
    public float sphRadius = 0.3f;
    public LayerMask groundMask; // Mascara de suelo

    bool isGrounded; // Comprobar que el jugador esta en el suelo

    // Cosas del salto
    [Header("Jump")]
    public float jumpHeight = 2f;
    public float jumpCd = 0.25f;
    public int jumps;
    bool isJumping;
    public float jumpGravity;

    [Header("Dashing")]
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    bool isDashing;

    [Header("Wallrun")]
    public float wallRunGravity;
    public float wallRunSpeedIncrease;
    public float wallRunSpeedDecrease;
    public LayerMask wall;
    bool isWallRunning;
    bool leftWall;
    bool rightWall;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    Vector3 wallNormal;
    Vector3 lastWallNormal;
    Vector3 forwardDirection;
    bool hasWallRun = false;

    [Header("Sliding")]
    public float slideSpeedIncrease;
    public float slideSpeedDecrease;
    bool    isSliding;
    float   slideTimer;
    public float   maxSlideTimer;
    float crouchHeight = .5f;
    float startHeight;
    float crouchingCamPosition;
    Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    Vector3 standingCenter = Vector3.zero;
    float startCamPosition;
    public float playerRange;

    public List<GameObject> weaponsPicked = new List<GameObject>();

    public List<GameObject> allWeapons = new List<GameObject>();

    private HealthComponent health;

    // Saber si las habilidades est�n en cooldown

    [HideInInspector] public bool dashInCooldown;
    [HideInInspector] public bool slideInCooldown;


    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed;
    [SerializeField] private float walkBobAmount;
    private float defaultYPos = 0;
    private float timer;
    Animator anim;

    bool groundTouch;

    private float slideCooldown = 3f;

    //Esto controla la caida del personaje
    float fallSpeed = 1.8f;

    void Awake()
    {
        TryGetComponent(out health);
        TryGetComponent(out controller);
        if(!controller)
            controller = GameObject.Find("CharacterModel").GetComponent<CharacterController>();
        fpsCam = GetComponentInChildren<Camera>();
        if(!audioManager)
            audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        if(!characterModel)
            characterModel = GameObject.Find("CharacterModel");
        if(!IM)
            IM = GameObject.Find("Input Manager").GetComponent<InputManager>();
        if (!LM)
            LM = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    private void Start()
    {
        startHeight = transform.localScale.y;
        startCamPosition = fpsCam.transform.position.y;
        crouchingCamPosition = fpsCam.transform.position.y / 2f;
        normalFov = fpsCam.fieldOfView;
        defaultYPos = fpsCam.transform.position.y;
        transform.position = LM.startPosition;
    }

    void IncreaseSpeed(float speedIncrease)
    {
        speed += speedIncrease;
    }
    void DecreaseSpeed(float speedDecrease)
    {
        speed -= speedDecrease * Time.deltaTime;
    }
    void CameraFX()
    {
        float fov = isWallRunning ? specialFov : isSliding ? specialFov : normalFov;
        fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, fov, cameraChangeTime * Time.deltaTime);

        if(isWallRunning)
        {
            if(rightWall)
            {
                tilt = Mathf.Lerp(tilt, wallRunTilt, cameraChangeTime * Time.deltaTime);
            }
            else if(leftWall)
            {
                tilt = Mathf.Lerp(tilt, -wallRunTilt, cameraChangeTime * Time.deltaTime);
            }
        }
        else
        {
            tilt = Mathf.Lerp(tilt, 0f, cameraChangeTime * Time.deltaTime);
        }
    }
    public void HandleInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        input = transform.TransformDirection(input);
        input = Vector3.ClampMagnitude(input, 1f);

        if (IM.jump.WasPressedThisFrame() && jumps > 0)
        {
            Jump();
            // La resta tiene que estar aqu� porque dentro de la funci�n da muchos problemas
            jumps--;
            Invoke(nameof(ResetJump), jumpCd);
        }
        
        if(IM.dash.WasPressedThisFrame() && !isDashing)
        {
            StartCoroutine(DashCoroutine(input));
            dashInCooldown = true;
            Invoke(nameof(ResetDash), dashCooldown);
        }
        
        
        if(IM.slide.WasPressedThisFrame() && !isJumping)
        {
            Slide();
            slideInCooldown = true;
            Invoke(nameof(ExitSlide), 0.5f);
        }
        
        HeadBobMovement();
    }
    void GroundMovement()
    {
        speed = runSpeed;

        if( input.x != 0)
        {
            playerMov.x += input.x * speed;
        }
        else
        {
            playerMov.x = 0;
        }
        if (input.z != 0)
        {
            playerMov.z += input.z * speed;
        }
        else
        {
            playerMov.z = 0;
        }
        playerMov = Vector3.ClampMagnitude(playerMov, speed);
        
        if(playerMov != Vector3.zero)
            audioManager.PlayPlayerWalkingSound();
        
    }

    void AirMovement()
    {
        playerMov.x += input.x * airSpeed;
        playerMov.z += input.z * airSpeed;

        playerMov = Vector3.ClampMagnitude(playerMov, speed);
    }

    void SlideMovement()
    {
        isGrounded = true;
        playerMov += forwardDirection;
        playerMov = Vector3.ClampMagnitude(playerMov, speed);
    }

    void DashMovement()
    {
        controller.Move(forwardDirection * dashSpeed * Time.deltaTime);
    }

    void WallRunMovement()
    {
        // Controlamos que estemos en un rango concreto
        if(input.z > (forwardDirection.z - 10f) && input.z < (forwardDirection.z + 10f))
        {
            playerMov += forwardDirection;
        }
        else if(input.z < (forwardDirection.z - 10f) && input.z > (forwardDirection.z + 10f))
        {
            playerMov.x = 0f;
            playerMov.z = 0f;
            ExitWallRun();
        }
        playerMov.x += input.x * airSpeed;
        playerMov = Vector3.ClampMagnitude(playerMov, speed);
    }

    void HeadBobMovement()
    {
        if (!isGrounded) return;
        /*
        if(Mathf.Abs(playerMov.x) > 0.1f || Mathf.Abs(playerMov.z) > 0.1f)
        {
            timer += Time.deltaTime * walkBobSpeed;
            fpsCam.transform.localPosition = new Vector3 (
                fpsCam.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * walkBobAmount,
                fpsCam.transform.localPosition.z
                );
        }
        */
    }

    void ApplyGravity()
    {
        gravity = isWallRunning ? wallRunGravity : isJumping ? jumpGravity : normalGravity;
        yVelocity.y += gravity * Time.deltaTime;
        controller.Move(yVelocity * Time.deltaTime);
    }

    void CheckGround()
    {
        //bool firstContactGround = Physics.CheckSphere(groundCheck.position, sphRadius, groundMask);
        RaycastHit hit;
        bool firstContactGround = Physics.Raycast(groundCheck.position, Vector3.down, out hit, 0.5f);

        if(!isGrounded && firstContactGround && hit.transform.gameObject.layer == (int) Layers.Floor)
        {
            groundTouch = true;
            //playerMov.y = -2f;  // Ponerlo a 0 puede dar problemas
            jumps = 2;
            hasWallRun = false;
            isJumping = false;
            if(groundTouch)
            {
                audioManager.PlayPlayerLandingSound();
                groundTouch = false;
            }
        }
        isGrounded = firstContactGround;
    }

    void CheckWallRun()
    {
        leftWall = Physics.Raycast(transform.position, -transform.right, out leftWallHit, .7f, wall);
        rightWall = Physics.Raycast(transform.position, transform.right, out rightWallHit, .7f, wall);
        if ((rightWall || leftWall) && !isWallRunning && !isGrounded)
        {
           TestWallRun();
        }
        if((!rightWall && !leftWall) && isWallRunning)
        {
            ExitWallRun();
        }
    }

    void Slide()
    {
         if(isGrounded && !slideInCooldown)
         {
            MoveCameraToCrouch();
            isSliding = true;
            characterModel.transform.localScale = new Vector3(characterModel.transform.localScale.x, characterModel.transform.localScale.y / 1.5f, characterModel.transform.localScale.z);
            controller.height = crouchHeight;
            //controller.center = crouchingCenter;
            forwardDirection = transform.forward;
            // Por alguna razon se necesita esto para que detecte que est� en el suelo
            //characterModel.transform.localPosition = new Vector3(characterModel.transform.localPosition.x, characterModel.transform.localPosition.y + 0.5f, characterModel.transform.localPosition.z);
            IncreaseSpeed(slideSpeedIncrease);
            audioManager.PlayPlayerSlidingSound();
         }
        slideTimer = maxSlideTimer;
        CameraFX();
    }

    private void MoveCameraToCrouch()
    {
        float yCam = fpsCam.transform.position.y;
        //yCam = Mathf.Lerp(yCam, crouchingCamPosition, cameraChangeTime * Time.deltaTime);
        yCam = Mathf.Lerp(yCam, GameObject.Find("CrouchingCamera").transform.position.y, cameraChangeTime * Time.deltaTime);

        fpsCam.transform.position = new Vector3(fpsCam.transform.position.x, yCam, fpsCam.transform.position.z);
    }

    private void MoveCameraToStanding()
    {
        float yCam = fpsCam.transform.position.y;
        //yCam = Mathf.Lerp(yCam, startCamPosition, cameraChangeTime * Time.deltaTime);
        yCam = Mathf.Lerp(yCam, GameObject.Find("StandingCamera").transform.position.y, cameraChangeTime * Time.deltaTime);


        fpsCam.transform.position = new Vector3(fpsCam.transform.position.x, yCam, fpsCam.transform.position.z);
    }

    void ExitSlide()
    {
        MoveCameraToStanding();
        characterModel.transform.localScale = new Vector3(characterModel.transform.localScale.x, startHeight, characterModel.transform.localScale.z);
        //characterModel.transform.localPosition = new Vector3(characterModel.transform.localPosition.x, characterModel.transform.localPosition.y - 0.5f, characterModel.transform.localPosition.z);
        controller.height = (startHeight * 2);
        controller.center = standingCenter;
        isSliding = false;
        Invoke(nameof(ResetSlide), slideCooldown);
    }

    void ResetSlide()
    {
        slideInCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
        HandleInput();
        HandleAnimations();

    }

    void HandleCamera()
    {
        if ((isGrounded && !isSliding))
        {
            startCamPosition = fpsCam.transform.position.y;
            crouchingCamPosition = startCamPosition / 1.5f;
            MoveCameraToStanding();
        }
            

    }

    private void FixedUpdate()
    {
        CheckWallRun();
        if (isGrounded && !isSliding)
        {
            GroundMovement();
        }
        else if (isJumping && !isGrounded && !isWallRunning)
        {
            AirMovement();
        }
        else if (isSliding && isGrounded)
        {
            SlideMovement();
            DecreaseSpeed(slideSpeedDecrease);
            slideTimer -= 1f * Time.deltaTime;

            if (slideTimer < 0)
            {
                isSliding = false;
            }
        }
        else if (isWallRunning)
        {
            WallRunMovement();
            DecreaseSpeed(wallRunSpeedDecrease);
        }
        CheckGround();
        controller.Move(playerMov * Time.deltaTime);
        ApplyGravity();
        CameraFX();
    }
    void Jump()
    {
        isJumping = true;
        if (isWallRunning)
        {
            ExitWallRun();
            IncreaseSpeed(wallRunSpeedIncrease);
        }
        // La idea es hacer que, cuando estamos realizando el segundo salto, las fuerzas que se apliquen al jugador
        // sean mayores para evitar que el jugador vuele
        //print(jumps);
        float jumpH = jumpHeight; // Vamos a restar la fuerza con la que se realiza el segundo salto
        float fallS = fallSpeed; // Idem para el fallSpeed
        if (jumps <= 1)
        {
            fallS = 0.8f;
            jumpH /= 1.2f;
        }
        // Aplicamos la velocidad a la Y mediante raices cuadradas
        yVelocity.y = Mathf.Sqrt(jumpH * -fallS * gravity);
    }

    private void ResetJump()
    {
        if(isGrounded)
            isJumping = false;
    }

    private IEnumerator DashCoroutine(Vector3 mov)
    {
        isDashing = true;
        float startTime = Time.time; // Para saber cuanto tienes que dashear

        while (Time.time < startTime + dashTime)
        {
            CameraFX();

            if(!mov.Equals(new Vector3(0,0,0)))
            {
                //controller.Move(mov * dashSpeed * Time.deltaTime);
            }
            else
            {
                //controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            }
            audioManager.PlayPlayerDashSound();
            yield return null;
        }
    }

    private void ResetDash()
    {
        isDashing = false;
        dashInCooldown = false;
    }
    void TestWallRun()
    {
        wallNormal = leftWall ? leftWallHit.normal : rightWallHit.normal;

        if (hasWallRun)
        {
            float wallAngle = Vector3.Angle(wallNormal, lastWallNormal);
            if(wallAngle > 15)
            {
                WallRun();
            }
        }
        else
        {
            WallRun();
            hasWallRun = true;
        }
    }
    void WallRun()
    {
        isWallRunning = true;
        jumps = 1;
        IncreaseSpeed(wallRunSpeedIncrease);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            yVelocity = Vector3.zero;

        wallNormal = leftWall ? leftWallHit.normal : rightWallHit.normal;
        forwardDirection = Vector3.Cross(wallNormal, Vector3.up);

        if(Vector3.Dot(forwardDirection, transform.forward) < 0)
        {
            forwardDirection = -forwardDirection;
        }
    }
    void ExitWallRun()
    {
        isWallRunning = false;
        lastWallNormal = wallNormal;
    }
    void HandleAnimations()
    {
        /*
        if(playerMov == Vector3.zero)
        {
            anim.SetFloat("Speed", 0f);
        }
        else
        {
            anim.SetFloat("Speed", 0.5f);
        }
        */
    }

    // Funcion recibidora de esta
    public void WeaponPickedListener(int id)
    {
        int tam = weaponsPicked.Count;
        if(id != allWeapons.Count)
        {
            weaponsPicked.Add(allWeapons[id]);
            WeaponSwitching ws = GetComponentInChildren<WeaponSwitching>();
            if (ws)
            {
                ws.selectedWeapon = id;
                ws.SelectWeapon();
            }
        }
    }
    public void HealPickedListener(float l)
    {
        if(health.life + l >= health.maxLife)
        {
            health.life = health.maxLife;
        }
        else
        {
            health.life += l;
        }
    }
}