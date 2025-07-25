using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using Fusion;
using UnityEngine;

public class AppleTree : NetworkBehaviour
{
    public GameObject[] apple;
    public GameObject cong1Tao;
    [Networked, OnChangedRender(nameof(TruTao))]
    public int countApple { get; set; } = 4;

    public override void Spawned()
    {
        countApple = apple.Count();
        cong1Tao.SetActive(false);
    }
    public override void FixedUpdateNetwork()
    {
        countApple = apple.Count();
    }

    public void TruTao()
    {
        foreach (var item in apple)
        {
            if(item != null)
            {
                item.gameObject.SetActive(false);
                break;
            }
        }
        cong1Tao.SetActive(true);
        StartCoroutine(Riset());
        cong1Tao.SetActive(false );

        

    }
    public void SetTao()
    {
        countApple -= 1;
    }
    public IEnumerator Riset()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
