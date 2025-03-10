﻿SemaphoreSlim semaphoreRequest = new(6);
SemaphoreSlim semaphoreFile = new(1);

Directory.CreateDirectory("Schledues");

await Task.WhenAll(Numbers.VALUES.Select((_, i) => 
    Task.Run(() => Handler.ProcessTimetableAsync(i, semaphoreRequest, semaphoreFile))));
