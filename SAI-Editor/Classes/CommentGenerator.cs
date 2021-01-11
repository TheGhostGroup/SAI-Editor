using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI_Editor.Enumerators;
using SAI_Editor.Properties;
using SAI_Editor.Classes.Database;
using SAI_Editor.Classes.Database.Classes;
using SAI_Editor.Forms;

namespace SAI_Editor.Classes
{
    class CommentGenerator
    {
        private readonly Dictionary<SmartEvent, string> smartEventStrings = new Dictionary<SmartEvent, string>();
        private readonly Dictionary<SmartAction, string> smartActionStrings = new Dictionary<SmartAction, string>();

        private static object _lock = new object();
        private static CommentGenerator _instance;

        public static CommentGenerator Instance
        {
            get
            {
                lock (_lock)
                    return _instance ?? (_instance = new CommentGenerator());
            }
        }

        public CommentGenerator()
        {
            smartEventStrings.Add(SmartEvent.SMART_EVENT_SPELLHIT, "On Spellhit '_spellNameEventParamOne_'");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_HAS_AURA, "On _hasAuraEventParamOne_ '_spellNameEventParamOne_'");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_TARGET_BUFFED, "On Target Buffed With '_spellNameEventParamOne_'");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_SPELLHIT_TARGET, "On Target Spellhit '_spellNameEventParamOne_'");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_FRIENDLY_MISSING_BUFF, "On Friendly Unit Missing Buff '_spellNameEventParamOne_'");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_HEALT_PCT, "Between _eventParamOne_-_eventParamTwo_% Health");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_MANA_PCT, "Between _eventParamOne_-_eventParamTwo_% Mana");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_RANGE, "Within _eventParamOne_-_eventParamTwo_ Range");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_OOC_LOS, "Within _eventParamOne_-_eventParamTwo_ Range Out of Combat LoS");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_TARGET_HEALTH_PCT, "Target Between _eventParamOne_-_eventParamTwo_% Health");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_FRIENDLY_HEALTH, "Friendly At _eventParamOne_ Health");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_TARGET_MANA_PCT, "Target Between _eventParamOne_-_eventParamTwo_% Mana");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_RECEIVE_EMOTE, "Received Emote _eventParamOne_");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_DAMAGED, "On Damaged Between _eventParamOne_-_eventParamTwo_");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_DAMAGED_TARGET, "On Target Damaged Between _eventParamOne_-_eventParamTwo_");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_MOVEMENTINFORM, "On Reached Point _eventParamTwo_");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_SUMMON_DESPAWNED, "On Summon _npcNameFirstParam_ Despawned");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_DATA_SET, "On Data Set _eventParamOne_ _eventParamTwo_");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_WAYPOINT_REACHED, "On Waypoint _eventParamOne_ Reached");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_TEXT_OVER, "On Text _eventParamOne_ Over");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_RECEIVE_HEAL, "On Received Heal Between _eventParamOne_-_eventParamTwo_");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_TIMED_EVENT_TRIGGERED, "On Timed Event _eventParamOne_ Triggered");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_GOSSIP_SELECT, "On Gossip Option _eventParamTwo_ Selected");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_GAME_EVENT_START, "On Game Event _eventParamOne_ Started");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_GAME_EVENT_END, "On Game Event _eventParamOne_ Ended");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_GO_EVENT_INFORM, "On Event _eventParamOne_ Inform");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_ACTION_DONE, "On Action _eventParamOne_ Done");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_FRIENDLY_HEALTH_PCT, "On Friendly Between _eventParamOne_-_eventParamTwo_% Health");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_UPDATE_IC, "In Combat");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_UPDATE_OOC, "Out of Combat");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_AGGRO, "On Aggro");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_KILL, "On Killed Unit");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_DEATH, "On Just Died");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_EVADE, "On Evade");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_RESPAWN, "On Respawn");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_VICTIM_CASTING, "On Victim Casting '_targetCastingSpellName_'");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_FRIENDLY_IS_CC, "On Friendly Crowd Controlled");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_SUMMONED_UNIT, "On Summoned Unit");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_ACCEPTED_QUEST, "On Quest '_questNameEventParamOne_' Taken");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_REWARD_QUEST, "On Quest '_questNameEventParamOne_' Finished");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_REACHED_HOME, "On Reached Home");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_RESET, "On Reset");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_IC_LOS, "In Combat LoS");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_PASSENGER_BOARDED, "On Passenger Boarded");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_PASSENGER_REMOVED, "On Passenger Removed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_CHARMED, "On Charmed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_CHARMED_TARGET, "On Target Charmed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_CORPSE_REMOVED, "On Corpse Removed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_AI_INIT, "On Initialize");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_WAYPOINT_START, "On Waypoint Started");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_TRANSPORT_ADDPLAYER_NYI, "On Transport Player Added");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_TRANSPORT_ADDCREATURE_NYI, "On Transport Creature Added");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_TRANSPORT_REMOVE_PLAYER_NYI, "On Transport Player Removed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_TRANSPORT_RELOCATE_NYI, "On Transport Relocate");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_INSTANCE_PLAYER_ENTER_NYI, "On Instance Player Enter");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_AREATRIGGER_ONTRIGGER, "On Trigger");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_QUEST_ACCEPTED_NYI, "On Quest Accepted");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_QUEST_OBJ_COPLETETION_NYI, "On Quest Object Completed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_QUEST_COMPLETION_NYI, "On Quest Completed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_QUEST_REWARDED_NYI, "On Quest Rewarded");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_QUEST_FAIL_NYI, "On Quest Failed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_JUST_SUMMONED, "On Just Summoned");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_WAYPOINT_PAUSED, "On Waypoint Paused");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_WAYPOINT_RESUMED, "On Waypoint Resumed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_WAYPOINT_STOPPED, "On Waypoint Stopped");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_WAYPOINT_ENDED, "On Waypoint Finished");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_UPDATE, "On Update");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_JUST_CREATED, "On Just Created");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_GOSSIP_HELLO, "On Gossip Hello");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_FOLLOW_COMPLETED, "On Follow Complete");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_DUMMY_EFFECT_NYI, "On Dummy Effect");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_IS_BEHIND_TARGET, "On Behind Target");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_GO_STATE_CHANGED, "On Gameobject State Changed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_ON_SPELLCLICK, "On Spellclick");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_DISTANCE_CREATURE, "On Distance To Creature");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_DISTANCE_GAMEOBJECT, "On Distance To GameObject");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_COUNTER_SET, "On Counter Set _eventParamOne_ _eventParamTwo_");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_SCENE_START, "On Scene _eventParamOne_ Start");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_SCENE_TRIGGER, "On Scene _eventParamOne_ Trigger");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_SCENE_CANCEL, "On Scene _eventParamOne_ Cancel");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_SCENE_COMPLETE, "On Scene _eventParamOne_ Completed");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_SUMMONED_UNIT_DIES, "On Summoned Unit Dies");
            smartEventStrings.Add(SmartEvent.SMART_EVENT_LINK, "_previousLineComment_");

            //! Filling up actions
            smartActionStrings.Add(SmartAction.SMART_ACTION_NONE, "No Action Type");
            smartActionStrings.Add(SmartAction.SMART_ACTION_TALK, "Say Line _actionParamOne_ for duration _actionParamTwo_ _UseTalkTarget_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_FACTION, "Set Faction _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_MORPH_TO_ENTRY_OR_MODEL, "_morphToEntryOrModelActionParams_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SOUND, "Play _PlaySoundDistance_ _actionParamOne_ _PlaySoundTarget_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_EMOTE, "Play Emote _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_FAIL_QUEST, "Fail Quest '_questNameActionParamOne_' - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_OFFER_QUEST, "Add Quest '_questNameActionParamOne_' _addQuestDirectly_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_REACT_STATE, "Set Reactstate _reactStateParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ACTIVATE_GOBJECT, "Activate Gameobject - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RANDOM_EMOTE, "Play Random Emote (_actionRandomParameters_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CAST, "Cast '_spellNameActionParamOne_' _getCastFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SUMMON_CREATURE, "Summon Creature '_creatureNameActionParamOne_' for _actionParamThree_ milliseconds _AttackInvoker_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_THREAT_SINGLE_PCT, "Set Threat _actionParamOne_-_actionParamTwo_ on single target - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_THREAT_ALL_PCT, "Set Threat _actionParamOne_-_actionParamTwo_ on all units in threatlist - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CALL_AREAEXPLOREDOREVENTHAPPENS, "Call AreaExploredOrEventHappens for Quest: '_questNameActionParamOne_' - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_16, "Set - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_EMOTE_STATE, "Set Emote State _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_UNIT_FLAG, "Set Flag_getUnitFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_UNIT_FLAG, "Remove Flag_getUnitFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_AUTO_ATTACK, "_enableDisableActionParamOne_ Auto Attack - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_COMBAT_MOVEMENT, "_enableDisableActionParamOne_ Combat Movement - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_EVENT_PHASE, "Set Event Phase _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_INC_EVENT_PHASE, "_incrementOrDecrementActionParamOne_ Phase - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_EVADE, "Evade and move _RespawnHomePosActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_FLEE_FOR_ASSIST, "Flee For Assist _AddEmoteTextActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CALL_GROUPEVENTHAPPENS, "Call GroupEventHappens for quest: '_questNameActionParamOne_' - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_COMBAT_STOP, "Stop Combat - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVEAURASFROMSPELL, "Remove Aura '_spellNameActionParamOne_' Charges: _actionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_FOLLOW, "_startOrStopBasedOnTargetType_ Follow _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RANDOM_PHASE, "Set Random Phase(_actionRandomParameters_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RANDOM_PHASE_RANGE, "Set Phase Random Between _actionParamOne_-_actionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RESET_GOBJECT, "Reset Gameobject - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CALL_KILLEDMONSTER, "Call KilledMonsterCredit '_questNameKillCredit_' - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_INST_DATA, "_setDataSetBossStateActionParamThree_ _actionParamOne_ to _actionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_INST_DATA64, "Set Guid Data _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UPDATE_TEMPLATE, "Update Template To '_creatureNameActionParamOne_' (_updateLevelActionParamTwo_)- Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_DIE, "Kill Self - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_IN_COMBAT_WITH_ZONE, "Call Set In Combat With Zone - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CALL_FOR_HELP, "Call for Help within _actionParamOne_ yards radius _addEmoteTexts_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_SHEATH, "Set Sheath _sheathActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_FORCE_DESPAWN, "Despawn _forceDespawnActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_INVINCIBILITY_HP_LEVEL, "_invincibilityHpActionParamsOneTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_MOUNT_TO_ENTRY_OR_MODEL, "_mountToEntryOrModelActionParams_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_PHASE_MASK, "Set Phase _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_DATA, "Set Data _actionParamOne_ _actionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_MOVE_FORWARD, "Move Forward _actionParamOne_ Yards - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_VISIBILITY, "Set Visibility _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_ACTIVE, "Set Active _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ATTACK_START, "Attack Start - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SUMMON_GO, "Summon Gameobject '_gameobjectNameActionParamOne_' for _actionParamTwo_ seconds - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_KILL_UNIT, "Kill Unit - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ACTIVATE_TAXI, "Activate Taxi Path _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_WP_START, "Start Waypoint Entry: _actionParamTwo_ (Run: _onOffActionParamOne_, Repeatable Path: _onOffActionParamThree_, Quest-ID: _actionParamFour_, Reactstate: _reactStateParamSix_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_WP_PAUSE, "Pause Waypoint for _actionParamOne_ milliseconds - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_WP_STOP, "Stop Waypoint - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ADD_ITEM, "Add _actionParamTwo_ time(s) Item _addItemBasedOnActionParams_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_ITEM, "Remove _actionParamTwo_ time(s) Item _addItemBasedOnActionParams_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_INSTALL_AI_TEMPLATE, "Install _updateAiTemplateActionParamOne_ Template - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_RUN, "Set Run _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_DISABLE_GRAVITY, "Set DisableGravity _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_SWIM, "Set Swim _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_TELEPORT, "Teleport - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_COUNTER, "Set Counter-ID: _actionParamOne_ Value: _actionParamTwo_, Reset: _onOffActionParamThree_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_STORE_TARGET_LIST, "Store Targetlist _actionParamOne_ - Target: _getTargetType_");            
            smartActionStrings.Add(SmartAction.SMART_ACTION_WP_RESUME, "Resume Waypoint Path - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_ORIENTATION, "Set Orientation _setOrientationTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CREATE_TIMED_EVENT, "Create Timed Event ID: _actionParamOne_ (InitialMin: _actionParamTwo_, InitialMax: _actionParamThree_, RepeatMin: _actionParamFour_, RepeatMax: _actionParamFive_, Chance: _actionParamSix_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_PLAYMOVIE, "Play Movie _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_MOVE_TO_POS, "Move To _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RESPAWN_TARGET, "Respawn _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_EQUIP, "Change Equip Template Entry: _actionParamOne_ (Slotmask: _actionParamTwo_, Item Entry 1: _actionParamThree_, Item Entry 2: _actionParamFour_, Item Entry 3: _actionParamFive_)  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CLOSE_GOSSIP, "Close Gossip - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_TRIGGER_TIMED_EVENT, "Trigger Timed Event _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_TIMED_EVENT, "Remove Timed Event _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ADD_AURA, "Add Aura '_spellNameActionParamOne_' - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_OVERRIDE_SCRIPT_BASE_OBJECT, "Override Script Base Object - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RESET_SCRIPT_BASE_OBJECT, "Reset Script Base Object - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CALL_SCRIPT_RESET, "Reset All Scripts - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_RANGED_MOVEMENT, "Set Ranged Movement (Attack Distance: _actionParamOne_, Attack Angle: _actionParamTwo_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CALL_TIMED_ACTIONLIST, "Call Actionlist _actionParamOne_ (TimerType: _TimerTypeActionParamTwo_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_NPC_FLAG, "Set Npc Flag_getNpcFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ADD_NPC_FLAG, "Add Npc Flag_getNpcFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_NPC_FLAG, "Remove Npc Flag_getNpcFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SIMPLE_TALK, "Simple Talk Line _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_INVOKER_CAST, "Invoker Cast '_spellNameActionParamOne_' _getCastFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CROSS_CAST, "Cross Cast '_spellNameActionParamOne_' _getCastFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CALL_RANDOM_TIMED_ACTIONLIST, "Run Random Script - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CALL_RANDOM_RANGE_TIMED_ACTIONLIST, "Run Random Script between Script: _actionParamOne_ and Script: _actionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RANDOM_MOVE, "_startStopActionParamOne_ Random Movement (Radius: _actionParamOne_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_UNIT_FIELD_BYTES_1, "Set Flag _getBytes1Flags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_UNIT_FIELD_BYTES_1, "Remove Flag _getBytes1Flags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_INTERRUPT_SPELL, "Interrupt Spell '_spellNameActionParamTwo_' _SpellInterruptDelayActionParamOne_ _SpellInterruptInstanceActionParamOne_  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SEND_GO_CUSTOM_ANIM, "Send Custom Animation _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_DYNAMIC_FLAG, "Set Dynamic Flag_getDynamicFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ADD_DYNAMIC_FLAG, "Add Dynamic Flag_getDynamicFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_DYNAMIC_FLAG, "Remove Dynamic Flag_getDynamicFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_JUMP_TO_POS, "Jump To Pos - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SEND_GOSSIP_MENU, "Send Gossip Menu ID: _actionParamOne_ NPC Text ID: _actionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_GO_SET_LOOT_STATE, "Set Lootstate _goStateActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SEND_TARGET_TO_TARGET, "Send Target _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_HOME_POS, "Set Home Position - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_HEALTH_REGEN, "Set Health Regeneration _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_ROOT, "Set Rooted _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_GO_FLAG, "Set Gameobject Flag_getGoFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ADD_GO_FLAG, "Add Gameobject Flag_getGoFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_GO_FLAG, "Remove Gameobject Flag_getGoFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SUMMON_CREATURE_GROUP, "Summon Creature Group _actionParamOne_ _AttackInvokerCreatureGroupActionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_POWER, "Set _powerTypeActionParamOne_ To _actionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ADD_POWER, "Add _actionParamTwo_ _powerTypeActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_POWER, "Remove _actionParamTwo_ _powerTypeActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_GAME_EVENT_STOP, "Stop Game Event _eventnameActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_GAME_EVENT_START, "Start Game Event _eventnameActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_START_CLOSEST_WAYPOINT, "Move to closest Waypoint _actionParamOne_ _actionParamTwo_ _actionParamThree_ _actionParamFour_ _actionParamFive_ _actionParamSix_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_MOVE_OFFSET, "Move Offset - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RANDOM_SOUND, "Play _PlayRandomSoundDistance_ _actionParamOne_ _actionParamTwo_ _actionParamThree_ _actionParamFour_ _PlayRandomSoundTarget_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_CORPSE_DELAY, "Set Corpse Delay _actionParamOne_ seconds - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_DISABLE_EVADE, "Disable Evade - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_GO_SET_GO_STATE, "Set Gameobject State _SetgoStateActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_CAN_FLY, "Set can Fly _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_AURAS_BY_TYPE, "Remove Aura Type: _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_SIGHT_DIST, "Set Sight Distance _actionParamOne_ yards - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_FLEE, "Set Flee Motiontype for _actionParamOne_ milliseconds - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ADD_THREAT, "Add _actionParamOne_ threat, Remove _actionParamTwo_ threat - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_LOAD_EQUIPMENT, "Load Equipment ID _actionParamOne_ _forcedUnforcedActionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_TRIGGER_RANDOM_TIMED_EVENT, "Trigger Random Timed Event between _actionParamOne_ and _actionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_ALL_GAMEOBJECTS, "Removes all gameobjects related to the unit - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_PAUSE_MOVEMENT, "Pause Movement (Movementslot: _MovementSlotActionParamOne_, PauseTime: _actionParamTwo_ ms, _forcedUnforcedActionParamThree_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_PLAY_ANIMKIT, "Play Animationkit (ID: _actionParamOne_, Type: _AnimKitTypeActionParamTwo_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SCENE_PLAY, "Play Scene ID _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SCENE_CANCEL, "Cancel Scene ID _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SPAWN_SPAWNGROUP, "Spawn Spawngroup ID _actionParamOne_ (Min Secs: _actionParamTwo_, Max Secs: _actionParamThree_, Spawnflags: _actionParamFour_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_DESPAWN_SPAWNGROUP, "Despawn Spawngroup ID _actionParamOne_ (Min Secs: _actionParamTwo_, Max Secs: _actionParamThree_, Spawnflags: _actionParamFour_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RESPAWN_BY_SPAWNID, "Respawn SpawnID _actionParamTwo_ with SpawnType _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_INVOKER_CAST_, "Cast '_spellNameActionParamOne_' _getCastFlags_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_PLAY_CINEMATIC, "Play Cinematic Entry: _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_MOVEMENT_SPEED, "Set Creature Movement Speed (MovementType: _actionParamOne_, SpeedInteger: _actionParamTwo_, SpeedFraction: _actionParamThree_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_PLAY_SPELL_VISUAL_KIT, "Play Spell Visual Kit ID: _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_OVERRIDE_LIGHT, "Set Zone Override Light in ZoneID: _actionParamOne_ with LightID: _actionParamTwo_ (FadeInTime: _actionParamThree_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_OVERRIDE_WEATHER, "Set Zone Weather in ZoneID: _actionParamOne_ with WeatherID: _actionParamTwo_ (WeatherGrade: _actionParamThree_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_AI_ANIM_KIT, "Set AI Animation Kit _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_HOVER, "Set Hover State: _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_142, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_143, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_144, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_145, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_146, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_147, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_148, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_149, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_150, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_151, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_152, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_153, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_154, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_155, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_156, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_157, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_158, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_159, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_160, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_161, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_162, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_163, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_164, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_165, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_166, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_167, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_168, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_169, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_170, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_171, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_172, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_173, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_174, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_175, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_176, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_177, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_178, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_179, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_180, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_181, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_182, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_183, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_184, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_185, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_186, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_187, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_188, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_189, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_190, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_191, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_192, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_193, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_194, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_195, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_196, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_197, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_198, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_199, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_CREATURE_RESPAWN_TIME, "Set Creature Respawntime to _actionParamOne_ seconds - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_MOVE_HOME_POS, "Move to Homeposition (PointID: _actionParamOne_, Pathfinding: _enabledDisabledActionParamThree_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_LOAD_GRID, "Load Grid - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_STORE_EVENT_PHASE, "Store Current Event Phase - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_STORED_EVENT_PHASE, "Load Stored Event Phase - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CHECK_DUPLICATE_AND_DESPAWN, "If more than _actionParamThree_ Creature(s) with Entry: _actionParamOne_ in _actionParamTwo_ yards range, creature will despawn - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_DISMOUNT, "Dismount _DespawnCurrentPetsParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CHECK_HOMEDIST_AND_EVADE, "Evade after moving _actionParamOne_ yards away from HomePosition - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_SPEED_RATE, "Set Speed Rate (Speed Walk Rate: _checkSpeedWalkActionParamOne_, Speed Run Rate: _checkSpeedRunActionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_LOAD_WP_PATH, "Load Waypoint-Data ID: _actionParamOne_ (AddonPath: _actionParamTwo_, _unrepeatableRepeatableActionParamThree_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_UNUSED_210, "Set  - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CAST_RANDOM_SPELL, "Cast Random Spell (Spell ID 1: '_spellNameActionParamOne_', Spell ID 2: '_spellNameActionParamTwo_', Spell ID 3: '_spellNameActionParamThree_', Spell ID 4: '_spellNameActionParamFour_', Cast Flags: '_actionParamFive_', Trigger Flags: _actionParamSix_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_COMBAT_STOP_WITH_PETS, "Combat Stop with Pets - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_PAUSE_WAYPOINT_DATA_MOVEMENT, "_pauseUnPauseActionParamOne_ Waypoint-Data Movement - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_MOVE_TO_POS_TARGET, "Move selected target to Position - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SAVE_CHECKPOINT, "Save Checkpoint Position - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_CROSS_CAST_IN_COMBAT, "Cross Cast '_spellNameActionParamOne_' _getCastFlags_ in Combat - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_EXIT_VEHICLE, "Exit Vehicle - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_HEALTH, "Set Health - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_STOP_MOTION, "Stop Motion (StopMovement: _actionParamOne_, MovementExpired: _actionParamTwo_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_LOAD_TEMPLATE, "Load SAI Template: _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SPAWN_CREATUREGROUP, "Spawn CreatureGroup ID _actionParamOne_ (Min Secs: _actionParamTwo_, Max Secs: _actionParamThree_, Spawnflags: _actionParamFour_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_DESPAWN_CREATUREGROUP, "Despawn CreatureGroup ID _actionParamOne_ (Min Secs: _actionParamTwo_, Max Secs: _actionParamThree_, Spawnflags: _actionParamFour_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SPAWN_GAMEOBJECTGROUP, "Spawn GameobjectGroup ID _actionParamOne_ (Min Secs: _actionParamTwo_, Max Secs: _actionParamThree_, Spawnflags: _actionParamFour_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_DESPAWN_GAMEOBJECTGROUP, "Despawn GameobjectGroup ID _actionParamOne_ (Min Secs: _actionParamTwo_, Max Secs: _actionParamThree_, Spawnflags: _actionParamFour_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_UNIT_MOVEMENT_FLAGS, "Set Unit Movement Flags: _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_COMBAT_DISTANCE, "Set Combat Distance: _actionParamOne_ Yard(s) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ADD_IMMUNITY, "Add Immunity ID: _actionParamOne_, Immunity Type: _actionParamTwo_, Immunity Value: _actionParamThree_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_IMMUNITY, "Remove Immunity ID: _actionParamOne_, Immunity Type: _actionParamTwo_, Immunity Value: _actionParamThree_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_MOVE_FALL, "Move Fall - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_EVENT_PHASE_RESET, "_EnableDisablePhaseResetActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ZONE_UNDER_ATTACK, "Set Zone Under Attack - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_REMOVE_LOOT_MODE, "Remove Loot Mode: _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_REPUTATION_GAIN, "_enableDisableActionParamOne_ Reputation Gain - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_ADD_LOOT_MODE, "Add Loot Mode: _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_COUNTER_RESET_OPTION, "_OnResetOnInitializeActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_GO_DELETE, "Delete Gameobject - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_LEVEL, "Set Level _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_CREATURE_SCALE, "Set Creature Scale _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_FEATHER_FALL, "Set Feather Fall _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_WATER_WALKING, "Set Water Walking _onOffActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_MELEE_DAMAGE_SCHOOL, "Set Melee Damage School Type: _MeleeDamageSchoolActionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_UNIT_STATE, "_addRemoveActionParamOne_ Unit State: _UnitStateActionParamTwo_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_RESET_LOOT_MODE, "Reset Loot Mode - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_LOOT_MODE, "Set Loot Mode: _actionParamOne_ - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_MOVE_SPAWN_POS, "Move to Respawn Position (PointID: _actionParamOne_, Pathfinding: _enabledDisabledActionParamThree_) - Target: _getTargetType_");
            smartActionStrings.Add(SmartAction.SMART_ACTION_SET_FLY_MODE, "_enableDisableActionParamOne_ Fly Mode - Target: _getTargetType_");
        }

        public async Task<string> GenerateCommentFor(SmartScript smartScript, EntryOrGuidAndSourceType entryOrGuidAndSourceType, bool forced = false, SmartScript smartScriptLink = null)
        {
            //! Don't generate a comment when:
            //- The world database is not supposed to be used;
            //- The setting to generate comments is turned off and the comment generating was not FORCED.
            if (!Settings.Default.UseWorldDatabase || (!forced && !Settings.Default.GenerateComments))
                return !String.IsNullOrWhiteSpace(smartScript.comment) ? smartScript.comment : SAI_Editor_Manager.Instance.GetDefaultCommentForSourceType((SourceTypes)smartScript.source_type);

            string fullLine = String.Empty;

            try
            {
                WorldDatabase worldDatabase = SAI_Editor_Manager.Instance.worldDatabase;
                SQLiteDatabase sqliteDatabase = SAI_Editor_Manager.Instance.sqliteDatabase;

                int entry = smartScript.entryorguid;

                switch (smartScript.source_type)
                {
                    case 0: //! Creature
                        fullLine += await worldDatabase.GetObjectNameByIdOrGuidAndSourceType(SourceTypes.SourceTypeCreature, smartScript.entryorguid, true) + " - ";
                        fullLine += smartEventStrings[(SmartEvent)smartScript.event_type];
                        break;
                    case 1: //! Gameobject
                        fullLine += await worldDatabase.GetObjectNameByIdOrGuidAndSourceType(SourceTypes.SourceTypeGameobject, smartScript.entryorguid, true) + " - ";
                        fullLine += smartEventStrings[(SmartEvent)smartScript.event_type];
                        break;
                    case 2: //! Areatrigger
                        fullLine += "Areatrigger - ";

                        switch ((SmartEvent)smartScript.event_type)
                        {
                            case SmartEvent.SMART_EVENT_AREATRIGGER_ONTRIGGER:
                            case SmartEvent.SMART_EVENT_LINK:
                                fullLine += "On Trigger";
                                break;
                            default:
                                fullLine += "INCORRECT EVENT TYPE";
                                break;
                        }

                        break;
                    case 9: //! Actionlist
                        if (entryOrGuidAndSourceType.sourceType == SourceTypes.SourceTypeScriptedActionlist)
                        {
                            List<EntryOrGuidAndSourceType> timedActionListOrEntries = await SAI_Editor_Manager.Instance.GetTimedActionlistsOrEntries(smartScript, SourceTypes.SourceTypeScriptedActionlist);

                            if (timedActionListOrEntries != null && timedActionListOrEntries.Count > 0)
                                fullLine += await worldDatabase.GetObjectNameByIdOrGuidAndSourceType(timedActionListOrEntries[0].sourceType, timedActionListOrEntries[0].entryOrGuid, true) + " - ";
                        }
                        else
                            fullLine += await worldDatabase.GetObjectNameByIdOrGuidAndSourceType(entryOrGuidAndSourceType.sourceType, entryOrGuidAndSourceType.entryOrGuid, true) + " - ";

                        fullLine += (SmartEvent)smartScript.event_type != SmartEvent.SMART_EVENT_UPDATE_IC ? "INCORRECT EVENT TYPE" : "On Script";
                        break;
                }

                if (fullLine.Contains("_previousLineComment_") && smartScriptLink != null)
                    fullLine = fullLine.Replace("_previousLineComment_", smartEventStrings[(SmartEvent)smartScriptLink.event_type]);

                fullLine = fullLine.Replace("_previousLineComment_", "MISSING LINK");

                //! This must be called AFTER we check for _previousLineComment_ so that copied event types don't need special handling
                if (fullLine.Contains("_eventParamOne_"))
                    fullLine = fullLine.Replace("_eventParamOne_", smartScript.event_param1.ToString());

                if (fullLine.Contains("_eventParamTwo_"))
                    fullLine = fullLine.Replace("_eventParamTwo_", smartScript.event_param2.ToString());

                if (fullLine.Contains("_eventParamThree_"))
                    fullLine = fullLine.Replace("_eventParamThree_", smartScript.event_param3.ToString());

                if (fullLine.Contains("_eventParamFour_"))
                    fullLine = fullLine.Replace("_eventParamFour_", smartScript.event_param4.ToString());

                if (fullLine.Contains("_spellNameEventParamOne_"))
                {
                    if (smartScript.event_param1 > 0)
                        fullLine = fullLine.Replace("_spellNameEventParamOne_", await worldDatabase.GetSpellNameById(smartScript.event_param1));
                    else
                        fullLine = fullLine.Replace(" '_spellNameEventParamOne_'", String.Empty);
                }

                if (fullLine.Contains("_targetCastingSpellName_"))
                {
                    if (smartScript.event_param3.ToString() != "0")
                        fullLine = fullLine.Replace("_targetCastingSpellName_", await worldDatabase.GetSpellNameById(smartScript.event_param3));
                    else
                        fullLine = fullLine.Replace(" '_targetCastingSpellName_'", String.Empty);
                }

                if (fullLine.Contains("_questNameEventParamOne_"))
                {
                    if (smartScript.event_param1 == 0) //! Any quest (SMART_EVENT_ACCEPTED_QUEST / SMART_EVENT_REWARD_QUEST)
                        fullLine = fullLine.Replace(" '_questNameEventParamOne_'", String.Empty);
                    else
                        fullLine = fullLine.Replace("_questNameEventParamOne_", await worldDatabase.GetQuestTitleById(smartScript.event_param1));
                }

                if (fullLine.Contains("_hasAuraEventParamOne_"))
                    fullLine = fullLine.Replace("_hasAuraEventParamOne_", smartScript.event_param1 > 0 ? "Has Aura" : "Aura Not Present");

                //! Action type
                fullLine += " - " + smartActionStrings[(SmartAction)smartScript.action_type];

                if (fullLine.Contains("_actionParamOne_"))
                    fullLine = fullLine.Replace("_actionParamOne_", smartScript.action_param1.ToString());

                if (fullLine.Contains("_actionParamTwo_"))
                    fullLine = fullLine.Replace("_actionParamTwo_", smartScript.action_param2.ToString());

                if (fullLine.Contains("_actionParamThree_"))
                    fullLine = fullLine.Replace("_actionParamThree_", smartScript.action_param3.ToString());

                if (fullLine.Contains("_actionParamFour_"))
                    fullLine = fullLine.Replace("_actionParamFour_", smartScript.action_param4.ToString());

                if (fullLine.Contains("_actionParamFive_"))
                    fullLine = fullLine.Replace("_actionParamFive_", smartScript.action_param5.ToString());

                if (fullLine.Contains("_actionParamSix_"))
                    fullLine = fullLine.Replace("_actionParamSix_", smartScript.action_param6.ToString());

                if (fullLine.Contains("_spellNameActionParamOne_"))
                {
                    if (smartScript.action_param1.ToString() != "0")
                        fullLine = fullLine.Replace("_spellNameActionParamOne_", await worldDatabase.GetSpellNameById(smartScript.action_param1));
                    else
                        fullLine = fullLine.Replace(" '_spellNameActionParamOne_'", String.Empty);
                }

                if (fullLine.Contains("_spellNameActionParamTwo_"))
                {
                    if (smartScript.action_param2.ToString() != "0")
                        fullLine = fullLine.Replace("_spellNameActionParamTwo_", await worldDatabase.GetSpellNameById(smartScript.action_param2));
                    else
                        fullLine = fullLine.Replace(" '_spellNameActionParamTwo_'", String.Empty);
                }

                if (fullLine.Contains("_spellNameActionParamThree_"))
                {
                    if (smartScript.action_param3.ToString() != "0")
                        fullLine = fullLine.Replace("_spellNameActionParamThree_", await worldDatabase.GetSpellNameById(smartScript.action_param3));
                    else
                        fullLine = fullLine.Replace(" '_spellNameActionParamThree_'", String.Empty);
                }

                if (fullLine.Contains("_spellNameActionParamFour_"))
                {
                    if (smartScript.action_param4.ToString() != "0")
                        fullLine = fullLine.Replace("_spellNameActionParamFour_", await worldDatabase.GetSpellNameById(smartScript.action_param4));
                    else
                        fullLine = fullLine.Replace(" '_spellNameActionParamFour_'", String.Empty);
                }

                if (fullLine.Contains("_spellNameActionParamFive_"))
                {
                    if (smartScript.action_param5.ToString() != "0")
                        fullLine = fullLine.Replace("_spellNameActionParamFive_", await worldDatabase.GetSpellNameById(smartScript.action_param5));
                    else
                        fullLine = fullLine.Replace(" '_spellNameActionParamFive_'", String.Empty);
                }

                if (fullLine.Contains("_questNameActionParamOne_"))
                    fullLine = fullLine.Replace("_questNameActionParamOne_", await worldDatabase.GetQuestTitleById(smartScript.action_param1));

                if (fullLine.Contains("_questNameCastCreatureOrGo_"))
                    fullLine = fullLine.Replace("_questNameCastCreatureOrGo_", await worldDatabase.GetQuestTitleByCriteria(smartScript.action_param1, smartScript.action_param1, smartScript.action_param1, smartScript.action_param1, smartScript.action_param2));

                if (fullLine.Contains("_questNameKillCredit_"))
                    fullLine = fullLine.Replace("_questNameKillCredit_", await worldDatabase.GetQuestTitleByCriteria(smartScript.action_param1, smartScript.action_param1, smartScript.action_param1, smartScript.action_param1));

                if (fullLine.Contains("_reactStateParamOne_"))
                {
                    switch (smartScript.action_param1)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_reactStateParamOne_", "Passive");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_reactStateParamOne_", "Defensive");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_reactStateParamOne_", "Aggressive");
                            break;
                        default:
                            fullLine = fullLine.Replace("_reactStateParamOne_", "<Unknown Reactstate>");
                            break;
                    }
                }

                if (fullLine.Contains("_reactStateParamSix_"))
                {
                    switch (smartScript.action_param1)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_reactStateParamSix_", "Passive");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_reactStateParamSix_", "Defensive");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_reactStateParamSix_", "Aggressive");
                            break;
                        default:
                            fullLine = fullLine.Replace("_reactStateParamSix_", "<Unknown Reactstate>");
                            break;
                    }
                }

                if (fullLine.Contains("_actionRandomParameters_"))
                {
                    string randomEmotes = smartScript.action_param1 + ", " + smartScript.action_param2;

                    if (smartScript.action_param3 > 0)
                        randomEmotes += ", " + smartScript.action_param3;

                    if (smartScript.action_param4 > 0)
                        randomEmotes += ", " + smartScript.action_param4;

                    if (smartScript.action_param5 > 0)
                        randomEmotes += ", " + smartScript.action_param5;

                    if (smartScript.action_param6 > 0)
                        randomEmotes += ", " + smartScript.action_param6;

                    fullLine = fullLine.Replace("_actionRandomParameters_", randomEmotes);
                }

                if (fullLine.Contains("_creatureNameActionParamOne_"))
                    fullLine = fullLine.Replace("_creatureNameActionParamOne_", await worldDatabase.GetCreatureNameById(smartScript.action_param1));

                if (fullLine.Contains("_eventnameActionParamOne_"))
                    fullLine = fullLine.Replace("_eventnameActionParamOne_", await worldDatabase.GetEventNameById(smartScript.action_param1));

                if (fullLine.Contains("_getUnitFlags_"))
                {
                    string commentUnitFlag = "";
                    int unitFlags = smartScript.action_param1;

                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_SERVER_CONTROLLED) != 0) commentUnitFlag += "Server Controlled & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_NON_ATTACKABLE) != 0) commentUnitFlag += "Not Attackable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_DISABLE_MOVE) != 0) commentUnitFlag += "Disable Movement & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PVP_ATTACKABLE) != 0) commentUnitFlag += "Pvp Attackable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_RENAME) != 0) commentUnitFlag += "Rename & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PREPARATION) != 0) commentUnitFlag += "Preparation & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_NOT_ATTACKABLE_1) != 0) commentUnitFlag += "Not Attackable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_IMMUNE_TO_PC) != 0) commentUnitFlag += "Immune To Players & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_IMMUNE_TO_NPC) != 0) commentUnitFlag += "Immune To NPC's & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_LOOTING) != 0) commentUnitFlag += "Looting & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PET_IN_COMBAT) != 0) commentUnitFlag += "Pet In Combat & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PVP) != 0) commentUnitFlag += "PvP & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_SILENCED) != 0) commentUnitFlag += "Silenced & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PACIFIED) != 0) commentUnitFlag += "Pacified & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_STUNNED) != 0) commentUnitFlag += "Stunned & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_IN_COMBAT) != 0) commentUnitFlag += "In Combat & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_DISARMED) != 0) commentUnitFlag += "Disarmed & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_CONFUSED) != 0) commentUnitFlag += "Confused & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_FLEEING) != 0) commentUnitFlag += "Fleeing & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PLAYER_CONTROLLED) != 0) commentUnitFlag += "Player Controlled & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_NOT_SELECTABLE) != 0) commentUnitFlag += "Not Selectable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_SKINNABLE) != 0) commentUnitFlag += "Skinnable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_MOUNT) != 0) commentUnitFlag += "Mounted & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_SHEATHE) != 0) commentUnitFlag += "Sheathed & ";

                    commentUnitFlag = commentUnitFlag.Trim(new[] { ' ', '&', ' ' }); //! Trim last ' & ' from the comment..

                    if (commentUnitFlag.Contains("&"))
                        fullLine = fullLine.Replace("_getUnitFlags_", "s_getUnitFlags_");

                    fullLine = fullLine.Replace("_getUnitFlags_", " " + commentUnitFlag);
                }

                if (fullLine.Contains("_getTriggerCastFlags_"))
                {
                    string commentTriggerCastFlag = "";
                    int triggercastFlags = smartScript.action_param1;

                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_NONE) != 0) commentTriggerCastFlag += "TRIGGERED_NONE & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_GCD) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_GCD & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_SPELL_AND_CATEGORY_CD) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_SPELL_AND_CATEGORY_CD & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_POWER_AND_REAGENT_COST) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_POWER_AND_REAGENT_COST & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_CAST_ITEM) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_CAST_ITEM & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_AURA_SCALING) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_AURA_SCALING & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_CAST_IN_PROGRESS) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_CAST_IN_PROGRESS & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_COMBO_POINTS) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_COMBO_POINTS & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_CAST_DIRECTLY) != 0) commentTriggerCastFlag += "TRIGGERED_CAST_DIRECTLY & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_AURA_INTERRUPT_FLAGS) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_AURA_INTERRUPT_FLAGS & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_SET_FACING) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_SET_FACING & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_SHAPESHIFT) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_SHAPESHIFT & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_CASTER_AURASTATE) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_CASTER_AURASTATE & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_DISALLOW_PROC_EVENTS) != 0) commentTriggerCastFlag += "TRIGGERED_DISALLOW_PROC_EVENTS & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_CASTER_MOUNTED_OR_ON_VEHICLE) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_CASTER_MOUNTED_OR_ON_VEHICLE & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_IGNORE_CASTER_AURAS) != 0) commentTriggerCastFlag += "TRIGGERED_IGNORE_CASTER_AURAS & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_DONT_RESET_PERIODIC_TIMER) != 0) commentTriggerCastFlag += "TRIGGERED_DONT_RESET_PERIODIC_TIMER & ";
                    if ((triggercastFlags & (int)TriggerCastFlags.TRIGGERED_DONT_REPORT_CAST_ERROR) != 0) commentTriggerCastFlag += "TRIGGERED_DONT_REPORT_CAST_ERROR & ";

                    commentTriggerCastFlag = commentTriggerCastFlag.Trim(new[] { ' ', '&', ' ' }); //! Trim last ' & ' from the comment..

                    if (commentTriggerCastFlag.Contains("&"))
                        fullLine = fullLine.Replace("_getTriggerCastFlags_", "s_getTriggerCastFlags_");

                    fullLine = fullLine.Replace("_getTriggerCastFlags_", " " + commentTriggerCastFlag);
                }

                if (fullLine.Contains("_getCastFlags_"))
                {
                    string commentCastFlag = "";
                    int castFlags = smartScript.action_param2;

                    if ((castFlags & (int)SmartCastFlags.SMARTCAST_INTERRUPT_PREVIOUS) != 0) commentCastFlag += "Interrupt Previous & ";
                    if ((castFlags & (int)SmartCastFlags.SMARTCAST_TRIGGERED) != 0) commentCastFlag += "Triggered & ";
                    if ((castFlags & (int)SmartCastFlags.SMARTCAST_AURA_NOT_PRESENT) != 0) commentCastFlag += "Aura Not Present & ";
                    if ((castFlags & (int)SmartCastFlags.SMARTCAST_COMBAT_MOVE) != 0) commentCastFlag += "CmC & ";

                    commentCastFlag = commentCastFlag.Trim(new[] { ' ', '&', ' ' }); //! Trim last ' & ' from the comment..

                    if (commentCastFlag.Contains("&"))
                        fullLine = fullLine.Replace("_getCastFlags_", "with CastFlags_getCastFlags_");

                    fullLine = fullLine.Replace("_getCastFlags_", " " + commentCastFlag);
                }

                if (fullLine.Contains("_getNpcFlags_"))
                {
                    string commentNpcFlag = "";
                    int npcFlags = smartScript.action_param1;

                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_NONE) != 0) commentNpcFlag += "None & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_GOSSIP) != 0) commentNpcFlag += "Gossip & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_QUESTGIVER) != 0) commentNpcFlag += "Questgiver & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_UNK1) != 0) commentNpcFlag += "Unknown 1 & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_UNK2) != 0) commentNpcFlag += "Unknown 2 & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_TRAINER) != 0) commentNpcFlag += "Trainer & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_TRAINER_CLASS) != 0) commentNpcFlag += "Class Trainer & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_TRAINER_PROFESSION) != 0) commentNpcFlag += "Profession Trainer & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_VENDOR) != 0) commentNpcFlag += "Vendor & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_VENDOR_AMMO) != 0) commentNpcFlag += "Ammo Vendor & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_VENDOR_FOOD) != 0) commentNpcFlag += "Food Vendor & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_VENDOR_POISON) != 0) commentNpcFlag += "Poison Vendor & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_VENDOR_REAGENT) != 0) commentNpcFlag += "Reagent Vendor & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_REPAIR) != 0) commentNpcFlag += "Repair Vendor & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_FLIGHTMASTER) != 0) commentNpcFlag += "Flightmaster & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_SPIRITHEALER) != 0) commentNpcFlag += "Spirithealer & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_SPIRITGUIDE) != 0) commentNpcFlag += "Spiritguide & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_INNKEEPER) != 0) commentNpcFlag += "Innkeeper & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_BANKER) != 0) commentNpcFlag += "Banker & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_PETITIONER) != 0) commentNpcFlag += "Petitioner & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_TABARDDESIGNER) != 0) commentNpcFlag += "Tabard Designer & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_BATTLEMASTER) != 0) commentNpcFlag += "Battlemaster & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_AUCTIONEER) != 0) commentNpcFlag += "Auctioneer & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_STABLEMASTER) != 0) commentNpcFlag += "Stablemaster & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_GUILD_BANKER) != 0) commentNpcFlag += "Guild Banker & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_SPELLCLICK) != 0) commentNpcFlag += "Spellclick & ";
                    if ((npcFlags & (int)NpcFlags.UNIT_NPC_FLAG_PLAYER_VEHICLE) != 0) commentNpcFlag += "Player Vehicle & ";

                    commentNpcFlag = commentNpcFlag.Trim(new[] { ' ', '&', ' ' }); //! Trim last ' & ' from the comment..

                    if (commentNpcFlag.Contains("&"))
                        fullLine = fullLine.Replace("_getNpcFlags_", "s_getNpcFlags_");

                    fullLine = fullLine.Replace("_getNpcFlags_", " " + commentNpcFlag);
                }

                if (fullLine.Contains("_enableDisableActionParamOne_"))
                {
                    if (smartScript.action_param1.ToString() == "0")
                        fullLine = fullLine.Replace("_enableDisableActionParamOne_", "Disable");
                    else
                        fullLine = fullLine.Replace("_enableDisableActionParamOne_", "Enable");
                }

                if (fullLine.Contains("_RespawnHomePosActionParamOne_"))
                {
                    if (smartScript.action_param1.ToString() == "0")
                        fullLine = fullLine.Replace("_RespawnHomePosActionParamOne_", "to Respawnposition");
                    else if (smartScript.action_param1.ToString() == "1")
                        fullLine = fullLine.Replace("_RespawnHomePosActionParamOne_", "to last stored Homeposition");
                }

                if (fullLine.Contains("_AddEmoteTextActionParamOne_"))
                {
                    if (smartScript.action_param1.ToString() == "0")
                        fullLine = fullLine.Replace("_AddEmoteTextActionParamOne_", "without Emotetext");
                    else if (smartScript.action_param1.ToString() == "1")
                        fullLine = fullLine.Replace("_AddEmoteTextActionParamOne_", "With Emotetext");
                }

                if (fullLine.Contains("_OnResetOnInitializeActionParamOne_"))
                {
                    if (smartScript.action_param1.ToString() == "0")
                        fullLine = fullLine.Replace("_OnResetOnInitializeActionParamOne_", "Set Counter Reset on Reset of creature only");
                    else if (smartScript.action_param1.ToString() == "1")
                        fullLine = fullLine.Replace("_OnResetOnInitializeActionParamOne_", "Set Counter Reset on Initialize of creature only");
                }

                if (fullLine.Contains("_incrementOrDecrementActionParamOne_"))
                {
                    if (smartScript.action_param1.ToString() == "1")
                        fullLine = fullLine.Replace("_incrementOrDecrementActionParamOne_", "Increment");
                    else if (smartScript.action_param2.ToString() == "1")
                        fullLine = fullLine.Replace("_incrementOrDecrementActionParamOne_", "Decrement");
                    else
                        fullLine = fullLine.Replace("_incrementOrDecrementActionParamOne_", "Increment or Decrement");
                }

                if (fullLine.Contains("_sheathActionParamOne_"))
                {
                    switch (smartScript.action_param1)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_sheathActionParamOne_", "Unarmed");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_sheathActionParamOne_", "Melee");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_sheathActionParamOne_", "Ranged");
                            break;
                        default:
                            fullLine = fullLine.Replace("_sheathActionParamOne_", "<Unknown Sheath>");
                            break;
                    }
                }

                if (fullLine.Contains("_MovementSlotActionParamOne_"))
                {
                    switch (smartScript.action_param1)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_MovementSlotActionParamOne_", "Default");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_MovementSlotActionParamOne_", "Active");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_MovementSlotActionParamOne_", "Controlled");
                            break;
                        default:
                            fullLine = fullLine.Replace("_MovementSlotActionParamOne_", "<Unknown MovementSlot>");
                            break;
                    }
                }

                if (fullLine.Contains("_TimerTypeActionParamTwo_"))
                {
                    switch (smartScript.action_param2)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_TimerTypeActionParamTwo_", "Out of Combat");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_TimerTypeActionParamTwo_", "In Combat");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_TimerTypeActionParamTwo_", "Always");
                            break;
                        default:
                            fullLine = fullLine.Replace("_TimerTypeActionParamTwo_", "<Unknown TimerType>");
                            break;
                    }
                }

                if (fullLine.Contains("_AnimKitTypeActionParamTwo_"))
                {
                    switch (smartScript.action_param2)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_AnimKitTypeActionParamTwo_", "oneShot");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_AnimKitTypeActionParamTwo_", "aiAnim");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_AnimKitTypeActionParamTwo_", "meleeAnim");
                            break;
                        case 3:
                            fullLine = fullLine.Replace("_AnimKitTypeActionParamTwo_", "movementAnim");
                            break;
                        default:
                            fullLine = fullLine.Replace("_AnimKitTypeActionParamTwo_", "<Unknown AnimationKitType>");
                            break;
                    }
                }

                if (fullLine.Contains("_MeleeDamageSchoolActionParamOne_"))
                {
                    switch (smartScript.action_param1)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_MeleeDamageSchoolActionParamOne_", "Normal");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_MeleeDamageSchoolActionParamOne_", "Holy");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_MeleeDamageSchoolActionParamOne_", "Fire");
                            break;
                        case 3:
                            fullLine = fullLine.Replace("_MeleeDamageSchoolActionParamOne_", "Nature");
                            break;
                        case 4:
                            fullLine = fullLine.Replace("_MeleeDamageSchoolActionParamOne_", "Frost");
                            break;
                        case 5:
                            fullLine = fullLine.Replace("_MeleeDamageSchoolActionParamOne_", "Shadow");
                            break;
                        case 6:
                            fullLine = fullLine.Replace("_MeleeDamageSchoolActionParamOne_", "Arcane");
                            break;
                        default:
                            fullLine = fullLine.Replace("_MeleeDamageSchoolActionParamOne_", "SPELL_SCHOOL_UNKNOWN");
                            break;
                    }
                }

                if (fullLine.Contains("_addRemoveActionParamOne_"))
                {
                    if (smartScript.action_param1 == 1)
                        fullLine = fullLine.Replace("_addRemoveActionParamOne_", "Apply");
                    else
                        fullLine = fullLine.Replace("_addRemoveActionParamOne_", "Unapply");
                }

                if (fullLine.Contains("_UnitStateActionParamTwo_"))
                {
                    switch (smartScript.action_param2)
                    {
                        case 1:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_DIED");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_MELEE_ATTACKING");
                            break;
                        case 4:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_CHARMED");
                            break;
                        case 8:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_STUNNED");
                            break;
                        case 16:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_ROAMING");
                            break;
                        case 32:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_CHASE");
                            break;
                        case 128:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_FLEEING");
                            break;
                        case 256:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_IN_FLIGHT");
                            break;
                        case 512:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_FOLLOW");
                            break;
                        case 1024:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_ROOT");
                            break;
                        case 2048:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_CONFUSED");
                            break;
                        case 4096:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_DISTRACTED");
                            break;
                        case 8192:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_ISOLATED");
                            break;
                        case 16384:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_ATTACK_PLAYER");
                            break;
                        case 32768:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_CASTING");
                            break;
                        case 65536:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_POSSESSED");
                            break;
                        case 131072:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_CHARGING");
                            break;
                        case 262144:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_JUMPING");
                            break;
                        case 1048576:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_MOVE");
                            break;
                        case 2097152:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_ROTATING");
                            break;
                        case 4194304:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_EVADE");
                            break;
                        case 8388608:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_ROAMING_MOVE");
                            break;
                        case 16777216:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_CONFUSED_MOVE");
                            break;
                        case 33554432:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_FLEEING_MOVE");
                            break;
                        case 67108864:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_CHASE_MOVE");
                            break;
                        case 134217728:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_FOLLOW_MOVE");
                            break;
                        case 268435456:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_IGNORE_PATHFINDING");
                            break;
                        case 536870912:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_DIFFERENT_PATHFINDING");
                            break;
                        default:
                            fullLine = fullLine.Replace("_UnitStateActionParamTwo_", "UNIT_STATE_UNKOWN");
                            break;
                    }
                }

                if (fullLine.Contains("_forceDespawnActionParamOne_"))
                {
                    if (smartScript.action_param1 > 2)
                        fullLine = fullLine.Replace("_forceDespawnActionParamOne_", "In " + smartScript.action_param1 + " ms");
                    else
                        fullLine = fullLine.Replace("_forceDespawnActionParamOne_", "Instant");
                }

                if (fullLine.Contains("_invincibilityHpActionParamsOneTwo_"))
                {
                    if (smartScript.action_param1 > 0)
                        fullLine = fullLine.Replace("_invincibilityHpActionParamsOneTwo_", "Set Invincibility at " + smartScript.action_param1.ToString() + " HP");
                    else if (smartScript.action_param2 > 0)
                        fullLine = fullLine.Replace("_invincibilityHpActionParamsOneTwo_", "Set Invincibility at " + smartScript.action_param2 + " percent HP");
                    else if (smartScript.action_param1 == 0 && smartScript.action_param2 == 0)
                        fullLine = fullLine.Replace("_invincibilityHpActionParamsOneTwo_", "Reset Invincibility HP");
                    else
                        fullLine = fullLine.Replace("_invincibilityHpActionParamsOneTwo_", "<Please Insert parameters>");
                }

                if (fullLine.Contains("_onOffActionParamOne_"))
                {
                    if (smartScript.action_param1 == 1)
                        fullLine = fullLine.Replace("_onOffActionParamOne_", "On");
                    else
                        fullLine = fullLine.Replace("_onOffActionParamOne_", "Off");
                }

                if (fullLine.Contains("_onOffActionParamThree_"))
                {
                    if (smartScript.action_param3 == 1)
                        fullLine = fullLine.Replace("_onOffActionParamThree_", "On");
                    else
                        fullLine = fullLine.Replace("_onOffActionParamThree_", "Off");
                }

                if (fullLine.Contains("_setDataSetBossStateActionParamThree_"))
                {
                    if (smartScript.action_param3 == 1)
                        fullLine = fullLine.Replace("_setDataSetBossStateActionParamThree_", "SetBossState");
                    else
                        fullLine = fullLine.Replace("_setDataSetBossStateActionParamThree_", "SetData");
                }

                if (fullLine.Contains("_updateLevelActionParamTwo_"))
                {
                    if (smartScript.action_param2 == 1)
                        fullLine = fullLine.Replace("_updateLevelActionParamTwo_", "Update Level");
                    else
                        fullLine = fullLine.Replace("_updateLevelActionParamTwo_", "Do not Update Level");
                }

                if (fullLine.Contains("_addEmoteTexts_"))
                {
                    if (smartScript.action_param2 == 1)
                        fullLine = fullLine.Replace("_addEmoteTexts_", "(Add Emote Text)");
                    else
                        fullLine = fullLine.Replace("_addEmoteTexts_", "");
                }

                if (fullLine.Contains("_pauseUnPauseActionParamOne_"))
                {
                    if (smartScript.action_param1 == 1)
                        fullLine = fullLine.Replace("_pauseUnPauseActionParamOne_", "Pause");
                    else
                        fullLine = fullLine.Replace("_pauseUnPauseActionParamOne_", "Resume");
                }

                if (fullLine.Contains("_startStopActionParamOne_"))
                {
                    if (smartScript.action_param1 > 0)
                        fullLine = fullLine.Replace("_startStopActionParamOne_", "Start");
                    else
                        fullLine = fullLine.Replace("_startStopActionParamOne_", "Stop");
                }

                if (fullLine.Contains("_SpellInterruptDelayActionParamOne_"))
                {
                    if (smartScript.action_param1 > 0)
                        fullLine = fullLine.Replace("_SpellInterruptDelayActionParamOne_", "with Delay");
                    else
                        fullLine = fullLine.Replace("_SpellInterruptDelayActionParamOne_", "without Delay");
                }

                if (fullLine.Contains("_SpellInterruptInstanceActionParamOne_"))
                {
                    if (smartScript.action_param3 > 0)
                        fullLine = fullLine.Replace("_SpellInterruptInstanceActionParamOne_", "instantly.");
                    else
                        fullLine = fullLine.Replace("_SpellInterruptInstanceActionParamOne_", "not instantly.");
                }

                if (fullLine.Contains("_AttackInvokerCreatureGroupActionParamTwo_"))
                {
                    if (smartScript.action_param2 > 0)
                        fullLine = fullLine.Replace("_AttackInvokerCreatureGroupActionParamTwo_", "(Attack Invoker)");
                    else
                        fullLine = fullLine.Replace("_AttackInvokerCreatureGroupActionParamTwo_", "(Do not attack Invoker)");
                }

                if (fullLine.Contains("_EnableDisablePhaseResetActionParamOne_"))
                {
                    if (smartScript.action_param1 == 1)
                        fullLine = fullLine.Replace("_EnableDisablePhaseResetActionParamOne_", "Enable Event-Phase-Reset on SAI-Reset");
                    else if (smartScript.action_param1 == 0)
                        fullLine = fullLine.Replace("_EnableDisablePhaseResetActionParamOne_", "Disable Event-Phase-Reset on SAI-Reset");
                }

                if (fullLine.Contains("_enabledDisabledActionParamThree_"))
                {
                    if (smartScript.action_param3 == 1)
                        fullLine = fullLine.Replace("_enabledDisabledActionParamThree_", "Disabled");
                    else
                        fullLine = fullLine.Replace("_enabledDisabledActionParamThree_", "Enabled");
                }

                if (fullLine.Contains("_DespawnCurrentPetsParamOne_"))
                {
                    if (smartScript.action_param1 >= 1)
                        fullLine = fullLine.Replace("_DespawnCurrentPetsParamOne_", "and Despawn all current Pets");
                    else
                        fullLine = fullLine.Replace("_DespawnCurrentPetsParamOne_", "");
                }

                if (fullLine.Contains("_checkSpeedWalkActionParamOne_"))
                {
                    if (smartScript.action_param1 > 0)
                        fullLine = fullLine.Replace("_checkSpeedWalkActionParamOne_", smartScript.action_param1.ToString());
                    else
                        fullLine = fullLine.Replace("_checkSpeedWalkActionParamOne_", "Speed Walk Rate not set");
                }

                if (fullLine.Contains("_checkSpeedRunActionParamTwo_"))
                {
                    if (smartScript.action_param2 > 0)
                        fullLine = fullLine.Replace("_checkSpeedRunActionParamTwo_", smartScript.action_param2.ToString());
                    else
                        fullLine = fullLine.Replace("_checkSpeedRunActionParamTwo_", "Speed Run Rate not set");
                }

                if (fullLine.Contains("_unrepeatableRepeatableActionParamThree_"))
                {
                    if (smartScript.action_param3 == 1)
                        fullLine = fullLine.Replace("_unrepeatableRepeatableActionParamThree_", "Repeatable Path");
                    else if (smartScript.action_param3 == 0)
                        fullLine = fullLine.Replace("_unrepeatableRepeatableActionParamThree_", "Non Repeatable Path");
                }

                if (fullLine.Contains("_forcedUnforcedActionParamTwo_"))
                {
                    if (smartScript.action_param2 == 1)
                        fullLine = fullLine.Replace("_forcedUnforcedActionParamTwo_", "(Forced)");
                    else
                        fullLine = fullLine.Replace("_forcedUnforcedActionParamTwo_", "(Default)");
                }

                if (fullLine.Contains("_forcedUnforcedActionParamThree_"))
                {
                    if (smartScript.action_param3 == 1)
                        fullLine = fullLine.Replace("_forcedUnforcedActionParamThree_", "(Forced)");
                    else
                        fullLine = fullLine.Replace("_forcedUnforcedActionParamThree_", "(Default)");
                }

                if (fullLine.Contains("_gameobjectNameActionParamOne_"))
                    fullLine = fullLine.Replace("_gameobjectNameActionParamOne_", await worldDatabase.GetGameobjectNameById(smartScript.action_param1));

                if (fullLine.Contains("_addItemBasedOnActionParams_"))
                {
                    fullLine = fullLine.Replace("_addItemBasedOnActionParams_", "'" + await worldDatabase.GetItemNameById(smartScript.action_param1) + "' ");

                    if (smartScript.action_param2 > 1)
                        fullLine += smartScript.action_param2 + " Times";
                    else
                        fullLine += "1 Time";
                }

                if (fullLine.Contains("_updateAiTemplateActionParamOne_"))
                {
                    switch ((SmartAiTemplates)smartScript.action_param1)
                    {
                        case SmartAiTemplates.SMARTAI_TEMPLATE_BASIC:
                            fullLine = fullLine.Replace("_updateAiTemplateActionParamOne_", "Basic");
                            break;
                        case SmartAiTemplates.SMARTAI_TEMPLATE_CASTER:
                            fullLine = fullLine.Replace("_updateAiTemplateActionParamOne_", "Caster");
                            break;
                        case SmartAiTemplates.SMARTAI_TEMPLATE_TURRET:
                            fullLine = fullLine.Replace("_updateAiTemplateActionParamOne_", "Turret");
                            break;
                        case SmartAiTemplates.SMARTAI_TEMPLATE_PASSIVE:
                            fullLine = fullLine.Replace("_updateAiTemplateActionParamOne_", "Passive");
                            break;
                        case SmartAiTemplates.SMARTAI_TEMPLATE_CAGED_GO_PART:
                            fullLine = fullLine.Replace("_updateAiTemplateActionParamOne_", "Caged Gameobject Part");
                            break;
                        case SmartAiTemplates.SMARTAI_TEMPLATE_CAGED_NPC_PART:
                            fullLine = fullLine.Replace("_updateAiTemplateActionParamOne_", "Caged Creature Part");
                            break;
                        default:
                            fullLine = fullLine.Replace("_updateAiTemplateActionParamOne_", "<_updateAiTemplateActionParamOne_ Unknown ai template>");
                            break;
                    }
                }

                if (fullLine.Contains("_LoadGridXTargetType_"))
                {
                    switch ((SmartTarget)smartScript.target_type)
                    {
                        case SmartTarget.SMART_TARGET_POSITION:
                            fullLine = fullLine.Replace("_LoadGridXTargetType_", smartScript.target_x.ToString());
                            break;
                    }
                }

                if (fullLine.Contains("_LoadGridYTargetType_"))
                {
                    switch ((SmartTarget)smartScript.target_type)
                    {
                        case SmartTarget.SMART_TARGET_POSITION:
                            fullLine = fullLine.Replace("_LoadGridYTargetType_", smartScript.target_y.ToString());
                            break;
                    }
                }

                if (fullLine.Contains("_setOrientationTargetType_"))
                {
                    switch ((SmartTarget)smartScript.target_type)
                    {
                        case SmartTarget.SMART_TARGET_SELF:
                            fullLine = fullLine.Replace("_setOrientationTargetType_", "to Home Position");
                            break;
                        case SmartTarget.SMART_TARGET_POSITION:
                            fullLine = fullLine.Replace("_setOrientationTargetType_", smartScript.target_o.ToString());
                            break;
                        default:
                            fullLine = fullLine.Replace("_setOrientationTargetType_", "to Targettype");
                            break;
                    }
                }

                if (fullLine.Contains("_getTargetType_"))
                    fullLine = fullLine.Replace("_getTargetType_", await GetStringByTargetType(smartScript, worldDatabase));

                if (fullLine.Contains("_goStateActionParamOne_"))
                {
                    switch (smartScript.action_param1)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_goStateActionParamOne_", "Not Ready");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_goStateActionParamOne_", "Ready");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_goStateActionParamOne_", "Activated");
                            break;
                        case 3:
                            fullLine = fullLine.Replace("_goStateActionParamOne_", "Deactivated");
                            break;
                        default:
                            fullLine = fullLine.Replace("_goStateActionParamOne_", "<Unknown Gameobject State>");
                            break;
                    }
                }

                if (fullLine.Contains("_SetgoStateActionParamOne_"))
                {
                    switch (smartScript.action_param1)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_SetgoStateActionParamOne_", "GO_STATE_ACTIVE");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_SetgoStateActionParamOne_", "GO_STATE_READY");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_SetgoStateActionParamOne_", "GO_STATE_ACTIVE_ALTERNATIVE");
                            break;
                        default:
                            fullLine = fullLine.Replace("_SetgoStateActionParamOne_", "<Unknown Gameobject State>");
                            break;
                    }
                }

                if (fullLine.Contains("_getGoFlags_"))
                {
                    string commentGoFlag = "";
                    int goFlags = smartScript.action_param1;

                    if ((goFlags & (int)GoFlags.GO_FLAG_IN_USE) != 0) commentGoFlag += "In Use & ";
                    if ((goFlags & (int)GoFlags.GO_FLAG_LOCKED) != 0) commentGoFlag += "Locked & ";
                    if ((goFlags & (int)GoFlags.GO_FLAG_INTERACT_COND) != 0) commentGoFlag += "Interact Condition & ";
                    if ((goFlags & (int)GoFlags.GO_FLAG_TRANSPORT) != 0) commentGoFlag += "Transport & ";
                    if ((goFlags & (int)GoFlags.GO_FLAG_NOT_SELECTABLE) != 0) commentGoFlag += "Not Selectable & ";
                    if ((goFlags & (int)GoFlags.GO_FLAG_NODESPAWN) != 0) commentGoFlag += "No Despawn & ";
                    if ((goFlags & (int)GoFlags.GO_FLAG_TRIGGERED) != 0) commentGoFlag += "Triggered & ";
                    if ((goFlags & (int)GoFlags.GO_FLAG_DAMAGED) != 0) commentGoFlag += "Damaged & ";
                    if ((goFlags & (int)GoFlags.GO_FLAG_DESTROYED) != 0) commentGoFlag += "Destroyed & ";

                    commentGoFlag = commentGoFlag.Trim(new[] { ' ', '&', ' ' }); //! Trim last ' & ' from the comment..

                    if (commentGoFlag.Contains("&"))
                        fullLine = fullLine.Replace("_getGoFlags_", "s_getGoFlags_");

                    fullLine = fullLine.Replace("_getGoFlags_", " " + commentGoFlag);
                }

                if (fullLine.Contains("_getDynamicFlags_"))
                {
                    string commentDynamicFlag = "";
                    int dynamicFlags = smartScript.action_param1;

                    if ((dynamicFlags & (int)DynamicFlags.UNIT_DYNFLAG_NONE) != 0) commentDynamicFlag += "None & ";
                    if ((dynamicFlags & (int)DynamicFlags.UNIT_DYNFLAG_LOOTABLE) != 0) commentDynamicFlag += "Lootable & ";
                    if ((dynamicFlags & (int)DynamicFlags.UNIT_DYNFLAG_TRACK_UNIT) != 0) commentDynamicFlag += "Track Units & ";
                    if ((dynamicFlags & (int)DynamicFlags.UNIT_DYNFLAG_TAPPED) != 0) commentDynamicFlag += "Tapped & ";
                    if ((dynamicFlags & (int)DynamicFlags.UNIT_DYNFLAG_TAPPED_BY_PLAYER) != 0) commentDynamicFlag += "Tapped By Player & ";
                    if ((dynamicFlags & (int)DynamicFlags.UNIT_DYNFLAG_SPECIALINFO) != 0) commentDynamicFlag += "Special Info & ";
                    if ((dynamicFlags & (int)DynamicFlags.UNIT_DYNFLAG_DEAD) != 0) commentDynamicFlag += "Dead & ";
                    if ((dynamicFlags & (int)DynamicFlags.UNIT_DYNFLAG_REFER_A_FRIEND) != 0) commentDynamicFlag += "Refer A Friend & ";
                    if ((dynamicFlags & (int)DynamicFlags.UNIT_DYNFLAG_TAPPED_BY_ALL_THREAT_LIST) != 0) commentDynamicFlag += "Tapped By Threatlist & ";

                    commentDynamicFlag = commentDynamicFlag.Trim(new[] { ' ', '&', ' ' }); //! Trim last ' & ' from the comment..

                    if (commentDynamicFlag.Contains("&"))
                        fullLine = fullLine.Replace("_getDynamicFlags_", "s_getDynamicFlags_");

                    fullLine = fullLine.Replace("_getDynamicFlags_", " " + commentDynamicFlag);
                }

                if (fullLine.Contains("_getBytes1Flags_"))
                {
                    switch ((UnitFieldBytes1Types)smartScript.action_param2)
                    {
                        case UnitFieldBytes1Types.UNIT_STAND_STAND_STATE_TYPE:
                        {
                            switch ((UnitStandStateType)smartScript.action_param1)
                            {
                                case UnitStandStateType.UNIT_STAND_STATE_STAND:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Stand Up");
                                    break;
                                case UnitStandStateType.UNIT_STAND_STATE_SIT:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Sit Down");
                                    break;
                                case UnitStandStateType.UNIT_STAND_STATE_SIT_CHAIR:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Sit Down Chair");
                                    break;
                                case UnitStandStateType.UNIT_STAND_STATE_SLEEP:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Sleep");
                                    break;
                                case UnitStandStateType.UNIT_STAND_STATE_SIT_LOW_CHAIR:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Sit Low Chair");
                                    break;
                                case UnitStandStateType.UNIT_STAND_STATE_SIT_MEDIUM_CHAIR:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Sit Medium Chair");
                                    break;
                                case UnitStandStateType.UNIT_STAND_STATE_SIT_HIGH_CHAIR:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Sit High Chair");
                                    break;
                                case UnitStandStateType.UNIT_STAND_STATE_DEAD:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Dead");
                                    break;
                                case UnitStandStateType.UNIT_STAND_STATE_KNEEL:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Kneel");
                                    break;
                                case UnitStandStateType.UNIT_STAND_STATE_SUBMERGED:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Standstate Submerged");
                                    break;
                                default:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "<Unknown bytes1 (UnitStandStateType)>");
                                    break;
                            }
                            break;
                        }
                        case UnitFieldBytes1Types.UNIT_PET_TALENTS_TYPE:
                        {
                            fullLine = fullLine.Replace("_getBytes1Flags_", "<Unknown bytes1 type>");
                            break;
                        }
                        case UnitFieldBytes1Types.UNIT_STAND_FLAGS_TYPE:
                        {
                            switch ((UnitStandFlags)smartScript.action_param1)
                            {
                                case UnitStandFlags.UNIT_STAND_FLAGS_UNK1:
                                case UnitStandFlags.UNIT_STAND_FLAGS_UNK4:
                                case UnitStandFlags.UNIT_STAND_FLAGS_UNK5:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "<Unknown>");
                                    break;
                                case UnitStandFlags.UNIT_STAND_FLAGS_CREEP:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Creep");
                                    break;
                                case UnitStandFlags.UNIT_STAND_FLAGS_UNTRACKABLE:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Untrackable");
                                    break;
                                default:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "<Unknown bytes1 (UnitStandFlags)>");
                                    break;
                            }
                            break;
                        }
                        case UnitFieldBytes1Types.UNIT_BYTES1_FLAGS_TYPE:
                        {
                            switch ((UnitBytes1_Flags)smartScript.action_param1)
                            {
                                case UnitBytes1_Flags.UNIT_BYTE1_FLAG_UNK_3:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "<Unknown>");
                                    break;
                                case UnitBytes1_Flags.UNIT_BYTE1_FLAG_HOVER:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Hover");
                                    break;
                                case UnitBytes1_Flags.UNIT_BYTE1_FLAG_ALWAYS_STAND:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "Always Stand");
                                    break;
                                default:
                                    fullLine = fullLine.Replace("_getBytes1Flags_", "<Unknown bytes1 (UnitBytes1_Flags)>");
                                    break;
                            }
                            break;
                        }
                        default:
                            break;
                    }
                }

                if (fullLine.Contains("_powerTypeActionParamOne_"))
                {
                    switch (smartScript.action_param1)
                    {
                        case 0:
                            fullLine = fullLine.Replace("_powerTypeActionParamOne_", "Mana");
                            break;
                        case 1:
                            fullLine = fullLine.Replace("_powerTypeActionParamOne_", "Rage");
                            break;
                        case 2:
                            fullLine = fullLine.Replace("_powerTypeActionParamOne_", "Focus");
                            break;
                        case 3:
                            fullLine = fullLine.Replace("_powerTypeActionParamOne_", "Energy");
                            break;
                        case 4:
                            fullLine = fullLine.Replace("_powerTypeActionParamOne_", "Happiness");
                            break;
                        case 5:
                            fullLine = fullLine.Replace("_powerTypeActionParamOne_", "Rune");
                            break;
                        case 6:
                            fullLine = fullLine.Replace("_powerTypeActionParamOne_", "Runic Power");
                            break;
                        default:
                            fullLine = fullLine.Replace("_powerTypeActionParamOne_", "<Unknown Powertype>");
                            break;
                    }
                }

                if (fullLine.Contains("_getUnitFlags_"))
                {
                    string commentUnitFlag = "";
                    int unitFlags = smartScript.action_param1;

                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_SERVER_CONTROLLED) != 0) commentUnitFlag += "Server Controlled & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_NON_ATTACKABLE) != 0) commentUnitFlag += "Not Attackable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_DISABLE_MOVE) != 0) commentUnitFlag += "Disable Move & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PVP_ATTACKABLE) != 0) commentUnitFlag += "PvP Attackable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_RENAME) != 0) commentUnitFlag += "Rename & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PREPARATION) != 0) commentUnitFlag += "Preparation & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_NOT_ATTACKABLE_1) != 0) commentUnitFlag += "Not Attackable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_IMMUNE_TO_PC) != 0) commentUnitFlag += "Immune To Players & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_IMMUNE_TO_NPC) != 0) commentUnitFlag += "Immune To Creatures & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_LOOTING) != 0) commentUnitFlag += "Looting & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PET_IN_COMBAT) != 0) commentUnitFlag += "Pet In Combat & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PVP) != 0) commentUnitFlag += "PvP & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_SILENCED) != 0) commentUnitFlag += "Silenced & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PACIFIED) != 0) commentUnitFlag += "Pacified & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_STUNNED) != 0) commentUnitFlag += "Stunned & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_IN_COMBAT) != 0) commentUnitFlag += "In Combat & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_TAXI_FLIGHT) != 0) commentUnitFlag += "Taxi Flight & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_DISARMED) != 0) commentUnitFlag += "Disarmed & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_CONFUSED) != 0) commentUnitFlag += "Confused & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_FLEEING) != 0) commentUnitFlag += "Fleeing & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_PLAYER_CONTROLLED) != 0) commentUnitFlag += "Player Controlled & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_NOT_SELECTABLE) != 0) commentUnitFlag += "Not Selectable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_SKINNABLE) != 0) commentUnitFlag += "Skinnable & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_MOUNT) != 0) commentUnitFlag += "Mounted & ";
                    if ((unitFlags & (int)UnitFlags.UNIT_FLAG_SHEATHE) != 0) commentUnitFlag += "Sheathed & ";

                    commentUnitFlag = commentUnitFlag.Trim(new[] { ' ', '&', ' ' }); //! Trim last ' & ' from the comment..

                    if (commentUnitFlag.Contains("&"))
                        fullLine = fullLine.Replace("_getUnitFlags_", "s_getUnitFlags_");

                    fullLine = fullLine.Replace("_getUnitFlags_", " " + commentUnitFlag);
                }

                if (fullLine.Contains("_morphToEntryOrModelActionParams_"))
                {
                    if (smartScript.action_param1 > 0)
                        fullLine = fullLine.Replace("_morphToEntryOrModelActionParams_", "Morph To Creature " + await worldDatabase.GetCreatureNameById(smartScript.action_param1));
                    else if (smartScript.action_param2 > 0)
                        fullLine = fullLine.Replace("_morphToEntryOrModelActionParams_", "Morph To Model " + smartScript.action_param2);
                    else
                        fullLine = fullLine.Replace("_morphToEntryOrModelActionParams_", "Demorph");
                }

                if (fullLine.Contains("_mountToEntryOrModelActionParams_"))
                {
                    if (smartScript.action_param1 > 0)
                        fullLine = fullLine.Replace("_mountToEntryOrModelActionParams_", "Mount To Creature " + await worldDatabase.GetCreatureNameById(smartScript.action_param1));
                    else if (smartScript.action_param2 > 0)
                        fullLine = fullLine.Replace("_mountToEntryOrModelActionParams_", "Mount To Model " + smartScript.action_param2);
                    else
                        fullLine = fullLine.Replace("_mountToEntryOrModelActionParams_", "Dismount");
                }

                if (fullLine.Contains("_PlayRandomSoundTarget_"))
                {
                    if (smartScript.action_param5 == 1)
                        fullLine = fullLine.Replace("_PlayRandomSoundTarget_", "to self");
                    else
                        fullLine = fullLine.Replace("_PlayRandomSoundTarget_", "to all");
                }

                if (fullLine.Contains("_UseTalkTarget_"))
                {
                    if (smartScript.action_param3 == 1)
                        fullLine = fullLine.Replace("_UseTalkTarget_", "with using Talktarget");
                    else
                        fullLine = fullLine.Replace("_UseTalkTarget_", "not using Talktarget");
                }

                if (fullLine.Contains("_PlayRandomSoundDistance_"))
                {
                    if (smartScript.action_param6 == 0)
                        fullLine = fullLine.Replace("_PlayRandomSoundDistance_", "direct random sound");
                    else if (smartScript.action_param6 == 1)
                        fullLine = fullLine.Replace("_PlayRandomSoundDistance_", "distance random sound");
                }

                if (fullLine.Contains("_PlaySoundDistance_"))
                {
                    if (smartScript.action_param3 == 0)
                        fullLine = fullLine.Replace("_PlaySoundDistance_", "direct sound");
                    else if (smartScript.action_param3 == 1)
                        fullLine = fullLine.Replace("_PlaySoundDistance_", "distance sound");
                }

                if (fullLine.Contains("_PlaySoundTarget_"))
                {
                    if (smartScript.action_param2 == 1)
                        fullLine = fullLine.Replace("_PlaySoundTarget_", "to self");
                    else
                        fullLine = fullLine.Replace("_PlaySoundTarget_", "to all");
                }

                if (fullLine.Contains("_addQuestDirectly_"))
                {
                    if (smartScript.action_param2 == 1)
                        fullLine = fullLine.Replace("_addQuestDirectly_", "directly without previous condition");
                    else
                        fullLine = fullLine.Replace("_addQuestDirectly_", "not directly with previous condition");
                }

                if (fullLine.Contains("_AttackInvoker_"))
                {
                    if (smartScript.action_param4 == 1)
                        fullLine = fullLine.Replace("_AttackInvoker_", "and attack Invoker");
                    else
                        fullLine = fullLine.Replace("_AttackInvoker_", "");
                }

                if (fullLine.Contains("_startOrStopBasedOnTargetType_"))
                {
                    if (smartScript.target_type == 0)
                    {
                        fullLine = fullLine.Replace("_startOrStopBasedOnTargetType_", "Stop");
                        fullLine = fullLine.Replace("_getTargetType_", String.Empty);
                    }
                    else
                        fullLine = fullLine.Replace("_startOrStopBasedOnTargetType_", "Start");
                }

                int event_phase_mask = smartScriptLink != null ? smartScriptLink.event_phase_mask : smartScript.event_phase_mask;

                if ((SmartPhaseMasks)event_phase_mask != SmartPhaseMasks.SMART_EVENT_PHASE_ALWAYS)
                {
                    List<int> listOfSplitPhases = new List<int>();

                    int event_phase_mask2 = event_phase_mask;
                    int log2 = 0;

                    while (event_phase_mask2 >= 2)
                    {
                        event_phase_mask2 /= 2;
                        log2++;
                    }

                    for (int l2 = log2; l2 >= 0; l2--)
                    {
                        int power = (int)Math.Pow(2, l2);

                        if (event_phase_mask >= power)
                        {
                            event_phase_mask -= power;
                            listOfSplitPhases.Add(power);
                        }
                    }

                    int[] arrayOfSplitPhases = listOfSplitPhases.ToArray();
                    Array.Reverse(arrayOfSplitPhases); //! Reverse them so they are ascending
                    fullLine += " (Phase";

                    if (listOfSplitPhases.Count > 1)
                        fullLine += "s";

                    fullLine += " " + String.Join(" & ", arrayOfSplitPhases) + ")";
                }

                SmartEventFlags event_flags = (SmartEventFlags)(smartScriptLink != null ? smartScriptLink.event_flags : smartScript.event_flags);

                if (event_flags != SmartEventFlags.EVENT_FLAG_NONE)
                {
                    if (((event_flags & SmartEventFlags.EVENT_FLAG_NOT_REPEATABLE) != 0))
                        fullLine += " (No Repeat)";

                    if (((event_flags & SmartEventFlags.EVENT_FLAG_NORMAL_DUNGEON) != 0) && ((event_flags & SmartEventFlags.EVENT_FLAG_HEROIC_DUNGEON) != 0) &&
                        ((event_flags & SmartEventFlags.EVENT_FLAG_NORMAL_RAID) != 0) && ((event_flags & SmartEventFlags.EVENT_FLAG_HEROIC_RAID) != 0))
                        fullLine += " (Dungeon & Raid)";
                    else
                    {
                        if (((event_flags & SmartEventFlags.EVENT_FLAG_NORMAL_DUNGEON) != 0) && ((event_flags & SmartEventFlags.EVENT_FLAG_HEROIC_DUNGEON) != 0))
                            fullLine += " (Dungeon)";
                        else
                        {
                            if (((event_flags & SmartEventFlags.EVENT_FLAG_NORMAL_DUNGEON) != 0))
                                fullLine += " (Normal Dungeon)";
                            else if (((event_flags & SmartEventFlags.EVENT_FLAG_HEROIC_DUNGEON) != 0))
                                fullLine += " (Heroic Dungeon)";
                        }

                        if (((event_flags & SmartEventFlags.EVENT_FLAG_NORMAL_RAID) != 0) && ((event_flags & SmartEventFlags.EVENT_FLAG_HEROIC_RAID) != 0))
                            fullLine += " (Raid)";
                        else
                        {
                            if (((event_flags & SmartEventFlags.EVENT_FLAG_NORMAL_RAID) != 0))
                                fullLine += " (Normal Raid)";
                            else if (((event_flags & SmartEventFlags.EVENT_FLAG_HEROIC_RAID) != 0))
                                fullLine += " (Heroic Raid)";
                        }
                    }

                    if (((event_flags & SmartEventFlags.EVENT_FLAG_DONT_RESET) != 0))
                        fullLine += " (No Reset)";

                    if (((event_flags & SmartEventFlags.EVENT_FLAG_WHILE_CHARMED) != 0))
                        fullLine += " (While Charmed)";
                }

                return fullLine;
            }
            catch
            {
                MessageBox.Show("The comment generating threw back an error and could not finish. The new comment might be incomplete.", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return fullLine;
        }

        private async Task<string> GetStringByTargetType(SmartScript smartScript, WorldDatabase worldDatabase)
        {
            switch ((SmartTarget)smartScript.target_type)
            {
                case SmartTarget.SMART_TARGET_SELF:
                    return "Self";
                case SmartTarget.SMART_TARGET_VICTIM:
                    return "Victim";
                case SmartTarget.SMART_TARGET_HOSTILE_SECOND_AGGRO:
                    return "Second On Threatlist";
                case SmartTarget.SMART_TARGET_HOSTILE_LAST_AGGRO:
                    return "Last On Threatlist";
                case SmartTarget.SMART_TARGET_HOSTILE_RANDOM:
                    return "Random On Threatlist";
                case SmartTarget.SMART_TARGET_HOSTILE_RANDOM_NOT_TOP:
                    return "Random On Threatlist Not Top";
                case SmartTarget.SMART_TARGET_ACTION_INVOKER:
                    return "Invoker";
                case SmartTarget.SMART_TARGET_POSITION:
                    return "Position";
                case SmartTarget.SMART_TARGET_CREATURE_RANGE:
                case SmartTarget.SMART_TARGET_CREATURE_DISTANCE:
                case SmartTarget.SMART_TARGET_CLOSEST_CREATURE:
                    return "Closest Creature '" + await worldDatabase.GetCreatureNameById(smartScript.target_param1) + "'";
                case SmartTarget.SMART_TARGET_CREATURE_GUID:
                    return "Closest Creature '" + await worldDatabase.GetCreatureNameByGuid(smartScript.target_param1) + "'";
                case SmartTarget.SMART_TARGET_GAMEOBJECT_RANGE:
                case SmartTarget.SMART_TARGET_GAMEOBJECT_DISTANCE:
                case SmartTarget.SMART_TARGET_CLOSEST_GAMEOBJECT:
                    return "Closest Gameobject '" + await worldDatabase.GetGameobjectNameById(smartScript.target_param1) + "'";
                case SmartTarget.SMART_TARGET_GAMEOBJECT_GUID:
                    return "Closest Gameobject '" + await worldDatabase.GetGameobjectNameByGuid(smartScript.target_param1) + "'";
                case SmartTarget.SMART_TARGET_INVOKER_PARTY:
                    return "Invoker's Party";
                case SmartTarget.SMART_TARGET_PLAYER_RANGE:
                    return "Range Player";
                case SmartTarget.SMART_TARGET_PLAYER_DISTANCE:
                    return "Distance Player";
                case SmartTarget.SMART_TARGET_CLOSEST_PLAYER:
                    return "Closest Player";
                case SmartTarget.SMART_TARGET_ACTION_INVOKER_VEHICLE:
                    return "Invoker's Vehicle";
                case SmartTarget.SMART_TARGET_OWNER_OR_SUMMONER:
                    return "Owner Or Summoner";
                case SmartTarget.SMART_TARGET_THREAT_LIST:
                    return "First Unit On Threatlist";
                case SmartTarget.SMART_TARGET_CLOSEST_ENEMY:
                    return "Closest Enemy";
                case SmartTarget.SMART_TARGET_CLOSEST_FRIENDLY:
                    return "Closest Friendly Unit";
                case SmartTarget.SMART_TARGET_LOOT_RECIPIENTS:
                    return "Loot Recipients";
                case SmartTarget.SMART_TARGET_STORED:
                    return "Stored Target";
                case SmartTarget.SMART_TARGET_FARTHEST:
                    return "Farthest Target";
                case SmartTarget.SMART_TARGET_VEHICLE_PASSENGER:
                    return "Vehicle Passenger";
                case SmartTarget.SMART_TARGET_CLOSEST_UNSPAWNED_GAMEOBJECT:
                    return "Closest Unspawned Gameobject '" + await worldDatabase.GetGameobjectNameById(smartScript.target_param1) + "'";
                default:
                    return "<Please Insert Target Type>";
            }
        }
    }
}
