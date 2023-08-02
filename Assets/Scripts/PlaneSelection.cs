using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlaneSelection : MonoBehaviour, IPointerClickHandler
{
    Sprite planeImage;
    public GameObject tips;
    public GameObject tipsImage;
    Button clickedButton;
    GameObject[] Planes;

    private Camera mainCamera; // 主摄像机

    //选择地图时获取对应的地图图片保存
    public static Sprite saveMap;

    void Start()
    {
        // 获取主摄像机
        mainCamera = Camera.main;
        foreach (KeyValuePair<string, Sprite> kvp in CreateItemController.buttonInfo)
        {
            string key = kvp.Key;
            Sprite value = kvp.Value;

            // 使用 key 和 value 进行需要的操作
            Debug.Log("字典中有" + CreateItemController.buttonInfo.Count + "个数据：" + key + "   " + value);
        }
    }

    private void Update()
    {
        // // 检测鼠标点击事件
        // if (Input.GetMouseButtonDown(0))
        // {

        //     Debug.Log("你鼠标点击了");
        //     // 发射一条射线从鼠标位置
        //     Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;

        //     // 检测是否点击到按钮
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         // 获取被点击的游戏对象
        //         GameObject clickedButton = hit.collider.gameObject;


        //         Debug.Log("你鼠标点击了annnnnnnnnnn");
        //         if (clickedButton.gameObject.name.Contains("Plane"))
        //         {
        //             //拿到末尾数字
        //             string index = transform.gameObject.name.Substring(-1);
        //             //保存
        //             SavingData.data.planeSprite = index;
        //         }
        //         if (clickedButton.gameObject.name.Contains("Radar"))
        //         {
        //             //拿到末尾数字
        //             string index = transform.gameObject.name.Substring(-1);
        //             //保存
        //             SavingData.data.planeSprite = index;
        //         }

        //         Image img = clickedButton.GetComponent<Image>();
        //         planeImage = img.sprite;

        //         // 在这里你可以对点击到的按钮游戏对象进行操作
        //         Debug.Log("你点击了按钮：" + clickedButton.name);
        //     }
        // }
    }

    //处理按钮点击事件
    public void PlaneSelected()
    {
        //获取点击按钮的飞机图片
        tipsImage.GetComponent<Image>().sprite = planeImage;

        tips.SetActive(true);
    }

    //获取点击按钮
    public void OnPointerClick(PointerEventData eventData)
    {
        //获取鼠标点击事件不为null
        if (eventData.pointerPress != null)
        {
            Debug.Log("触发点击事件");
            clickedButton = eventData.pointerPress.GetComponent<Button>();

            //如果有按钮被按下
            if (clickedButton != null)
            {
                Debug.Log(clickedButton.gameObject.name);
                planeImage = clickedButton.GetComponentInChildren<Image>().sprite;

                //获取点击按钮的飞机图片
                tipsImage.GetComponent<Image>().sprite = planeImage;
                //拿到末尾数字
                string index = clickedButton.gameObject.name[clickedButton.gameObject.name.Length - 1].ToString();
                //如果是飞机
                if (clickedButton.gameObject.name.Contains("Plane"))
                {
                    //保存
                    SavingData.data.planeNum = index;
                }
                else if (clickedButton.gameObject.name.Contains("Radar"))  //如果是雷达
                {
                    SavingData.data.redarNUm = index;
                }
                else if (clickedButton.gameObject.name.Contains("Scene"))  //如果有按钮被按下，且按下的按钮为场景选择按钮
                {

                    //在Resource文件夹中找到对应图片
                    saveMap = Resources.Load<Sprite>(index);
                    SavingData.data.sceneNum = index;

                    //切换到地图场景
                    //StartCoroutine(SL.LoadScene(Convert.ToInt32(index) + 3));

                }

                tips.SetActive(true);

                //除了当前点击按扭外的其他按钮设置为不可点击
                Planes = GameObject.FindGameObjectsWithTag("Plane");
                for (int i = 0; i < Planes.Length; i++)
                {
                    if (!Planes[i].Equals(clickedButton))
                    {
                        Planes[i].GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
    }
}
