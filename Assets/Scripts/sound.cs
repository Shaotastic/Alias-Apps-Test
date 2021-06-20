using UnityEngine;

public class sound : MonoBehaviour, IAction
{
    [SerializeField] AudioSource m_AudioSource;
    void Start()
    {
        if (m_AudioSource == null)
            m_AudioSource = gameObject.AddComponent<AudioSource>();
        else
            m_AudioSource = GetComponent<AudioSource>();

        m_AudioSource.playOnAwake = false;

        var audioClip = Resources.Load<AudioClip>("Audio/sound");

        m_AudioSource.clip = audioClip;
    }

    public void Action()
    {
        m_AudioSource.Play();
    }

    public void OnMouseDown()
    {
        Action();
    }
}
