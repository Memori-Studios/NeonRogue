using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal, vertical;
    public bool esc_Input, rollFlag, roll_Input, reload_Input, firingMode_Input, slot_1_Input, slot_2_Input, slot_3_Input, fire_Input, sandevistan_Input;
    public float moveAmount;
    public Vector2 movementInput, cameraInput;
    public PlayerControls inputActions;
    public bool fireModeAuto = true;
    PlayerMovement playerMovement;
    WeaponController weaponController;
    UIManager uIManager;
    PlayerManager playerManager;
    PlayerStats playerStats;

    private void Start()
    {
        weaponController = GetComponent<WeaponController>();
        playerMovement = GetComponent<PlayerMovement>();
        uIManager = PlayerManager.instance.uIManager;
        playerManager = GetComponent<PlayerManager>();
        playerStats = GetComponent<PlayerStats>();
        fireModeAuto = !fireModeAuto;
    }

    public void OnEnable(){
        if (inputActions == null){
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed+= inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerActions.Escape.performed += i => esc_Input = true;
            inputActions.PlayerActions.Roll.performed += i => roll_Input = true;
            inputActions.PlayerActions.Reload.performed += i => reload_Input = true;
            inputActions.PlayerActions.FiringMode.performed += i => firingMode_Input = true;
            inputActions.PlayerActions.Fire.performed += i => fire_Input = true;
            inputActions.PlayerActions.Fire.canceled += i => fire_Input = false;
            inputActions.PlayerMovement.Camera.performed+= i => cameraInput = i.ReadValue<Vector2>();
            
            inputActions.PlayerActions.Slot_1.performed += i => slot_1_Input = true;
            inputActions.PlayerActions.Slot_2.performed += i => slot_2_Input = true;
            inputActions.PlayerActions.Slot_3.performed += i => slot_3_Input = true;

            inputActions.PlayerActions.Sandevistan.performed += i => sandevistan_Input = true;
        }
        inputActions.Enable();
    }
    private void OnDisable(){
        inputActions.Disable();
    }
    public void TickInput(float delta){
        HandleMoveInput(delta);
        HandlePauseInput();
        HandleShootInput();
        HandleRoll();
        HandleReload();
        HandleWeaponSelection();
        HandleFiringMode();
        HandleCyberware();
        HandleHealthRegen(delta);
    }
    private void HandleCyberware()
    {
        if(slot_1_Input)
        {
            slot_1_Input = false;
            PlayerManager.instance.cyberwareManager.OverrideSandy();
            PlayerManager.instance.cyberwareManager.HandleCyberware(1);
        }
        else if(slot_2_Input)
        {
            slot_2_Input = false;
            PlayerManager.instance.cyberwareManager.HandleCyberware(2);
        }
        else if(slot_3_Input)
        {
            slot_3_Input = false;
            PlayerManager.instance.cyberwareManager.HandleCyberware(3);
        }
    }
    private void HandleHealthRegen(float delta)
    {
        float regenedHealth = playerStats.HealthRegenRate * delta * playerStats.healthRegenSpeedMultiplier;
        playerManager.Heal(regenedHealth);
    }
    private void HandleMoveInput(float delta){

        if(playerMovement.animator.GetBool("isInteracting"))
            return;

        horizontal = movementInput.x;
        vertical = movementInput.y;
        // moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal)+Mathf.Abs(vertical));
        playerMovement.Move(movementInput);
        playerMovement.UpdateAnimatorValues(vertical, horizontal);//, playerManager.isSprinting);
        // mouseX = cameraInput.x;
        // mouseY = cameraInput.y;
    }
    private void HandlePauseInput(){

        if(esc_Input)
        {
            esc_Input = false;
            if(!uIManager.Paused)
                uIManager.OpenPauseCanvas();
            else
                uIManager.ClosePauseCanvas();
        }
    }
    private void HandleShootInput()
    {
        if(fireModeAuto)
        {
            weaponController.Shoot();
        }
        else if(fire_Input)
        {
            weaponController.Shoot();
        }
    }
    private void HandleRoll()
    {
        if(roll_Input)
        {
            roll_Input = false;
            rollFlag = true;
        }
    }
    private void HandleReload()
    {
        if(reload_Input)
        {
            reload_Input = false;

            if(weaponController.CanReload() && !PlayerManager.instance.isReloading)
            {
                StartCoroutine(PlayerManager.instance.Reload());
            }
        }
    }
    private void HandleWeaponSelection()
    {
        // if(slot_1_Input)
        // {
        //     slot_1_Input = false;
        //     if(weaponController.WeaponCount>0)
        //         weaponController.SelectWeapon(0);
        // }
        // else if(slot_2_Input)
        // {
        //     slot_2_Input = false;
        //     if(weaponController.WeaponCount>1)
        //         weaponController.SelectWeapon(1);
        // }
        // else if(slot_3_Input)
        // {
        //     slot_3_Input = false;
        //     if(weaponController.WeaponCount>2)
        //         weaponController.SelectWeapon(2);
        // }
    }
    private void HandleFiringMode()
    {
        if(firingMode_Input)
        {
            firingMode_Input = false;
            fireModeAuto = !fireModeAuto;
            weaponController.ToggleFireMode();
        }
    }
}
