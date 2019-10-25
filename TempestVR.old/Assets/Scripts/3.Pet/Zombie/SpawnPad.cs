using System.Collections;
using UnityEngine;

public class SpawnPad : MonoBehaviour
{
    public int MaxAttackZombie = 10, MaxFetchZombies = 5;
    public float MaxSpawnInterval = 10f;

    public GameObject FetchZombie, AttackZombie;
    public static int numAttackZombies, numFetchZombies;

	void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (numAttackZombies < MaxAttackZombie)
            {
                try
                {
                    Instantiate(AttackZombie, transform.position, Quaternion.identity);
                    numAttackZombies++;
                }
                catch (UnassignedReferenceException)
                {
                    Debug.Log("Error: AttackZombie prefab isn't set!");
                }
            }
            else if (numFetchZombies < MaxFetchZombies)
            {
                try
                {
                    Instantiate(FetchZombie, transform.position, Quaternion.identity);
                    numFetchZombies++;
                }
                catch (UnassignedReferenceException)
                {
                    Debug.Log("Error: FetchZombie prefab isn't set!");
                }
            }
            yield return new WaitForSeconds(Random.Range(1f, MaxSpawnInterval));
        }
    }
}
