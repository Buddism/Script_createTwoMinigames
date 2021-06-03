registerOutputEvent(GameConnection, "incGameScore",  "int -1000000 1000000 0" TAB "bool");
registerOutputEvent(GameConnection, "joinMinigame",  "string 50 200");
registerOutputEvent(GameConnection, "leaveMinigame", "");

//think slayer has one of these already
function findMinigameByTitle(%minigameName)
{
	if(!isObject(%SO = Slayer.Minigames))
		return 0;

	%count = %SO.getCount();
	for(%I = 0; %I < %count; %I++)
		if(strpos(%SO.getObject(%I).title, %minigameName) >= 0)
			return %SO.getObject(%I);
			
	return 0;
}
function GameConnection::joinMinigame(%client, %miniName)
{
	%mini = findMinigameByTitle(%miniName);
	if(%client.minigame == %mini || !isObject(%mini))
		return;

	if(isObject(%client.minigame))
		%client.minigame.removeMember(%client);

	%mini.addMember(%client);
}
function GameConnection::leaveMinigame(%client)
{
	if(!isObject(%client.minigame))
		return;

	%client.minigame.removeMember(%client);
}