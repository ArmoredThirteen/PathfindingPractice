using ATE.Pathing.BreadthFirst;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Pathing
{
	public class RunBreadthFirst : MonoBehaviour
	{
        public GameObject visitedObj;
        public GameObject frontierObj;
        public GameObject pathObj;

        public PathTarget from;
        public PathTarget to;


        private List<GameObject> feedbackObjs;
        private PathBuilder builder;
        

        private void Start()
        {
            feedbackObjs = new List<GameObject> ();
            builder = new PathBuilder ();
            builder.RestartPathing (from.currNode, to.currNode);
        }

        private void Update()
        {
            if (builder.complete)
                return;

            //while (!builder.complete)
                builder.IteratePath ();
            
            // Rebuild all feedback objects rather hackily
            while (feedbackObjs.Count > 0)
            {
                Destroy (feedbackObjs[0]);
                feedbackObjs.RemoveAt (0);
            }
            // Instantiate visited feedback objects
            for (int i = 0; i < builder.visited.Count; i++)
                GameObject.Instantiate (visitedObj, builder.visited[i].transform.position, transform.rotation, transform);
            // Instantiate frontier feedback objects
            for (int i = 0; i < builder.frontier.Count; i++)
                GameObject.Instantiate (frontierObj, builder.frontier.ToArray()[i].transform.position, transform.rotation, transform);
        }

    }
}