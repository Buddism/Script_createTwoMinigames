function GameConnection::getAdminLevel(%client)
{
    if(%client.isSuperAdmin)
        return 3;
    if(%client.isAdmin)
        return 2;
    if(%client.isModerator)
        return 1;
    return 0;
}

function servercmdlobbykick(%client, %a0, %a1, %a2, %a3, %a4)
{
    if(%client.getAdminLevel() < 1) //not a moderator
        return;

    %str = trim(%a0 SPC %a1 SPC %a2 SPC %a3 SPC %a4);

    if(!isObject(%victim = findclientbyBL_ID(%str))) //blid check first
	{
        if(!isObject(%victim = findclientbyname(%str)))
        {
            %client.chatMessage("\c6Could not find \""@ %str @"\" by Name or BLID");
            return;
        }
	}

    if(%victim.minigame == $minigame2)
    {
        %client.chatMessage("\c6"@ %victim.getPlayerName() @" is already in the lobby.");
        return;
    }

    announce("\c3"@ %client.getPlayerName() @"\c2 kicked \c3"@ %victim.getPlayerName() @"\c2 from the Deathrun minigame.");
    //messageAdmins(2,"[/LobbyKick]"SPC %client.getPlayerName() SPC"lobby kicked"SPC %victim.getPlayerName());

    if(!isObject(%victim.minigame))
        $minigame2.addMember(%victim);
    else {
        %victim.leaveMinigame();
        $minigame2.addMember(%victim);

        //%victim.schedule(500, setSpectatorFix, %victim.player);
    }
}

function servercmdLobby(%client)
{
    if(!isObject(%client.minigame))
	{
        $minigame2.addMember(%client);
		return;
	}
    if(%client.minigame == $minigame2)
	{
        %client.chatMessage("\c6You're already in the lobby.");
		return;
	}
	if(isObject(%client.player))
	{
		%client.chatMessage("\c6You cant use this command while alive.");
		return;
	}

	%client.leaveMinigame();

	%msgB = '\c3%1 \c5has joined <color:800080>Lobby'; //lobby msg requires single quotes (%1 is player name)

	$minigame1.messageAllExcept(%client, '', %msgB, %client.getPlayerName());
	$minigame2.addMember(%client);

	//what is this
	//%victim.schedule(500, setSpectatorFix, %victim.player);
}