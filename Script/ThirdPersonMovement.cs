using UnityEngine;
using Ludiq;  //2020用这个
//using Unity.VisualScripting; //2021用这个



[RenamedFrom("角色功能")]
public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform camaraMainTransform;

    Vector3 velocity;
    [HideInInspector]
    public bool isGrounded;
    

    public void Start()
    {
        if(GetComponent<CharacterController>() != null)
        controller = GetComponent<CharacterController>();
        camaraMainTransform = Camera.main.transform;
    }
    [RenamedFrom("角色移动")]
    public void characterMove(float x,float y,float Speed,float rotateSpeed)
    {
        //移动
        Vector3 direction = new Vector3(x, 0f, y).normalized;

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
    //跳跃
    public void characterJump(float jumpHeight = 3,float gravity = -9.81f)
    {
        if(isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

    }
    //重力
    public void GravitySystem(Transform groundCheck,LayerMask groundMask,float gravity = -9.81f,float groundDistance = 0.4f)
    {
         isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
         if (isGrounded && velocity.y < 0)
         {
            velocity.y = -5f;
         }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

}


