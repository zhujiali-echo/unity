using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {

    private Animator animator;
    public GameObject button;

    private void Awake()
    {

        animator = GetComponent<Animator>();
    }

    public void Pause()
    {
        animator.SetBool("isPause",true);
        button.SetActive(false);
        if (GameManager._initiated.birds.Count > 0)
        {
            if(GameManager._initiated.birds[0].isRealsed==false)
            {
                GameManager._initiated.birds[0].canMove = false;
            }
        }

    }
   
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
        
    }
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);

    }



    public void Resume()
    {
        Time.timeScale = 1;
        animator.SetBool("isPause", false);
        if (GameManager._initiated.birds.Count > 0)
        {
            if (GameManager._initiated.birds[0].isRealsed == false)
            {
                GameManager._initiated.birds[0].canMove = true;
            }
        }
    }

   

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd()

    {
        button.SetActive(true);
    }
}