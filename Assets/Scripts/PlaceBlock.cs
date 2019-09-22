using UnityEngine;

/// <summary>
/// Put on player's hand, places block on right click.</summary>
public class PlaceBlock : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject highlighterPrefab;

    private GameObject highlighterBlock;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (highlighterBlock != null)
            {
                Instantiate(blockPrefab, highlighterBlock.transform.position, Quaternion.identity);
                Destroy(highlighterBlock);
            }
            else
            {
                Instantiate(blockPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var block = other.gameObject;
        var normal = GetNormalVector(block);

        Destroy(highlighterBlock);
        highlighterBlock = Instantiate(highlighterPrefab, other.transform.position + normal, Quaternion.identity);
    }

    private void OnTriggerExit()
    {
        Destroy(highlighterBlock);
    }

    private Vector3 GetNormalVector(GameObject other)
    {
        //vector3 of path from hand's position to other gameobject's position
        var pathToBlock = transform.position - other.transform.position;

        //Gets magnitude of components.
        float xComponent = pathToBlock.x * pathToBlock.x;
        float yComponent = pathToBlock.y * pathToBlock.y;
        float zComponent = pathToBlock.z * pathToBlock.z;

        //Checks which component is greatest, and returns a new vector3 of ONLY that component of pathToBlock.
        if (xComponent > yComponent && xComponent > zComponent)
        {
            return new Vector3(pathToBlock.x, 0, 0);
        }
        if (yComponent > xComponent && yComponent > zComponent)
        {
            return new Vector3(0, pathToBlock.y, 0);
        }
        return new Vector3 (0, 0, pathToBlock.z);
    }
}

