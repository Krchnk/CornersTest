using UnityEngine;

public class FigureView : MonoBehaviour
{
    public void Move(Vector2Int destination)
    {
        transform.position = new Vector3(destination.x, destination.y, transform.position.z);
    }
}
