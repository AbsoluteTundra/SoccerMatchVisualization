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
    
    // Rotation is predicted based on the data of the next frame
    // This however is not how most soccer player walk/move since they can also move diagonally like most of judges do
    // I thought movement orientation would be the solution but that data is missing for most players and there not usable
    public void SetRotation(BaseActorData baseActorData)
    {
        if (baseActorData != null)
            transform.LookAt(new Vector3(baseActorData.Position[0], 0, baseActorData.Position[2]));
    }
}
