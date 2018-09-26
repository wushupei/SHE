using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int topScore;
    string _path;
    Sprite[] numbers; //储存所有的数字图片
    public ScoreManager(string path)
    {
        _path = path;
        numbers = Resources.LoadAll<Sprite>(_path);
    }
    public void ShowScore(int score, Image[] sprites) //根据分数更换精灵图片
    {
        switch (score.ToString().Length) //判断分数的位数
        {
            case 1:
                sprites[0].sprite = numbers[score]; //取个位
                break;
            case 2:
                sprites[0].sprite = numbers[score % 10];

                sprites[1].gameObject.SetActive(true); //取十位
                sprites[1].sprite = numbers[score / 10];
                break;
            case 3:
                sprites[0].sprite = numbers[score % 10];

                sprites[1].gameObject.SetActive(true);
                sprites[1].sprite = numbers[score / 10 % 10];

                sprites[2].gameObject.SetActive(true); //取百位
                sprites[2].sprite = numbers[score / 100];
                break;
            case 4:
                sprites[0].sprite = numbers[score % 10];
                sprites[1].gameObject.SetActive(true); //取十位
                sprites[1].sprite = numbers[score / 10 % 10];

                sprites[2].gameObject.SetActive(true); //取百位
                sprites[2].sprite = numbers[score / 100 % 10];

                sprites[2].gameObject.SetActive(true); //取千位
                sprites[3].sprite = numbers[score / 1000];
                break;
        }
    }
    public void SaveTopScor(int score) //保存最高分
    {
        if (PlayerPrefs.HasKey("最高分")) //获取最高分
            topScore = PlayerPrefs.GetInt("最高分");
        if (score > topScore) //当前分数超过最高分,则刷新最高分
            PlayerPrefs.SetInt("最高分", score);
    }
}
