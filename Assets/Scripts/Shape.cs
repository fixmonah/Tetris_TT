using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
    /// <summary>
    /// angle the shape is rotated to
    /// </summary>
    private int _currentAngle;
    /// <summary>
    /// angles that the shape can turn
    /// </summary>
    private ShapeAngles[] _rotationShapes;
    /// <summary>
    /// type of this shape
    /// </summary>
    private ShapeType _shapeType;

    /// <summary>
    /// a flag that allows the shape to be moved after touching the ground or another shape
    /// </summary>
    private bool _flagBeforeStop = false;
    /// <summary>
    /// shape control lock flag
    /// </summary>
    private bool _blockedControlKey = false;

    /// <summary>
    /// position shape in playing field
    /// </summary>
    private Vector2 _position;

    #region Init
    /// <summary>
    /// create shape at the specified position of the playing field
    /// </summary>
    /// <param name="type"></param>
    /// <param name="position"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void Init(ShapeType type, Vector2 position)
    {
        _position = position;
        _currentAngle = 0;
        _shapeType = type;
        switch (_shapeType)
        {
            case ShapeType.T:
                InitT(); 
                break;
            case ShapeType.J:
                InitJ();
                break;
            case ShapeType.L:
                InitL();
                break;
            case ShapeType.S:
                InitS();
                break;
            case ShapeType.Z:
                InitZ();
                break;
            case ShapeType.I:
                InitI();
                break;
            case ShapeType.O:
                InitO();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        PasteShape();
    }

    /// <summary>
    /// shape "T" initiation
    /// </summary>
    private void InitT()
    {
        /*
         * 0 - empty
         * 2 - part of figure
         */

        _rotationShapes = new ShapeAngles[4];
        _rotationShapes[0].Angle = 0;
        _rotationShapes[0].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,2,0,0},
            {2,2,2,0},
            {0,0,0,0}
        };
        _rotationShapes[1].Angle = 1;
        _rotationShapes[1].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,2,0,0},
            {0,2,2,0},
            {0,2,0,0}
        };
        _rotationShapes[2].Angle = 2;
        _rotationShapes[2].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,0,0,0},
            {2,2,2,0},
            {0,2,0,0}
        };
        _rotationShapes[3].Angle = 3;
        _rotationShapes[3].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,2,0,0},
            {2,2,0,0},
            {0,2,0,0}
        };
    }
    /// <summary>
    /// shape "J" initiation
    /// </summary>
    private void InitJ()
    {
        /*
         * 0 - empty
         * 2 - part of figure
         */

        _rotationShapes = new ShapeAngles[4];
        _rotationShapes[0].Angle = 0;
        _rotationShapes[0].Map = new int[4,4]
        {
            {0,0,0,0},
            {2,0,0,0},
            {2,2,2,0},
            {0,0,0,0}
        };
        _rotationShapes[1].Angle = 1;
        _rotationShapes[1].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,2,2,0},
            {0,2,0,0},
            {0,2,0,0}
        };
        _rotationShapes[2].Angle = 2;
        _rotationShapes[2].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,0,0,0},
            {2,2,2,0},
            {0,0,2,0}
        };
        _rotationShapes[3].Angle = 3;
        _rotationShapes[3].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,2,0,0},
            {0,2,0,0},
            {2,2,0,0}
        };
    }
    /// <summary>
    /// shape "L" initiation
    /// </summary>
    private void InitL()
    {
        /*
         * 0 - empty
         * 2 - part of figure
         */

        _rotationShapes = new ShapeAngles[4];
        _rotationShapes[0].Angle = 0;
        _rotationShapes[0].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,0,2,0},
            {2,2,2,0},
            {0,0,0,0}
        };
        _rotationShapes[1].Angle = 1;
        _rotationShapes[1].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,2,0,0},
            {0,2,0,0},
            {0,2,2,0}
        };
        _rotationShapes[2].Angle = 2;
        _rotationShapes[2].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,0,0,0},
            {2,2,2,0},
            {2,0,0,0}
        };
        _rotationShapes[3].Angle = 3;
        _rotationShapes[3].Map = new int[4,4]
        {
            {0,0,0,0},
            {2,2,0,0},
            {0,2,0,0},
            {0,2,0,0}
        };
    }
    /// <summary>
    /// shape "S" initiation
    /// </summary>
    private void InitS()
    {
        /*
         * 0 - empty
         * 2 - part of figure
         */

        _rotationShapes = new ShapeAngles[2];
        _rotationShapes[0].Angle = 0;
        _rotationShapes[0].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,0,0,0},
            {0,2,2,0},
            {2,2,0,0}
        };
        _rotationShapes[1].Angle = 1;
        _rotationShapes[1].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,2,0,0},
            {0,2,2,0},
            {0,0,2,0}
        };
    }
    /// <summary>
    /// shape "Z" initiation
    /// </summary>
    private void InitZ()
    {
        /*
         * 0 - empty
         * 2 - part of figure
         */

        _rotationShapes = new ShapeAngles[2];
        _rotationShapes[0].Angle = 0;
        _rotationShapes[0].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,0,0,0},
            {2,2,0,0},
            {0,2,2,0}
        };
        _rotationShapes[1].Angle = 1;
        _rotationShapes[1].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,0,2,0},
            {0,2,2,0},
            {0,2,0,0}
        };
    }
    /// <summary>
    /// shape "I" initiation
    /// </summary>
    private void InitI()
    {
        /*
         * 0 - empty
         * 2 - part of figure
         */

        _rotationShapes = new ShapeAngles[2];
        _rotationShapes[0].Angle = 0;
        _rotationShapes[0].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,0,0,0},
            {2,2,2,2},
            {0,0,0,0}
        };
        _rotationShapes[1].Angle = 1;
        _rotationShapes[1].Map = new int[4,4]
        {
            {0,2,0,0},
            {0,2,0,0},
            {0,2,0,0},
            {0,2,0,0}
        };
    }
    /// <summary>
    /// shape "O" initiation
    /// </summary>
    private void InitO()
    {
        /*
         * 0 - empty
         * 1 - part of figure
         */

        _rotationShapes = new ShapeAngles[1];
        _rotationShapes[0].Angle = 0;
        _rotationShapes[0].Map = new int[4,4]
        {
            {0,0,0,0},
            {0,0,0,0},
            {2,2,0,0},
            {2,2,0,0}
        };
    }

    /// <summary>
    /// insert a shape into the game board
    /// </summary>
    private void PasteShape()
    {
        int[,] gameField = (int[,])GameManager.instance.GetPlayingField().GetPlayingField().Clone();
        gameField = RemoveShape(gameField);
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int valueOnMap = _rotationShapes[_currentAngle].Map[j, i];
                if (valueOnMap != 0)
                {
                    int shapePosX = (int) _position.x + i;
                    int shapePosY = (int) _position.y + j;
                    gameField[shapePosY, shapePosX] = _rotationShapes[_currentAngle].Map[j, i];
                }
            }
        }

        GameManager.instance.GetPlayingField().SetFieldData(gameField);
    }
    /// <summary>
    /// clear the playing field from the shape
    /// </summary>
    /// <param name="gameField">playing field</param>
    /// <returns>playing field without a shape</returns>
    private int[,] RemoveShape(int[,] gameField)
    {
        int gameFieldWidth = GameManager.instance.GetPlayingField().GetFieldWidth();
        int gameFieldHeight = GameManager.instance.GetPlayingField().GetFieldHeight();
        for (int y = 0; y < gameFieldHeight; y++) {
            for (int x = 0; x < gameFieldWidth; x++) {				
                if (gameField [y, x] == 2) {
                    gameField [y, x] = 0;
                }
            }
        }
        return gameField;
    }
    
    #endregion
    #region Orders
    
    /// <summary>
    /// disables shape control
    /// </summary>
    public void DestroyShape()
    {
        _blockedControlKey = true;
        GameManager.instance.ShapeIsDown();
    }
    /// <summary>
    /// get permission to move the shape to the left
    /// </summary>
    /// <returns>permission result</returns>
    private bool GetOrderMoveLeft()
    {
        int[,] matrixCurrentPosition =  MergeMatrix(_rotationShapes[_currentAngle].Map,new Vector2(-1,0));
        bool resultValue = true;
        foreach (var element in matrixCurrentPosition)
        {
            if (element >= 5)
            {
                resultValue = false;
            }
        }
        return resultValue;
    }
    /// <summary>
    /// get permission to move the shape to the right
    /// </summary>
    /// <returns>permission result</returns>
    private bool GetOrderMoveRight()
    {
        int[,] matrixCurrentPosition =  MergeMatrix(_rotationShapes[_currentAngle].Map,new Vector2(1,0));
        bool resultValue = true;
        foreach (var element in matrixCurrentPosition)
        {
            if (element >= 5)
            {
                resultValue = false;
            }
        }
        return resultValue;
    }
    /// <summary>
    /// gets permission to move the shape downward
    /// </summary>
    /// <returns>permission result</returns>
    private bool GetOrderMoveDown()
    {
        int[,] matrixCurrentPosition =  MergeMatrix(_rotationShapes[_currentAngle].Map,new Vector2(0,1));
        bool resultValue = true;
        foreach (var element in matrixCurrentPosition)
        {
            if (element >= 5)
            {
                resultValue = false;
            }
        }
        return resultValue;
    }
    /// <summary>
    /// gets permission to rotate the shape
    /// </summary>
    /// <returns></returns>
    private bool GetOrderRotation()
    {
       int[,] matrixRotationToNextAngle =  MergeMatrix(_rotationShapes[GetNextRotateAngle()].Map,new Vector2(0,0));
       bool resultValue = true;
       foreach (var element in matrixRotationToNextAngle)
       {
           if (element >= 5)
           {
               resultValue = false;
           }
       }
       return resultValue;
    }
    /// <summary>
    /// Combines information about a shape with information from around the shape
    /// </summary>
    /// <param name="shapeMap">shape map</param>
    /// <param name="offset">offset position</param>
    /// <returns></returns>
    private int[,] MergeMatrix(int[,] shapeMap, Vector2 offset)
    {
        int positionX = (int) _position.x + (int) offset.x;
        int positionY = (int) _position.y + (int) offset.y;
        int[,] zoneAroundFigure = (int[,])GameManager.instance.GetPlayingField().GetZoneAround(positionX, positionY).Clone();
        int[,] zoneInInt = new int[4,4];
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                zoneInInt[j, i] = zoneAroundFigure[j, i] + shapeMap[j, i];
            }
        }
        
        return zoneInInt;
    }

    #endregion
    #region Moove and Rotate
    /// <summary>
    /// move shape to the left
    /// </summary>
    public void MoveLeft()
    {
        if (GetOrderMoveLeft() & !_blockedControlKey)
        {
            int newPositionX = (int) _position.x - 1;
            int newPositionY = (int) _position.y;
            _position = new Vector2(newPositionX, newPositionY);
            PasteShape();
        }
    }
    /// <summary>
    /// move shape to the right
    /// </summary>
    public void MoveRight()
    {
        if (GetOrderMoveRight() & !_blockedControlKey)
        {
            int newPositionX = (int) _position.x + 1;
            int newPositionY = (int) _position.y;
            _position = new Vector2(newPositionX, newPositionY);
            PasteShape();
        }
    }
    /// <summary>
    /// move shape a down
    /// </summary>
    public void MoveDown()
    {
        if (GetOrderMoveDown() & !_blockedControlKey)
        {
            int newPositionX = (int) _position.x;
            int newPositionY = (int) _position.y + 1;
            _position = new Vector2(newPositionX, newPositionY);
            PasteShape();
            _flagBeforeStop = false;
        }
        else
        {
            DestroyShape();
            _flagBeforeStop = true;
        }
    }
    /// <summary>
    /// rotate the figure
    /// </summary>
    public void Rotate()
    {
        if (GetOrderRotation() & !_blockedControlKey)
        {
            _currentAngle = GetNextRotateAngle();
            PasteShape();
        }
    }
    /// <summary>
    /// get the next angle to rotate
    /// </summary>
    /// <returns></returns>
    private int GetNextRotateAngle()
    {
        int angle = _currentAngle;
        
        if (angle == _rotationShapes.Length - 1)
        {
            angle = 0;
        }
        else
        {
            angle++;
        }

        return angle;
    }

    #endregion
}
