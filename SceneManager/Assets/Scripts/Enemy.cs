﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int number; //缩短数量
    TextMesh text; //自身数字
    float timer; //计时器
    BodyManager bm; //声明肢节管理器脚本
    Player p; //声明主角脚本
    void OnEnable()
    {
        //来一个随机数字,获取子物体数字
        number = Random.Range(1, 50);
        text = GetComponentInChildren<TextMesh>();

        bm = FindObjectOfType<BodyManager>(); //获取肢体管理器脚本

        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.HSVToRGB(0.65f, 1.2f, 1)); //随机发光
    }
    void Update()
    {
        
        //实时将随机数字显示
        text.text = number.ToString("0");
        if (number < 1) //数字小于1(为0),删除自身
        {
            Destroy(gameObject);
            p.bump = false; //接触主角的碰壁状态
        }
    }

    private void OnCollisionStay2D(Collision2D player) //碰撞中持续调用
    {
        if (Bump(player.transform)) //如果碰上,缩短
            Shorten();
        else        
            timer = 0; //默认为0      
    }
    bool Bump(Transform player) //根据主角和敌人的位置判断是否撞上
    {
        //获取主角和他的脚本
        Transform pt = player;
        p = pt.GetComponent<Player>();

        //获取双方碰撞器大小
        Vector3 es = GetComponent<BoxCollider2D>().bounds.size; //获取自身碰撞器大小

        //比较x轴上的偏差,如果x轴距离小于碰撞器厚度,判断成立
        float x_dis = Mathf.Abs(transform.position.x - pt.position.x); //获取双方x上距离
        float x_off = (p.size.x + es.x) / 2; //获取双方横向尺寸相加的一半

        //比较Y轴上的偏差,如果y轴距离约定于碰撞器厚度,判断成立
        float y_dis = Mathf.Abs(transform.position.y - pt.position.y); //获取双方y轴上距离
        float y_off = (p.size.y + es.y) / 2; //获取双方纵向尺寸相加的一半

        //如果x轴和y轴的位置判断都成立,且主角在敌人位置之下,判断为撞上
        p.bump = x_dis < x_off && y_dis - y_off < 0.1f && pt.position.y < transform.position.y;
        return p.bump;
    }

    void Shorten()//缩短
    {
        timer += Time.deltaTime;
        if (timer > 0)
        {
            bm.RemoveBody(); //调用肢节管理器的移除方法
            number -= 1; //自身数字-1           
            timer = -0.05f; //没0.05秒调用一次           
        }
    }
}
