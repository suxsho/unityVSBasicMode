using UnityEngine;
using Unity.VisualScripting;



[Inspectable]
public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform camaraMainTransform;

    Vector3 velocity;
    [HideInInspector]
    public bool isGrounded;
    

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
            velocity.y = -2f;
         }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

}


