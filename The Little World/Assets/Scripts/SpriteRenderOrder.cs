using UnityEngine;

public class SpriteRenderOrder : MonoBehaviour
{

    // Calculate the sorting order of all sprite renderers in the scene
    void Update()
    {
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingOrder = (int)(renderer.transform.position.y * -100);
        }
    }
}
