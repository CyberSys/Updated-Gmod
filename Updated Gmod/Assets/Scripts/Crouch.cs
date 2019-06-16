using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public CharacterController characterController;
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            characterController.height = 1f;
        }
        else
        {
            characterController.height = 2f;
        }
    }
}
