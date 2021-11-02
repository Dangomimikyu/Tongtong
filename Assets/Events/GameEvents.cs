// purpose of this is to have a centralised place to declare all types of events
// naming convention follows: Category_EventName
namespace DangoMimikyu.EventManagement
{
	public enum GameEvents
	{
		None,
		// networking
		Networking_ClientConnect,		// client connected
		Networking_ClientDisconnect,	// client disconnected
		Networking_DailyReset,			// reset daily rewards and quests

		// gameplay events
		Gameplay_QuestStart,			// player starts a quest
		Gameplay_QuestEnd,				// player ends a quest (details of success can be passed in by the params)
		Gameplay_QuestAccepted,			// player accepts a quest
		Gameplay_QuestAbandoned,		// player abandoned the quest
		Gameplay_WeaponCrafted,			// crafted a weapon
		Gameplay_WeaponDiscarded,		// discarded a weapon
		Gameplay_ArmourCrafted,			// crafted an armour
		Gameplay_ArmourDiscarded,		// discarded an armour
		
		// input events
		Input_Drum,					// any drum input 
		Input_CommandComplete,			// command is complete (haven't validate)
		Input_CommandSuccess,			// command successful
		Input_CommandFail,				// command failed
		Input_MenuNavigation,			// any menu navigation function
		Input_MenuButton,				// any menu button selection
	}
}