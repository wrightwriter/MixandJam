using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414 


public class Resource
{
    public string name;
    public float amount;
    // copy constructor
    public Resource(Resource _previousResource)
    {
        name = _previousResource.name;
        amount = _previousResource.amount;
    }
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
    public List<int> m_playerCarrying;
    public GameObject m_player;


    void Start()
    {
        InitializeResources();
        InvokeRepeating("Logic", 1.0f, 1.0f);
    }
    void InitializeResources() { 
        for (int idxTrader = 0; idxTrader < 8; idxTrader++) {
            List<Resource> resourceCopy = new List<Resource>(resources.Count);
            resources.ForEach((item) =>
            {
                resourceCopy.Add( new Resource(item));
            });
            m_inventory.Add(resourceCopy);
            m_velocity.Add(0.0f);


            for (int idxResource = 0; idxResource < 8; idxResource++) {
                m_inventory[idxTrader][idxResource].amount = UnityEngine.Random.Range(0, 4.0f);
            }
        }


    }

    void Logic()
    {
        for (int idxTrader = 0; idxTrader < 8; idxTrader++) {
            m_velocity[idxTrader] = Mathf.PerlinNoise(Time.fixedTime*0.05f + idxTrader*200f, 0.1f)  + 0.1f;
            m_inventory[idxTrader][idxTrader].amount += m_velocity[idxTrader] * 0.05f;
        }

    }

    public void CarryItem( Vector2 _input)
    {
        if (m_playerCarrying.Count < 4)
        {
            int charIdx = (int)_input.x;
            int resIdx = (int)_input.y;

            if (m_inventory[charIdx][resIdx].amount >= 1.0f)
            {
                m_playerCarrying.Add(resIdx);
                m_inventory[charIdx][resIdx].amount--;
            }

        }
        
    }
    
    public void DropItem( Vector2 _input)
    {
        if (m_playerCarrying.Count > 0)
        {
            int charIdx = (int)_input.x;
            int resIdx = (int)_input.y;

            for (int idxItem = m_playerCarrying.Count - 1; idxItem > 0; idxItem--)
            {
                if (m_playerCarrying[idxItem] == resIdx) { 
                    m_inventory[charIdx][resIdx].amount++;
                    m_playerCarrying.RemoveAt(idxItem);
                    return;
                }
            }

        }
        
    }

    void Update()
    {
        
    }
}
