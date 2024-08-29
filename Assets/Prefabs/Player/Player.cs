using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITeamInterface
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] JoyStick aimStick;
    [SerializeField] CharacterController characterController; 
    [SerializeField] float moveSpeed = 20f; 
    [SerializeField] float maxMoveSpeed = 80f; 
    [SerializeField] float minMoveSpeed = 5f; 
    [SerializeField] float animTurnSpeed = 30f;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] int TeamID = 1;

    internal void AddMoveSpeed(float boostAmt)
    {
        moveSpeed += boostAmt;
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    [Header("Inventory")]
    [SerializeField] InventoryComponent inventoryComponent;

    [Header("HeathAndDamage")]
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] PlayerValueGauge healthBar;

    [Header("AbilityAndStamina")]
    [SerializeField] AbilityComponent abilityComponent;
    [SerializeField] PlayerValueGauge staminaBar;

    [Header("UI")]
    [SerializeField] UIManager uiManager;
    Vector2 moveInput;
    Vector2 aimInput;

    Camera mainCam;
    CameraController cameraController;
    Animator animator;

    float animatorTurnSpeed;

    public int GetTeamID()
    {
        return TeamID;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveStick.onStickValueUpdated += moveStickUpdated;
        aimStick.onStickValueUpdated += aimStickUpdated;
        aimStick.onStickTaped += StartSwichWeapon;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();
        healthComponent.onHealthChange += HealthChanged;
        healthComponent.onHealthEmpty += StartDeathSequence;
        healthComponent.BroadcastHealthValueImmeidately();

        abilityComponent.onStaminaChange += StaminaChanged;
        abilityComponent.BroadcastStaminaChangeImmedietely();
        GameplayStatics.GameStarted();
    }

    private void StaminaChanged(float newAmount, float maxAmount)
    {
        staminaBar.UpdateValue(newAmount, 0, maxAmount);
    }

    private void StartDeathSequence(GameObject Killer)
    {
        animator.SetLayerWeight(2, 1);
        animator.SetTrigger("Death");
        uiManager.SetGameplayControlEnabled(false);
    }

    private void HealthChanged(float health, float delta, float maxHealth)
    {
        healthBar.UpdateValue(health, delta, maxHealth);
    }

    public void AttackPoint()
    {
        if(inventoryComponent.HasWeapon())
        {
            inventoryComponent.GetActiveWeapon().Attack();
        }
    }
    void StartSwichWeapon()
    {
        if(inventoryComponent.HasWeapon())
        {
            animator.SetTrigger("switchWeapon");
        }
    }

    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }

    void aimStickUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
        if(inventoryComponent.HasWeapon())
        {
            if (aimInput.magnitude > 0)
            {
                animator.SetBool("attacking", true);
            }
            else
            {
                animator.SetBool("attacking", false);
            }
        } 
    }

    void moveStickUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    Vector3 StickInputToWorldDir(Vector2 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * inputVal.x + upDir * inputVal.y;
    }

    // Update is called once per frame
    void Update()
    {
        PerformMoveAndAim();
        UpdateCamera();
    }

    private void PerformMoveAndAim()
    {
        Vector3 MoveDir = StickInputToWorldDir(moveInput);

        characterController.Move(MoveDir * Time.deltaTime * moveSpeed);

        UpdateAim(MoveDir);

        float forward = Vector3.Dot(MoveDir, transform.forward);
        float right = Vector3.Dot(MoveDir, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);

        characterController.Move(Vector3.down * Time.deltaTime * 10f);
    }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 AimDir = currentMoveDir;
        if (aimInput.magnitude != 0)
        {
            AimDir = StickInputToWorldDir(aimInput);
        }
        RotateTowards(AimDir);
    }

    private void UpdateCamera()
    {
        //player is move but not aiming, and cameraController exists
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 AimDir)
    {
        float currentTurnSpeed = movementComponent.RotateTowards(AimDir);
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);

        animator.SetFloat("turnSpeed", animatorTurnSpeed);
    }

    public void DeathFinished()
    {
        uiManager.SwithToDeathMenu();
    }
}
