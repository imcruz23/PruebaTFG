using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponInteractable : Interactable
{
    public int id;

    private Image pickupBox;
    private TMP_Text pickupText;
    //private string pickupName;
    private RawImage controlImage;

    // Eventos cuando agarras un interactuable
    //public delegate void weaponPicked();
    //public static event weaponPicked weaponPickedInfo;

    [SerializeField] private InputManager IM;

    [SerializeField] private UIManager UIM;

    [SerializeField] private PlayerComponent PC;

    
    private void Awake()
    {
        if(!pickupBox)
            pickupBox = GameObject.Find("Pickup Box").GetComponent<Image>();
        if(!pickupText)
            pickupText = GameObject.Find("Pickup Box").GetComponentInChildren<TMP_Text>();
        if(!controlImage)
            controlImage = GameObject.Find("ControlImage").GetComponent<RawImage>();
    }

    /*
    private void Start()
    {
        pickupText.text = "PICK UP";
        pickupBox.gameObject.SetActive(false);
        pickupText.gameObject.SetActive(false);
    }
    */

    private void Start()
    {
        if(!IM)
            IM = GameObject.Find("Input Manager").GetComponent<InputManager>();
        if(!UIM)
            UIM = GameObject.Find("UI Manager").GetComponent<UIManager>();
        if(!PC)
            PC = GameObject.Find("Character").GetComponent<PlayerComponent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!base.inRange)
            return;

        if (IM.interact.WasReleasedThisFrame())
        {
            gameObject.SetActive(false);
            UIM.UndrawTriggerBox();
            //weaponPickedInfo();
            PC.WeaponPickedListener(id);
        }
    }
    /*
    private void ActivateTriggerBox()
    {
        var index = 0;

        if (IM.playerInput.currentControlScheme.ToLower() == "keyboard&mouse")
            index = 0;
        else if(IM.playerInput.currentControlScheme.ToLower() == "gamepad")
            index = 1;

        controlImage.texture = images[index];
        pickupBox.gameObject.SetActive(true);
        pickupText.gameObject.SetActive(true);
    }
    */
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        UIM.DrawTriggerBox();
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        UIM.UndrawTriggerBox();
    }
}
