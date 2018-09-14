using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //挂主角
{
    Rigidbody2D r2d;
    float y_speed, x_speed; //声明x和y轴的速度
    public bool bump; //是否撞到东西
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (bump == false) //没撞到东西时
            y_speed += Time.deltaTime;
        else
            y_speed = transform.position.y;

    }
    void FixedUpdate()
    {
        PlayerMove();
        print(r2d.velocity.y);
    }
    void PlayerMove() //移动方法
    {
        //x轴速度随鼠标移动
        Vector2 mp = Input.mousePosition;
        if (mp.x >= 0 && mp.x <= Screen.width)
            x_speed = Camera.main.ScreenToWorldPoint(mp).x;
        else if (mp.x < 0)
            x_speed = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        else
            x_speed = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        r2d.MovePosition(new Vector2(x_speed, y_speed));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform obj = collision.transform;
        if (obj.tag == "Enemy") //如果撞到敌人
        {
            //获取双方碰撞器大小
            Vector3 ps = GetComponent<BoxCollider>().bounds.size; //获取主角碰撞器大小
            Vector3 es = obj.GetComponent<BoxCollider>().bounds.size; //获取敌人碰撞器大小

            //比较x轴上的偏差,如果x轴距离小于碰撞器厚度,判断成立
            float off = (ps.x + ps.x) / 2; //获取双方横向尺寸相加的一半
            float dis = Mathf.Abs(transform.position.x - collision.transform.position.x); //获取双方x上距离

            //比较Y轴上距离

        }
        print("碰撞中");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //FindObjectOfType<Player>().bump = false;
        print("离开碰撞");
    }
}
