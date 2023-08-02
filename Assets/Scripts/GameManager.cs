using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;



[System.Serializable]
public class LineRendererData
{
    public Vector3Wrapper[] positions;
}

[System.Serializable]
public class Vector3Wrapper
{
    public float x;
    public float y;
    public float z;

    public Vector3Wrapper(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public static class BezierCurve
{
    public static Vector3 GetPointOnCurve(Vector3[] points, float t)
    {
        int numPoints = points.Length;
        int numSegments = numPoints - 1;

        int currentIndex = Mathf.FloorToInt(numSegments * t);
        int nextIndex = currentIndex + 1;

        if (nextIndex >= numPoints)
        {
            return points[currentIndex];
        }

        float segmentT = t * numSegments - currentIndex;

        return Vector3.Lerp(points[currentIndex], points[nextIndex], segmentT);
    }
}

public class GameManager : MonoBehaviour
{
    public GameObject flagPrefab;               //旗帜预制件
    public GameObject lineRendererPrefab;       //线段预制件
    public Transform flagParent;                //所有Flag实例的父Transform组件
    public Transform lineRendererParent;        //所有线段实例的父Transform组件

    private List<Vector2> flagPositions;        //存储所有Flag实例的位置
    private List<GameObject> lineRenderers;     //存储所有线段实例的GameObject
    private bool canSpawnFlag;                  //标志是否可以生成旗帜


    public GameObject Image;
    RectTransform imageRec;
    //绘制完成弹窗UI
    public GameObject TipsUI;
    //保存背景图片宽高大小
    float backgroundHeight, backgroundWidth;

    //规定比例尺宽度
    float scaleLength = 80f;

    //保存对应的像素坐标
    Vector2 localMousePos;
    Vector2 finalPos;

    //延线段移动速度
    public float speed = 1.0f;
    private float moveVariable = 0.0f;
    public LineRenderer lineSave;

    void Start()
    {
        flagPositions = new List<Vector2>();
        lineRenderers = new List<GameObject>();
        canSpawnFlag = true;                     //初始状态下允许生成旗帜

        //获取图片位置信息组件
        imageRec = Image.GetComponent<RectTransform>();
        backgroundHeight = imageRec.rect.height;
        Debug.Log("图片高度:" + imageRec.rect.height);
        backgroundWidth = imageRec.rect.width;
        Debug.Log("图片高度:" + imageRec.rect.width);

    }

    void Update()
    {
        if (canSpawnFlag && Input.GetMouseButtonDown(0))
        {
            //获取屏幕上坐标到世界坐标
            //屏幕坐标？
            Vector2 mousePos = Input.mousePosition;
            //Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //将屏幕坐标转换成图片像素坐标，存放到localMousePos变量中
            RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRec, mousePos, Camera.main, out localMousePos);
            // 将 localMousePosition 转换为相对于背景图片大小的像素坐标
            //将坐标原定设定在图片左下角
            finalPos.x = ((backgroundWidth * 0.5f) + localMousePos.x);
            finalPos.y = ((backgroundHeight * 0.5f) + localMousePos.y);

            Debug.Log("坐标坐标坐标坐标坐标坐标" + finalPos);
            SpawnFlag(Camera.main.ScreenToWorldPoint(finalPos));
        }



        // // 根据速度和时间控制移动变量的增加
        // moveVariable += speed * Time.deltaTime;

        // // 如果移动变量超过1，则重置为0，实现循环移动
        // if (moveVariable > 1.0f)
        // {
        //     moveVariable = 0.0f;
        // }

        // // 获取曲线上的点，并计算物体的当前位置
        // Vector3[] positions = new Vector3[lineSave.positionCount];
        // lineSave.GetPositions(positions);
        // Vector3 currentPosition = BezierCurve.GetPointOnCurve(positions, moveVariable);

        // // 将物体的位置设置为计算得到的位置
        // transform.position = currentPosition;

    }

    //设置当前是否允许生成旗帜的状态
    public void SetCanSpawnFlag(bool value)
    {
        canSpawnFlag = value;
    }

    //生成一个旗帜实例
    void SpawnFlag(Vector2 position)
    {
        GameObject flagInstance = Instantiate(flagPrefab, position, Quaternion.identity, flagParent);
        //存下标记位置信息，之后绘制线段
        flagPositions.Add(position);

        flagInstance.GetComponent<FlagController>().SetText(new Vector2(finalPos.x / scaleLength, finalPos.y / scaleLength).ToString());
    }

    //绘制所有线段
    public void DrawLine()
    {

        if (flagPositions.Count < 2)
            return;

        LineRenderer lineRenderer = Instantiate(lineRendererPrefab, lineRendererParent).GetComponent<LineRenderer>();
        lineRenderer.positionCount = flagPositions.Count;

        for (int i = 0; i < flagPositions.Count; i++)
        {
            lineRenderer.SetPosition(i, flagPositions[i]);
        }

        //SmoothString(lineRenderer);
        lineRenderers.Add(lineRenderer.gameObject);

        //保存下线段信息
        lineSave = lineRenderer;

        #region 将线段保存为json格式
        // 将LineRenderer的位置信息转换为可序列化的类
        LineRendererData lineData = new LineRendererData();
        int positionCount = lineRenderer.positionCount;
        lineData.positions = new Vector3Wrapper[positionCount];
        for (int i = 0; i < positionCount; i++)
        {
            //将线段的坐标信息一个一个录入
            Vector2 savePosition = Camera.main.WorldToScreenPoint(lineRenderer.GetPosition(i));
            savePosition.x -= (backgroundWidth * 0.5f);
            savePosition.y -= (backgroundHeight * 0.5f);
            Debug.Log("savePosition.x = " + savePosition.x);
            Debug.Log("savePosition.y = " + savePosition.y);
            lineData.positions[i] = new Vector3Wrapper(Camera.main.ScreenToWorldPoint(savePosition));
        }

        SavingData.data.lineInfo = lineData;

        string json = JsonUtility.ToJson(SavingData.data);
        string filePath = Application.streamingAssetsPath + "/Line.json";

        using (StreamWriter sw = new StreamWriter(filePath))
        {
            //第二个参数true能让json文件中数据自动回车对齐
            sw.WriteLine(json);
            //使用完毕关闭文件流
            sw.Close();
            sw.Dispose();
        }
        #endregion

        flagPositions.Clear();

    }

    public void ClickShowTips()
    {
        if (!TipsUI.activeInHierarchy)
        {
            //显示提示UI
            TipsUI.SetActive(true);


        }
    }

    public void SmoothString(LineRenderer lineRenderer)
    {
        //取两两点之间的距离
        float[] distances = new float[flagPositions.Count - 1];
        for (int i = 0; i < flagPositions.Count - 1; i++)
        {
            distances[i] = Vector3.Distance(flagPositions[i], flagPositions[i + 1]);
        }

        //总长度
        float totalDistance = 0;
        for (int i = 0; i < distances.Length; i++)
        {
            totalDistance += distances[i];
        }

        //使用总长度求每段长度占总长度的比例
        float[] factors = new float[flagPositions.Count];
        factors[0] = 0f;
        for (int i = 1; i < factors.Length - 1; i++)
        {
            factors[i] = factors[i - 1] + (distances[i - 1] / totalDistance);
        }
        factors[factors.Length - 1] = 1f;

        //线性插值算法，计算每个点在曲线上的坐标
        List<Vector3> curvePoints = new List<Vector3>();
        for (int i = 0; i < flagPositions.Count - 1; i++)
        {
            for (float t = 0; t <= 1; t += 0.05f) // 这里的 0.05f 是分配的间隔数，可以调整
            {

                float tDistance = factors[i] * totalDistance + t * distances[i];

                float tFactor = tDistance / totalDistance;
                Vector3 point = Vector3.Lerp(flagPositions[i], flagPositions[i + 1], tFactor);
                curvePoints.Add(point);
            }
        }

        //平滑曲线点列表
        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
        lineRenderer.loop = false; // 禁用自动连接功能

    }

    //移除所有旗帜实例和线段实例
    public void ClearAll()
    {
        // foreach (GameObject flag in flagParent.gameObject)
        // {
        //     Destroy(flag);
        // }

        foreach (GameObject lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer);
        }

        flagPositions.Clear();
        lineRenderers.Clear();
    }


}
