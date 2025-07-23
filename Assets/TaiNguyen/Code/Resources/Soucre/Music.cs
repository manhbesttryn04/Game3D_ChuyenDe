using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] public AudioSource MusicBackround;
    [SerializeField] public AudioSource River;
    void Start()
    {
        MusicBackround.Play();
       // River.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
