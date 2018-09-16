using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    public Transform player; //把主角拖进去
    TextMesh number; //肢节数量
    public GameObject body; //肢节预制体拖进去
    List<Transform> balls = new List<Transform>(); //肢节集合
    void Start()
    {
        balls.Add(player); //将头添加进集合
        number = player.GetComponentInChildren<TextMesh>();
    }
    void Update()
    {
        Swing();
        number.text = (balls.Count - 1).ToString();
    }
    void Swing() //摆动
    {
        float speed = Time.deltaTime * 25; //摆动速度
        for (int i = 1; i < balls.Count; i++)
        {
            Vector2 sp = balls[i].position; //当前肢节的位置
            Vector2 BroPos = balls[i - 1].position; //上一个肢节的位置
            balls[i].position = Vector2.Lerp(sp, BroPos + Vector2.down * 0.7f, speed);
        }
    }
    public void AddBody() //增加肢节
    {
        GameObject newBody = Instantiate(body, player.position, Quaternion.identity); //生成肢节
        balls.Add(newBody.transform); //添加进集合
        newBody.transform.SetParent(transform); //设为脚本物体的子物体
    }
    public void RemoveBody() //将肢节从数组移除并删除
    {
        if (balls.Count > 1) //有肢节时才删除
        {
            Destroy(balls[balls.Count - 1].gameObject);
            balls.Remove(balls[balls.Count - 1]);
        }
        else
            Death();
    }
    void Death()
    {
        Time.timeScale = 0;
    }
}
