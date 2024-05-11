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
    private float destroyDelay = 10f; // Klonlar�n ka� saniye sonra silinece�ini belirler

    private List<GameObject> activeBlocks = new List<GameObject>(); // Bloklar� takip eden liste

    void Start()
    {
        for (int i = 0; i < numberBlocksInScreen; i++)
        {
            SpawnBlocks();
        }

        // Bloklar� s�rayla temizlemek i�in Coroutine ba�lat�r
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

    // Bloklar� s�rayla silmek i�in Coroutine
    IEnumerator DestroyBlocksCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(destroyDelay); // Belirtilen s�re bekle

            // E�er listede blok varsa, s�radaki blok silinir
            if (activeBlocks.Count > 0)
            {
                GameObject blockToDestroy = activeBlocks[0];
                activeBlocks.RemoveAt(0); // Listeden ��kar
                Destroy(blockToDestroy); // Sahneden kald�r
            }
        }
    }
}
