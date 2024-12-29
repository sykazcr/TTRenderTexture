using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    Transform tf; // Camera��Transform
    [SerializeField] private Camera cam; //sub Camera��Camera

    // Start is called before the first frame update
    void Start()
    {
        tf = this.cam.GetComponent<Transform>(); //Main Camera��Transform���擾����B
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I)) //I�L�[��������Ă����
        {
            cam.orthographicSize = cam.orthographicSize - 1.0f; //�Y�[���C���B
        }
        else if (Input.GetKey(KeyCode.O)) //O�L�[��������Ă����
        {
            cam.orthographicSize = cam.orthographicSize + 1.0f; //�Y�[���A�E�g�B
        }
        if (Input.GetKey(KeyCode.UpArrow)) //��L�[��������Ă����
        {
            tf.position = tf.position + new Vector3(0.0f, 1.0f, 0.0f); //�J��������ֈړ��B
        }
        else if (Input.GetKey(KeyCode.DownArrow)) //���L�[��������Ă����
        {
            tf.position = tf.position + new Vector3(0.0f, -1.0f, 0.0f); //�J���������ֈړ��B
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //���L�[��������Ă����
        {
            tf.position = tf.position + new Vector3(-1.0f, 0.0f, 0.0f); //�J���������ֈړ��B
        }
        else if (Input.GetKey(KeyCode.RightArrow)) //�E�L�[��������Ă����
        {
            tf.position = tf.position + new Vector3(1.0f, 0.0f, 0.0f); //�J�������E�ֈړ��B
        }
    }
}

