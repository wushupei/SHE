using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int hp; //血量
    TextMesh text; //血量显示文本
    float timer; //计时器 
    BodyManager bm; //声明肢节管理器脚本
    Player p; //声明主角脚本

    Material material; //自身材质
    float h = 0.05f; //颜色的H值
    void OnEnable()
    {
        //来一个随机数字作为血量并显示
        hp = Random.Range(1, 61);
        text = GetComponentInChildren<TextMesh>();
        text.text = hp.ToString();

        bm = FindObjectOfType<BodyManager>(); //获取肢体管理器脚本
        p = FindObjectOfType<Player>(); //主角脚本

        material = GetComponent<Renderer>().material;
        RandomColor(hp);
    }
    void RandomColor(int number) //根据初始血量发不同的光
    {
        if (number < 11)
            h = 0.8f;
        else if (number < 21)
            h = 0.6f;
        else if (number < 31)
            h = 0.45f;
        else if (number < 41)
            h = 0.3f;
        else if (number < 51)
            h = 0.2f;      
        material.SetColor("_EmissionColor", Color.HSVToRGB(h, 1.2f, 1)); //发光
    }
    private void OnCollisionStay2D(Collision2D player) //碰撞中持续调用
    {
        if (Bump(player.transform)) //如果碰上
        {
            Shorten(); //缩短
            material.SetColor("_EmissionColor", Color.HSVToRGB(Random.value, 1.2f, 1)); //闪光
        }
        else
            material.SetColor("_EmissionColor", Color.HSVToRGB(h, 1.2f, 1)); //显示最初颜色

        if (hp < 1) //数字小于1(为0),删除自身
        {
            Destroy(gameObject);
            //播放特效和声音
            Instantiate(Resources.Load("粒子/敌人爆炸/EnemyBoom"),transform.position,Quaternion.identity);
            FindObjectOfType<AudioManager>().PlayAudio("E"); 
            p.bump = false; //解除主角的碰壁状态
        }
    }
    bool Bump(Transform player) //根据主角和敌人的位置判断是否撞上
    {
        //获取自身碰撞器大小
        Vector3 es = GetComponent<BoxCollider2D>().bounds.size; //获取自身碰撞器大小

        //比较x轴上的偏差,如果x轴距离小于碰撞器厚度,判断成立
        float x_dis = Mathf.Abs(transform.position.x - player.position.x); //获取双方x上距离
        float x_off = (p.size.x + es.x) / 2; //获取双方横向尺寸相加的一半

        //比较Y轴上的偏差,如果y轴距离约定于碰撞器厚度,判断成立
        float y_dis = Mathf.Abs(transform.position.y - player.position.y); //获取双方y轴上距离
        float y_off = (p.size.y + es.y) / 2; //获取双方纵向尺寸相加的一半

        //如果x轴和y轴的位置判断都成立,且主角在敌人位置之下,判断为撞上
        p.bump = x_dis < x_off && y_dis - y_off < 0.1f && player.position.y < transform.position.y;
        return p.bump;
    }
    void Shorten()//缩短
    {
        timer += Time.deltaTime;
        if (timer > 0)
        {
            bm.RemoveBody(); //调用肢节管理器的移除方法   
            text.text = (--hp).ToString(); //血量-1并显示
            timer = -0.05f; //每0.05秒调用一次               
        }
    }
    void OnBecameInvisible() //离开摄像机视锥时销毁
    {
        Destroy(gameObject);
    }
}
