using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] GameObject hP;
    [SerializeField] GameObject maNa;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject deff;
   
   
    public void DropRandomItems(Vector3 vector)
    {
        float random = Random.Range(0f, 100f);
        vector.y += 1f;
        if(random < 99f)
        {
            random = Random.Range(0f, 10f);
                if(random < 5f)
            {
                Instantiate(hP, vector, Quaternion.identity);  

            }else
            {
                Instantiate(maNa, vector, Quaternion.identity);
            }
        }
        else if (random < 99.5f)
        {
            Instantiate (sword, vector, Quaternion.identity);
        }
        else
        {
            Instantiate(deff, vector, Quaternion.identity);
        }
    }
}
