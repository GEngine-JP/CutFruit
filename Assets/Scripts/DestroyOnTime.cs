using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 按照时间销毁
/// </summary>
public class DestroyOnTime : MonoBehaviour
{
    public float desTime = 2f;

    void Start()
    {
        Invoke("Dead", desTime);
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
