exec("./minigames.cs");
exec("./commands.cs");

if(!$startedMultipleMinigames)
    schedule(10000, 0, startMultipleMinigames);