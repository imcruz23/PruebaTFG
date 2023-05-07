using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    // Para el Score
    [SerializeField] private TMP_Text scorePText;

    // Managers
    [SerializeField] private InputManager IM;
    [SerializeField] private LevelManager LM;

    // Para los pickups
    [SerializeField] private Image pickupBox;
    [SerializeField] private TMP_Text pickupText;
    //private string pickupName;
    [SerializeField] private RawImage controlImage;
    [SerializeField] private List<Texture2D> images = new List<Texture2D>();

    // Texto de recarga
    [SerializeField] private TMP_Text reloadText;

    // Rachas
    [SerializeField] private TMP_Text spreeText;
    [SerializeField] private List<string> spreeT = new List<string>();

    // Barra de vida
    [SerializeField] private HealthComponent healthC;
    [SerializeField] private Image healthBar;

    // En tiempo
    [SerializeField] private TMP_Text inTimeText;


    private void Awake()
    {
        if(!IM)
            IM = GameObject.Find("Input Manager").GetComponent<InputManager>();
        if(!LM)
            LM = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        if (!pickupBox)
            pickupBox = GameObject.Find("Pickup Box").GetComponent<Image>();
        if(!pickupText)
            pickupText = GameObject.Find("Pickup Box").GetComponentInChildren<TMP_Text>();
        if(!controlImage)
            controlImage = GameObject.Find("ControlImage").GetComponent<RawImage>();
        if(!scorePText)
            scorePText = GameObject.Find("ScoreP").GetComponent<TMP_Text>();
        if(!reloadText)
            reloadText = GameObject.Find("ReloadText").GetComponent<TMP_Text>();
        if(!spreeText)
            spreeText = GameObject.Find("SpreeText").GetComponent<TMP_Text>();
        if(!healthC)
            healthC = GameObject.Find("Character").GetComponent<HealthComponent>();
        if(!healthBar)
            healthBar = GameObject.Find("Health Bar").GetComponent<Image>();
    }

    private void Start()
    {
        pickupBox.gameObject.SetActive(false);
        pickupText.gameObject.SetActive(false);
        spreeText.gameObject.SetActive(false);
        inTimeText.gameObject.SetActive(false);

    }

    private void Update()
    {
        DrawScore();
        healthBar.rectTransform.sizeDelta = new Vector2(healthC.life, 100);
    }

    public void DrawTriggerBox()
    {
        var index = 0;

        if (IM.playerInput.currentControlScheme.ToLower() == "keyboard&mouse")
            index = 0;
        else if (IM.playerInput.currentControlScheme.ToLower() == "gamepad")
            index = 1;

        pickupText.text = "PICK UP";
        controlImage.texture = images[index];
        pickupBox.gameObject.SetActive(true);
        pickupText.gameObject.SetActive(true);
    }

    public void UndrawTriggerBox()
    {
        pickupBox.gameObject.SetActive(false);
        pickupText.gameObject.SetActive(false);
    }

    public void DrawReloadText()
    {
        reloadText.gameObject.SetActive(true);
    }

    public void UndrawReloadText()
    {
        if (!reloadText.gameObject.activeSelf)
            return;

       reloadText.gameObject.SetActive(false);
    }

    public void DrawScore()
    {
        scorePText.text = "Score: " + LM.score.ToString();
    }

    public void DrawSpree(int spree)
    {
        if(spree > spreeT.Count-1)
            spreeText.text = "KILLING SPREE";
        else
            spreeText.text = spreeT[spree];
        spreeText.gameObject.SetActive(true);
    }

    public void UndrawSpree()
    {
        spreeText.gameObject.SetActive(false);
    }

    public void DrawInTime()
    {
        inTimeText.gameObject.SetActive(true);
        Invoke(nameof(UndrawInTime), 0.5f);
    }

    public void UndrawInTime()
    {
        inTimeText.gameObject.SetActive(false);
    }
}
