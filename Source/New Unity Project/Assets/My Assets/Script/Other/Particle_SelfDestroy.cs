using UnityEngine;
using System.Collections;

public class Particle_SelfDestroy : MonoBehaviour
{

    public float time;
    // Use this for initialization
    void Start()
    {
        Invoke("SelfDestroy", time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
