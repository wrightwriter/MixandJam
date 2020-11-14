using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{

    public List<GameObject> m_resourceBoxes;
    Canvas m_canvas;
    void Start()
    {
        m_canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        m_canvas.enabled = Input.GetKey("tab");
    }
}
