using UnityEngine;

public class Tile : MonoBehaviour
{
    public int posX;
    public int posZ;
    public Tower tower;
    public bool spawn;
    public bool goal;

    public bool IsEmpty => tower == null && !goal && !spawn;
}