using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    public bool ZoomActive;


    public Camera Cam;
   public  bool MobileEnable = false;
  
 
    public float Speed;
    public float ReSpeed;

    void Start()
    {
        Cam = Camera.main;
    }

  
    void Update()
    {
        if(ZoomActive)
        {
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 25, Speed);
          
            
            
        }
        else
        {
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 7, ReSpeed);
         
           
        }
        
    }
    void LateUpdate()
    {
        if (MobileEnable)
        {
            if (Input.touchCount >= 1)
            {
                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    ZoomActive = true;

                }
                else
                {
                    ZoomActive = false;

                }

            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                ZoomActive = true;

            }
            else
            {
                ZoomActive = false;

            }

        }

    } 

}
