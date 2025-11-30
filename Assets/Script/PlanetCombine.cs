using UnityEngine;

public class PlanetCombine : MonoBehaviour
{
    private PlanetInfo planetInfo;
    private void Awake()
    {
        planetInfo = GetComponent<PlanetInfo>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet"))
        {
            PlanetInfo collisionPlanetInfo = collision.gameObject.GetComponent<PlanetInfo>();
            if (collisionPlanetInfo.index == planetInfo.index)
            {
                int collisionId = collision.gameObject.GetInstanceID();
                int id = gameObject.GetInstanceID();
                if (id > collisionId)
                {
                    GameManager.instance.UpdateScore(planetInfo.points);
                    if (planetInfo.index < SpawnManager.instance.planetPhysics.Count - 1)
                    {
                        Debug.Log(QueueManager.instance.GetCount());
                        QueueManager.instance.AddQueue(gameObject, collision.gameObject, planetInfo.index);
                        // GameManager.instance.NotifyObserver(PlayerAction.Combine);
                        // SpawnManager.instance.SpawnCombinedPlanet(planetInfo.index, gameObject.transform.position, collision.gameObject.transform.position);
                    }
                }
            }
        }
    }
}
