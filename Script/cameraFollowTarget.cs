using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//挂在target摄像机上
public class cameraFollowTarget : MonoBehaviour
{
private float foPositionY;
public Transform playerTarget;
private Vector3 velocity = Vector3.zero;
    
    void LateUpdate()
    {
        Vector3 characterViewPos = Camera.main.WorldToViewportPoint(playerTarget.position + playerTarget.GetComponent<CharacterController>().velocity * Time.deltaTime);
        //超出屏幕高度的时候跟随
        if (characterViewPos.y > 0.85f || characterViewPos.y < 0.3f)
        {
            foPositionY = playerTarget.position.y;
        }
        //角色在地面的时候跟随
        else if(playerTarget.GetComponent<CharacterController>().isGrounded)
        {
            foPositionY = playerTarget.position.y;
        }
        var desiredPosition = new Vector3(playerTarget.position.x, foPositionY, playerTarget.position.z);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, desiredPosition, ref velocity, 0.15f);
    }
}
