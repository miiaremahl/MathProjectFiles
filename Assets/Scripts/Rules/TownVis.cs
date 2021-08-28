using System.Collections;
using System.Collections.Generic;
using SVS;
using UnityEngine;
using static Visualizer;

public class TownVis : MonoBehaviour
{
    public LSystemGenerator lsystem;
    List<Vector3> positions = new List<Vector3>();
    public RoadHelper rh;

    public StructureHelper structureHelper;

    [SerializeField]
    private int length = 8;
    [SerializeField]
    private float angle = 90;
    private int initrl;

    // Start is called before the first frame update
    void Start()
    {
        initrl = length;
        createTown();
    }

    // Update is called once per frame
   public void createTown()
    {
        length = initrl;
        rh.Reset();
        structureHelper.Reset();
        positions.Clear();
        var sequence = lsystem.GenerateSentance();
        VisualizeSeq(sequence);
        
    }

    public int Length
    {
        get
        {
            if (length > 0) { return length; }
            else
            {
                return 1; //possibly random
                //Random r = new Random();
                //return r.Next(1, 3);
            }


        }
        set => length = value;
    }

    private void VisualizeSeq(string seq)
    {
        Stack<AgentParamaters> savePoints = new Stack<AgentParamaters>();
        var curPos = Vector3.zero;
        Vector3 direction = Vector3.forward;
        Vector3 tempPosition = Vector3.zero;

        positions.Add(curPos);

        foreach (var letter in seq)
        {
            letters l = (letters)letter;
            switch (l)
            {

                case letters.save:
                    savePoints.Push(new AgentParamaters
                    {
                        position = curPos,
                        direction = direction,
                        length = length
                    });

                    break;

                case letters.load:
                    if (savePoints.Count != 0)
                    {
                        var age = savePoints.Pop();
                        curPos = age.position;
                        direction = age.direction;
                        length = age.length;
                    }
                    break;
                case letters.draw:
                    tempPosition = curPos;
                    curPos += direction * length;
                    // put houses
                    rh.PlaceStreet(tempPosition, Vector3Int.RoundToInt(direction), length);
                    //Random r = new Random();
                    //length -= r.Next(1,3); //2
                    length -= 2;
                    positions.Add(curPos);
                    break;
                case letters.right:
                    direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                    break;

                case letters.left:
                    direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                    break;
                default:
                    break;
            }
        }
        rh.fixRoad();
        structureHelper.PlaceStructuresAroundRoad(rh.GetRoadPos());
        
    }

}
