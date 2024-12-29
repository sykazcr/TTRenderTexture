using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    Transform tf; // CameraのTransform
    [SerializeField] private Camera cam; //sub CameraのCamera

    // Start is called before the first frame update
    void Start()
    {
        tf = this.cam.GetComponent<Transform>(); //Main CameraのTransformを取得する。
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I)) //Iキーが押されていれば
        {
            cam.orthographicSize = cam.orthographicSize - 1.0f; //ズームイン。
        }
        else if (Input.GetKey(KeyCode.O)) //Oキーが押されていれば
        {
            cam.orthographicSize = cam.orthographicSize + 1.0f; //ズームアウト。
        }
        if (Input.GetKey(KeyCode.UpArrow)) //上キーが押されていれば
        {
            tf.position = tf.position + new Vector3(0.0f, 1.0f, 0.0f); //カメラを上へ移動。
        }
        else if (Input.GetKey(KeyCode.DownArrow)) //下キーが押されていれば
        {
            tf.position = tf.position + new Vector3(0.0f, -1.0f, 0.0f); //カメラを下へ移動。
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //左キーが押されていれば
        {
            tf.position = tf.position + new Vector3(-1.0f, 0.0f, 0.0f); //カメラを左へ移動。
        }
        else if (Input.GetKey(KeyCode.RightArrow)) //右キーが押されていれば
        {
            tf.position = tf.position + new Vector3(1.0f, 0.0f, 0.0f); //カメラを右へ移動。
        }
    }
}

