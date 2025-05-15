//Since all "actors (Ball, Player, Etc" share the same values decided to make a base class to avoid duplicate data 
public class BaseActorData
{
    public string Id;
    public float[] Position;
    public float Speed;
}
