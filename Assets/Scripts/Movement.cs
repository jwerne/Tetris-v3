using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class Movement : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    static int pieceNumber;
    private SpeechOut speechOut;
    private bool gameStarted;
    Vector3 handlePosition;
    float zPosition;
    float lastPosition;


    int CurrentNode = 0;
    public static GameObject[] outline;
    public static GameObject L_Shape;
    public static GameObject lehandle;
    float threshold = 0.1f;
    static GameObject CurrentPositionHolder;
    static bool feel_finished = false;
    public static int fallmove = 25;


    //public AudioSource clear;
    //public AudioSource fall;

    // Start is called before the first frame update
    async void Start()
    {
        outline = GameObject.FindGameObjectsWithTag("l_shape");
        L_Shape = GameObject.FindGameObjectWithTag("l_shape_main");
        lehandle = GameObject.FindGameObjectWithTag("MeHandle");
        speechOut = new SpeechOut();
        await speechOut.Speak("For every new Level you will feel the blocks like this");
        UpperHandle upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await upperHandle.SwitchTo(outline[0]);
    }


    // Update is called once per frame
    async void Update()
    {
        UpperHandle upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        
        //await upperHandle.SwitchTo(gameObject);
        // do the update here!
        if (!feel_finished)
        {
            feel_outline(upperHandle);
        }


        //print(feel_finished);
        //upperHandle.FreeRotation();
        if (feel_finished)
        {
            // Movements not needed for this version of level 1, do not delete
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                    transform.position += new Vector3(1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                

                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                    transform.position += new Vector3(-1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 1, 0), 90);
                if (!ValidMove())
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, -1, 0), 90);
            }

            Vector3 handlePosition = upperHandle.GetPosition();
            float zPosition = handlePosition.z;
            float lastPosition = 0;


            // to feel block not needed

            if ((Time.time - previousTime > ((zPosition - lastPosition > fallmove) ? fallTime / 10 : fallTime) && feel_finished))
            {
                transform.position += new Vector3(0, 0, -1);
                //fall.Play();

                if (!ValidMove())
                {
                    transform.position += new Vector3(0, 0, 1);
                    this.enabled = false;
                    AddToGrid();
                    CheckLines();
                    //FindObjectOfType<Spawn>().NewPiece(pieceNumber);
                }

                previousTime = Time.time;
                lastPosition = handlePosition.z;
            }
        }




    }

    async void OnTriggerEnter()
    {
        await speechOut.Speak("now feel how the block is moving");
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(4);
    }

    async void feel_outline(UpperHandle upperHandle)
    {

        float dist = Vector3.Distance(outline[CurrentNode].transform.position, lehandle.transform.position);

        if (dist > threshold)
        {
            await upperHandle.SwitchTo(outline[CurrentNode]); //= Vector3.Lerp(startPosition, CurrentPositionHolder, Timer);
        }
        else
        {
            if (CurrentNode < outline.Length - 1)
            {
                CurrentNode++;
            }
            else
            {
                feel_finished = true;
                OnTriggerEnter();
            }

        }


    }

    float return_positive(float n)
    {
        return (n < 0) ? -n : n;
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        //clear.Play();
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void MoveDown(int i)
    {
        for (int h = i; h < height; h++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, h] != null)
                {
                    grid[j, h - 1] = grid[j, h];
                    grid[j, h] = null;
                    grid[j, h - 1].transform.position = new Vector3(0, 0, 1);
                }
            }
        }
    }

    void CheckLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                MoveDown(i);
            }
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedZ = Mathf.RoundToInt(children.transform.position.z);

            grid[roundedX, roundedZ] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedZ = Mathf.RoundToInt(children.transform.position.z);

            if (roundedX < 0 || roundedZ < 0 || roundedX >= width || roundedZ >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedZ] != null)
                return false;

        }

        return true;
    }
}
