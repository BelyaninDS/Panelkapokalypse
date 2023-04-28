using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFaling : MonoBehaviour
{
    private AudioSource fallSoundHandler;

    // Start is called before the first frame update
    void Start()
    {
       fallSoundHandler = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        fallSoundHandler.Play();
    }
}
