using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBoxManager : MonoBehaviour
{
    public static ChainBoxManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public bool[] locks;
    public GameObject floorOneBlock;
    public GameObject floorTwoBlock;
    public int CurrentFloor;
    public Animator DragonAnim;
    public Animator FlyAnimator;
    public List<Spawner> f1Spawners, f2Spawners, f3Spawners;

    // Start is called before the first frame update
    void Start()
    {
        CurrentFloor = 1;
        ActivateFloorOneSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckCompletions()
    {
        CheckFloorOneComplete();
        CheckFloor2Complete();
        CheckFloor3Complete();

    }

    public void CheckFloorOneComplete()
    {
        if (locks[0] && locks[1])
        {
            floorOneBlock.SetActive(false);
            CurrentFloor = 2;
            ActivateFloor2Spawners();
        }
    }
    public void CheckFloor2Complete()
    {
        if (locks[2])
        {
            floorTwoBlock.SetActive(false);
            CurrentFloor = 3;
            ActivateFloor3Spawners();
        }
    }
    public void CheckFloor3Complete()
    {
        if (locks[3])
        {
            StartCoroutine(DragonLeave());
        }
    }
    IEnumerator DragonLeave()
    {
        AudioManager.instance.Play("DragonGrowl", DragonAnim.gameObject);
        DragonAnim.SetTrigger("BreakChains");
        

        yield return new WaitForSeconds(6f);
        MenuController.instance.LoadScene(2);
    }
    public void FlyUp()
    {
        FlyAnimator.SetTrigger("Fly");
    }

    public void FinishedLock(int x)
    {
        locks[x] = true;
    }

    public void ActivateFloorOneSpawners()
    {
        foreach(Spawner spawner in f1Spawners)
        {
            spawner.Activate();
        }
    }
    public void ActivateFloor2Spawners()
    {
        foreach (Spawner spawner in f2Spawners)
        {
            spawner.Activate();
        }
    }
    public void ActivateFloor3Spawners()
    {
        foreach (Spawner spawner in f3Spawners)
        {
            spawner.Activate();
        }
    }

}
