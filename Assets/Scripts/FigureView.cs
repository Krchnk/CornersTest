using UnityEngine;
using System.Collections;

public class FigureView : MonoBehaviour
{
    public void Move(Vector2Int destination)
    {
        StartCoroutine(MoveTo(gameObject, transform.position, new Vector3(destination.x, destination.y, 0f)));
    }
    private IEnumerator MoveTo(GameObject gameObject, Vector3 from, Vector3 to)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
