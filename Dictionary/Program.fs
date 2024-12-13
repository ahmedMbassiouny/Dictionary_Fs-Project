module Program

open System.IO
open System.Text.Json

type Dictionary = Map<string, string>
let mutable dictionary: Dictionary = Map.empty

let saveToFile (filePath: string) =
    let jsonData = JsonSerializer.Serialize(dictionary)
    File.WriteAllText(filePath, jsonData)
