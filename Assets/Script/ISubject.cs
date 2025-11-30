public interface ISubject
{
    public void NotifyObserver(PlayerAction action);
    public void AddObserver(IObserver observer);
    public void RemoveObserver(IObserver observer);
}
