using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }
    private int maximumIndex;
    [SerializeField] private Transform topLineTransform;
    [SerializeField] private Image nextPlanetSprite;
    [SerializeField] private List<Sprite> planetSprites;
    [SerializeField] private List<GameObject> planetNonPhysics;
    [SerializeField] private Transform box;
    [HideInInspector] public GameObject nextPlanet;
    [HideInInspector] public GameObject currentPlanet;
    public List<GameObject> planetPhysics;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        maximumIndex = 2;
    }

    public void SpawnNonPhysicsRandomPlanet()
    {
        currentPlanet = Instantiate(nextPlanet, Player.instance.lineTransform.position, Quaternion.identity, Player.instance.lineTransform);
        CapsuleCollider2D planetCollider = currentPlanet.GetComponent<CapsuleCollider2D>();
        BoundaryController.instance.ChangeBoundary(planetCollider);
        nextPlanet = null;
    }


    public void GenerateNextPlanet()
    {
        int index = Random.Range(0, maximumIndex + 1);
        nextPlanetSprite.sprite = planetSprites[index];
        nextPlanet = planetNonPhysics[index];
    }

    public void GetNextPlanet()
    {
        currentPlanet = nextPlanet;
        SpawnNonPhysicsRandomPlanet();
        GenerateNextPlanet();
    }

    public void SpawnCombinedPlanet(int index, Vector3 pos1, Vector3 pos2)
    {
        Vector3 middlePos = (pos1 + pos2) / 2;
        GameObject combinedPlanet = Instantiate(planetPhysics[index + 1], middlePos, Quaternion.identity, box);
        PlanetCollide collide = combinedPlanet.GetComponent<PlanetCollide>();
        if (collide != null)
        {
            collide.isCombined = true;
        }
    }

    public void SpawnPhysicsPlanet()
    {
        int index = currentPlanet.GetComponent<PlanetInfo>().index;
        Instantiate(planetPhysics[index], Player.instance.lineTransform.position, Quaternion.identity, box);
        Destroy(currentPlanet);
        currentPlanet = null;
    }
}

