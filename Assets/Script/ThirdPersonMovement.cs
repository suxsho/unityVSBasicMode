using UnityEngine;
using Unity.VisualScripting;



[Inspectable]
public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    private Transform camaraMainTransform;

    public void Start()
    {
        controller = GetComponent<CharacterController>();
        camaraMainTransform = Camera.main.transform;
    }

    public void characterMove(Vector2 InputMove,float Speed,float rotateSpeed,float rotateTime = 0.1f)
    {
        //移动
        Vector3 direction = new Vector3(InputMove.x, 0f, InputMove.y).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camaraMainTransform.eulerAngles.y;
            //旋转
            Quaternion rotation = Quaternion.Euler(0,targetAngle,0f);
            transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime * rotateSpeed);
            //前进
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * Speed * Time.deltaTime);
        }
    }
    // TODO: 跳跃功能封装
    //重力
    public void GravitySystem()
    {
        //当角色碰到地面的时候无重力
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}


