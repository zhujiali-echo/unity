using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Bird> birds;
    public List<Pig> pigs;

    public static GameManager _initiated;
    public new Vector3 origina;

    public GameObject win;
    public GameObject lose;

    public GameObject[] stars;

    private int starNum = 0;//每一关的星星数量
    private int totalNum = 10;
    
    private void Awake()
    {
        _initiated = this;
        if(birds.Count>0)
        {
            origina = birds[0].transform.position;
        }
    }
    private void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].transform.position = origina;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].canMove = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canMove = false;
            }
        }
    }
    //游戏逻辑
    public void Nextfly()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                Initialized();//
            }
            else{
                lose.SetActive(true);//小鸟输了
            }
        }
        else
        {
            win.SetActive(true);//小猪输了
        }
    }

    public void StarShow()
    {
        StartCoroutine("show");
        Debug.Log(1);
    }
    IEnumerator show()
    {
        for (; starNum < birds.Count + 1; starNum++)
        {
            if (starNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starNum].SetActive(true);
        }
        Debug.Log(starNum);
    }

    public void Replay()
    {
        SaveDate();
        SceneManager.LoadScene(2);
        Debug.Log("点击");
    }
    public void Home()
    {
        SaveDate();
        SceneManager.LoadScene(1);
    }

    public void SaveDate()
    {
        if(starNum>PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
        PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starNum);//存储目前这level的最高的星星数量
        }
        int sum = 0;
        for (int i = 1; i <= totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);//存储总的星星数量
    }

}
