using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Invader : MonoBehaviour
{
    private Path path;
    [SerializeField] private float health;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float width = 4f;
    [SerializeField] private float height = 5f;
    [SerializeField] private int scoreGain;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _deadSoundSource;

    Action<Invader> gainScore;

    private bool isGetInPosition;


    public bool IsGetInPosition { get => isGetInPosition; set => isGetInPosition = value; }
    public int ScoreGain { get => scoreGain; set => scoreGain = value; }

    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360.0f), 5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    public void initializeData(Path path, Action<Invader> gainScore)
    {
        IsGetInPosition = false;
        this.path = path;
        this.gainScore = gainScore;
    }

    private void moveFollowPath()
    {
        transform.DOPath(path.WaysPoint, 5f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMove(transform.parent.position, 1f).OnComplete(() =>
            {
                IsGetInPosition = true;
            });
        });
    }

    private void OnEnable()
    {
        moveFollowPath();
    }

    public void loosingHealth(float health)
    {
        this.health -= health;
        if (this.health <= 0)
        {
            ParticleSystem ps = Instantiate(_particleSystem, transform.position, Quaternion.identity, transform.parent);
            Destroy(ps, 2f);
            gainScore.Invoke(this);
            Destroy(gameObject);
        }
    }
}
