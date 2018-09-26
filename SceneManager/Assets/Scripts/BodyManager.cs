using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyManager : MonoBehaviour
{
    public Transform player; //把主角拖进去
    public GameObject body; //肢节预制体拖进去   
    List<Transform> bodys = new List<Transform>(); //存放所有肢节
    TextMesh number; //肢节数量文本显示

    ScoreManager sm; //声明分数管理器
    public Image[] sprites; //当前分数图片
    int score; //当前分数
    int topScore; //历史最高分
    void Start()
    {
        sm = new ScoreManager("Sprites/Number1"); //加载该组精灵图片
        number = player.GetComponentInChildren<TextMesh>();
    }
    void Update()
    {
        Swing();
    }
    void Swing() //摆动
    {
        if (bodys.Count > 0) //如果有肢节存在
        {
            float speed = Time.deltaTime * 15; //摆动速度
            Vector3 p = player.position; //主角的位置
            bodys[0].position = new Vector3(Mathf.Lerp(bodys[0].position.x, p.x, speed), p.y - 1, p.z); //第一节跟着主角混 
            bodys[0].rotation = Quaternion.FromToRotation(Vector2.up, p - bodys[0].position); //面向自身到主角方向

            //下一节跟着上一节混
            for (int i = 1; i < bodys.Count; i++)
            {
                Vector3 sp = bodys[i].position; //当前肢节的位置
                Vector3 lp = bodys[i - 1].position; //上一个肢节的位置      
                bodys[i].position = new Vector3(Mathf.Lerp(sp.x, lp.x, speed), lp.y - 0.7f, lp.z);
                bodys[i].rotation = Quaternion.FromToRotation(Vector2.up, lp - sp); //面向自身到上一节方向
            }
        }
    }
    public void AddBody() //增加肢节
    {
        //根据肢节数量获取生成位置,如果没有肢节就获取主角位置
        Vector3 pos = (bodys.Count > 0 ? bodys[bodys.Count - 1] : player).position ;
        //在该位置下方生成肢节
        GameObject newBody = Instantiate(body, pos + Vector3.down * 0.7f, Quaternion.identity);

        bodys.Add(newBody.transform); //加入集合
        newBody.transform.SetParent(transform); //成为脚本物体的子物体
        number.text = (bodys.Count).ToString(); //显示数量
    }
    public void RemoveBody() //将肢节从数组移除并删除
    {
        if (bodys.Count > 0) //有肢节时才删除
        {
            Destroy(bodys[bodys.Count - 1].gameObject); //从数组中最后开始删除 
            bodys.Remove(bodys[bodys.Count - 1]); //从数组移除其索引
            sm.ShowScore(++score, sprites); //加分并显示
            number.text = (bodys.Count).ToString(); //显示数量
        }
        else
        {
            sm.SaveTopScor(score); //保存最高分
            player.GetComponent<Player>().Death(); //死亡
            number.text = "";
        }
    }   
}
