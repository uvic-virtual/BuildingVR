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
        float blockSize = block.transform.lossyScale.x;
        Vector3 normal = GetNormalVector(block) * blockSize;
        highlighterBlock = Instantiate(highlighterPrefab, other.transform.position + normal, Quaternion.identity);
    }

    private void OnTriggerExit()
    {
        Destroy(highlighterBlock);
    }

    private Vector3 GetNormalVector(GameObject other)
    {
        var pathToBlock = transform.position - other.transform.position;
        float xComponent = pathToBlock.x * pathToBlock.x;
        float yComponent = pathToBlock.y * pathToBlock.y;
        float zComponent = pathToBlock.z * pathToBlock.z;

        if (xComponent > yComponent && xComponent > zComponent)
        {
            return Vector3.left;
        }
        if (yComponent > xComponent && yComponent > zComponent)
        {
            return Vector3.up;
        }
        return Vector3.forward;
    }
}

