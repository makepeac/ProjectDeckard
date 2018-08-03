using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    RectTransform npcStartingLocation;

    [SerializeField]
    List<NPC> npcs;

    public void UpdateNpcs()
    {
        foreach (NPC npc in npcs)
        {
            Transform spawnPoint = npc.getCurrentSpawnPoint();
            if (spawnPoint)
            {
                Debug.Log(spawnPoint.position);
                npc.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y);
            }
            else
            {
                npc.transform.position = new Vector3(npcStartingLocation.position.x, npcStartingLocation.position.y);
            }

        }
    }
}
