using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementBehavior : MonoBehaviour
{

    public GridOccupant occupant;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public MovementData calculateMoveToTarget(Vector2Int startCell, Vector2Int target, int maxSteps, Predicate<Vector2Int> isCellOccupied) {

            Vector2Int selected = new Vector2Int(startCell.x, startCell.y);

            for (int index = maxSteps; index> 0; index -=1) {
    

                 Debug.Log( "Index ee  " + index);
                if (GridOccupant.ManhattanDistanceTo(selected, target) <=0 )
                    break;


                Vector2Int[] candidates = new Vector2Int[] {
                    new Vector2Int(selected.x -1, selected.y),
                    new Vector2Int(selected.x +1, selected.y),
                    new Vector2Int(selected.x, selected.y -1),
                    new Vector2Int(selected.x, selected.y +1),
                    };


                Vector2Int least = selected;
                int distance = GridOccupant.EuclideanDistanceSquareTo(selected, target);

                for (int i = 0; i < candidates.Length; i +=1 ) {

                    Vector2Int sel = candidates[i];

                    if (isCellOccupied(sel)) {
                        continue;
                    } 
                    int dist = GridOccupant.EuclideanDistanceSquareTo(sel, target);

                    if (dist < distance) {

                        Debug.Log( "Choose new min " + least + " - "+ sel + " -  " + dist);
                        least = sel;
                        distance = dist;
                    }
                }


                if (least == selected) {
                    Debug.Log( "Skip next least " + least + " - " + selected);
                    break;
                } else {
                    selected = least;
                     Debug.Log( "Choose next least " + least);
                  
                }
        
            }
            return new MovementData(selected, GridOccupant.ManhattanDistanceTo(startCell, selected));
    }

    public class MovementData {

        public MovementData(Vector2Int finalPosition, int travelDistance) {

            FinalPosition = finalPosition;
            TravelDistance = travelDistance;
        }

        public Vector2Int FinalPosition {get;}

        public int TravelDistance {get;}
    }
}
