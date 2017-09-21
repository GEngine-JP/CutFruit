using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    /// <summary>
    /// 直线渲染器
    /// </summary>
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private AudioSource audioSource;

    /// <summary>
    /// 是否第一次鼠标按下
    /// </summary>
    private bool firstMouseDown = false;

    /// <summary>
    /// 是否鼠标一直按下
    /// </summary>
    private bool mouseDown = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMouseDown = true;
            mouseDown = true;

            //播放声音
            audioSource.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        onDrawLine();

        firstMouseDown = false;
    }


    /// <summary>
    /// 保存的所有坐标
    /// </summary>
    private Vector3[] positions = new Vector3[10];

    /// <summary>
    /// 当前保存的坐标数量
    /// </summary>
    private int posCount = 0;

    /// <summary>
    /// 代表这一帧鼠标的位置 就 头的坐标
    /// </summary>
    private Vector3 head;

    /// <summary>
    /// 代表上一帧鼠标的位置
    /// </summary>
    private Vector3 last;

    /// <summary>
    /// 画线
    /// </summary>
    private void onDrawLine()
    {
        if (firstMouseDown)
        {
            //先把计数器设为0
            posCount = 0;

            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            last = head;
        }

        if (mouseDown)
        {
            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(head, last) > 0.01f)
            {
                savePosition(head);
                posCount++;

                //发射一条射线
                onRayCast(head);
            }

            last = head;
        }
        else
        {
            positions = new Vector3[10];
        }

        changePositions(positions);
    }


    /// <summary>
    /// 发射射线
    /// </summary>
    /// <param name="pos">Position.</param>
    private void onRayCast(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        //检测到所有的物体
        RaycastHit[] hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            //Debug.Log(hits[i].collider.gameObject.name);
            //TODO 
            hits[i].collider.gameObject.SendMessage("OnCut", SendMessageOptions.DontRequireReceiver);
        }
    }



    /// <summary>
    /// 保存坐标点
    /// </summary>
    /// <param name="pos">Position.</param>
    private void savePosition(Vector3 pos)
    {
        pos.z = 0;

        if (posCount <= 9)
        {
            for (int i = posCount; i < 10; i++)
            {
                positions[i] = pos;
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
                positions[i] = positions[i + 1];

            positions[9] = pos;
        }
    }


    /// <summary>
    /// 修改直线渲染器的坐标
    /// </summary>
    /// <param name="postions">Postions.</param>
    private void changePositions(Vector3[] postions)
    {
        lineRenderer.SetPositions(postions);
    }
}
