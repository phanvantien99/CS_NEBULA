using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Invader[] _invaderPrefab;
    private int level = 0;
    private Path _path;
    private int _amountInvader;
    private Formation _formation;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void initializeData(Path _path, int amountInvader, Formation formation, int level, Action<Invader> gainScore)
    {
        this._path = _path;
        _amountInvader = amountInvader;
        _formation = formation;
        _amountInvader = _formation.Slots.Count;
        this.level = level;
        StartCoroutine(spawnInvader(gainScore));
    }

    private IEnumerator spawnInvader(Action<Invader> gainScore)
    {
        for (int i = 0; i < _amountInvader; i++)
        {
            // spawn as child of the slot to control the postion
            Invader invader = Instantiate(_invaderPrefab[level], transform.position, Quaternion.identity, _formation.Slots[i].transform);
            invader.initializeData(_path, gainScore);
            invader.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
        }
    }
}
