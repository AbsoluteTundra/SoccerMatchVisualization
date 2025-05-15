using UnityEngine;

//The base class of all moving actor in the match for example (players, judges, ball)
//Shares functions that all need for example SetPosition
// Make it easier to extend and build upon later
public class BaseActor : MonoBehaviour
{
    public void SetPosition(BaseActorData baseActorData)
    {
        if (baseActorData != null)
            transform.position = new Vector3(baseActorData.Position[0], baseActorData.Position[1], baseActorData.Position[2]);
    }
    
    public void SetRotation(BaseActorData nextFrameDataPersonBaseActorData)
    {
        //TODO Make this rotation smoother when the next position is really small the rotation is jittery
        if (nextFrameDataPersonBaseActorData != null)
            transform.LookAt(new Vector3(nextFrameDataPersonBaseActorData.Position[0], 0, nextFrameDataPersonBaseActorData.Position[2]));
    }
}
