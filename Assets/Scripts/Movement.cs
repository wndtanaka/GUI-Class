using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//Character Movement
[AddComponentMenu("Character Set Up/Character Movement")]
//This script requires the component Character controller
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    #region Variables
    [Header("Characters MoveDirection")]
    public Vector3 moveDir = Vector3.zero;
    private CharacterController charC;

    [Header("Character Variables")]
    public float jumpSpeed = 8f;
    public float speed = 6f;
    public float gravity = 20f;

    #endregion
    #region Start
    void Start()
    {
        charC = this.GetComponent<CharacterController>();
    }
    #endregion
    #region Update
    void Update()
    {
        if (charC.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }
        }
        moveDir.y -= gravity * Time.deltaTime;
        charC.Move(moveDir * Time.deltaTime);
    }
    #endregion
}










