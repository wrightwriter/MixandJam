using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRenderer : MonoBehaviour
{
    public Sprite[] clouds;
    GameObject[] cloud_objects;
    // Start is called before the first frame update
    void Start()
    {
        GenNewCloud();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenNewCloud()
    {
        GameObject new_cloud = new GameObject();
        new_cloud.AddComponent<SpriteRenderer>();
        new_cloud.GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length)];
        new_cloud.transform.position = new Vector3(10,Random.Range(-4.0f,16.0f),0);
        float scale = Random.Range(1.0f, 3.0f);
        new_cloud.transform.localScale = new Vector3(scale,scale,scale);
        new_cloud.GetComponent<SpriteRenderer>().sortingOrder = new int[]{1,3 }[(int)Random.Range(0,2)];
        new_cloud.transform.parent = gameObject.transform;
    }
}
