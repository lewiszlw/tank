using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // 移动速度 米每秒
    public float moveSpeed = 3;
    // 子弹旋转角度
    private Vector3 bulletEulerAngles;
    // 子弹发射间隔
    private float timeVal;

    private SpriteRenderer spriteRenderer;

    // 坦克四个方向精灵（转向）上右下左
    public Sprite[] tankDirectionSprites;

    public GameObject bulletPrefab;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // 在每次渲染新的一帧的时候会调用，更新频率和设备的性能有关以及被渲染的物体
    void Update() {
        if (timeVal > 0.3f) {
            Attack();
        } else {
            timeVal += Time.deltaTime;
	    }
    }

    // 物理帧，推荐用这个处理物理模拟
    // 固定的时间间隔执行，不受游戏帧率的影响，固定更新的时间间隔是0.02秒，也就是1秒执行50次
    private void FixedUpdate() {
        Move();
    }


    // 坦克攻击
    private void Attack() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(bulletEulerAngles));
            timeVal = 0;
	    }
    }


    // 坦克移动
    private void Move() {
        // x轴移动（返回由 axisName 标识的虚拟轴的值）
        // 对于键盘WSAD，h=1往x轴正方向移动，h=-1往x轴负方向移动，由于未对输入进行平滑处理，键盘输入将始终为 -1、0 或 1
        // 对于手柄，对于游戏杆的水平轴，值为 1 表示游戏杆向右推到底，值为 -1 表示游戏杆向左推到底；值为 0 表示游戏杆处于中性位置
        // 对于鼠标，值为当前鼠标增量乘以轴灵敏度，并且不会在 -1...1 的范围内。通常，正值表示鼠标向右/向下移动，负值表示鼠标向左/向上移动
        float h = Input.GetAxisRaw("Horizontal");

        // Transform.Translate函数是物体移动方式之一
        // 向量速度，坐标系
        // 增量时间，每秒帧数不固定，为保证1秒内不管多少帧移动相同距离，提出增量时间
        // 1秒30帧，那增量时间就是 1/30 秒，1秒60帧，那增量时间就是 1/60 秒
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        // 利用切换图片来显示坦克方向变化
        if (h > 0) {
            spriteRenderer.sprite = tankDirectionSprites[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        if (h < 0) {
            spriteRenderer.sprite = tankDirectionSprites[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }

        // 同一时间一个方向运动
        if (h != 0) {
            return;
        }

        // y轴移动
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (v > 0) {
            spriteRenderer.sprite = tankDirectionSprites[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        if (v < 0) {
            spriteRenderer.sprite = tankDirectionSprites[2];
            bulletEulerAngles = new Vector3(0, 0, 180);
        }
    }
}
