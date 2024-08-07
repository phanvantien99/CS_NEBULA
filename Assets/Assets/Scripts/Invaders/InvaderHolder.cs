using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InvaderHolder : MonoBehaviour
{
    [SerializeField] private Invader[] invaders;

    [SerializeField] private int rows = 5;

    [SerializeField] private int columns = 5;

    [SerializeField] private float spacing = 2f;

    [SerializeField] private float speed = 1f;
    private Vector3 _direction = Vector2.right;

    [SerializeField] private Vector2 anchor;

    // public Vector3 Direction { get => _direction; set => _direction = value; }

    private void Awake()
    {
        // float width = spacing * (this.columns - 1);
        // float height = spacing * (this.rows - 1);
        // Vector2 centering = new Vector2(-width / 2, -height / 2);
        // for (int row = 0; row < this.rows; row++)
        // {
        //     Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * spacing), 0.0f);
        //     for (int column = 0; column < this.columns; column++)
        //     {
        //         Invader invader = Instantiate(this.invaders[row], this.transform);
        //         Vector3 position = rowPosition;
        //         position.x += column * spacing;
        //         // relative to his parent
        //         invader.transform.localPosition = position;
        //     }
        // }
        Invader invader = Instantiate(this.invaders[0], this.transform);
        // invader.Path = 
    }

    private void Start()
    {
        // transform.DOMove(_direction, 4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // private void Update()
    // {
    //     this.transform.position += this.Direction * this.speed * Time.deltaTime;
    // }

    IEnumerator InstatiatePeriod(int frequency)
    {
        while (frequency > 0)
        {
            yield return new WaitForSeconds(1.0f);
            Invader invader = Instantiate(this.invaders[0], this.transform);
            frequency--;
            yield return null;
        }
    }
}
