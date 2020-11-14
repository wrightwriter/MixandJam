using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414 


public class Resource
{
    public string name;
    public float amount;
    public Resource(string _name, float _amount)
    {
        name = _name;
        amount = _amount;
    }
}



public class Brain : MonoBehaviour
{
    static int CATNIP = 0;
    static int LEMONADE = 1;
    static int NUTS = 2;
    static int PARTS = 3;
    static int MEAT = 4;
    static int TENNISBALL = 5;
    static int BERRIES = 6;
    static int FISH = 7;
    public static List<Resource> resources = new List<Resource>() { 
        new Resource( "catnip", 0.0f),
        new Resource( "lemonade", 0.0f),
        new Resource( "nuts", 0.0f),
        new Resource( "parts", 0.0f),
        new Resource( "meat", 0.0f),
        new Resource( "tennisball", 0.0f),
        new Resource( "berries", 0.0f),
        new Resource( "fish", 0.0f),
    };
    [System.NonSerialized] public List<List<Resource>> m_inventory = new List<List<Resource>>();
    [System.NonSerialized] public List<float> m_velocity = new List<float>();

    public List<GameObject> m_traders;


    void Start()
    {
        InitializeResources();
        InvokeRepeating("Logic", 1.0f, 1.0f);
    }
    void InitializeResources() { 
        for (int idxTrader = 0; idxTrader < 8; idxTrader++) {
            m_inventory.Add(new List<Resource>(resources));
            m_velocity.Add(0.0f);


            for (int idxResource = 0; idxResource < 8; idxResource++) {
                m_inventory[idxTrader][idxResource].amount = Random.Range(0, 4.0f);
            }
        }


    }

    void Logic()
    {
        Debug.Log("called brain logic");
        for (int idxTrader = 0; idxTrader < 8; idxTrader++) {
            m_velocity[idxTrader] = Mathf.PerlinNoise(Time.fixedTime*0.05f + idxTrader*200f, 0.1f)  + 0.2f;
            m_inventory[idxTrader][idxTrader].amount += m_velocity[idxTrader] * 0.1f;
        }

    }

    void Update()
    {
        
    }
}
