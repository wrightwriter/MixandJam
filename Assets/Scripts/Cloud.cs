using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Sprite[] clouds;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length)];
        gameObject.transform.position = new Vector3(29, Random.Range(-4.0f, 16.0f), 0);
        float scale = Random.Range(1f, 5.0f);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = new int[] { 1, 3 }[(int)Random.Range(0, 2)];
    }
}
