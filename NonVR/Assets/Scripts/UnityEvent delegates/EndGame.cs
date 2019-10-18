using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private string Message = "Game Over!";

    public void PrintGameOver()
    {
        Debug.Log(Message);
    }
}
