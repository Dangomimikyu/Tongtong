// define the event arguments datatype so that any type can be used
namespace DangoMimikyu.EventManagement
{
    public interface IEventArgData
    {
        public GameEvents eventName { get; } // name of the event to listen to, this variable is set as a read-only accessor

        public object[] eventParams { get; }
    }

    public class EventArgumentData : IEventArgData
    {
        public GameEvents eventName { private set; get; }
        public object[] eventParams { private set; get; }

        public EventArgumentData(GameEvents EventToSub, params object[] otherArgs)
        {
            eventName = EventToSub;
            eventParams = otherArgs;
        }
    }
}