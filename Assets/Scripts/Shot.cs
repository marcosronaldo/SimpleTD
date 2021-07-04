using UnityEngine;

public class Shot : MonoBehaviour
{
    public Tower tower;
    public float timer = 1.5f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }
}