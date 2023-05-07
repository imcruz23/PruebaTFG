using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour
{
    [Header("ICONOS")]
    [SerializeField] private RawImage dashIcon;
    [SerializeField] private RawImage slideIcon;
    [SerializeField] private PlayerComponent PC;
    [SerializeField] private Image dashCooldown;
    [SerializeField] private Image slideCooldown;

    // 0-1 DASH
    // 2-3 SLIDE

    public List<Texture2D> sprites = new List<Texture2D>();
    // Start is called before the first frame update
    void Awake()
    {
        if(!PC)
            PC = GameObject.Find("Character").GetComponent<PlayerComponent>();
        if (!dashIcon)
            dashIcon = GameObject.Find("Dash").GetComponent<RawImage>();
        if (!slideIcon)
            slideIcon = GameObject.Find("Slide").GetComponent<RawImage>();
    }

    private void Start()
    {
        dashCooldown.gameObject.SetActive(false);
        slideCooldown.gameObject.SetActive(false);
        if (dashIcon)
            dashIcon.texture = sprites[0];

        if (slideIcon)
            slideIcon.texture = sprites[2];
    }

    // Update is called once per frame
    void Update()
    {
        CheckDashCD();
        CheckSlideCD();
    }

    private void CheckDashCD()
    {
       
        if (!PC.dashInCooldown)
            dashIcon.texture = sprites[0];
        else
            dashIcon.texture = sprites[1];
        /*
        float cooldownTimer = PC.dashCooldown;
        if(PC.dashInCooldown)
        {
            cooldownTimer -= Time.deltaTime;
           if (cooldownTimer < 0.0f)
                dashCooldown.fillAmount = 0.0f;
           else
                dashCooldown.fillAmount = cooldownTimer / PC.dashCooldown;
        }
        */
    }

    private void CheckSlideCD()
    {
        if (!PC.slideInCooldown)
            slideIcon.texture = sprites[2];
        else
            slideIcon.texture = sprites[3];
    }
}
