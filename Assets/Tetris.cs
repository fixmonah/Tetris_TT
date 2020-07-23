//http://doctrina-kharkov.blogspot.com/2016/07/programme.html
using UnityEngine;

public class Tetris : MonoBehaviour
{
	public GameObject pfbBlock; //префаб блока

	public int[,] pole = new int[,]{
		{0,0,0,0,0,0,0,0},
		{0,0,0,1,1,1,0,0},
		{0,0,0,0,1,0,0,0},
		{0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0},
		{2,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,2,2},
		{2,0,0,0,0,2,2,2},
		{2,0,0,0,2,2,2,2},
		{2,0,0,0,2,2,2,2},
		{2,0,0,0,2,2,2,2},
	};
	public GameObject[,] blocks; //массив блоков на экране

	protected int[,] figI = new int[,]{
		{0,1,0,0},
		{0,1,0,0},
		{0,1,0,0},
		{0,1,0,0},
	};
	int leftX = 3; //левая верхняя координата фигуры
	int leftY = 1; //левая верхняя координата фигуры
	int figSize = 3; //размер фигуры


	void Start ()
	{
		FillAll ();
	}

	void FillAll ()
	{
		blocks = new GameObject[16, 8];

		for (int y = 0; y < 16; y++) {
			for (int x = 0; x < 8; x++) {
				blocks [y, x] = GameObject.Instantiate (pfbBlock);
				blocks [y, x].transform.position = new Vector3 (x, 15 - y, 0); //в массиве координаты по Y увеличиваются вниз, а в Unity вверх 
			}
		}
	}

	void Draw ()
	{
		for (int y = 0; y < 16; y++) {
			for (int x = 0; x < 8; x++) {
				if (pole [y, x] > 0) {
					blocks [y, x].SetActive (true);
					if ( pole[y,x] == 2 ){
						blocks[y,x].GetComponent<SpriteRenderer>().color = Color.white;				
					}
					if ( pole[y,x] == 1 ){
						blocks[y,x].GetComponent<SpriteRenderer>().color = Color.red;				
					}
				} else {
					blocks [y, x].SetActive (false);
				}
			}
		}
	}

	void Replace ()
	{
		for (int y = 0; y < 16; y++) {
			for (int x = 0; x < 8; x++) {				
				if (pole [y, x] == 1) {
					pole [y, x] = 2;
				} else {
					pole [y, x] = pole [y, x];
				}
			}
		}
	}

	void MoveRight ()
	{

		int[,] tmp = new int[16, 8];

		for (int y = 0; y < 16; y++) {
			for (int x = 0; x < 8; x++) {

				if (x == 6 && pole [y, x + 1] == 1) {	
					return;
				}

				if (pole [y, x] == 1 && pole [y, x + 1] == 2) {
					return;
				}

				if (pole [y, x] == 1) {
					tmp [y, x + 1] = 1;
				}

				if (pole [y, x] == 2) {
					tmp [y, x] = pole [y, x];
				}
			}
		}

		leftX++;

		pole = tmp;
	}

	void MoveLeft ()
	{		
		int[,] tmp = new int[16, 8];
		
		for (int y = 0; y < 16; y++) {
			for (int x = 7; x >= 0; x--) {
				
				if (x == 1 && pole [y, x - 1] == 1) {	
					return;
				}
				
				if (pole [y, x] == 1 && pole [y, x - 1] == 2) {
					return;
				}
				
				if (pole [y, x] == 1) {
					tmp [y, x - 1] = 1;
				}
				
				if (pole [y, x] == 2) {
					tmp [y, x] = pole [y, x];
				}
			}
		}

		leftX--;

		pole = tmp;
	}

	void MoveDown ()
	{
		int[,] tmp = new int[16, 8];

		for (int y = 15; y >= 0; y--) {
			for (int x = 0; x < 8; x++) {

				if (y > 0) {
					if (pole [y, x] == 2 && pole [y - 1, x] == 1) {
						Replace (); //заменяем все единицы на двойки
						return; //преккащаем
					}
				}

				if (y == 15 && pole [y, x] == 1) { //добрались до низа						
					Replace (); //заменяем все единицы на двойки
					return; //преккащаем
				}			

				if (y < 15) {
					if (pole [y, x] == 1) {
						tmp [y + 1, x] = 1;
					}
				}

				if (pole [y, x] == 2) {
					tmp [y, x] = 2;
				}
			}
		}

		leftY++;

		pole = tmp;

	}

	void Rotate ()
	{
		
		int[,] tmp = new int[figSize, figSize];
		
		for (int y = leftY; y < leftY+figSize; y++) {
			for (int x = leftX; x < leftX+figSize; x++) {
				tmp [y - leftY, x - leftX] = pole [y, x];
				if (pole [y, x] == 2) {
					return;
				}
			}
		}
		
		for (int y = 0; y < figSize; y++) {
			for (int x = 0; x < figSize; x++) {
				pole [y + leftY, x + leftX] = tmp [(figSize - 1) - x, y];
			}
		}
		
		
	}
	
	void CleanLine (int line)
	{
		for (int x=0; x<8; x++) {
			pole [line, x] = 0;
		}
	}
	
	void Clean ()
	{
		
		int cleanedLines = 0;
		for (int y = 15; y >= 0; y--) {
			int sum = 0;
			for (int x = 0; x < 8; x++) {
				sum = sum + pole [y, x];
				if (sum == 16) {
					CleanLine (y);
					cleanedLines++;
				}
			}
		}
		
		for (int i = 0; i < cleanedLines; i++) {
			for (int y = 15; y > 1; y--) {
				for (int x = 0; x < 8; x++) {
					if (pole [y, x] == 0 && pole [y - 1, x] == 2) {
						pole [y, x] = 2;
						pole [y - 1, x] = 0;
					}
				}
			}
		}
	}
	
	void AddFigure ()
	{		
		leftX = 3; //левая верхняя координата фигуры
		leftY = 1; //левая верхняя координата фигуры
		figSize = 4;
		
		for (int y = 0; y < figSize; y++) {
			for (int x = 0; x < figSize; x++) {
				pole [y + leftY, x + leftX] = figI [y, x];
			}
		}
	}

	void Update ()
	{	
		if (Input.GetKeyDown ("down")) {
			MoveDown ();
		}

		if (Input.GetKeyDown ("right")) {
			MoveRight ();
		}

		if (Input.GetKeyDown ("left")) {
			MoveLeft ();
		}

		if (Input.GetKeyDown ("c")) {
			Clean ();
		}
		
		if (Input.GetKeyDown ("space")) {
			Rotate ();
		}
		
		if (Input.GetKeyDown ("n")) {
			AddFigure ();
		}

		Draw ();
	}
}