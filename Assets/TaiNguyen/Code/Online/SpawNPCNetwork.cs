using System.Linq;
using Fusion;
using UnityEngine;
using UnityEngine.AI;

public class SpawNPCNetwork : NetworkBehaviour

{
    [Header("Spawn NPC")]
    public Transform[] transformsNPC;
    
    public GameObject[] npcPrefab;
    public override void Spawned()
    {
       
        // Chỉ thực hiện khi có quyền sở hữu trạng thái
        if (Object.HasStateAuthority)
        {
           
            for (int i = 0; i < transformsNPC.Count(); i++)
            {
                var randomIndex = Random.Range(0, npcPrefab.Length);
                Runner.Spawn(npcPrefab[randomIndex], transformsNPC[i].position, transformsNPC[i].rotation);
                   
               
               
            }

        }
    }
}
