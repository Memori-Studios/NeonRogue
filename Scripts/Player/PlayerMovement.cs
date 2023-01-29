using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float standardSpeedModifier = 1f, sandevistanSpeedModifier = 5f;
    private float speedModifier = 1f;
    [SerializeField] private float rollCharges;
    Vector2 velocity;
    Rigidbody myRigidbody;
    PlayerManager playerManager;
    PlayerStats playerStats;
    InputHandler inputHandler;
    UIManager uIManager;
    Sandevistan sandevistan;

    public Animator animator;
    int vertical,horizontal;
    public Vector3 lookPoint;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        playerStats = GetComponent<PlayerStats>();
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponent<Animator>();
        sandevistan = GetComponent<Sandevistan>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }
    private void Start()
    {
        uIManager = PlayerManager.instance.uIManager; 
    }
    public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("canRotate", canRotate);
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0.2f);
    }
    public void Move(Vector2 _velocity)
    {
        velocity = _velocity;
    }
    private void FixedUpdate()
    {
        // speedModifier = sandevistan.sandevistanActive ? sandevistanSpeedModifier : standardSpeedModifier;
        // Vector3 horizontal = (Vector3.right * velocity.x) + (Vector3.forward * velocity.y);
        myRigidbody.MovePosition(myRigidbody.position + new Vector3(velocity.x,0,velocity.y) * Time.fixedDeltaTime * standardSpeedModifier * playerStats.MoveSpeed);
    }
    public void RechargeRollCharges(float delta)
    {
        // Recharges roll charges
        rollCharges += playerStats.RollChargeRate*delta;
        rollCharges = Mathf.Clamp(rollCharges, 0, playerStats.RollCharges);
        uIManager.DisplayRollCharge(rollCharges);
    }
    public void HandleRolling()
    {
        if(inputHandler.rollFlag)
        {
            if(rollCharges < 1)
                return;

            rollCharges--;
            uIManager.DisplayRollCharge(rollCharges);
            Vector3 movementDirection = new Vector3 (inputHandler.horizontal, 0f, inputHandler.vertical);
            Quaternion rollRotation = Quaternion.identity;
            if(movementDirection != Vector3.zero)
            {
                rollRotation = Quaternion.LookRotation(movementDirection);
                this.transform.rotation = rollRotation;
            }

            animator.SetBool("invulnerable", true);
            PlayTargetAnimation("Rolling", true);
        }
    }
    public void LookAt(Vector3 _lookPoint)
    {
        if(!animator.GetBool("canRotate"))
            return;

        lookPoint = _lookPoint;
        heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }
    Vector3 heightCorrectedPoint;
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(heightCorrectedPoint, 1);
    }
    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)//, bool isSprinting)
    {
        Vector3 axisVector = new Vector3(horizontalMovement,0,verticalMovement);
        
        if (axisVector.magnitude > 0) 
        {
            Vector3 normalizedLookingAt = lookPoint - transform.position;
            normalizedLookingAt.Normalize ();
            verticalMovement = Mathf.Clamp (Vector3.Dot (axisVector, normalizedLookingAt), -1, 1);

            Vector3 perpendicularLookingAt = new Vector3 (normalizedLookingAt.z, 0, -normalizedLookingAt.x);
            horizontalMovement = Mathf.Clamp (Vector3.Dot (axisVector, perpendicularLookingAt), -1, 1);

            // animator.SetBool("IsMoving", true);

        } else
        {
            animator.SetFloat(vertical, 0, 0.1f, Time.deltaTime);
            animator.SetFloat(horizontal, 0, 0.1f, Time.deltaTime);
        }

        // #region Vertical
        // float v = 0;
        // if (verticalMovement>0 && verticalMovement < 0.55f){
        //     v = 0.5f;
        // }
        // else if(verticalMovement>0.55f){
        //     v=1;
        // }
        // else if (verticalMovement<0&& verticalMovement> -0.55f){
        //     v = -0.5f;
        // }
        // else if (verticalMovement< -0.55f){
        //     v = -1;
        // }
        // else{
        //     v=0;
        // }
        // #endregion

        // #region Horizontal
        // float h = 0;
        // if (horizontalMovement>0 && horizontalMovement < 0.55f){
        //     h = 0.5f;
        // }
        // else if(horizontalMovement>0.55f){
        //     h=1;
        // }
        // else if (horizontalMovement<0&& horizontalMovement> -0.55f){
        //     v = -0.5f;
        // }
        // else if (horizontalMovement< -0.55f){
        //     h = -1;
        // }
        // else{
        //     h=0;
        // }
        // #endregion

        // if(isSprinting){
        //     v = 2;
        //     h = horizontalMovement;
        // }
        // Vector3 localmove = transform.InverseTransformDirection();
        // XMovement = localmove.x;
        // ZMovement = localmove.z;

        

        // update the animator parameters
        // animator.SetFloat ("Forward", forwardBackwardsMagnitude);
        // animator.SetFloat ("Right", rightLeftMagnitude);

        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(horizontal, horizontalMovement, 0.1f, Time.deltaTime);
    }
    public float forwardBackwardsMagnitude, rightLeftMagnitude;
}
