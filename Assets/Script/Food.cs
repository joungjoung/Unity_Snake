using UnityEngine;

public class Food : MonoBehaviour
{

    public Collider2D foodArea;

    private void RandomizePosition()
    {
        Bounds bounds = foodArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snake"))
        {
            RandomizePosition();
        }
    }
}
