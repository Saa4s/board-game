using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [Header("Objects")]
    public GameObject openField;
    public GameObject redChip;
    public GameObject yellowChip;
    public GameObject orangeChip;
    public GameObject blockField;
    public GameObject pictureBlock;
    public GameObject backGround;

    public List<GameObject> chipsList;
    public List<GameObject> chipsL;
    Dictionary<GameObject, int> chipsCount = new Dictionary<GameObject, int>();

    public List<GameObject> firstList;
    public List<GameObject> secondList;
    public List<GameObject> thirdList;
    private List<List<GameObject>> gbList;

    [Header("Start Position")]
    public float size = 2;
    public float startX = -3.75f;
    public float startY = -3;
    public float startZ = 5;
    public float picX = -3.7f;
    public float picY = 5.45f;
    public float picZ = 5;

    [Header("Materials")]
    Color orange = new Color(1, 0.4063355f, 0, 1);
    Color yellow = new Color(1, 0.927565f, 0, 1);
    Color red = Color.red;

    public List<Color> colorList;
    public List<Color> colorL;

    void Start()
    {
        
        CreateBlock();
        CreateChips();
    }

    private void Awake()
    {
        RandomColor();
        CreateBackground();
        CreateOpenField();
    }

    void Update()
    {
        Final();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void CreateBackground()
    {
        GameObject bg = Instantiate<GameObject>(backGround);

        Vector3 newPos = new Vector3(picX, picY, picZ);
        GameObject fPic = Instantiate<GameObject>(pictureBlock);
        fPic.transform.position = new Vector3(newPos.x + 0 * size, newPos.y, newPos.z);
        fPic.GetComponent<Renderer>().material.color = colorL[0];

        GameObject sPic = Instantiate<GameObject>(pictureBlock);
        sPic.transform.position = new Vector3(newPos.x + 2 * size, newPos.y, newPos.z);
        sPic.GetComponent<Renderer>().material.color = colorL[1];

        GameObject thPic = Instantiate<GameObject>(pictureBlock);
        thPic.transform.position = new Vector3(newPos.x + 4 * size, newPos.y, newPos.z);
        thPic.GetComponent<Renderer>().material.color = colorL[2];
    }

    void CreateOpenField()
    {
        Vector3 newPos = new Vector3(startX, startY, startZ);
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject openF = Instantiate<GameObject>(openField);
                openF.transform.position = new Vector3(newPos.x + i * size, newPos.y + j * size, newPos.z + 5);
                switch (i)
                {
                    case 0:
                        firstList.Add(openF);
                        break;
                    case 2:
                        secondList.Add(openF);
                        break;
                    case 4:
                        thirdList.Add(openF);
                        break;

                }
            }
        }
        gbList = new List<List<GameObject>> {firstList, secondList, thirdList};
    }

    void CreateBlock()
    {
        Vector3 newPos = new Vector3(startX, startY, startZ);
        for (int i = 1; i < 5; i += 2)
        {
            for (int j = 0; j < 5; j += 2)
            {
                GameObject blockF = Instantiate<GameObject>(blockField);
                blockF.transform.position = new Vector3(newPos.x + i * size, newPos.y + j * size, newPos.z);

            }
        }
    }

    void CreateChips()
    {
        int n = 0;
        Vector3 newPos = new Vector3(startX, startY, startZ);
        for (int i = 0; i < 5; i += 2)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject chipF = Instantiate<GameObject>(chipsL[n++]);
                chipF.transform.position = new Vector3(newPos.x + i * size, newPos.y + j * size, newPos.z);
            }
        }
    }

    void RandomColor()
    {
        colorList = new List<Color>() { red, yellow, orange };
        chipsList = new List<GameObject>() { redChip, yellowChip, orangeChip };
        colorL = new List<Color>();
        chipsL = new List<GameObject>();
        chipsCount = new Dictionary<GameObject, int>()
        {
            {redChip, 0 },
            {yellowChip, 0 },
            {orangeChip, 0 }

        };

        for (int i = 3; i > 0; --i)
        {
            int randVal = Random.Range(0, i);
            Color color = colorList[randVal];
            colorList.RemoveAt(randVal);
            colorL.Add(color);
        }
        
        while (chipsL.Count < 15)
        {
            int randVal = Random.Range(0, 3);
            if (chipsL.Count > 2 && chipsL[chipsL.Count - 1] == chipsList[randVal])
            {
                randVal = (int)((Mathf.Log(Random.Range(0, 100)) * 7) % 3);  
            }
            if (chipsCount[chipsList[randVal]] < 5)
            {
                chipsL.Add(chipsList[randVal]);
                chipsCount[chipsList[randVal]]++;
            }
        }

    }
    bool Checking(int i)
    {
        bool total = true;
        foreach (GameObject openF in gbList[i])
        {
            Ray ray = new Ray(openF.transform.position, -Vector3.forward);
            RaycastHit chip;
            if (Physics.Raycast(ray, out chip))
            {
                if (chip.collider.GetComponent<Renderer>().material.color != colorL[i])
                {
                    total = false;
                    break;

                }

            }
            else
            {
                total = false;
                break;
            }
        }
        if (total)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Final()
    {
        if (Checking(0) && Checking(1) && Checking(2))
        {
            Debug.Log("онаедю");
            Application.Quit();
        }
    }
}
