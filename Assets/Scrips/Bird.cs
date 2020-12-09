using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    public Transform rightPos;//右边弹弓画线的位置
    public Transform leftPos;
    public float maxDis = 1.5f;
    public static GameManager _instiate;
   

    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rigidbody;
    

    public LineRenderer rightline;
    public LineRenderer leftline;
    public GameObject boom;
    public float smooth = 3;
    
    public Sprite hurt;//受伤时的图片转换
    protected SpriteRenderer render;

    public AudioClip flyClip;
    public AudioClip selectClip;
    //public AudioClip collisionClip;

    private bool isFly=false;

    private bool isClick = false;
    protected TestMyTrail testtrial;

    [HideInInspector]
    public bool canMove=true;
    public bool isRealsed = false;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        testtrial = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
       
        
    }
    private void OnMouseDown()//鼠标按下
    {
        
        if (canMove)
        {
            Audioclap(selectClip);
            isClick = true;
            rigidbody.isKinematic = true;
        }
        

    }
    // Use this for initialization
    private void OnMouseUp()//鼠标弹起
    {
        if (canMove)
        {
            isClick = false;
            rigidbody.isKinematic = false;
            Invoke("Fly", 0.1f);//延迟0.1f
            //禁用划线
            leftline.enabled = false;
            rightline.enabled = false;
            canMove = false;
        }

       
    }

    private void Update()
    {

        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }
            Line();


        }

        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 2, 15),
            Camera.main.transform.position.y, Camera.main.transform.position.z), smooth * Time.deltaTime);

        //鼠标左键按下速度提升
        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }

    }
    void Fly()
    {
        isFly = true;
        Audioclap(flyClip);
        testtrial.StartTrails();
        sp.enabled = false;
        Invoke("Next", 5);
    }

    public virtual void Next()
    {
        GameManager._initiated.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._initiated.Nextfly();

    }
    void Line()//画出弹弓的线
    {
        leftline.enabled = true;
        rightline.enabled = true;
        leftline.SetPosition(0, leftPos.position);
        leftline.SetPosition(1, transform.position);

        rightline.SetPosition(0, rightPos.position);
        rightline.SetPosition(1, transform.position);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        testtrial.ClearTrails();
    }

    public void Audioclap(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public virtual void ShowSkill()
    {
        isFly = false;
    }

    public void Hurt()
    {
        render.sprite = hurt;
    }

}