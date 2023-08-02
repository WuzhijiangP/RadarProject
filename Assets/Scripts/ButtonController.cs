using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameManager gameManager;



    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }



    //鼠标在可交互UI上点击时，不会生成旗帜
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameManager.SetCanSpawnFlag(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameManager.SetCanSpawnFlag(true);
    }
}
