using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // 子弹速度
    private float bulletSpeed = 5;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // transform.up 代表物体自身坐标轴y轴
        transform.Translate(transform.up * bulletSpeed * Time.deltaTime, Space.World);
    }

    // 碰撞检测-进入
    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "TankPlayer":
                break;
            case "Wall":
                break;
            case "Enemy":
                break;
            case "Heart":
                break;
	    }
    }
}
