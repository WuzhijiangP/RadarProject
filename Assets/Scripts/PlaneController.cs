using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public GameObject lineRendererPrefab;
    public float speed = 10f;

    float time = 0f;


    private List<Vector3> pathPoints;
    private int currentWaypointIndex;

    void Start()
    {
        // // 获取LineRenderer的路径顶点
        // pathPoints = new List<Vector3>();
        // for (int i = 0; i < InitialLine.lineRenderer.positionCount; i++)
        // {
        //     pathPoints.Add(InitialLine.lineRenderer.GetPosition(i));
        // }

        // currentWaypointIndex = 0;
        StartCoroutine(Fly());
    }

    void Update()
    {
        // if (pathPoints.Count == 0)
        // {
        //     Debug.LogWarning("No flight path defined.");
        //     return;
        // }

        // // 获取当前路径顶点和下一个路径顶点
        // Vector3 currentWaypoint = pathPoints[currentWaypointIndex];
        // Vector3 nextWaypoint = pathPoints[(currentWaypointIndex + 1) % pathPoints.Count];

        // // 计算当前位置和下一个路径顶点的方向，并进行插值移动
        // Vector3 moveDirection = (nextWaypoint - currentWaypoint).normalized;
        // transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, speed * Time.deltaTime);

        // // 旋转飞机以面向前进方向
        // Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

        // // 到达下一个路径顶点时，更新当前索引
        // if (transform.position == nextWaypoint)
        // {
        //     currentWaypointIndex = (currentWaypointIndex + 1) % pathPoints.Count;
        // }
    }

    IEnumerator Fly()
    {
        int numPoints = InitialLine.lineRenderer.positionCount;

        while (true)
        {
            while (time < 1.0f)
            {
                time += Time.deltaTime / (speed * numPoints);
                //获取每段需要位移到的坐标
                Vector3 position = GetInterpolatedPosition();
                transform.position = position;
                yield return null;
            }

            // 重置参数
            time = 0f;
            yield return new WaitForSeconds(1f); // 在到达最后一个点后停留1秒

            // 返回起始点
            while (time < 1f)
            {
                time += Time.deltaTime / (speed * numPoints);
                Vector3 position = GetInterpolatedPosition();
                transform.position = position;
                yield return null;
            }

            // 重置参数
            time = 0f;
            yield return new WaitForSeconds(1f); // 在返回起始点后停留1秒
        }

    }

    Vector3 GetInterpolatedPosition()
    {
        int numPoints = InitialLine.lineRenderer.positionCount;
        float index = time * (numPoints - 1);
        int startIndex = Mathf.FloorToInt(index);
        int endIndex = Mathf.CeilToInt(index);

        Vector3 startPosition = InitialLine.lineRenderer.GetPosition(startIndex);
        Vector3 endPosition;
        if (InitialLine.lineRenderer.positionCount > endIndex)
        {
            endPosition = InitialLine.lineRenderer.GetPosition(endIndex);
        }
        else
        {
            endPosition = InitialLine.lineRenderer.GetPosition(InitialLine.lineRenderer.positionCount - 1);
        }

        return Vector3.Lerp(startPosition, endPosition, index % 1);
    }
}
