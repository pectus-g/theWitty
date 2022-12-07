
using UnityEngine;
//get back this script and make it quaternion.lerp or slerp
public class CameraRotate : MonoBehaviour
{
    public float Speed = 5;
    
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButton(1))
        {
             
       transform.eulerAngles += Speed * new Vector3( 0,Input.GetAxis("Mouse X"),0) ; 
     // Quaternion rotation = Quaternion.Euler(0, Input.GetAxis("Mouse X"), 0);

     
        }
    }
}
