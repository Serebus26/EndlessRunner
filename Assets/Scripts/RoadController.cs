using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public Transform playerT;
    public GameObject blocksPref;
    public float spawnZ = 0f;
    public float blockLength = 10f;
    private int numberBlocksInScreen = 5;
    private float destroyDelay = 10f; // Klonlarýn kaç saniye sonra silineceðini belirler

    private List<GameObject> activeBlocks = new List<GameObject>(); // Bloklarý takip eden liste

    void Start()
    {
        for (int i = 0; i < numberBlocksInScreen; i++)
        {
            SpawnBlocks();
        }

        // Bloklarý sýrayla temizlemek için Coroutine baþlatýr
        StartCoroutine(DestroyBlocksCoroutine());
    }

    void Update()
    {
        if (playerT.position.z > spawnZ - (numberBlocksInScreen * blockLength))
        {
            SpawnBlocks();
        }
    }

    private void SpawnBlocks()
    {
        GameObject go = Instantiate(blocksPref) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += blockLength;

        // Listeye ekle
        activeBlocks.Add(go);
    }

    // Bloklarý sýrayla silmek için Coroutine
    IEnumerator DestroyBlocksCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(destroyDelay); // Belirtilen süre bekle

            // Eðer listede blok varsa, sýradaki blok silinir
            if (activeBlocks.Count > 0)
            {
                GameObject blockToDestroy = activeBlocks[0];
                activeBlocks.RemoveAt(0); // Listeden çýkar
                Destroy(blockToDestroy); // Sahneden kaldýr
            }
        }
    }
}
