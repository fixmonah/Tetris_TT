using System;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    /// <summary>
    /// playing field width
    /// </summary>
    private int _fieldWidth = 15;
    /// <summary>
    /// playing field height
    /// </summary>
    private int _fieldHeight = 24;
    /// <summary>
    /// field top border
    /// </summary>
    private int _borderTop = 2;
    /// <summary>
    /// field below border
    /// </summary>
    private int _borderBelow = 2;
    /// <summary>
    /// field right border
    /// </summary>
    private int _borderRight = 2;
    /// <summary>
    /// field left border
    /// </summary>
    private int _borderLeft = 3;
    /// <summary>
    /// playing field
    /// </summary>
    private int[,] _field = new int[,]{
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,0,0,0,0,0,0,0,0,0,0,4,4,0},
        {4,4,4,4,4,4,4,4,4,4,4,4,4,4,0},
        {4,4,4,4,4,4,4,4,4,4,4,4,4,4,0},
    };
    /// <summary>
    /// backup playing field
    /// </summary>
    private int[,] _fieldBackUp;
    /// <summary>
    /// game object for showing the playing field
    /// </summary>
    private GameObject[,] _blocks;

    /// <summary>
    /// sprite for visualization playing field
    /// </summary>
    [SerializeField] private GameObject _block;
    /// <summary>
    /// color for visualization border block on playing field
    /// </summary>
    [SerializeField] private Color _borderColor = Color.white;	
    /// <summary>
    /// color for visualization shape block on playing field
    /// </summary>
    [SerializeField] private Color _shapeColor = Color.yellow;	
    /// <summary>
    /// color for visualization block on playing field
    /// </summary>
    [SerializeField] private Color _blockColor = Color.gray;	
    /// <summary>
    /// color for visualization removed block on playing field
    /// </summary>
    [SerializeField] private Color _blockForRemoveColor = Color.red;	
    
    /// <summary>
    /// start point for shape
    /// </summary>
    private Vector2 _spawnPoint = new Vector2(6,1);
    
    void Start()
    {
        _fieldBackUp = (int[,])_field.Clone();
        ShowOnScreen();
    }

    /// <summary>
    /// getter for return field width
    /// </summary>
    /// <returns>field width</returns>
    public int GetFieldWidth()
    {
        return _fieldWidth;
    }
    /// <summary>
    /// getter for return field height
    /// </summary>
    /// <returns>field height</returns>
    public int GetFieldHeight()
    {
        return _fieldHeight;
    }
    /// <summary>
    /// getter start point for shape
    /// </summary>
    /// <returns></returns>
    public Vector2 GetSpawnPoint()
    {
        return _spawnPoint;
    }
    /// <summary>
    /// Get part play field around shape
    /// </summary>
    /// <param name="startZoneX">x</param>
    /// <param name="startZoneY">y</param>
    /// <returns>part play field around shape</returns>
    public int[,] GetZoneAround(int startZoneX, int startZoneY)
    {
        int[,] subarray = new int[4,4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int cellValue = _field[startZoneY + j, startZoneX + i];
                if (cellValue == 2) // find figure
                {
                    cellValue = 0; // remove figure
                }
                subarray[j, i] = cellValue;
            }
        }
        return subarray;
    }
    /// <summary>
    /// Getter playing Field data
    /// </summary>
    /// <returns>playing Field data</returns>
    public int[,] GetPlayingField()
    {
        return _field;
    }
    /// <summary>
    /// Setter playing Field data
    /// </summary>
    /// <param name="newData">playing Field data</param>
    public void SetFieldData(int [,] newData)
    {
        _field = newData;
        Paint ();
    }
    /// <summary>
    /// mark the fallen shape like a brick
    /// </summary>
    public void Replace()
    {
        for (int y = 0; y < _fieldHeight; y++) {
            for (int x = 0; x < _fieldWidth; x++) {				
                if (_field [y, x] == 2) {
                    _field [y, x] = 3;
                }
            }
        }
        Paint ();
    }
    
    /// <summary>
    /// create a playing field from game objects
    /// </summary>
    private void ShowOnScreen ()
    {
        _blocks = new GameObject[_fieldHeight, _fieldWidth];

        for (int y = 0; y < _fieldHeight; y++) {
            for (int x = 0; x < _fieldWidth; x++) {
                _blocks [y, x] = Instantiate (_block,transform);
                _blocks [y, x].transform.position = new Vector3 (x, _fieldHeight - y, 0);
            }
        }
        Paint ();
    }
    /// <summary>
    /// paint game objects
    /// </summary>
    private void Paint ()
    {
        for (int y = 0; y < _fieldHeight; y++) {
            for (int x = 0; x < _fieldWidth; x++) {
                if (_field [y, x] > 0) {
                    _blocks [y, x].SetActive (true);
                    if ( _field[y,x] == 4 ){
                        _blocks[y,x].GetComponent<SpriteRenderer>().color = _borderColor;				
                    }
                    if ( _field[y,x] == 2 ){
                        _blocks[y,x].GetComponent<SpriteRenderer>().color = _shapeColor;				
                    }
                    if ( _field[y,x] == 3 ){
                        _blocks[y,x].GetComponent<SpriteRenderer>().color = _blockColor;				
                    }
                    if ( _field[y,x] == 5 ){
                        _blocks[y,x].GetComponent<SpriteRenderer>().color = _blockForRemoveColor;				
                    }
                } else {
                    _blocks [y, x].SetActive (false);
                }
            }
        }
    }

    /// <summary>
    /// looking for a filled line
    /// </summary>
    /// <returns>search results</returns>
    public bool FindFillLine()
    {
        bool retValue = false;
        
        for (int i = _borderTop; i < _fieldHeight - _borderBelow; i++)
        {
            int sumInLine = 0;
            for (int j = _borderRight; j < _fieldWidth - _borderLeft; j++)
            {
                sumInLine += _field[i, j];
            }

            if (sumInLine == (_fieldWidth - _borderRight - _borderLeft) * 3)
            {
                MarkCompletedLine(i);
                retValue = true;
            }
        }

        return retValue;
    }
    /// <summary>
    /// mark filled lines
    /// </summary>
    /// <param name="indexLine">line position</param>
    private void MarkCompletedLine(int indexLine)
    {
        for (int i = 0; i < _fieldWidth; i++)
        {
            if (_field[indexLine, i] == 3)
            {
                _field[indexLine, i] = 5;
            }
        }
    }
    /// <summary>
    /// Remove a filled line
    /// </summary>
    public void RemoveFilledLine()
    {
        for (int i = _fieldHeight - _borderBelow; i > _borderTop; i--)
        {
            if (_field[i, 3] == 5)
            {
                GameManager.instance.AddScore();
                for (int y = i; y > _borderTop; y--)
                {
                    for (int x = _borderRight; x < _fieldWidth - _borderLeft; x++)
                    {
                        _field[y, x] = _field[y - 1, x];
                    }
                }
                i++;
            }
        }
        Paint();
    }

    /// <summary>
    /// The playing field is full
    /// </summary>
    /// <returns>answer</returns>
    public bool FieldIsFull()
    {
        bool retValue = false;
        for (int x = _borderRight; x < _fieldWidth - _borderLeft; x++)
        {
            if (_field[3, x] >= 3)
            {
                retValue = true;
            }
        }

        return retValue;
    }
    /// <summary>
    /// Restored the playing field
    /// </summary>
    public void RestoreField()
    {
        _field = (int[,])_fieldBackUp.Clone();
    }
}
