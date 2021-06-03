exec("./minigames.cs");
exec("./commands.cs");
exec("./events.cs");

if(!$startedMultipleMinigames)
    schedule(10000, 0, startMultipleMinigames);