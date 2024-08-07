using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MapSO")]
public class MapData : ScriptableObject
{
    private int numberOfEnemy;
    public List<Path> pathFollows;
    public Formation formation;

    public int NumberOfEnemy { get => numberOfEnemy; set => numberOfEnemy = value; }
}
