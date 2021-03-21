using UnityEngine;
using System.Collections;

public class FigureView : MonoBehaviour
{
    [SerializeField] float _speed;
    public void Move(Vector2Int destination)
    {
        StartCoroutine(MoveTo(gameObject, transform.position, new Vector3(destination.x, destination.y, 0f), _speed));
    }

    public void ChangeHighlightColor(Color color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    private IEnumerator MoveTo(GameObject gameObject, Vector3 from, Vector3 to, float speed)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            gameObject.transform.position = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
