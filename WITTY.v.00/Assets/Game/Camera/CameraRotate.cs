
using UnityEngine;
//get back this script and make it quaternion.lerp or slerp
public class CameraRotate : MonoBehaviour
{
    public float Speed = 20;
    private float pitch = 0.0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(Input.GetMouseButton(1))
        {
           // pitch = Mathf.Clamp(Input.GetAxis("Mouse Y"), 10f, 60f);
             float Xaxis=Input.GetAxis("Mouse X");
             float Yaxis=Input.GetAxis("Mouse Y");
        float clampY= Mathf.Clamp(Yaxis,-0.2f,0.2f);

          
       transform.eulerAngles += Speed * new Vector3( 0,Xaxis,0) ; 
       
  // transform.eulerAngles += Speed * new Vector3( clampY,0,0) ; 
     // Quaternion rotation = Quaternion.Euler(0, Input.GetAxis("Mouse X"), 0);

     
        }
    }
}
