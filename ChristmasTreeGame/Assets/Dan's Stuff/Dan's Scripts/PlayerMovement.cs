using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 moveValue;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    [SerializeField] private float smoothInputSpeed = 0.2f;

    private bool jumped = false;

    [SerializeField] private float rotationSpeed = 0.1f;

    [Header("Grab variables")]

    public bool grabedObject = false;

    public bool canMove = true;

    public bool interactPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

    }

  

    public void OnMove(InputAction.CallbackContext context)
    {
        /*
        if (GameSystem.Instance.CurrentGameState == GameSystem.GameStates.GamePlay)
        {

        }
        */


        //movementInput = value;
        movementInput = context.ReadValue<Vector2>();
        movementInput = Vector2.SmoothDamp(movementInput, currentInputVector, ref smoothInputVelocity, smoothInputSpeed);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        /*
        if (GameSystem.Instance.CurrentGameState == GameSystem.GameStates.GamePlay)
        {

        }
        */
        jumped = context.action.triggered;
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        /*
        if (GameSystem.Instance.CurrentGameState == GameSystem.GameStates.GamePlay)
        {

        }
        */
        grabedObject = context.action.triggered;

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        /*
        if (GameSystem.Instance.CurrentGameState == GameSystem.GameStates.GamePlay)
        {

        }
        */
        interactPressed = context.action.triggered;

    }

    public void OnStart(InputAction.CallbackContext context)
    {
        GameSystem.Instance.StartButtonPressed();
    }


    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        if (canMove)
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
        else if (!canMove)
        {
            StartCoroutine(ResumeMove());
        }

        if(move.sqrMagnitude > 0.1f)
        {
            gameObject.transform.LookAt(transform.position + move);


        }

        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }



        

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private IEnumerator ResumeMove()
    {
        yield return new WaitForSeconds(1f);

        canMove = true;
    }
    

    
}
