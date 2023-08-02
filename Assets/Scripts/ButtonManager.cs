using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    SceneLoader SL = new SceneLoader();

    GameObject[] Planes;

    public void SwitchToNextScene()
    {
        StartCoroutine(SL.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadToNumScene(int sceneIndexNum)
    {
        StartCoroutine(SL.LoadScene(sceneIndexNum));
    }

    public void BackToCreScene()
    {
        StartCoroutine(SL.LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void CloseUI()
    {
        //按下Close案件，关闭UI
        gameObject.SetActive(false);

        //将所有按钮设置为激活状态
        Planes = GameObject.FindGameObjectsWithTag("Plane");
        for (int i = 0; i < Planes.Length; i++)
        {
            Planes[i].GetComponent<Button>().interactable = true;
        }
    }
}
