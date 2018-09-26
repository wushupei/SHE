using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //挂主角
{
    Rigidbody2D r2d; //刚体   
    public Vector2 size; //声明自身大小
    public ParticleSystem hitEffect; //撞击特效
    public ParticleSystem deathEffect; //死亡特效
    AudioSource hitAudio; //撞击声音
    public GameObject menu; //菜单

    float y_speed, x_speed; //声明x和y轴的速度
    public bool bump; //是否撞到东西
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        size = GetComponent<BoxCollider2D>().bounds.size;
        hitAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (bump == false) //没撞到东西时
        {
            y_speed += Time.deltaTime * (5 + transform.position.y / 100);
            //停止特效和声音
            hitEffect.Stop();
            hitAudio.Stop();
        }
        else
        {
            //播放特效和声音
            hitEffect.Play();
            if (!hitAudio.isPlaying)
                hitAudio.Play();
        }
        if (r2d)
            PlayerMove();
    }
    void PlayerMove() //移动方法
    {
        //x轴速度随鼠标移动
        x_speed = Mathf.Lerp(x_speed, Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Time.deltaTime * 10);
        r2d.MovePosition(new Vector2(x_speed, y_speed));
    }
    public void Death() //死亡方法
    {
        Destroy(r2d); //移除刚体
        bump = false; //解除碰撞状态
        Instantiate(deathEffect, transform.position, Quaternion.identity); //在此生成爆炸特效
        FindObjectOfType<AudioManager>().PlayAudio("D"); //播放死亡声音
        Destroy(GetComponent<SpriteRenderer>()); //销毁自身精灵组件       
        menu.SetActive(true); //启动菜单
    }
}
