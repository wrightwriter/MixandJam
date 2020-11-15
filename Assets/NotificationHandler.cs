using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Notification
{
    public string text;
    public int sender;
    public Notification(int Sender,string Text)
    {
        this.text = Text;
        this.sender = Sender;
    }
}

public class NotificationHandler : MonoBehaviour
{
    public Sprite[] character_sprites;
    public AudioClip[] character_notif_sounds;
    public Animator notif_animator;
    public Text notif_text;
    public Image notif_character_icon;
    public AudioSource notif_sound_source;
    public AudioSource main_sound_source;
    private float notif_countdown;
    private List<Notification> notifs;

    private void Start()
    {
        notifs = new List<Notification>();


        for(int i = 0; i < 8; i++)
        {
            notifs.Add(new Notification(i, "Hey"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(notif_animator.GetBool("NotifShowing") && notif_animator.GetCurrentAnimatorStateInfo(0).IsName("Notification Show") && notif_countdown > 0)
        {
            notif_countdown -= Time.deltaTime;
            if (notif_countdown <= 0)
            {
                notif_animator.SetBool("NotifShowing", false);
            }
        }

        if (notif_countdown < 4)
            main_sound_source.volume = Mathf.Lerp(main_sound_source.volume, 0.4f, 0.1f * Time.deltaTime);

        if (notifs.Count > 0 && notif_countdown <= 0 && notif_animator.GetCurrentAnimatorStateInfo(0).IsName("Start State"))
        {
            CallNotification(notifs[0].sender, notifs[0].text);
            notifs.RemoveAt(0);
        }
    }


    void CallNotification(int Sender, string Text)
    {
        notif_animator.SetBool("NotifShowing", true);
        notif_text.text = Text;
        notif_character_icon.sprite = character_sprites[Sender];
        main_sound_source.volume = 0.2f;
        notif_sound_source.PlayOneShot(character_notif_sounds[Sender]);
        notif_countdown = 5;
    }
}
