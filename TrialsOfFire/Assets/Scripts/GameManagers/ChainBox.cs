using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBox : MonoBehaviour
{
    public Health health;
    public int number;

    private void Awake()
    {
        health.onHealthChange += Damaged;
    }

    private void Damaged(float amount, Transform changedBy = null)
    {
        if (health.Dead)
        {
            ChainBoxManager.Instance.FinishedLock(number);
            ChainBoxManager.Instance.CheckCompletions();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        health.onHealthChange -= Damaged;
    }
}
