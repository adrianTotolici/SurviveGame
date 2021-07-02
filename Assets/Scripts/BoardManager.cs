using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
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
        public GameObject[] res_tree;
        public GameObject[] res_fish;
        // object instance od a terrain tile
        private GameObject tileInstance;
        // boarHolder
        private Transform boardHolder;

        // restriction in N of current tile 
        private string nord_face = none;
        // restriction in V of current tile 
        private string vest_face = none;
        // random tile selection for adding on map
        private int randomTile;
        // random rotation of curent selected tile
        private int rotation;
        // array with all restriction on V tile
        private List<String> drawPosition = new List<string>();

        private GameObject chosenTile;
        private GameObject resChoise;
        private float gridPositionX;
        private float gridPositionY;

        private List<Vector3> posibleResPosition = new List<Vector3>();


        private const string land = "land";
        private const string water = "water";
        private const string none = "not set";
        private const string boardName = "Board";
        private const float fishRes = 2f;
        private const float treeRes = 1f;
        public const int columns = 20;
        public const int rows = 20;

        void BoardSetup()
        {
            boardHolder = new GameObject(boardName).transform;
            List<String> prev_drawPosition = new List<string>();


            for (int x = 0; x < columns; x++)
            {
                gridPositionX = x*2.4f;
                nord_face = none;
                if (x > 0)
                {
                    prev_drawPosition = drawPosition.ToList();
                    drawPosition.Clear();
                }
                Debug.Log("------------------------------------------------");
                for (int y = 0; y < rows; y++)
                {
                    gridPositionY = y*2.4f;
                    bool validating = true;
                    Debug.Log("+++++++++");
                    if (x > 0)
                    {
                        vest_face = prev_drawPosition[y].ToString();
                    }
                    if (y == 0)
                        nord_face = none;

                    Debug.Log("1. Vest Face: " + vest_face);
                    Debug.Log("2. Nord Face: " + nord_face);
                    while (validating) {
                        randomTile = Random.Range(1, 6);
                        switch (randomTile)
                        {
                            case 1:
                                chosenTile = terrain1[0];
                                Debug.Log("Tile type 1");
                                break;
                            case 2:
                                chosenTile = terrain2[0];
                                Debug.Log("Tile type 2");
                                break;
                            case 3:
                                chosenTile = terrain3[0];
                                Debug.Log("Tile type 3");
                                break;
                            case 4:
                                chosenTile = terrain4[0];
                                Debug.Log("Tile type 4");
                                break;
                            default:
                                chosenTile = terrain5[Random.Range(0, terrain5.Length)];
                                Debug.Log("Tile type 5");
                                break;
                        }

                    if (TilePlacement())
                    {
                        validating = false;
                        Debug.Log("Validation Success");
                    }
                    else
                        Debug.Log("Validation failed");
                    }             
                }
            }
        }

        Boolean ValidateTile()
        {
            // Validate first tile row 1 column 1 
            if ((nord_face is none) && (vest_face is none))
            {
                switch (randomTile)
                {
                    case 1:
                        nord_face = land;
                        drawPosition.Add(land);
                        return true;
                    case 2:
                        if ((rotation == 0) || (rotation == 270))
                            nord_face = land;
                        else
                            nord_face = water;

                        if ((rotation == 0) || (rotation == 90))
                            drawPosition.Add(land);
                        else
                            drawPosition.Add(water);
                        return true;
                    case 3:
                        if (rotation == 0)
                            nord_face = land;
                        else
                            nord_face = water;

                        if (rotation == 90)
                            drawPosition.Add(land);
                        else
                            drawPosition.Add(water);
                        return true;
                    case 4:
                        if (rotation == 90)
                            nord_face = water;
                        else
                            nord_face = land;

                        if (rotation == 180)
                            drawPosition.Add(water);
                        else
                            drawPosition.Add(land);
                        return true;
                    case 5:
                        nord_face = water;
                        drawPosition.Add(water);
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
                        drawPosition.Add(land);
                        return true;
                    case 2:
                        if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition.Add(land);
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = water;
                            drawPosition.Add(water);
                            return true;
                        }
                        else
                            return false;
                    case 3:
                        if (rotation == 180)
                        {
                            nord_face = water;
                            drawPosition.Add(water);
                            return true;
                        }
                        else
                            return false;
                    case 4:
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition.Add(land);
                            return true;
                        }
                        else if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition.Add(land);
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition.Add(water);
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
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition.Add(land);
                            return true;
                        }
                        else if (rotation == 270)
                        {
                            nord_face = land;
                            drawPosition.Add(water);
                            return true;
                        }
                        else
                            return false;
                    case 3:
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition.Add(water);
                            return true;
                        }else if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition.Add(land);
                            return true;
                        }else if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition.Add(water);
                            return true;
                        }
                        else
                            return false;
                    case 4:
                        if (rotation == 270)
                        {
                            nord_face = land;
                            drawPosition.Add(land);
                            return true;
                        }
                        else
                            return false;
                    case 5:
                        nord_face = water;
                        drawPosition.Add(water);
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
                        drawPosition.Add(land);
                        return true;
                    case 2:
                        if (rotation == 180)
                        {
                            nord_face = water;
                            drawPosition.Add(water);
                            return true;
                        }else if (rotation == 270)
                        {
                            nord_face = land;
                            drawPosition.Add(water);
                            return true;
                        } else
                            return false;
                    case 3:
                        if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition.Add(water);
                            return true;
                        } else
                            return false;
                    case 4:
                        if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition.Add(land);
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition.Add(water);
                            return true;
                        }
                        else if (rotation == 270)
                        {
                            nord_face = land;
                            drawPosition.Add(land);
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
                            nord_face = land;
                            drawPosition.Add(land);
                            return true;
                        }
                        else if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition.Add(land);
                            return true;
                        }
                        else
                            return false;
                    case 3:
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition.Add(water);
                            return true;
                        }
                        else if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition.Add(land);
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = water;
                            drawPosition.Add(water);
                            return true;
                        }
                        else
                            return false;
                    case 4:
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition.Add(land);
                            return true;
                        }
                        else
                            return false;
                    case 5:
                        nord_face = water;
                        drawPosition.Add(water);
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
                        drawPosition.Add(land);
                        return true;

                    case 2:
                        if (rotation == 180)
                        {
                            nord_face = water;
                            drawPosition.Add(water);
                            return true;
                        } else
                            return false;

                    case 3:
                        return false;

                    case 4:
                        if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition.Add(land);
                            return true;
                        }
                        else if (rotation == 180)
                        {
                            nord_face = land;
                            drawPosition.Add(water);
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
                        if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition.Add(land);
                            return true;
                        }
                        else
                            return false;

                    case 3:
                        if (rotation == 180)
                        {
                            nord_face = water;
                            drawPosition.Add(water);
                            return true;
                        }
                        else
                            return false;

                    case 4:
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition.Add(land);
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
                        if (rotation == 270)
                        {
                            nord_face = land;
                            drawPosition.Add(water);
                            return true;
                        }
                        else
                            return false;

                    case 3:
                        if (rotation == 270)
                        {
                            nord_face = water;
                            drawPosition.Add(water);
                            return true;
                        }
                        else
                            return false;

                    case 4:
                        if (rotation == 270)
                        {
                            nord_face = land;
                            drawPosition.Add(land);
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
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition.Add(land);
                            return true;
                        }
                        else
                            return false;

                    case 3:
                        if (rotation == 0)
                        {
                            nord_face = land;
                            drawPosition.Add(water);
                            return true;
                        }
                        else if (rotation == 90)
                        {
                            nord_face = water;
                            drawPosition.Add(land);
                            return true;
                        }
                        else
                            return false;

                    case 4:
                        return false;

                    case 5:
                        nord_face = water;
                        drawPosition.Add(water);
                        return true;

                }
            }
            return false;

        }

        Boolean TilePlacement()
        { 
            List<int> randomRotationList = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                while (true)
                {
                    int randomRotation = Random.Range(0, 4);
                    if (!randomRotationList.Contains(randomRotation*90))
                    {
                        randomRotationList.Add(randomRotation*90);
                        break;
                    }
                }
            }

            bool valid = false;
                for  (int i = 0; i < 4; i++)
                {
                    rotation = randomRotationList[i];
                    if (ValidateTile())
                        {
                            valid = true;
                            if (rotation == 270)
                                rotation = 90;
                            else if (rotation == 90)
                                rotation = -90;
                            Debug.Log("rotation: " + rotation);
                            tileInstance = Instantiate(chosenTile, new Vector3(gridPositionX, gridPositionY, 0f), Quaternion.identity);
                            tileInstance.transform.SetParent(boardHolder);
                            tileInstance.transform.localRotation = Quaternion.Euler(0,0,rotation);
                            
                            if ((randomTile ==1) || (randomTile == 5))
                            {
                                float res;
                                if (randomTile == 1)
                                    res = treeRes;
                                else
                                    res = fishRes;
                                posibleResPosition.Add(new Vector3(gridPositionX, gridPositionY, res));
                            }
                           
                            break;
                        }           
                }
            return valid;
        }

        void PlaceResources()
        {
            for (int i = 0; i < posibleResPosition.Count; i++)
            {
                int randomRes = Random.Range(0, 2);
                if (randomRes == 1)
                {
                    Vector3 posibleRes = posibleResPosition.ElementAt(i);
                    switch (posibleRes.z)
                    {
                        case 1:
                            resChoise = res_tree[Random.Range(0, res_tree.Length)];
                            break;
                        case 2:
                            resChoise = res_fish[0];
                            break;
                    }

                    GameObject resInstance = Instantiate(resChoise, new Vector3(posibleRes.x, posibleRes.y, 0f), Quaternion.identity);
                    resInstance.transform.SetParent(boardHolder);
                }
            }
        }

        public void SetupScene()
        {
            GameObject.Find("MainGameCamera").transform.position = new Vector3((columns * 1.2f / 2f), (rows * 1.2f / 2f), -10f);
            BoardSetup();
            PlaceResources();
        }
    }
}


