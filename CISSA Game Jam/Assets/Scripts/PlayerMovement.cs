using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public bool NoInput = false;

    [SerializeField] private GameObject PlayerObj; //this is where we set the OBJECT of the player
    [SerializeField] private Rigidbody2D PlayerRb; //this is the rigid body of the player
    [SerializeField] private Animator PlayerAnimator;

    [SerializeField] private Transform LookTransform;

    public float moveSpeed = 1.0f; //public incase we want to add things that slow or speed up the player in other scripts
    
    private Vector2 inputMoveDirection;
    private Vector3 inputLookDirection;
    //animations parameters
    private bool animUpState, animDownState, animRightState;
    private float animMoveMultiplier;

    //you can add more actions here
    private InputAction moveAction;
    private InputAction lookAction;

    //the following is for the airlock room
    private Rigidbody2D rb;
    public Transform suctionPoint;
    private float forceAmount = 50.0f; // The force added to the player in the direction of the suction point (airlock room)
    public bool suctionForce = true;

    private void Start()
    {
        //these are set in [edit > project settings > input system package]
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        //Gets Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

    }

    private void AnimatorParameterUpdate()
    {
        PlayerAnimator.SetBool("Up",animUpState);
        PlayerAnimator.SetBool("Down",animDownState);
        PlayerAnimator.SetBool("Right",animRightState);
        
        animUpState = false;
        animDownState = false;
        animRightState = false;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void TakeInput()
    {
        
        inputMoveDirection =  moveAction.ReadValue<Vector2>().normalized;
        Vector3 pointerPos = Camera.main.ScreenToWorldPoint(lookAction.ReadValue<Vector2>());
        inputLookDirection = (Vector2)(pointerPos - transform.position ).normalized;
        if (NoInput)
        {
            inputMoveDirection = Vector2.zero;
            inputLookDirection = Vector2.zero;
        }

    }

    private void PlayerAim()
    {
        transform.localScale = new Vector3((Mathf.Sign(inputLookDirection.x)), 1f, 1f);
        LookTransform.right = inputLookDirection * (Mathf.Sign(inputLookDirection.x));
        //UPDATE PLAYER FACING BASED ON MOUSE AIMING
        if (Vector2.Angle(inputLookDirection,Vector2.up) < 45f)
        {
            animUpState = true;
        }
        else if(45f <= Vector2.Angle(inputLookDirection, Vector2.up) && Vector2.Angle(inputLookDirection, Vector2.up) < 135f)
        {
            animRightState=true;
        }
        else
        {
            animDownState=true;
        }
        //if the angle between where you are looking to where you want to walk is greater than 90deg then play animation in reverse;
        if (inputMoveDirection.magnitude != 0)
        {
            PlayerAnimator.SetBool("Walking", true);
            PlayerAnimator.SetFloat("Move Multiplier", inputMoveDirection.magnitude);
            if (Vector2.Angle(inputLookDirection,inputMoveDirection) < 90f)
            {
                PlayerAnimator.SetFloat("Move Multiplier", inputMoveDirection.magnitude*-1f);
            }
        }
        else
        {
            PlayerAnimator.SetBool("Walking", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        PlayerAim();
        AnimatorParameterUpdate();
    }

    // Fixed Update is called multiple times in a frame
    // IF ITS NOT USING UNITY PHYSICS DON'T PUT IT HERE
    private void FixedUpdate()
    {
        PlayerRb.linearVelocity = inputMoveDirection*moveSpeed;

        if (SceneManager.GetActiveScene().name == "Air Lock Room" && suctionForce)
        {
            Vector2 direction = (suctionPoint.position - transform.position).normalized;
            rb.AddForce(direction * forceAmount);
        }
    }
}
