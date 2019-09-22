using UnityEngine;

public class HighlightBlock : MonoBehaviour
{
    [SerializeField] private GameObject highlighterBlockPrefab;

    private GameObject highlighterBlock;

    private void OnTriggerEnter(Collider other)
    {
        var block = other.gameObject;
        float blockSize = block.transform.lossyScale.x;
        Vector3 normal = GetNormalVector(block)* blockSize;
        highlighterBlock = Instantiate(highlighterBlockPrefab, other.transform.position + normal, Quaternion.identity);
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

        if (xComponent > yComponent && xComponent >zComponent)
        {
            return Vector3.left;
        }
        else if (yComponent > xComponent && yComponent > zComponent)
        {
            return Vector3.up;
        }
        else
        {
            return Vector3.forward;
        }
    }
}
