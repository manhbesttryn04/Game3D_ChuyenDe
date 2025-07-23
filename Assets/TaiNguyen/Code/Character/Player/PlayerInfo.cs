using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private string namePlayer;
    [SerializeField] private int level;
    [SerializeField] private float exp;
    [SerializeField] private float attack;
    [SerializeField] private float deff;
    public int[] levels = new int[15];
    public int solevel = 0;
   
    void Start()
    {
        
        level = 1;
        exp = 0;
        attack = 10f;
        deff = 5f;
        levels[0] = 100; ;//cap 1 len 2 can 100exp
        for (int i = 1; i < levels.Length; i++)
        {
            levels[i] = levels[i -1] +(int)(levels[i -1]*0.5);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddExp(10);
        }
    }
    public void AddExp(float exp)
    {
        this.exp += exp;
        if(this.exp >= levels[level -1 ])
        {
            this.exp = 0;
            level++;
            attack += 20f;
            deff += 10;
            solevel++;
        }
    }
    public int GetLevel() {
        return level;
    }
    public int GetSoLevel()
    {
        return levels[solevel];
    }
   
        public float GetExp()
    {
        return exp;
    }
}
