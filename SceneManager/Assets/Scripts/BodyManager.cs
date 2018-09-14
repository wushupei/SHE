using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    public Transform player; //把主角拖进去
    public GameObject body; //肢节预制体拖进去
    List<Transform> balls=new List<Transform>(); //肢节集合
    void Start()
    {

    }
    void Update()
    {
        if (balls.Count > 0) //如果有肢节了就跟随身体摆动
            Swing();
    }
    void Swing() //摆动
    {
        float speed = Time.deltaTime * 25;
        balls[0].position = Vector2.Lerp(balls[0].position, player.position + Vector3.down, speed);
        for (int i = 1; i < balls.Count; i++)
        {
            Vector2 sp = balls[i].position; //当前肢节的位置
            Vector2 BroPos = balls[i - 1].position; //兄肢节的位置
            balls[i].position = Vector2.Lerp(sp, BroPos + Vector2.down, speed);
        }
    }
    public void AddBody() //增加肢节
    {
        GameObject newBody = Instantiate(body, player.position, Quaternion.identity); //生成肢节
        balls.Add(newBody.transform); //添加进集合
        newBody.transform.SetParent(transform); //设为脚本物体的子物体
    }
}
