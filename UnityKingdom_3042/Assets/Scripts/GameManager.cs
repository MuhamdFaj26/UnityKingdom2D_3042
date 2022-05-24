using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Origin_SkeltCreep => Resources.Load<GameObject>("Prefabs/Enemies/SkeltCreep");
    public GameObject Origin_CastLight => Resources.Load<GameObject>("Prefabs/Stuff/CastLight");


    public static GameManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

}


public enum EnemySet
{
    Default, Damaged, Native, Warrior, Witch, Skeleton,
}

public enum UnitState
{
    Idle, Move, Attack, Cast, Dead,
}