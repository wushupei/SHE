using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //挂主角
{
    Rigidbody2D r2d;
    float y_speed, x_speed; //声明x和y轴的速度
    public bool bump; //是否撞到东西
    public Vector2 size; //声明自身大小
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        size = GetComponent<BoxCollider2D>().bounds.size;
    }
    void Update()
    {
        if (bump == false) //没撞到东西时
            y_speed += Time.deltaTime * 5;

        PlayerMove();
    }
    void PlayerMove() //移动方法
    {
        //x轴速度随鼠标移动
        x_speed =Mathf.Lerp(x_speed, Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Time.deltaTime*10);
        r2d.MovePosition(new Vector2(x_speed, y_speed));
    }
}
