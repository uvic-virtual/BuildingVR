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
        //Right click to place highlighted block (if it exsists).
        if (Input.GetMouseButtonDown(1) && highlighterBlock != null)
        {
            Instantiate(blockPrefab, highlighterBlock.transform.position, Quaternion.identity);
        }

        //Left click to place block on hand.
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var block = other.gameObject;
        var normal = GetNormalVector(block);
        float blockSize = block.transform.lossyScale.x;

        Destroy(highlighterBlock);
        highlighterBlock = Instantiate(highlighterPrefab, other.transform.position + normal*blockSize, Quaternion.identity);
    }

    private void OnTriggerExit()
    {
        Destroy(highlighterBlock);
    }

   /// <summary>
   /// Finds the "cardinal direction" (up down right left forwards backwards) between this gameobject and another.
   /// </summary>
   /// <param name="other">Other Gameobject we want to find "cardinal direction" to.</param>
   /// <returns>Returns a Vector3 with magnitude 1, and only 1 non-zero component.</returns>
    private Vector3 GetNormalVector(GameObject other)
    {
        //vector3 of path from hand's position to other gameobject's position
        var pathToBlock = transform.position - other.transform.position;

        //Gets magnitude of components.
        float xComponent = pathToBlock.x * pathToBlock.x;
        float yComponent = pathToBlock.y * pathToBlock.y;
        float zComponent = pathToBlock.z * pathToBlock.z;

        //Checks which component is greatest, and returns a new vector3 of ONLY that component of pathToBlock.

        //is X the biggest?
        if (xComponent > yComponent && xComponent > zComponent)
        {
            //is the path left or right?
            if (pathToBlock.x > 0)
            {
                return Vector3.right;
            }
            return Vector3.left;
        }

        //is Y the biggest?
        if (yComponent > xComponent && yComponent > zComponent)
        {
            //is the path up or down?
            if (pathToBlock.y > 0)
            {
                return Vector3.up;
            }
            return Vector3.down;
        }

        //Z must be biggest.
        //is the path forwards or backwards?
        if (pathToBlock.z > 0)
        {
            return Vector3.forward;
        }
        return Vector3.back;
    }
}

