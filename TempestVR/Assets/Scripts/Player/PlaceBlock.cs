using UnityEngine;

public class PlaceBlock : MonoBehaviour
{
    /// <summary>
    /// Controls scroll rate of mouse. </summary>
    [SerializeField] private float MouseScale = 0.1f;

    /// <summary>
    /// Prefab that gets placed.</summary>
    [SerializeField] private GameObject BlockPrefab;

    /// <summary>
    /// Prefab used to "highlight" where a block will go if you click.</summary>
    [SerializeField] private GameObject HighlighterPrefab;

    /// <summary>
    /// Instance of highlighter prefab.</summary>
    private GameObject highlighter;

    void Update()
    {
        //Right click to place block on highlighter block (if it exsists).
        if (Input.GetMouseButtonDown(1) && highlighter != null)
        {
            Instantiate(BlockPrefab, highlighter.transform.position, Quaternion.identity);
        }

        //Left click to place block on hand.
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(BlockPrefab, transform.position, Quaternion.identity);
        }

        transform.localPosition += new Vector3(0f, 0f, Input.mouseScrollDelta.y * MouseScale);
    }

    /// <summary>
    /// Creates highlighter block immediately adjacent to block touched by cursor.</summary>
    private void OnTriggerEnter(Collider other)
    {
        //Does the other gameobject have a block tag?
        if (other.tag.Equals(BlockPrefab.tag))
        {
            //Other block cursor is touching
            var block = other.gameObject;

            //Normal vector pointing out from surface being touched.
            var normal = GetNormalVector(block);

            //Size of the cursor block, prevents clipping.
            float blockSize = block.transform.lossyScale.x;

            //Clean up old highlighter.
            Destroy(highlighter);

            //Create a new highlighter next to block in direction of normal vector.
            highlighter = Instantiate(HighlighterPrefab, other.transform.position + normal * blockSize, Quaternion.identity);
        }
    }

    private void OnTriggerExit()
    {
        Destroy(highlighter);
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
