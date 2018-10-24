# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.28] - 2018-10-23
### Added
- Database.SpawnCreatures initial.
- QuestgiverStatusFlag enum
- CMSG_GOSSIP_HELLO 
- CMSG_QUESTGIVER_STATUS_QUERY
- CMSG_QUESTGIVER_HELLO
- SMSG_QUESTGIVER_QUEST_LIST
- SMSG_QUESTGIVER_STATUS
- SMSG_NPC_WONT_TALK
- PlayerUnit added list SpawnCreatures.
- PlayerManager.SpawnObjeto

### Changed
- command "npc xx" now spawn and add to database.
- Fixed UnityEntity

## [0.0.27] - 2018-10-23
### Added
- DatabaseModel.InsertItems seeds the database with items.
- DatabaseModel.InsertCreatures seeds the database with Creatures/NPC.
- Table.Items added Patch to identify patch added item.
- Table.Items added UsedAt to internal purpose.
- Table.Items added CountLoot to internal purpose.
- Items.7z with all Vanilla items in JSON.
- Tables.Creatures need improvement.
- Added command "npc xx" to spawn npc.
- SMSG_UPDATE_OBJECT.CreateUnit to spawn unit.
- UnitEntity with basic information.
- mongo.txt with helper commands.
- CMSG_CREATURE_QUERY
- SMSG_CREATURE_QUERY_RESPONSE

### Changed
- DatabaseModel removed Items Json.
- SMSG_ACTION_BUTTONS changed.
- SMSG_LOGIN_SETTIMESPEED try another function to send time speed.
- SMSG_QUERY_TIME_RESPONSE try another method to sen time response.
- OnItemQuerySingle fixed and added a update to UsedAt.
- CMSG_CREATURE_QUERY.CreatureId to CreatureEntry.
- CMSG_ITEM_QUERY_SINGLE.ItemIdo to ItemEntry.
- SMSG_UPDATE_OBJECT changed all functions to internal.
- ObjectEntity changed Entry cast type (byte) to (int).
- Gitignore now ignore all json files in '/Seeds' folder.

## [0.0.26] - 2018-10-21
### Added
- on Database.Character.Create fixed inventory.
- OnLogout dispatch despawn player and clear all knowPlayers.
- SMSG_UPDATE_OBJECT.CreateOutOfRangeUpdate
- PlayerManager.DespawnPlayer
- PlayerManager now check range to spawn and despawn Players.

### Changed
- Items Table default values
- Some Summary of files.
- OnItemQueryResponse now check two Ids.
- OnPlayerLogin.inventory now is RealmServerSession.SendInventory
- Fixed some spaces.
- SMSG_ITEM_QUERY_SINGLE_RESPONSE 90% done.

## [0.0.25] - 2018-10-20
### Added
- Enums 'Emote'
- Enums 'SheatheType'
- OnInspect.Handler on CMSG_INSPECT
- OnPlayedTime.Handler partial.
- now send SMSG_INITIALIZE_FACTIONS on PlayerLogin.
- OnSetSelection.Handler on CMSG_SET_SELECTION
- OnSetSheathed.Handler on CMSG_SETSHEATHED.
- OnTextEmote.Handler on CMSG_TEXT_EMOTE.
- OnTogglePvp.Handler
- SMSG_INITIALIZE_FACTIONS
- SMSG_INSPECT
- SMSG_PLAYED_TIME partial.
- World Manager
  - PlayersWhoKnow
  - SessionsWhoKnow

### Changed
- OnMessageChat now send Whisper Me/Target.
- OnMessageChat now send Say/Yell/Emote to Local/Near Players
- OnMessageChat now send Chat message to All.
- OnStandStateChange fixed
- CMSG_INSPECT changed Uid to PlayerUid
- CMSG_SET_SELECTION changed Uid to PlayedUid
- CMSG_MESSAGECHAT set default value.

## [0.0.24] - 2018-10-20
### Added
- PacketServer.Global.PsMovement transmit movement to all know players.
- Now you can see another players in the world. :)

## [0.0.23] - 2018-10-20
### Added
- Class Table Items added default value if not exist.
- Inventory partial is showing. (:

### Changed
- Moved SMSG_ITEM_QUERY_SINGLE_RESPONSE to correct file.
- Moved OnItemQuerySingle to correct file.
- Fixed partial of SMSG_UPDATE_OBJECT.CreateItem
- Fixed partial of ItemEntity.

## [0.0.22] - 2018-10-19
### Added
- Config Mongo ConnectionString to future.

### Changed
- Fixed all Config Project dependencies.
- Fixed XML Races ActionButtons Spells name.
- Moved some functions to correct file (WriteBytes and GenerateGuidBytes) to PacketServer.
- Readme now container instructions to run server.

## [0.0.21] - 2018-10-19
### Added
- Added Command Helper
- Command [gps] show current cordinates
- Command [emote Id] flag emote
- ItemEntity
- Command [level x] gain level
- Command [inv1] generate inventory
- Command [inv2] same
- SMSG_UPDATE_OBJECT.UpdateValues
- SMSG_UPDATE_OBJECT.CreateItem
- PlayerManager

### Changed
- Rename some Uid to correct PlayerUid
- SMSG_ITEM_QUERY_SINGLE_RESPONSE Partial

## [0.0.20] - 2018-10-18
### Changed
- StyleCI

## [0.0.19] - 2018-10-18
### Added
- SMSG_FRIEND_LIST
- SMSG_IGNORE_LIST
- FriendResults (Enum)
- SubFriends Character Database.
- SubIgnoreds Character Database.
- MOTD Support.
- OnFriendList.
- OnMessageChat *Partial.
- OnFriendAdd *Partial.

## [0.0.18] - 2018-10-18
### Added
- CharacterHelper.GetRaceModel
- CharacterHelper.GetScale
- SMSG_ACTION_BUTTONS
- SMSG_INITIAL_SPELLS
- WorldManager.OnPlayerSpawn
- WorldManager.OnPlayerDespawn

### Changed
- Fixed Model and Scale on Login
- Level now retrieve from Database
- (Field) PLAYER_FIELD_WATCHED_FACTION_INDEX is now correct
- Skills is available on Login World
- Spells is available on Login World
- Action Buttons partial available on Login World

## [0.0.17] - 2018-10-18
### Added
- OnLogout.Request
- OnLogout.Cancel
- CMSG_MOVE_TIME_SKIPPED
- MSG_MOVE_TIME_SKIPPED
- SMSG_FORCE_MOVE_ROOT
- SMSG_FORCE_MOVE_UNROOT
- SMSG_LOGOUT_CANCEL_ACK
- SMSG_LOGOUT_COMPLETE
- SMSG_LOGOUT_RESPONSE
- SMSG_TRADE_STATUS

### Changed
- AuthServer disabled timer to check RealmStatus
- MSG_MOVE_FALL_LAND default switch now write console Type

## [0.0.16] - 2018-10-18
### Added
- CMSG_ACTIVATETAXIEXPRESS
- CMSG_AREA_SPIRIT_HEALER_QUERY
- CMSG_AREA_SPIRIT_HEALER_QUEUE
- CMSG_AUCTION_LIST_BIDDER_ITEMS
- CMSG_AUCTION_LIST_ITEMS
- CMSG_AUCTION_LIST_OWNER_ITEMS
- CMSG_AUCTION_PLACE_BID
- CMSG_AUCTION_REMOVE_ITEM
- CMSG_AUCTION_SELL_ITEM
- CMSG_AUTOBANK_ITEM
- CMSG_AUTOEQUIP_ITEM_SLOT
- CMSG_AUTOSTORE_BANK_ITEM
- CMSG_BATTLEFIELD_JOIN
- CMSG_BATTLEFIELD_LIST
- CMSG_BATTLEFIELD_PORT
- CMSG_BATTLEMASTER_HELLO
- CMSG_BATTLEMASTER_JOIN
- CMSG_BUY_STABLE_SLOT
- CMSG_BUYBACK_ITEM
- CMSG_DUEL_ACCEPTED
- CMSG_FAR_SIGHT
- CMSG_GAMEOBJ_USE
- CMSG_GET_MAIL_LIST
- CMSG_GROUP_ASSISTANT_LEADER
- CMSG_GROUP_CHANGE_SUB_GROUP
- CMSG_GROUP_SWAP_SUB_GROUP
- CMSG_GUILD_INFO_TEXT
- CMSG_ITEM_NAME_QUERY
- CMSG_ITEM_TEXT_QUERY
- CMSG_LEARN_TALENT
- CMSG_LEAVE_BATTLEFIELD
- CMSG_LOOT_MASTER_GIVE
- CMSG_LOOT_ROLL
- CMSG_MAIL_CREATE_TEXT_ITEM
- CMSG_MAIL_DELETE
- CMSG_MAIL_MARK_AS_READ
- CMSG_MAIL_RETURN_TO_SENDER
- CMSG_MAIL_TAKE_ITEM
- CMSG_MAIL_TAKE_MONEY
- CMSG_MEETINGSTONE_JOIN
- CMSG_PET_CANCEL_AURA
- CMSG_PET_SPELL_AUTOCAST
- CMSG_PET_STOP_ATTACK
- CMSG_PET_UNLEARN
- CMSG_QUESTGIVER_STATUS_QUERY
- CMSG_QUESTLOG_SWAP_QUEST
- CMSG_REPAIR_ITEM
- CMSG_REQUEST_PARTY_MEMBER_STATS
- CMSG_RESURRECT_RESPONSE
- CMSG_SEND_MAIL
- CMSG_SET_ACTIONBAR_TOGGLES
- CMSG_SET_AMMO
- CMSG_SET_FACTION_INACTIVE
- CMSG_SET_WATCHED_FACTION
- CMSG_STABLE_PET
- CMSG_STABLE_SWAP_PET
- CMSG_SUMMON_RESPONSE
- CMSG_UNSTABLE_PET
- MSG_AUCTION_HELLO
- MSG_LIST_STABLED_PETS
- MSG_QUEST_PUSH_RESULT

## [0.0.15] - 2018-10-17
### Added
- CMSG_ACTIVATETAXI
- CMSG_BANKER_ACTIVATE
- CMSG_BINDER_ACTIVATE
- CMSG_BUG
- CMSG_BUY_ITEM
- CMSG_BUY_ITEM_IN_SLOT
- CMSG_CHAT_IGNORED
- CMSG_GMTICKET_UPDATETEXT
- CMSG_GUILD_ADD_RANK
- CMSG_GUILD_RANK
- CMSG_GUILD_SET_OFFICER_NOTE
- CMSG_GUILD_SET_PUBLIC_NOTE
- CMSG_LIST_INVENTORY
- CMSG_OFFER_PETITION
- CMSG_PET_CAST_SPELL
- CMSG_PETITION_BUY
- CMSG_PETITION_QUERY
- CMSG_PETITION_SHOW_SIGNATURES
- CMSG_PETITION_SHOWLIST
- CMSG_PETITION_SIGN
- CMSG_QUEST_POI_QUERY
- CMSG_RECLAIM_CORPSE
- CMSG_REQUEST_ACCOUNT_DATA
- CMSG_SELL_ITEM
- CMSG_SETSHEATHED
- CMSG_SPIRIT_HEALER_ACTIVATE
- CMSG_TAXINODE_STATUS_QUERY
- CMSG_TAXIQUERYAVAILABLENODES
- CMSG_TRAINER_BUY_SPELL
- CMSG_TRAINER_LIST
- CMSG_TURN_IN_PETITION
- CMSG_UNLEARN_SKILL
- CMSG_WRAP_ITEM
- MSG_MINIMAP_PING
- MSG_PETITION_DECLINE
- MSG_RANDOM_ROLL
- MSG_SAVE_GUILD_EMBLEM
- MSG_TABARDVENDOR_ACTIVATE

### Changed
- CMSG_PLAYER_MACRO_OBSOLETE to CMSG_QUEST_POI_QUERY
- SMSG_PLAYER_MACRO_OBSOLETE to SMSG_QUEST_POI_QUERY_RESPONSE

## [0.0.14] - 2018-10-17
### Added
- Some ENUM's
- Debug Log in all Packet Reader.
- CMSG_ADD_FRIEND
- CMSG_ADD_IGNORE
- CMSG_AREATRIGGER
- CMSG_ATTACKSWING
- CMSG_AUTOEQUIP_ITEM
- CMSG_AUTOSTORE_BAG_ITEM
- CMSG_AUTOSTORE_LOOT_ITEM
- CMSG_CANCEL_AURA
- CMSG_CANCEL_CAST
- CMSG_CANCEL_CHANNELLING
- CMSG_CAST_SPELL
- CMSG_CHANNEL_ANNOUNCEMENTS
- CMSG_CHANNEL_BAN
- CMSG_CHANNEL_INVITE
- CMSG_CHANNEL_KICK
- CMSG_CHANNEL_LIST
- CMSG_CHANNEL_MODERATE
- CMSG_CHANNEL_MODERATOR
- CMSG_CHANNEL_MUTE
- CMSG_CHANNEL_OWNER
- CMSG_CHANNEL_PASSWORD
- CMSG_CHANNEL_SET_OWNER
- CMSG_CHANNEL_UNBAN
- CMSG_CHANNEL_UNMODERATOR
- CMSG_CHANNEL_UNMUTE
- CMSG_CLEAR_TRADE_ITEM
- CMSG_CREATURE_QUERY
- CMSG_DEL_FRIEND
- CMSG_DEL_IGNORE
- CMSG_DESTROYITEM
- CMSG_EMOTE
- CMSG_GAMEOBJECT_QUERY
- CMSG_GMTICKET_CREATE
- CMSG_GOSSIP_HELLO
- CMSG_GOSSIP_SELECT_OPTION
- CMSG_GROUP_INVITE
- CMSG_GROUP_SET_LEADER
- CMSG_GROUP_UNINVITE
- CMSG_GROUP_UNINVITE_GUID
- CMSG_GUILD_CREATE
- CMSG_GUILD_DEMOTE
- CMSG_GUILD_INVITE
- CMSG_GUILD_LEADER
- CMSG_GUILD_MOTD
- CMSG_GUILD_PROMOTE
- CMSG_GUILD_QUERY
- CMSG_GUILD_REMOVE
- CMSG_INITIATE_TRADE
- CMSG_INSPECT
- CMSG_ITEM_QUERY_SINGLE
- CMSG_LEAVE_CHANNEL
- CMSG_LOOT
- CMSG_LOOT_METHOD
- CMSG_MESSAGECHAT
- CMSG_NPC_TEXT_QUERY
- CMSG_OPEN_ITEM
- CMSG_PAGE_TEXT_QUERY
- CMSG_PET_ABANDON
- CMSG_PET_ACTION
- CMSG_PET_NAME_QUERY
- CMSG_PET_RENAME
- CMSG_PET_SET_ACTION
- CMSG_PUSHQUESTTOPARTY
- CMSG_QUEST_QUERY
- CMSG_QUESTGIVER_ACCEPT_QUEST
- CMSG_QUESTGIVER_CHOOSE_REWARD
- CMSG_QUESTGIVER_COMPLETE_QUEST
- CMSG_QUESTGIVER_HELLO
- CMSG_QUESTGIVER_QUERY_QUEST
- CMSG_QUESTGIVER_REQUEST_REWARD
- CMSG_QUESTLOG_REMOVE_QUEST
- CMSG_READ_ITEM
- CMSG_SET_ACTION_BUTTON
- CMSG_SET_SELECTION
- CMSG_SET_TRADE_GOLD
- CMSG_SET_TRADE_ITEM
- CMSG_SPLIT_ITEM
- CMSG_SWAP_INV_ITEM
- CMSG_SWAP_ITEM
- CMSG_TEXT_EMOTE
- CMSG_USE_ITEM
- CMSG_WHO
- CMSG_WHOIS
- SMSG_CHANNEL_NOTIFY
- SMSG_GMTICKET_CREATE
- SMSG_GMTICKET_GETTICKET
- SMSG_GMTICKET_SYSTEMSTATUS
- SMSG_QUERY_TIME_RESPONSE

### Changed
- OnQueryTime now send SMSG_QUERY_TIME_RESPONSE
- OnJoinChannel now send SMSG_CHANNEL_NOTIFY
- OnGmTicketGetTicket now send SMSG_GMTICKET_GETTICKET

## [0.0.13] - 2018-10-15
### Added
- Added FindCharacaterByUid RealmServer Character Model
- Added SMSG_TRIGGER_CINEMATIC
- Added CMSG_COMPLETE_CINEMATIC OnCompleteCinematic
- Added CMSG_TUTORIAL_FLAG OnTutorialFlag
- Added CMSG_TUTORIAL_CLEAR OnTutorialClear
- Added CMSG_TUTORIAL_RESET OnTutorialReset
- Added CMSG_SET_FACTION_ATWAR OnSetFactionAtWar
- Added SMSG_SET_FACTION_STANDING
- Added SMSG_NAME_QUERY_RESPONSE

### Changed
- OnNameQuery correct way.

## [0.0.12] - 2018-10-15
### Added
- Character Table
  - Flag = CharacterFlag(Enum)
  - Cinematic = bool
- Item Table added.
- RealmServer
  - Added InventorySlots Enum.

### Changed
- Common DBC Splited to Matching Files.
- RealmServer: SMSG_CHAR_ENUM now show item gear.

## [0.0.11] - 2018-10-15
### Added
- RealmServer
  - Characters Spells (SubDocument Database)
  - Characters ActionBar (SubDocument Database)
  - Characters Factions (SubDocument Database)
  - Characters Skill (SubDocument Database)
  - Characters Inventory (SubDocument Database)
  - Some ENUMs (ChatMessageLanguage, ChatMessageType)
  - Movement partial Added
  - SMSG_MESSAGECHAT *partial
- Common
  - RaceXML Added with start attributes

## [0.0.10] - 2018-10-14
### Added
- World server is ready to walk!!!
- RealmServer
  - Added some Enums.
  - SMSG_UPDATE_OBJECT with CreateOwnCharacterUpdate.
  - CMSG_UPDATE_ACCOUNT_DATA
  - CMSG_STANDSTATECHANGE
  - CMSG_NAME_QUERY
  - CMSG_REQUEST_RAID_INFO
  - CMSG_GMTICKET_GETTICKET
  - CMSG_QUERY_TIME
  - MSG_QUERY_NEXT_MAIL_TIME
  - CMSG_BATTLEFIELD_STATUS
  - CMSG_MEETINGSTONE_INFO
  - CMSG_ZONEUPDATE
  - CMSG_JOIN_CHANNEL
  - CMSG_SET_ACTIVE_MOVER
  - MSG_MOVE_FALL_LAND
- World Entity

### Changed
- RealmServerSession users to user.

## [0.0.9] - 2018-10-13
### Added
- AuthServer
  - AuthServerSession added Try.Catch and Connected check.
  - Added new method on PsAuthLogonChallenge
  - Added new method on PsAuthLogonProof
- Common
  - Create unique index 'name' on Characters
  - Add function to generate random ULONG "GenerateRandUlong".
- RealmServer
  - Database.Characters: Added DeleteCharacter.
  - CMSG_CHAR_DELETE
  - SMSG_CHAR_DELETE
  - CMSG_PLAYER_LOGIN
  - SMSG_ACCOUNT_DATA_TIMES
  - SMSG_BINDPOINTUPDATE
  - SMSG_CORPSE_RECLAIM_DELAY
  - SMSG_INIT_WORLD_STATES
  - SMSG_LOGIN_SETTIMESPEED
  - SMSG_LOGIN_VERIFY_WORLD
  - SMSG_SET_REST_START
  - SMSG_TUTORIAL_FLAGS
  - SMSG_UPDATE_OBJECT

### Changed
- AuthServer
  - Moved handlers to each class.
  - Database rewrite to support MongoDB.
  - PsAuthRealmList correct.
- Common
  - Fix Character table structure
  - Users Model 'username' & 'password' default to Upper.
- RealmServer
  - Database.Characters: Removed shaolinq.
  - Database.Characters: rewrite all queries to support MongoDB.
  - SMSG_CHAR_ENUM fixed new typo

### Removed
- AuthServerHandler
- Common: removed Write null byte function.

## [0.0.8] - 2018-10-11
### Added
- RealmServer Database
  - Added Character model

### Changed
- Moved some models character to correct class
- Adjust StackTrace print
- Apply Style All Code

## [0.0.7] - 2018-10-10
### Added
- AuthServer Database
  - UpdateRealmStatus
- AuthServerHelper
  - CheckRealmStatus
- AuthServer
  - Added Timer to check realm alive.
- RealmServer DBC Reader

### Changed
- Removed reference to sqlite
- Fix Global.Enums Typo and Documentation
- README
- CultureInfo
- SMSG_AUTH_CHALLENGE

## [0.0.6] - 2018-10-09
### Added
- Common.Globals.Enum
  - CharacterFlag
- RealmEnums.SMSG_ADDON_INFO Partial
- RealmEnums.SMSG_CHAR_ENUM
- RealmServer Database
  - GetCharacters (from user)
  - FindCharacaterByName

### Changed
- OnAuthSession documented.

## [0.0.5] - 2018-10-09
### Added
- RealmEnums.CMSG_PING
- RealmEnums.SMSG_PONG
- INITIAL Config File Utils

## [0.0.4] - 2018-10-09
### Added
- RealmEnums.CMSG_AUTH_SESSION wow send this packet to handle session.
- RealmEnums.SMSG_AUTH_RESPONSE server return this with OK or error.
- Doc for Common.Utils.ByteArrayToHex
- RealmServer Session to handler sessions between players
- RealmServer Database
  - GetAccount
- Common.Utils.DumpPacket

### Removed
- Common.Utils.Int32ToBigEndianHexByteString

### Moved
- Common.Utils.Decode moved to RealmServerSession.Decode
- Common.Utils.Encode moved to RealmServerSession.Encode

## [0.0.3] - 2018-10-08
### Added
- RealmServer Enums.
- LogPacket (Sending/Receive) on Common.Helpers.Log.
- Encode/Decode Packet for Vanilla.
- RealmServer Class to handle connections.
- RealmServer Router to handle opcodes.
- RealmEnums.SMSG_AUTH_CHALLENGE this packet is sending for every new connection.

### Removed
- Removed Print unused function.

## [0.0.2] - 2018-10-08
### Added
- App.config file

### Changed
- Improved code (Styled).

### Removed
- Non used imports.
- Log.Print of "User disconnected".

## [0.0.1] - 2018-10-08
### Added
- AuthLogonChallenge ( all packets for 1.2.* ).

### Changed
- Moved PacketReader handlers to correct folder.
- Moved PacketServer handlers to correct folder.
- Improve code.

### Removed
- All RealmServer Files.