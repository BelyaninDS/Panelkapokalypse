using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid<TGridObject> 
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width,height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
            for (int y = 0; y < gridArray.GetLength(1); y++)
                gridArray[x, y] = createGridObject(this, x, y);


        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), new Color(1f, 1f, 1f, 0.5f), 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), new Color(1f, 1f, 1f, 0.5f), 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), new Color(1f,1f,1f,0.5f), 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), new Color(1f, 1f, 1f, 0.5f), 100f);
    }


    private Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    //Конвертировать вектор положения в int x и int y
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.x - originPosition.x) / cellSize);
        y = Mathf.FloorToInt((worldPosition.y - originPosition.y) / cellSize);
    }


    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }


    //Получить значение ячейки
    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x <= width && y <= height)
            return gridArray[x, y];
        else
            return default(TGridObject);
    }
    
    //Перегрузка
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }


    //Установить значение ячейки
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    //Перегрузка
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x <= width && y <= height)
            gridArray[x, y] = value;
    }


    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }
        


}
