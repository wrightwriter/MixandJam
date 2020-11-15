using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRenderer : MonoBehaviour
{
    public GameObject cloudObject;
    List<GameObject> cloud_objects;
    private float cloud_spawnCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        cloud_objects = new List<GameObject>();
        GenNewCloud();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = cloud_objects.Count-1; i >= 0; i--)
        {
            if (cloud_objects[i] != null)
            {
                cloud_objects[i].transform.position = new Vector3(cloud_objects[i].transform.position.x - Time.deltaTime * cloud_objects[i].transform.localScale.x, cloud_objects[i].transform.position.y, cloud_objects[i].transform.position.z);
                if (cloud_objects[i].transform.position.x < -45)
                {
                    Destroy(cloud_objects[i]);
                    cloud_objects.Remove(cloud_objects[i]);
                    continue;
                }
            }
        }

        cloud_spawnCount += Random.Range(0.0f, 2.0f);
        if(cloud_spawnCount > 1000)
        {
            GenNewCloud();
            cloud_spawnCount = 0;
        }
    }

    void GenNewCloud()
    {
        GameObject new_cloud = Instantiate(cloudObject, transform);
        new_cloud.transform.parent = gameObject.transform;
        cloud_objects.Add(new_cloud);
    }
}
