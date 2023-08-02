using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GotoDrawLine : MonoBehaviour
{
    SceneLoader SL = new SceneLoader();

    public static Sprite save;

    public void OnSureButtonClick()
    {
        //拿到末尾数字
        string index = SavingData.data.sceneNum;
        //在Resource文件夹中找到对应图片
        save = Resources.Load<Sprite>(index);

        Debug.Log("场景编号：" + index);
        StartCoroutine(SL.LoadScene(Convert.ToInt32(index) + 4));

    }

}
