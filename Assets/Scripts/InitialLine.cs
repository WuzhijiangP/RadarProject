using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InitialLine : MonoBehaviour
{

    public GameObject lineRendererPrefab;
    public static LineRenderer lineRenderer;

    Vector2 localMousePos;
    public GameObject backGround;
    public GameObject minMap;
    RectTransform imageRec;
    RectTransform minMapRec;
    Vector2 finalPos;
    float backgroundHeight, backgroundWidth;
    float scale;

    // Start is called before the first frame update
    void Start()
    {
        minMapRec = minMap.GetComponent<RectTransform>();
        imageRec = backGround.GetComponent<RectTransform>();
        backgroundHeight = imageRec.rect.height;
        backgroundWidth = imageRec.rect.width;


        lineRenderer = Instantiate(lineRendererPrefab).GetComponent<LineRenderer>();
        string json;
        // string filePath = Application.streamingAssetsPath + "/Line.json";

        // using (StringReader sr = new StringReader(filePath))
        // {
        //     //读出数据到json
        //     json = sr.ReadToEnd();
        //     sr.Close();  //关闭数据流
        // }

        var files = Directory.GetFiles(Application.streamingAssetsPath);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".mate"))
            {
                continue;
            }
            json = File.ReadAllText(files[i]);
            // 反序列化JSON并重新构建LineRenderer
            SavingData.data = JsonUtility.FromJson<Data>(json);
            lineRenderer.positionCount = SavingData.data.lineInfo.positions.Length;

            for (int j = 0; j < SavingData.data.lineInfo.positions.Length; j++)
            {
                //获取LineRenderer中存储的世界坐标
                Vector2 saveposition = SavingData.data.lineInfo.positions[j].GetVector3();

                //世界坐标转成屏幕坐标
                Vector3 mousePos = Camera.main.WorldToScreenPoint(saveposition);
                //Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //将屏幕坐标转换成图片像素坐标，存放到localMousePos变量中
                RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRec, mousePos, Camera.main, out localMousePos);

                // 将 localMousePosition 转换为相对于背景图片大小的像素坐标
                // finalPos.x = localMousePos.x;
                // finalPos.y = localMousePos.y;
                finalPos.x = (mousePos.x + (backgroundWidth * 0.5f));
                finalPos.y = (mousePos.y + (backgroundHeight * 0.5f));

                Debug.Log("finalPos.x ====== " + finalPos.x);
                Debug.Log("finalPos.y ====== " + finalPos.y);


                finalPos.x = backgroundWidth - (minMapRec.rect.width - ((finalPos.x / backgroundWidth) * minMapRec.rect.width));
                finalPos.y = backgroundHeight - (minMapRec.rect.width - ((finalPos.y / backgroundHeight) * minMapRec.rect.height));

                Debug.Log("finalPos.x ==================== " + finalPos.x);
                Debug.Log("finalPos.y ==================== " + finalPos.y);

                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(finalPos);
                //imageRec.TransformPoint(finalPos);
                worldPoint.z = 0;

                lineRenderer.SetPosition(j, worldPoint);
            }
        }

        // lineRenderer.positionCount = deserializedLineData.positions.Length;
        // for (int i = 0; i < deserializedLineData.positions.Length; i++)
        // {
        //     lineRenderer.SetPosition(i, deserializedLineData.positions[i].GetVector3());
        // }

        // 渲染线段到屏幕上
        lineRenderer.enabled = true;

        Debug.Log("LineRenderer created from JSON");
    }

}
