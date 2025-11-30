using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public static QueueManager instance { get; private set; }

    private List<(GameObject, GameObject, int)> queue;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        queue = new List<(GameObject, GameObject, int)>();
    }

    public void AddQueue(GameObject planet1, GameObject planet2, int index)
    {
        queue.Add((planet1, planet2, index));
    }

    private void Update()
    {
        if (queue.Count == 0)
        {
            return;
        }

        (GameObject, GameObject, int) targetPlanets = queue[0];
        queue.RemoveAt(0);
        RemoveSamePlanetsInQueue(targetPlanets.Item1);
        RemoveSamePlanetsInQueue(targetPlanets.Item2);

        GameManager.instance.NotifyObserver(PlayerAction.Combine);
        SpawnManager.instance.SpawnCombinedPlanet(targetPlanets.Item3, targetPlanets.Item1.transform.position, targetPlanets.Item2.transform.position);

        Destroy(targetPlanets.Item1);
        Destroy(targetPlanets.Item2);
    }

    public int GetCount()
    {
        return queue.Count;
    }

    private void RemoveSamePlanetsInQueue(GameObject planet)
    {
        int index = 0;
        while (index < queue.Count)
        {
            if (queue[index].Item1 == planet || queue[index].Item2 == planet)
            {
                queue.RemoveAt(0);
                continue;
            }
            index++;
        }
    }

}
