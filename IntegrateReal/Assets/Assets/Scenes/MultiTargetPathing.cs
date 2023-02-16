using System.Collections;
using UnityEngine;
using Pathfinding;

/*
public class MultiTargetPathing : MonoBehaviour
{
    public Path path;
    // Start is called before the first frame update
    void Start()
    {
        // Find the seeker component
        Seeker seeker = GetComponent<Seeker>;

        seeker.pathCallback = OnPathComplete;

        Vector3[] endPoints = new Vector3[transform.childCount];
        int c = 0;

        foreach(Transform child in transform)
        {
            endPoints[c] = child.position;
            c++;
        }

        // Start a multitarget path
        seeker.StartMultiTargetPath(transform.position, endPoints, true);
    }

    public void OnPathComplete (Path p)
    {
        Debug.Log("Got Callback");

        if (p.error)
        {
            Debug.Log("Path returned an error\nError: " + p.errorLog);
            return;
        }

        MultiTargetPath mp = p as MultiTargetPath;
        if (mp == null)
        {
            Debug.LogError("Path was not multitarget");
            return;
        }

        // All paths
        List<Vector3>[] paths = mp.vectorPaths;

        for (int i = 0; i < paths.Length; i++)
        {
            //plot path i
            List<Vector3> path = paths[i];

            if(path == null)
            {
                Debug.Log("Path " + i + " could not be found");
                continue;

            }

            for (int j = 0; j < path.Count - 1; j++)
            {
                // Plot segment j to j+1 with a color from Pathfinding.Astarmath.intocolor
                Debug.DrawLine(path[j], path[j + 1], AstarMath.IntToColor(i, 0.5F));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


*/
// need MultiTargetPathing package