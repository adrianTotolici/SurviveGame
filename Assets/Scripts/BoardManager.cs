using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Completed
{
    public class BoardManager : MonoBehaviour
    {

        [Serializable]
        public class Count
        {
            public int minimum;
            public int maximum;

            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }

        // Terrain type 1
        public GameObject[] terrain1;
        // Terrain type 2
        public GameObject[] terrain2;
        // Terrain type 3
        public GameObject[] terrain3;
        // Terrain type 4
        public GameObject[] terrain4;
        // Terrain type 5
        public GameObject[] terrain5;
        // List with all terrain tiles
        private GameObject[] terrainTiles;
        // object instance od a terrain tile
        private GameObject tileInstance;

        // boarHolder
        private Transform boardHolder;
        // store position of tiles on grid
        private List<Vector3> gridPostions = new List<Vector3>();

        // restriction in N of current tile 
        private string nord_face;
        // restriction in V of current tile 
        private string vest_face;
        // random tile selection for adding on map
        private int randomTile;
        // random rotation of curent selected tile
        private int rotation;
        // array with all restriction on V tile
        private string[] drawPosition = new string[columns];

        private const string land = "land";
        private const string water = "water";
        private const string none = "not set";
        private const string boardName = "Board";
        public const int columns = 32;
        public const int rows = 32;

        void InitialiseList()
        {
            gridPostions.Clear();
            for (int x = 1; x < columns - 1; x++)
            {
                for (int y = 1; y < rows - 1; y++)
                {
                    gridPostions.Add(new Vector3(x, y, 0f));
                }
            }
        }

        void BoardSetup()
        {
            boardHolder = new GameObject(boardName).transform;
            nord_face = none;
            vest_face = none;
            GameObject toInstantiate;
            string[] prev_drawPosition;


            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    bool validating = true;
                    if (x > 0)
                    {
                        prev_drawPosition = drawPosition;
                        drawPosition.Initialize();
                        vest_face = prev_drawPosition[y];
                    }
                    if (y == 0)
                        nord_face = none;

                    while (validating) {
                        randomTile = Random.Range(1, 5);
                        switch (randomTile)
                        {
                            case 1:
                                toInstantiate = terrainTiles[Random.Range(0, terrain1.Length)];
                                tileInstance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                                if (tilePlacement())
                                    validating = false;
                                tileInstance.transform.SetParent(boardHolder);
                                break;
                            case 2:
                                toInstantiate = terrainTiles[Random.Range(0, terrain2.Length)];
                                tileInstance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                                if (tilePlacement())
                                    validating = false;
                                tileInstance.transform.SetParent(boardHolder);
                                break;
                            case 3:
                                toInstantiate = terrainTiles[Random.Range(0, terrain3.Length)];
                                tileInstance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                                if (tilePlacement())
                                    validating = false;
                                tileInstance.transform.SetParent(boardHolder);
                                break;
                            case 4:
                                toInstantiate = terrainTiles[Random.Range(0, terrain4.Length)];
                                tileInstance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                                if (tilePlacement())
                                    validating = false;
                                tileInstance.transform.SetParent(boardHolder);
                                break;
                            case 5:
                                toInstantiate = terrainTiles[Random.Range(0, terrain5.Length)];
                                tileInstance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                                if (tilePlacement())
                                    validating = false;
                                tileInstance.transform.SetParent(boardHolder);
                                break;
                        }
                    }             
                }
            }
        }

        Boolean validateTile()
        {
            // Validate first tile row 1 column 1 
            if ((nord_face is none) && (vest_face is none))
            {
                switch (randomTile)
                {
                    case 1:
                        nord_face = land;
                        drawPosition = new List<string>(drawPosition) { land }.ToArray();
                        return true;
                    case 2:
                        if ((rotation == 0) || (rotation == 270))
                            nord_face = water;
                        else
                            nord_face = land;

                        if ((rotation == 0) || (rotation == 90))
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                        else
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                        return true;
                    case 3:
                        if (rotation == 180)
                            nord_face = land;
                        else
                            nord_face = water;

                        if (rotation == 90)
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                        else
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                        return true;
                    case 4:
                        if (rotation == 270)
                            nord_face = water;
                        else
                            nord_face = land;

                        if (rotation == 180)
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                        else
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                        return true;
                    case 5:
                        nord_face = water;
                        drawPosition = new List<string>(drawPosition) { water }.ToArray();
                        return true;
                }
            }
            // Validate next tile on column 1 row x, land - none
            else if ((nord_face is land) && (vest_face is none))
            {
                switch (randomTile)
                {
                    case 1:
                        nord_face = land;
                        drawPosition = new List<string>(drawPosition) { land }.ToArray();
                        return true;
                    case 2:
                        if (rotation == 0)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 3:
                        if (rotation == 0)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 4:
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 5:
                        return false;
                }
            }
            // Validate next tile on column 1 row x, water - none
            else if ((nord_face is water) && (vest_face is none))
            {
                switch (randomTile)
                {
                    case 1:
                        return false;
                    case 2:
                        if (rotation == 90)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 3:
                        if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }else if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }else if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 4:
                        if (rotation == 90)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 5:
                        nord_face = water;
                        drawPosition = new List<string>(drawPosition) { water }.ToArray();
                        return true;
                }
            }
            // Validate next tile on column x row 1, none - land
            else if ((nord_face is none) && (vest_face is land))
            {
                switch (randomTile)
                {
                    case 1:
                        nord_face = land;
                        drawPosition = new List<string>(drawPosition) { land }.ToArray();
                        return true;
                    case 2:
                        if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }else if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        } else
                            return false;
                    case 3:
                        if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        } else
                            return true;
                    case 4:
                        if (rotation == 90)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 5:
                        return false;
                }
            }
            // Validate next tile on column x row 1, none - water
            else if ((nord_face is none) && (vest_face is water))
            {
                switch (randomTile)
                {
                    case 1:
                        return false;
                    case 2:
                        if (rotation == 0)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else if (rotation == 90)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 3:
                        if (rotation == 0)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 4:
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;
                    case 5:
                        nord_face = water;
                        drawPosition = new List<string>(drawPosition) { water }.ToArray();
                        return true;
                }
            }
            // Validate next tile on column x row x, land - land
            else if ((nord_face is land) && (vest_face is land))
            {
                switch (randomTile)
                {
                    case 1:
                        nord_face = land;
                        drawPosition = new List<string>(drawPosition) { land }.ToArray();
                        return true;

                    case 2:
                        if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        } else
                            return false;

                    case 3:
                        return false;

                    case 4:
                        if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;

                    case 5:
                        return false;

                }
            }
            // Validate next tile on column x row x, land - water
            else if ((nord_face is land) && (vest_face is water))
            {
                switch (randomTile)
                {
                    case 1:
                        return false;

                    case 2:
                        if (rotation == 0)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;

                    case 3:
                        if (rotation == 0)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else
                            return false;

                    case 4:
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;

                    case 5:
                        return false;

                }
            }
            // Validate next tile on column x row x, water - land
            else if ((nord_face is water) && (vest_face is land))
            {
                switch (randomTile)
                {
                    case 1:
                        return false;

                    case 2:
                        if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else
                            return false;

                    case 3:
                        if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else
                            return false;

                    case 4:
                        if (rotation == 90)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;

                    case 5:
                        return false;

                }
            }
            // Validate next tile on column x row x, water - water
            else if ((nord_face is water) && (vest_face is water))
            {
                switch (randomTile)
                {
                    case 1:
                        return false;

                    case 2:
                        if (rotation == 90)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else
                            return false;

                    case 3:
                        if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition = new List<string>(drawPosition) { land }.ToArray();
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition = new List<string>(drawPosition) { water }.ToArray();
                            return true;
                        }
                        else
                            return false;

                    case 4:
                        return false;

                    case 5:
                        nord_face = water;
                        drawPosition = new List<string>(drawPosition) { water }.ToArray();
                        return true;

                }
            }
            return false;

        }

        Boolean tilePlacement()
        { 
            List<int> randomRotationList = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                while (true)
                {
                    int randomRotation = Random.Range(0, 3);
                    if (!randomRotationList.Contains(randomRotation*90))
                    {
                        randomRotationList.Add(randomRotation*90);
                        break;
                    }
                }
            }

            bool validating = true;
            while (validating)
            {
                for  (int i = 0; i < 3; i++)
                {
                    rotation = randomRotationList[i];
                    if (validateTile())
                    {
                        validating = false;
                        tileInstance.transform.eulerAngles = Vector3.forward * rotation;
                        break;
                    }           
                }
            }
            return !validating;
        }

        public void SetupScene()
        {
            BoardSetup();
            InitialiseList();

            for (int i = 0; i < rows; i++) {
                for (int j=0; j < columns; j++)
                {
                    GameObject tileChoise = terrainTiles[i];
                    Instantiate(tileChoise, new Vector3(i, j, 0f), Quaternion.identity);
                }

            }
            
        }
    }
}


