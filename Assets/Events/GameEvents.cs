// purpose of this is to have a centralised place to declare all types of events
// naming convention follows: Category_EventName
namespace DangoMimikyu.EventManagement
{
	public enum GameEvents
	{
		None,
		// misc
		Misc_SceneChange,				// changed scene
		Misc_SaveReady,					// save file is ready to be read

		// menu
		Menu_Pause,						// open pause menu

		// gameplay events
		/// Home base events
		Gameplay_QuestStart,			// player starts a quest
		Gameplay_QuestEnd,				// player ends a quest (details of success/failure can be passed in by the params)
		Gameplay_QuestAccepted,			// player accepts a quest
		Gameplay_QuestAbandoned,		// player abandoned the quest
		Gameplay_WeaponCrafted,			// crafted a weapon
		Gameplay_WeaponDiscarded,		// discarded a weapon
		Gameplay_ArmourCrafted,			// crafted an armour
		Gameplay_ArmourDiscarded,		// discarded an armour
		Gameplay_UpdateUnits,			// something happened with units

		/// Expedition events
		Gameplay_MetronomeBeat,			// metronome is supposed to beat
		Gameplay_BreakCombo,			// combo broken
		Gameplay_ComboFever,			// combo fever

		// input events
		Input_Drum,						// any drum input
		Input_CommandComplete,			// command is complete (haven't validate)
		Input_CommandSuccess,			// command successful
		Input_CommandFail,				// command failed
		Input_MenuNavigation,			// any menu navigation function
		Input_MenuButton,				// any menu button selection

		// unit events
		Unit_EquipItem,					// when unit equips/unequips something
		Unit_Spawn,						// when unit spawns
		Unit_Shoot,						// when unit shoots
		Unit_Damaged,					// unit takes damage
		Unit_Died,						// unit was killed
		Unit_FinishWaiting,				// finished waiting after 4 counts, so should remove the overhead UI

		// enemy events
		Enemy_Spawn,					// enemy spawned
		Enemy_Active,					// enemy bot entered the range near the player to be active
		Enemy_Damaged,					// enemy bot took damage
		Enemy_Died,						// enemy was killed
	}
}