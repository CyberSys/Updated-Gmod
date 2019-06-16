using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private bool isJumping;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    //Calls functions and basic character movements.
    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed * Time.deltaTime;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed * Time.deltaTime;

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
        if (movementSpeed == 200f && !isJumping == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                movementSpeed = 600f;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                movementSpeed = 200f;
            }
        }
    }

    //This checks if Player speed is >= 200 and is not jumping then slows the Player accordingly.
    private void Crouching()
    {
        if (movementSpeed >= 200f && !isJumping == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                movementSpeed = 50f;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                movementSpeed = 200f;
            }
        }
    }
}
