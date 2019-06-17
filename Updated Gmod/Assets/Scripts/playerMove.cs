using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;
	private float setSpeed;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private bool isJumping;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
		setSpeed = movementSpeed;
    }

    private void Update()
    {
        PlayerMovement();
    }

    //Calls functions and basic character movements.
    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        JumpInput();

        Running();

        Crouching();
    }

    //Checks for Space Bar press.
    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    // Calculates jumping if available.
    private IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f;

        do
        {
            float jumpforce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpforce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;

            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }

    //This checks if Player is walking and is not in the air and applies more speed.
    private void Running()
    {
        if (movementSpeed == setSpeed && !isJumping == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                movementSpeed = setSpeed * 2;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                movementSpeed = setSpeed;
            }
        }
    }

    //This checks if Player speed is >= 200 and is not jumping then slows the Player accordingly.
    private void Crouching()
    {
        if (movementSpeed >= setSpeed && !isJumping == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
				charController.height = 1f;
                movementSpeed = setSpeed/4;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
				charController.height = 2f;
                movementSpeed = setSpeed;
            }
        }
    }
}
