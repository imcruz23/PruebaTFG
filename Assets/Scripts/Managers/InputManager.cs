using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInputActions playerControls;
    public PlayerInput playerInput;
    [HideInInspector] public InputAction move;
    [HideInInspector] public InputAction look;
    [HideInInspector] public InputAction fire;
    [HideInInspector] public InputAction dash;
    [HideInInspector] public InputAction slide;
    [HideInInspector] public InputAction jump;
    [HideInInspector] public InputAction reload;
    [HideInInspector] public InputAction change;
    [HideInInspector] public InputAction interact;
    [HideInInspector] public InputAction switchW;
    [HideInInspector] public InputAction pauseM;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();

        dash = playerControls.Player.Dash;
        dash.Enable();

        slide = playerControls.Player.Slide;
        slide.Enable();

        look = playerControls.Player.Look;
        look.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();

        reload = playerControls.Player.Reload;
        reload.Enable();

        change = playerControls.Player.Change;
        change.Enable();

        interact = playerControls.Player.Interact;
        interact.Enable();

        switchW = playerControls.Player.Switch;
        switchW.Enable();

        pauseM = playerControls.Player.PauseMenu;
        pauseM.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        dash.Disable();
        slide.Disable();
        look.Disable();
        jump.Disable();
        reload.Disable();
        change.Disable();
        interact.Disable();
        switchW.Disable();
        pauseM.Disable();
    }

    public void DisableInGameControls()
    {
        move.Disable();
        fire.Disable();
        dash.Disable();
        slide.Disable();
        look.Disable();
        jump.Disable();
        reload.Disable();
        change.Disable();
        interact.Disable();
        switchW.Disable();
        look.Disable();
    }

    public void EnableInGameControls()
    {
        move.Enable();
        fire.Enable();
        dash.Enable();
        slide.Enable();
        look.Enable();
        jump.Enable();
        reload.Enable();
        change.Enable();
        interact.Enable();
        switchW.Enable();
    }


    public void EnablePauseControls()
    {
        pauseM.Enable();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }*/
    }
}
