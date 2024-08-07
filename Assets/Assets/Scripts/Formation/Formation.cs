using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private List<GameObject> slots = new List<GameObject>();

    public List<GameObject> Slots { get => slots; set => slots = value; }
    private Sequence transitionSequence;

    private bool readyToMove = false;
    bool isChangedValue = false;



    private void Awake()
    {
        foreach (Transform children in transform)
        {
            slots.Add(children.gameObject);
        }
    }

    Action<Formation> _onCompleteLevels;
    Action<Formation> _onCompleteLevelsBatch;

    private void Start()
    {
        // transitionTweener = transform.DOMove(_endPoint.position, 2f)
        //                             .SetLoops(-1, LoopType.Yoyo)
        //                             .SetEase(Ease.Linear);
        // transitionTweener.Pause();

        transitionSequence = DOTween.Sequence();
        transitionSequence.Append(transform.DOMove(new Vector3(-7f, transform.position.y, transform.position.z), 2f).SetEase(Ease.Linear));
        transitionSequence.Append(transform.DOMove(new Vector3(7f, transform.position.y, transform.position.z), 2f).SetEase(Ease.Linear));
        transitionSequence.SetLoops(-1, LoopType.Yoyo);
        transitionSequence.Pause();
    }

    private void Update()
    {
        Invader[] invaders = transform.GetComponentsInChildren<Invader>();
        readyToMove = invaders.All(obj => obj.IsGetInPosition);
        if (readyToMove && !isChangedValue)
        {
            transform.DOMove(new Vector3(7f, transform.position.y, transform.position.z), 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transitionSequence.Play();
            });
            isChangedValue = true;
        }

        if (invaders.Length == 0)
        {
            _onCompleteLevels.Invoke(this);
            Destroy(this.gameObject);
        }
    }

    public void initializeData(Action<Formation> _onCompleteLevels, Action<Formation> _onCompleteLevelsBatch)
    {
        this._onCompleteLevels = _onCompleteLevels;
        this._onCompleteLevelsBatch = _onCompleteLevelsBatch;
    }
}
