using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<MapData> mapDatas;
    [SerializeField] private GameObject mapHolder;
    [SerializeField] private List<EnemySpawner> _enemySpawners;
    [SerializeField] private Transform _formationHolder;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverText;

    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Text titleOverPanel;
    public static GameController instance;

    [SerializeField] private AudioClip[] _inGameAudio;
    [SerializeField] private AudioClip winningAudio;

    [SerializeField] private int totalScore;


    private MapData _currentMap;
    private AudioSource audioSource;
    private int _currentLevels = 0;
    private int _spawnerLevel = 0;
    private int _currentAudioClip = 0;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = _inGameAudio[_currentAudioClip];
    }
    private void Start()
    {
        _currentMap = mapDatas[_currentLevels];
        CreateMap();
    }

    private void CreateMap()
    {
        for (int i = 0; i < _currentMap.pathFollows.Count; i++)
        {
            // create path for enemy
            Path _path = Instantiate(_currentMap.pathFollows[i], mapHolder.transform);
            _path.gameObject.SetActive(true);
            _path.convertWayPoint();

            // create fomation of enemy
            Formation _formation = Instantiate(_currentMap.formation, _formationHolder);
            _formation.initializeData(increaseLevels, decreaseLevels);
            _formation.gameObject.SetActive(true);

            // spawn enemy
            _enemySpawners[i].initializeData(_path, _currentMap.NumberOfEnemy, _formation, _spawnerLevel, gainScore);
        }
    }

    private void Update()
    {
        _score.text = "Score: " + totalScore;
        if (!audioSource.isPlaying)
        {
            _currentAudioClip++;
            if (_currentAudioClip > _inGameAudio.Length - 1)
            {
                _currentAudioClip = 0;
            }
            audioSource.clip = _inGameAudio[_currentAudioClip];
            audioSource.Play();
        }
    }

    public void increaseLevels(Formation formation)
    {
        _currentLevels++;
        if (_currentLevels > 2)
        {
            _spawnerLevel++;
            if (_spawnerLevel > 2)
            {
                audioSource.Stop();
                audioSource.clip = winningAudio;
                audioSource.Play();
                // if maximum spawner level then end game
                EndGame("You saved the whole nebula!! good job hero xD");
                return;
            }
            _currentLevels = 0;

        }
        _currentMap = mapDatas[_currentLevels];
        CreateMap();

    }

    public void decreaseLevels(Formation formation)
    {
        _currentLevels--;
        _currentMap = mapDatas[_currentLevels];
        CreateMap();
    }

    private void gainScore(Invader invader)
    {
        totalScore += invader.ScoreGain;
    }

    public void EndGame(String titleText)
    {
        titleOverPanel.text = titleText;
        gameOverText.text = totalScore.ToString();
        gameOverPanel.SetActive(true);
    }
}
