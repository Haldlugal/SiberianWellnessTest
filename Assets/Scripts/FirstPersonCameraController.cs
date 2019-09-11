using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{      
    public float rotateSpeed;    
    public Vector3 thirdPersonOffset;
    public Vector3 topDownOffset;
    
    public Transform target;
    public Transform pivot;
    public Transform targetBody;
    private Vector3 offset;
    private string cameraType="thirdPerson";  
    
    void Start()
    {
        offset = thirdPersonOffset;
        pivot.transform.position = target.position;        
        pivot.transform.parent = null;
    }

    void LateUpdate()
    {
        if (!GameManager.paused)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (cameraType == "thirdPerson")
                {
                    cameraType = "topDown";
                    offset = topDownOffset;
                }
                else if (cameraType == "topDown")
                {
                    cameraType = "thirdPerson";
                    pivot.rotation = targetBody.rotation;                    
                    offset = thirdPersonOffset;
                }
            }


            switch (cameraType)
            {
                case "thirdPerson":
                    {
                        pivot.transform.position = target.transform.position;
                        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
                        pivot.Rotate(0, horizontal, 0);                        

                        float desiredYAngle = pivot.eulerAngles.y;
                        
                        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
                        transform.position = target.position - (rotation * offset);

                        if (transform.position.y < target.position.y)
                        {
                            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
                        }
                        break;
                    }
                case "topDown":
                    {
                        transform.position = target.position - offset;
                        break;
                    }

            }
            transform.LookAt(target);
        }
    }
}
