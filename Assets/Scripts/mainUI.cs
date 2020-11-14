using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainUI : MonoBehaviour
{
    public Brain m_brain;
    public List<Image> m_images;
    public List<Sprite> sprites;

    void Start()
    {
        InvokeRepeating("UpdateUI", 0.0f, 0.05f);
    }

    // TODO: make this unly update in some cases
    void UpdateUI()
    {
        for( int i = 0; i < m_brain.m_playerCarrying.Count; i++)
        {
            Show(m_images[i].gameObject);
            m_images[i].sprite = sprites[m_brain.m_playerCarrying[i]];
        }
        for( int i = 0; i < 4 - m_brain.m_playerCarrying.Count; i++)
        {
            Hide(m_images[3 - i].gameObject);
        }
    }
    void Hide( GameObject _go)
    {
        _go.GetComponent<Image>().enabled = false;
    }
    void Show( GameObject _go)
    {
        _go.GetComponent<Image>().enabled = true;
    }

    void Update()
    {
       
    }
}
