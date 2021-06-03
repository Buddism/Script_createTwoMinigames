function startMultipleMinigames()
{
    $minigame1 = Slayer_MiniGameHandlerSG.addMinigame(0, $Slayer::Server::ConfigDir @ "/config_saved/config_minigame1");
    $minigame2 = Slayer_MiniGameHandlerSG.addMinigame(0, $Slayer::Server::ConfigDir @ "/config_saved/config_minigame2");

    Slayer.minigames.defaultMinigame = $minigame2;

    $minigame2.isDefaultMinigame = 1;
    $minigame1.isDefaultMinigame = 1;

    announce("Set up minigames");
    echo("Set up minigames");

    $startedMultipleMinigames = 1;
}

function servercmdSaveMinigame(%client)
{
    servercmdSaveMinigames(%client);
}

function servercmdSaveMinigames(%client)
{
    if(%client.getBLID() != getNumKeyID()) //replace getNumKeyID() with your blid if it is causing troubles
    {
        %client.chatMessage("\c6You are not allowed to use this!");
        return;
    }

    %path = $Slayer::Server::ConfigDir @ "/config_saved/config_minigame1"; //$minigame1.title originally

	Slayer.Prefs.exportMiniGamePreferences(%path @ ".mgame.csv", $minigame1);
	Slayer.TeamPrefs.exportTeamPreferences(%path @ ".teams.csv", $minigame1);

    %path = $Slayer::Server::ConfigDir @ "/config_saved/config_minigame2"; //$minigame2.title originally

	Slayer.Prefs.exportMiniGamePreferences(%path @ ".mgame.csv", $minigame2);
	Slayer.TeamPrefs.exportTeamPreferences(%path @ ".teams.csv", $minigame2);

    announce("\c6Saved minigame preferences");
}