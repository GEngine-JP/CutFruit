using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用来产生 水果 炸弹 
/// 也让它 能控制销毁
/// </summary>
public class Spawner : MonoBehaviour
{
    [Header("水果的预设")]
    public GameObject[] Fruits;

    [Header("炸弹的预设")]
    public GameObject Bomb;

    /// <summary>
    /// 播放声音的组件
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// 产生时间
    /// </summary>
    float spwanTime = 3f;

    /// <summary>
    /// 是否在玩游戏
    /// </summary>
    bool isPlaying = true;

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }
			
        spwanTime -= Time.deltaTime;

        if (0 > spwanTime)
        {
			
            //到时间了就开始产生水果
            int fruitCount = Random.Range(1, 5);
            for (int i = 0; i < fruitCount; i++)
                onSpawn(true);

            //随机产生炸弹
            int bombNum = Random.Range(0, 100);
            if (bombNum > 70)
            {
                onSpawn(false);
            }

            spwanTime = 3f;
        }
    }

    /// <summary>
    /// 临时存储当前水果的z坐标
    /// </summary>
    private int tmpZ = 0;

    /// <summary>
    /// 产生水果和炸弹
    /// </summary>
    /// <param name="isfruit">是否是水果</param>.
    private void onSpawn(bool isFruit)
    {
        //播放音乐
        this.audioSource.Play();

        //x范围 ： -8.4 - 8.4
        //y范围: transform.pos.y

        //得知坐标范围
        float x = Random.Range(-8.4f, 8.4f);
        float y = transform.position.y;
        float z = tmpZ;

        //使水果不在一个平面上
        tmpZ = tmpZ - 2;
        //tmpZ -= 2;
        if (tmpZ <= -10)
            tmpZ = 0;

        //实例化水果
        int fruitIndex = Random.Range(0, Fruits.Length);

        GameObject go;
        if (isFruit)
            go = Instantiate<GameObject>(Fruits[fruitIndex], new Vector3(x, y, z), Random.rotation);
        else
            go = Instantiate<GameObject>(Bomb, new Vector3(x, y, z), Random.rotation);

        //水果的速度
        Vector3 velocity = new Vector3(-x * Random.Range(0.2f, 0.8f), -Physics.gravity.y * Random.Range(1.2f, 1.5f), 0);

        //Rigidbody rigidbody = transform.GetComponent<Rigidbody> ();
        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        rigidbody.velocity = velocity;
    }



    /// <summary>
    /// 有物体碰撞的时候调用
    /// </summary>
    /// <param name="other">Other.</param>
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }

}
