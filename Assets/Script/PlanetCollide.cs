using UnityEngine;

public class PlanetCollide : MonoBehaviour
{
    private bool hasCollide;
    public bool isCombined;
    private void Awake()
    {
        hasCollide = false;
        isCombined = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCombined)
        {
            Destroy(this);
            return;
        }

        if (!hasCollide)
        {
            hasCollide = true;
            Player.instance.canThrow = true;
            SpawnManager.instance.GetNextPlanet();
            Destroy(this);
        }
    }
}
