using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float maxSpeed = 10;
    public float minSpeed = 5;
    public GameObject boom;
    public GameObject score;
    public bool isPig;

    public Sprite render;//小猪受伤害后变换的图片

    public AudioClip collisionClip;
    public AudioClip deadClip;
    public AudioClip birdcollisionClip;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Audioclap(birdcollisionClip);
            collision.transform.GetComponent<Bird>().Hurt();
        }
            if (collision.relativeVelocity.magnitude > maxSpeed)//获取碰撞产生的相对速度的大小进行判断
            {
                Dead();
            }
            else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
            {
                sr.sprite = render;
                Audioclap(collisionClip);
            }

        

    }
    public void Dead()//判断小猪的死亡，销毁
    {
        if (isPig)
        {
            GameManager._initiated.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject go = Instantiate(score, transform.position + new Vector3(0, 1, 0), Quaternion.identity);//创建分数对象
        Destroy(go, 1.5f);//分数1.5秒后消失
        Audioclap(deadClip);
    }

    public void Audioclap(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}
