﻿// -----------------------------------------------------------------
// <copyright file="IGame.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Author: Jose L. Nunez de Caceres
// http://linkedin.com/in/jlnunez89
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------

namespace OpenTibia.Server.Contracts.Abstractions
{
    using System;
    using System.Buffers;
    using Microsoft.Extensions.Hosting;
    using OpenTibia.Communications.Contracts.Abstractions;
    using OpenTibia.Data.Entities;
    using OpenTibia.Server.Contracts.Enumerations;
    using OpenTibia.Server.Contracts.Structs;

    /// <summary>
    /// Interface for the game instance.
    /// </summary>
    public interface IGame : IHostedService
    {
        /// <summary>
        /// The default combat rount time in milliseconds.
        /// </summary>
        const int DefaultCombatRoundTimeInMs = 2000;

        /// <summary>
        /// Gets the game's current time.
        /// </summary>
        DateTimeOffset CurrentTime { get; }

        /// <summary>
        /// Gets the game world's light color.
        /// </summary>
        byte WorldLightColor { get; }

        /// <summary>
        /// Gets the game world's light level.
        /// </summary>
        byte WorldLightLevel { get; }

        /// <summary>
        /// Gets the game world's state.
        /// </summary>
        WorldState Status { get; }

        /// <summary>
        /// Requests a notification be scheduled to be sent.
        /// </summary>
        /// <param name="notification">The notification.</param>
        void RequestNofitication(INotification notification);

        /// <summary>
        /// Gets the description bytes of the map in behalf of a given player at a given location.
        /// </summary>
        /// <param name="player">The player for which the description is being retrieved for.</param>
        /// <param name="location">The center location from which the description is being retrieved.</param>
        /// <returns>A sequence of bytes representing the description.</returns>
        ReadOnlySequence<byte> GetDescriptionOfMapForPlayer(IPlayer player, Location location);

        void ApplyDamage(ICombatant attacker, ICombatant target, AttackType attackType, int damageToApply, bool wasBlockedByArmor, bool wasShielded, TimeSpan exhaustion);

        void Request_AutoAttack(ICombatant combatant, ICombatant targetCombatant);

        /// <summary>
        /// Gets the description bytes of the map in behalf of a given player for the specified window.
        /// </summary>
        /// <param name="player">The player for which the description is being retrieved for.</param>
        /// <param name="startX">The starting X coordinate of the window.</param>
        /// <param name="startY">The starting Y coordinate of the window.</param>
        /// <param name="startZ">The starting Z coordinate of the window.</param>
        /// <param name="endZ">The ending Z coordinate of the window.</param>
        /// <param name="windowSizeX">The size of the window in X.</param>
        /// <param name="windowSizeY">The size of the window in Y.</param>
        /// <param name="startingZOffset">Optional. A starting offset for Z.</param>
        /// <returns>A sequence of bytes representing the description.</returns>
        ReadOnlySequence<byte> GetDescriptionOfMapForPlayer(IPlayer player, ushort startX, ushort startY, sbyte startZ, sbyte endZ, byte windowSizeX = IMap.DefaultWindowSizeX, byte windowSizeY = IMap.DefaultWindowSizeY, sbyte startingZOffset = 0);

        /// <summary>
        /// Gets the description bytes of a single tile of the map in behalf of a given player at a given location.
        /// </summary>
        /// <param name="player">The player for which the description is being retrieved for.</param>
        /// <param name="location">The location from which the description of the tile is being retrieved.</param>
        /// <returns>A sequence of bytes representing the tile's description.</returns>
        ReadOnlySequence<byte> GetDescriptionOfTile(IPlayer player, Location location);

        /// <summary>
        /// Attempts to schedule a creature's auto walk movements.
        /// </summary>
        /// <param name="creature">The creature making the request.</param>
        /// <param name="directions">The directions to walk to.</param>
        /// <param name="stepIndex">Optional. The index in the directions array at which to start moving. Defaults to zero.</param>
        /// <returns>True if the auto walk request was accepted, false otherwise.</returns>
        bool CreatureRequest_AutoWalk(ICreature creature, Direction[] directions, int stepIndex = 0);

        /// <summary>
        /// Attempts to cancel a player's auto attack operations.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool PlayerRequest_CancelAutoAttacks(IPlayer player);

        /// <summary>
        /// Attempts to cancel all of a player's pending movements.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool PlayerRequest_CancelPendingMovements(IPlayer player);

        /// <summary>
        /// Attempts to close a player's container.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <param name="containerId">The id of the container to close.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool PlayerRequest_CloseContainer(IPlayer player, byte containerId);

        /// <summary>
        /// Attempts to move up a contianer opened by a player.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <param name="containerId">The id of the container to move up from.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool PlayerRequest_MoveUpContainer(IPlayer player, byte containerId);

        /// <summary>
        /// Attempts to log a player in to the game.
        /// </summary>
        /// <param name="character">The character that the player is logging in to.</param>
        /// <param name="connection">The connection that the player uses.</param>
        /// <returns>An instance of the new <see cref="IPlayer"/> in the game, or null if it couldn't be instantiated.</returns>
        IPlayer PlayerRequest_Login(CharacterEntity character, IConnection connection);

        /// <summary>
        /// Attempts to log a player out of the game.
        /// </summary>
        /// <param name="player">The player to attempt to attempt log out.</param>
        /// <returns>True if the log-out request was accepted, false otherwise.</returns>
        bool PlayerRequest_Logout(IPlayer player);

        /// <summary>
        /// Attempts to schedule a creature's walk in the provided direction.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <param name="direction">The direction to walk to.</param>
        /// <returns>True if the walk request was accepted, false otherwise.</returns>
        bool PlayerRequest_WalkToDirection(IPlayer player, Direction direction);

        /// <summary>
        /// Attempts to rotate an item.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <param name="atLocation">The location at which the item to rotate is.</param>
        /// <param name="index">The index where the item to rotate is.</param>
        /// <param name="typeId">The type id of the item to rotate.</param>
        /// <returns>True if the turn request was accepted, false otherwise.</returns>
        bool PlayerRequest_RotateItemAt(IPlayer player, Location atLocation, byte index, ushort typeId);

        /// <summary>
        /// Attempts to turn a player to the requested direction.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <param name="direction">The direction to turn to.</param>
        /// <returns>True if the turn request was accepted, false otherwise.</returns>
        bool PlayerRequest_TurnToDirection(IPlayer player, Direction direction);

        /// <summary>
        /// Attempts to move a thing from the map on behalf of a player.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <param name="thingId">The id of the thing attempting to be moved.</param>
        /// <param name="fromLocation">The location from which the thing is being moved.</param>
        /// <param name="fromStackPos">The position in the stack of the location from which the thing is being moved.</param>
        /// <param name="toLocation">The location to which the thing is being moved.</param>
        /// <param name="count">The amount of the thing that is being moved.</param>
        /// <returns>True if the thing movement was accepted, false otherwise.</returns>
        bool PlayerRequest_MoveThingFromMap(IPlayer player, ushort thingId, Location fromLocation, byte fromStackPos, Location toLocation, byte count);

        /// <summary>
        /// Attempts to move a thing from a specific container on behalf of a player.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <param name="thingId">The id of the thing attempting to be moved.</param>
        /// <param name="containerId">The id of the container from which the thing is being moved.</param>
        /// <param name="containerIndex">The index within the container from which the thing is being moved.</param>
        /// <param name="toLocation">The location to which the thing is being moved.</param>
        /// <param name="amount">The amount of the thing that is being moved.</param>
        /// <returns>True if the thing movement was accepted, false otherwise.</returns>
        bool PlayerRequest_MoveThingFromContainer(IPlayer player, ushort thingId, byte containerId, byte containerIndex, Location toLocation, byte amount);

        /// <summary>
        /// Attempts to move a thing from an inventory slot on behalf of a player.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <param name="thingId">The id of the thing attempting to be moved.</param>
        /// <param name="slot">The inventory slot from which the thing is being moved.</param>
        /// <param name="toLocation">The location to which the thing is being moved.</param>
        /// <param name="count">The amount of the thing that is being moved.</param>
        /// <returns>True if the thing movement was accepted, false otherwise.</returns>
        bool PlayerRequest_MoveThingFromInventory(IPlayer player, ushort thingId, Slot slot, Location toLocation, byte count);

        /// <summary>
        /// Attempts to use an item on behalf of a player.
        /// </summary>
        /// <param name="player">The player making the request.</param>
        /// <param name="itemClientId">The id of the item attempting to be used.</param>
        /// <param name="fromLocation">The location from which the item is being used.</param>
        /// <param name="fromStackPos">The position in the stack of the location from which the item is being used.</param>
        /// <param name="index">The index of the item being used.</param>
        /// <returns>True if the use item request was accepted, false otherwise.</returns>
        bool PlayerRequest_UseItem(IPlayer player, ushort itemClientId, Location fromLocation, byte fromStackPos, byte index);

        /// <summary>
        /// Gets a cylinder from a location.
        /// </summary>
        /// <param name="fromLocation">The location from which to decode the cylinder information.</param>
        /// <param name="index">The index within the cyclinder to target.</param>
        /// <param name="subIndex">The sub-index within the cylinder to target.</param>
        /// <param name="creature">Optional. The creature that owns the target cylinder to target.</param>
        /// <returns>An instance of the target <see cref="ICylinder"/> of the location.</returns>
        ICylinder GetCyclinder(Location fromLocation, ref byte index, ref byte subIndex, ICreature creature = null);

        /// <summary>
        /// Attempts to find an item at the given location.
        /// </summary>
        /// <param name="typeId">The type id of the item to look for.</param>
        /// <param name="location">The location at which to look for the item.</param>
        /// <param name="creature">Optional. The creature that the location's cyclinder targets, if any.</param>
        /// <returns>An item instance, if found at the location.</returns>
        IItem FindItemByIdAtLocation(ushort typeId, Location location, ICreature creature = null);

        /// <summary>
        /// Immediately attempts to perform a creature movement to a tile on the map.
        /// </summary>
        /// <param name="creature">The creature being moved.</param>
        /// <param name="toLocation">The tile to which the movement is being performed.</param>
        /// <param name="isTeleport">Optional. A value indicating whether the movement is considered a teleportation. Defaults to false.</param>
        /// <param name="requestorCreature">Optional. The creature that this movement is being performed in behalf of, if any.</param>
        /// <returns>True if the movement was successfully performed, false otherwise.</returns>
        /// <remarks>Changes game state, should only be performed after all pertinent validations happen.</remarks>
        bool PerformCreatureMovement(ICreature creature, Location toLocation, bool isTeleport = false, ICreature requestorCreature = null);

        /// <summary>
        /// Immediately attempts to perform an item movement between two cylinders.
        /// </summary>
        /// <param name="item">The item being moved.</param>
        /// <param name="fromCylinder">The cylinder from which the movement is being performed.</param>
        /// <param name="toCylinder">The cylinder to which the movement is being performed.</param>
        /// <param name="fromIndex">Optional. The index within the cylinder to move the item from.</param>
        /// <param name="toIndex">Optional. The index within the cylinder to move the item to.</param>
        /// <param name="amountToMove">Optional. The amount of the thing to move. Defaults to 1.</param>
        /// <param name="requestorCreature">Optional. The creature that this movement is being performed in behalf of, if any.</param>
        /// <returns>True if the movement was successfully performed, false otherwise.</returns>
        /// <remarks>Changes game state, should only be performed after all pertinent validations happen.</remarks>
        bool PerformItemMovement(IItem item, ICylinder fromCylinder, ICylinder toCylinder, byte fromIndex = 0xFF, byte toIndex = 0xFF, byte amountToMove = 1, ICreature requestorCreature = null);

        /// <summary>
        /// Immediately attempts to perform an item use in behalf of the requesting creature, if any.
        /// </summary>
        /// <param name="item">The item being used.</param>
        /// <param name="fromCylinder">The cylinder from which the use is happening.</param>
        /// <param name="index">Optional. The index within the cylinder from which to use the item.</param>
        /// <param name="requestor">Optional. The creature requesting the use.</param>
        /// <returns>True if the item was successfully used, false otherwise.</returns>
        /// <remarks>Changes game state, should only be performed after all pertinent validations happen.</remarks>
        bool PerformItemUse(IItem item, ICylinder fromCylinder, byte index = 0xFF, ICreature requestor = null);

        /// <summary>
        /// Immediately attempts to perform an item change in behalf of the requesting creature, if any.
        /// </summary>
        /// <param name="item">The item being changed.</param>
        /// <param name="toTypeId">The type id of the item being changed to.</param>
        /// <param name="atCylinder">The cylinder at which the change is happening.</param>
        /// <param name="index">Optional. The index within the cylinder from which to change the item.</param>
        /// <param name="requestor">Optional. The creature requesting the change.</param>
        /// <returns>True if the item was successfully changed, false otherwise.</returns>
        /// <remarks>Changes game state, should only be performed after all pertinent validations happen.</remarks>
        bool PerformItemChange(IItem item, ushort toTypeId, ICylinder atCylinder, byte index = 0xFF, ICreature requestor = null);

        /// <summary>
        /// Immediately attempts to perform an item creation in behalf of the requesting creature, if any.
        /// </summary>
        /// <param name="typeId">The id of the item being being created.</param>
        /// <param name="atCylinder">The cylinder from which the use is happening.</param>
        /// <param name="index">Optional. The index within the cylinder from which to use the item.</param>
        /// <param name="requestor">Optional. The creature requesting the creation.</param>
        /// <returns>True if the item was successfully created, false otherwise.</returns>
        /// <remarks>Changes game state, should only be performed after all pertinent validations happen.</remarks>>
        bool PerformItemCreation(ushort typeId, ICylinder atCylinder, byte index = 0xFF, ICreature requestor = null);

        /// <summary>
        /// Immediately attempts to perform an item deletion in behalf of the requesting creature, if any.
        /// </summary>
        /// <param name="item">The item being deleted.</param>
        /// <param name="fromCylinder">The cylinder from which the deletion is happening.</param>
        /// <param name="index">Optional. The index within the cylinder from which to delete the item.</param>
        /// <param name="requestor">Optional. The creature requesting the deletion.</param>
        /// <returns>True if the item was successfully deleted, false otherwise.</returns>
        /// <remarks>Changes game state, should only be performed after all pertinent validations happen.</remarks>
        bool PerformItemDeletion(IItem item, ICylinder fromCylinder, byte index = 0xFF, ICreature requestor = null);

        /// <summary>
        /// Attempts to display an animated efect on the given location.
        /// </summary>
        /// <param name="location">The location at which to display the effect.</param>
        /// <param name="animatedEffect">The effect to display.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_AddAnimatedEffectAt(Location location, AnimatedEffect animatedEffect);

        /// <summary>
        /// Attempts to change a given item to the supplied id.
        /// </summary>
        /// <param name="thing">The thing to change.</param>
        /// <param name="toItemId">The id of the item type to change to.</param>
        /// <param name="animatedEffect">An optional effect to send as part of the change.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_ChangeItem(IThing thing, ushort toItemId, AnimatedEffect animatedEffect);

        /// <summary>
        /// Attempts to change a given item to the supplied id at a given location.
        /// </summary>
        /// <param name="location">The location at which the change will happen.</param>
        /// <param name="fromItemId">The id of the item from which the change is happening.</param>
        /// <param name="toItemId">The id of the item to which the change is happening.</param>
        /// <param name="animatedEffect">An optional effect to send as part of the change.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_ChangeItemAt(Location location, ushort fromItemId, ushort toItemId, AnimatedEffect animatedEffect);

        /// <summary>
        /// Attempts to create an item at a given location.
        /// </summary>
        /// <param name="location">The location at which to create the item.</param>
        /// <param name="itemType">The type of the item to create.</param>
        /// <param name="effect">An effect to use when the creation takes place.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_CreateItemAt(Location location, ushort itemType, byte effect);

        /// <summary>
        /// Attempts to delete an item.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_DeleteItem(IItem item);

        /// <summary>
        /// Attempts to delete an item at a given location.
        /// </summary>
        /// <param name="location">The location at which to delete the item.</param>
        /// <param name="itemType">The type of the item to delete.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_DeleteItemAt(Location location, ushort itemType);

        /// <summary>
        /// Attempts to get the description (attribute only) of the item.
        /// </summary>
        /// <param name="itemToDescribe">The item to get the description of.</param>
        /// <param name="player">The player who the description is for.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_DescriptionOf(IItem itemToDescribe, IPlayer player);

        /// <summary>
        /// Attempts to move a creature to a given location.
        /// </summary>
        /// <param name="creature">The creature to move.</param>
        /// <param name="targetLocation">The location to move the creature to.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_MoveCreature(ICreature creature, Location targetLocation);

        /// <summary>
        /// Attempts to move all items and creatures in a location to a given location.
        /// </summary>
        /// <param name="fromLocation">The location from which to move everything.</param>
        /// <param name="targetLocation">The location to move everything to.</param>
        /// <param name="exceptTypeIds">Optional. Any type ids to explicitly exclude.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_MoveEverythingTo(Location fromLocation, Location targetLocation, params ushort[] exceptTypeIds);

        /// <summary>
        /// Attempts to move an item to a given location.
        /// </summary>
        /// <param name="item">The item to move.</param>
        /// <param name="targetLocation">The location to move the item to.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_MoveItemTo(IItem item, Location targetLocation);

        /// <summary>
        /// Attempts to move an item of the given type from the given location to another location.
        /// </summary>
        /// <param name="itemType">The type of the item to move.</param>
        /// <param name="fromLocation">The location from which to move the item.</param>
        /// <param name="toLocation">The location to which to move the item.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_MoveItemOfTypeTo(ushort itemType, Location fromLocation, Location toLocation);

        /// <summary>
        /// Attempts to log out a player.
        /// </summary>
        /// <param name="player">The player to log out.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_Logout(IPlayer player);

        /// <summary>
        /// Attempts to place a new monster at the given location.
        /// </summary>
        /// <param name="location">The location at which to place the monster.</param>
        /// <param name="monsterType">The type of the monster to place.</param>
        /// <returns>True if the request was accepted, false otherwise.</returns>
        bool ScriptRequest_PlaceMonsterAt(Location location, ushort monsterType);

        /// <summary>
        /// Checks if a throw between two locations is valid.
        /// </summary>
        /// <param name="fromLocation">The first location.</param>
        /// <param name="toLocation">The second location.</param>
        /// <param name="checkLineOfSight">Optional. A value indicating whether to consider line of sight.</param>
        /// <returns>True if the throw is valid, false otherwise.</returns>
        bool CanThrowBetweenMapLocations(Location fromLocation, Location toLocation, bool checkLineOfSight = true);
    }
}