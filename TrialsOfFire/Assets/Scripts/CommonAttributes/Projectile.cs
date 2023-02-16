using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public bool fired;
    private Vector3 direction;
    private float speed;
    public delegate void OnCollison(Collider other);
    //public event OnCollison OnHitDetected;
    public OnCollison OnHitDetected;
    public void Fire(Vector3 direction, float speed)
    {
        fired = true;
        this.speed = speed;
        this.direction = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);

    }

    void Update()
    {
        if (fired)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnHitDetected != null)
            OnHitDetected(other);
        
    }

    
}
