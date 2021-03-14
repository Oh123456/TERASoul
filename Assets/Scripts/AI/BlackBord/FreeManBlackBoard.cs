using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackProbability
{
    public string key;
    public float percent;
    // 공격 모션
    public int num;
}



public class FreeManBlackBoard : BlackBoard
{
    public GameObject target;
    public GameObject owner;
    public float attack3_dir = 8.0f;
    public bool isMove;
    public bool isattackRange;
    public float originSpeed;
    public float attackRange = 3.0f;

    [SerializeField]
    private List<AttackProbability> list;
    public Dictionary<string, float> Probability;

    private void Awake()
    {
        Probability = new Dictionary<string, float>();
    }


    private void Start()
    {
        foreach (AttackProbability item in list)
        {
            Probability.Add(item.key, item.percent);
        }

       
    }

}