using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region SingleTone

    public static GameManager instance = null;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if(instance == this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    #endregion
    
    /// <summary>
    /// link on playing field game object
    /// </summary>
    [SerializeField] private PlayingField _playingField;
    /// <summary>
    /// link on "score" UI text element
    /// </summary>
    [Header("UI")]
    [SerializeField] private Text _scoreUI;
    /// <summary>
    /// link on "Game over" UI panel
    /// </summary>
    [SerializeField] private GameObject _gameOverPanel;
    /// <summary>
    /// link on game UI
    /// </summary>
    [SerializeField] private GameObject _gameUI;
    
    /// <summary>
    /// link on UI image for show next shape
    /// </summary>
    [Space]
    [SerializeField] private Image _nextShapeImage; 
    /// <summary>
    /// link on sprite "T" shape
    /// </summary>
    [SerializeField] private Sprite _shapeT;
    /// <summary>
    /// link on sprite "J" shape
    /// </summary>
    [SerializeField] private Sprite _shapeJ; 
    /// <summary>
    /// link on sprite "L" shape
    /// </summary>
    [SerializeField] private Sprite _shapeL; 
    /// <summary>
    /// link on sprite "S" shape
    /// </summary>
    [SerializeField] private Sprite _shapeS; 
    /// <summary>
    /// link on sprite "Z" shape
    /// </summary>
    [SerializeField] private Sprite _shapeZ; 
    /// <summary>
    /// link on sprite "O" shape
    /// </summary>
    [SerializeField] private Sprite _shapeO; 
    /// <summary>
    /// link on sprite "I" shape
    /// </summary>
    [SerializeField] private Sprite _shapeI; 
    
    /// <summary>
    /// the variable holds the type of the next shape
    /// </summary>
    private ShapeType _nextShape;
    
    /// <summary>
    /// normal time, after which there is a fall by one position
    /// </summary>
    private float _normalFallingTime = 1f;
    /// <summary>
    /// short time, after which there is a fall by one position
    /// </summary>
    private float _speedFallingTime = 0.1f;
    /// <summary>
    /// variable for temporary storage of the fall time
    /// </summary>
    private float _fallTime;
    /// <summary>
    /// variable for storage time
    /// </summary>
    private float _previousTime;
    /// <summary>
    /// factor for upped speed shape
    /// </summary>
    private float _speedFactor = 0.01f;
    
    /// <summary>
    /// user score
    /// </summary>
    private int _score = 0;
    /// <summary>
    /// score per filled line
    /// </summary>
    private int _lineFillScore = 100;
    /// <summary>
    /// flag that game are running
    /// </summary>
    private bool _gameIsStarted = false;
    
    /// <summary>
    /// shape in playing field
    /// </summary>
    private Shape _shape;
    
    /// <summary>
    /// getter for playing field
    /// </summary>
    /// <returns>link that playing field</returns>
    public PlayingField GetPlayingField()
    {
        return _playingField;
    }
    /// <summary>
    /// preparation for the start of the game
    /// </summary>
    public void StartGame()
    {
        _normalFallingTime = 1;
        _score = 0;
        _scoreUI.text = "" + _score;
        _fallTime = _normalFallingTime;
        _nextShape = CreateNextShape();

        InstantiateFigure();
        _gameIsStarted = true;
    }
    /// <summary>
    /// init game again
    /// </summary>
    public void PlayAgain()
    {
        _playingField.RestoreField();
        StartGame();
    }
    /// <summary>
    /// instantiate shape on position
    /// </summary>
    private void InstantiateFigure()
    {
        _shape = new Shape();
        Vector2 shapePosition = GetPlayingField().GetSpawnPoint();
        _shape.Init(_nextShape, shapePosition);
        _nextShape = CreateNextShape();
    }

    /// <summary>
    /// key for game control
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) & _gameIsStarted)
        {
            _shape.Rotate();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) & _gameIsStarted)
        {
            _shape.MoveRight();
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow) & _gameIsStarted)
        {
            _shape.MoveLeft();
        }else if (Input.GetKeyDown(KeyCode.DownArrow) & _gameIsStarted)
        {
            _fallTime = _speedFallingTime;
        }else if (Input.GetKeyUp(KeyCode.DownArrow) & _gameIsStarted)
        {
            _fallTime = _normalFallingTime;
        }
    
        if ((Time.time - _previousTime > _fallTime) & _gameIsStarted)
        {
            _shape.MoveDown();
            _previousTime = Time.time;
        }
    }
   
    /// <summary>
    /// instantiate new shape
    /// </summary>
    /// <returns>shape</returns>
    private ShapeType CreateNextShape()
    {
        Array array = Enum.GetValues(typeof(ShapeType));
        System.Random random = new System.Random();
        ShapeType randomShape = (ShapeType)array.GetValue(random.Next(array.Length));

        _nextShapeImage.sprite = ShowShapeOnUI(randomShape);
        return randomShape;
    }
    /// <summary>
    /// find image shape for UI
    /// </summary>
    /// <param name="nextShapeType">shape type</param>
    /// <returns>sprite on shape</returns>
    private Sprite ShowShapeOnUI(ShapeType nextShapeType)
    {
        Sprite returnValue = _shapeT;
        switch (nextShapeType)
        {
            case ShapeType.T: returnValue = _shapeT;
                break;
            case ShapeType.J: returnValue = _shapeJ;
                break;
            case ShapeType.L: returnValue = _shapeL;
                break;
            case ShapeType.S: returnValue = _shapeS;
                break;
            case ShapeType.Z: returnValue = _shapeZ;
                break;
            case ShapeType.I: returnValue = _shapeI;
                break;
            case ShapeType.O: returnValue = _shapeO;
                break;
        }
        return returnValue;
    }
    /// <summary>
    /// when the shape fall
    /// </summary>
    public void ShapeIsDown()
    {
        _playingField.Replace();
        if (_playingField.FindFillLine())
        {
            _playingField.RemoveFilledLine();
        }

        if (!_playingField.FieldIsFull())
        {
            InstantiateFigure();
        }
        else
        {
            _gameIsStarted = false;
            GameOver();
        }
    }
    /// <summary>
    /// when game is over
    /// </summary>
    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _gameUI.SetActive(false);
    }

    /// <summary>
    /// added score for dropping line
    /// </summary>
    public void AddScore()
    {
        _score += _lineFillScore;
        _scoreUI.text = "" + _score;

        int speed = _score / _lineFillScore;
        _normalFallingTime = 1 - speed * _speedFactor;
    }
}
