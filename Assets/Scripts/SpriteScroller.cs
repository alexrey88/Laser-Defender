using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material material;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;

        if (material == null)
        {
            Debug.LogError("Material component of SpriteScroller is null.");
        }
    }

    void Update()
    {
        MoveSprite();
    }

    void MoveSprite()
    {
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
