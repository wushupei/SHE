using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyManager : MonoBehaviour
{
    public Transform player; //把主角拖进去
    public GameObject body; //肢节预制体拖进去   
    List<Transform> bodys = new List<Transform>(); //存放所有肢节
    TextMesh number; //肢节数量文本

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
            bodys[0].position = new Vector2(Mathf.Lerp(bodys[0].position.x, p.x, speed), p.y - 1); //第一节跟着主角混 
            Vector3 dir = p - bodys[0].position; //到上一节的方向
            bodys[0].rotation = Quaternion.FromToRotation(Vector2.up, dir);

            //下一节跟着上一节混
            for (int i = 1; i < bodys.Count; i++)
            {
                Vector2 sp = bodys[i].position; //当前肢节的位置
                Vector2 lp = bodys[i - 1].position; //上一个肢节的位置      
                bodys[i].position = new Vector2(Mathf.Lerp(sp.x, lp.x, speed), lp.y - 0.7f);

                Vector2 d = lp - sp; //到上一节的方向
                bodys[i].rotation = Quaternion.FromToRotation(Vector2.up, d);
            }
        }
    }
    public void AddBody() //增加肢节
    {
        //将新的肢节生成在最后一个肢节下,如果没有肢节就生成在主角下,并添加进集合
        GameObject newBody = Instantiate(body,
            (bodys.Count == 0 ? player.position : bodys[bodys.Count - 1].position)
            + Vector3.down * 0.7f, Quaternion.identity);

        bodys.Add(newBody.transform); //加入集合
        newBody.transform.SetParent(transform); //成为脚本物体的子物体
        number.text = (bodys.Count).ToString();
    }
    public void RemoveBody() //将肢节从数组移除并删除
    {
        if (bodys.Count > 0) //有肢节时才删除
        {
            Destroy(bodys[bodys.Count - 1].gameObject); //从子物体移除 
            bodys.Remove(bodys[bodys.Count - 1]);
            sm.ShowScore(++score, sprites);
            number.text = (bodys.Count).ToString();
        }
        else
        {
            SaveTopScor(); //储存最高分
            player.GetComponent<Player>().Death(); //死亡
            number.text = "";
        }
    }
    void SaveTopScor() //保存最高分
    {
        if (PlayerPrefs.HasKey("最高分")) //获取最高分
            topScore = PlayerPrefs.GetInt("最高分");
        if (score > topScore) //刷新最高分
        {
            topScore = score;
            PlayerPrefs.SetInt("最高分", topScore);
        }
    }
}
