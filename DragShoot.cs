using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragShoot : MonoBehaviour
{
    
    public float Power = 10f;
  
    public Slider Sli;
    public Rigidbody2D rb;
    public Vector2 MinPow;
    public Vector2 MaxPow;
   
    public Vector3 pos { get { return transform.position; } }

    Line Lt;
    Camera Cam;
    Vector2 force;
    Vector3 StartPoint;
    Vector3 EndPoint;
    Vector2 direction;
 

    PlayerVfx m_playerVFX;

    public TimeManager timeManager;

    public bool MobileEnable = false;

    public TrackPath TRack;


    Animator _animator;
    public bool Easy=false;

  
    float distance;
    float timeStamp;
    public GameObject Menu;
    public  float FaceSpeed=20f;

    Transform _transform;
    public LayerMask layerMask;
    public bool Boostup=false;
     PowerSlider pol;

    Animator Ainm;
    Animator Aini;
    
    
    public GameObject Mis;
    public LaunchProjectiles lau;
  
    private void Start()
    {
        Cam = Camera.main;
        Lt=GetComponent<Line>();
        Sli.value = Power;
        m_playerVFX = GetComponent<PlayerVfx>();
        _animator = GetComponent<Animator>();
        pol = GameObject.Find("PowerSlider").GetComponent<PowerSlider>();
        Ainm = GameObject.Find("CameraHolder").GetComponent<Animator>();
        Aini = GameObject.Find("Hidden").GetComponent<Animator>();
       
       



    }
    void Awake()
    {
        Menu.SetActive(false);
    }
   
   
    private void Update()
    {
        Power = Sli.value;
       
        if (MobileEnable)
        {
            if (Input.touchCount >= 1)
            {

                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        Time.timeScale = 1;
                        StartPoint = Cam.ScreenToWorldPoint(Input.mousePosition);
                        StartPoint.z = 15;
                    if (Easy)
                    {
                        DeactiveRb();

                    }
                       
                        m_playerVFX.ChangeDotActiveState(true);
                    }

              if(Input.touches[0].phase==TouchPhase.Moved)
                {

                    timeManager.DoSlowMotion();
                    Vector3 currentPoint = Cam.ScreenToWorldPoint(Input.mousePosition);
                    Lt.RenderLine(StartPoint, currentPoint);
                    TRack.Show();
                    OnDrag();
                    if (Easy)
                    {
                        activeRb();
                    }
                    EndPoint.z = 15;



                }
                
               if(Input.touches[0].phase==TouchPhase.Ended)
                {
                    Time.timeScale = 1;
                    EndPoint = Cam.ScreenToWorldPoint(Input.mousePosition);
                    EndPoint.z = 15;


                    force = new Vector2(Mathf.Clamp(EndPoint.x - StartPoint.x, MinPow.x, MaxPow.x), Mathf.Clamp(EndPoint.y - StartPoint.y, MinPow.y, MaxPow.y));
                    rb.AddForce(force * Power, ForceMode2D.Impulse);
                    Lt.EndLine();
                    if (Easy)
                    {
                        activeRb();
                    }
                    TRack.Hide();
                    m_playerVFX.ChangeDotActiveState(false);

                }
            }
        }
        
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
               
                if (Easy)
                {
                    DeactiveRb();
                }
                Time.timeScale = 1;
                StartPoint = Cam.ScreenToWorldPoint(Input.mousePosition);
                StartPoint.z = 15;
                if (Easy)
                {
                    activeRb();
                }
               
                
            }

           if (Input.GetMouseButton(0))
            {
               
                timeManager.DoSlowMotion();
                Face();
                Vector3 currentPoint = Cam.ScreenToWorldPoint(Input.mousePosition);
                Lt.RenderLine(StartPoint, currentPoint);
                EndPoint.z = 15;
                TRack.Show();
                OnDrag();
                if (Easy)
                {
                    activeRb();
                }




            }
            if (Input.GetMouseButtonUp(0))
            {
                Time.timeScale = 1;
                EndPoint = Cam.ScreenToWorldPoint(Input.mousePosition);
                EndPoint.z = 15;
                force = new Vector2(Mathf.Clamp(StartPoint.x - EndPoint.x, MinPow.x, MaxPow.x), Mathf.Clamp(StartPoint.y - EndPoint.y, MinPow.y, MaxPow.y));
                rb.velocity = force * Power;
                Lt.EndLine();
                TRack.Hide();
                _animator.SetBool("Throw", false);
             

            }


        }

      
    
    }
    void OnDrag()
    {
        
        _animator.SetBool("Throw",true);
        EndPoint = Cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(EndPoint,StartPoint);
        direction = (StartPoint - EndPoint).normalized;
        Vector2  Tempforce = direction*distance;
        TRack.UpdateDots(pos,Tempforce);
       
    }
   

    void Face()
    {
        Vector2 Dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, FaceSpeed * Time.deltaTime);
    }
   
   
    public void  BoostUp()
    {
        if (Boostup)
        {
            
            Ainm.SetTrigger("Boom");     
            lau.Tuk();
            PowerSlider.currentPower = 0;
            Boostup = false;
           

        }
    }

    public void Misiles()
    {
        if (PowerSlider.currentPower>=0.2f)
        {
            GameObject NMis = Instantiate(Mis, transform.position, transform.rotation);
            NMis.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * 3f * Time.deltaTime, 0);
            PowerSlider.currentPower -= 0.2f;
        }
    }
   void activeRb()
    {
       
        rb.isKinematic = false;

    }

    void DeactiveRb()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
        
    }

  public void KidMode()
    {
        Easy = !Easy;
    }
    public void Dead()
    {
        
        this.enabled = false;
        Menu.SetActive(true);

    }
 
    IEnumerator Tik()
    {
        yield return new WaitForSeconds(0.3f);
     
    }

}
