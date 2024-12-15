module Program

open System.IO
open System.Text.Json

type Dictionary = Map<string, string>
let mutable dictionary: Dictionary = Map.empty

let saveToFile (filePath: string) =
    let jsonData = JsonSerializer.Serialize(dictionary)
    File.WriteAllText(filePath, jsonData)


let loadFromFile (filePath: string) =
    if File.Exists(filePath) then
        let jsonData = File.ReadAllText(filePath)
        dictionary <- JsonSerializer.Deserialize<Dictionary>(jsonData)
    else
        failwith "File not found!"
let deleteWord (word: string): string =
    if dictionary.ContainsKey(word.ToLower()) then
        dictionary <- dictionary.Remove(word.ToLower())
        "Word deleted successfully."
    else
        "Word not found."

let searchWord (query: string): Map<string, string> =
    dictionary |> Map.filter (fun key _ -> key.Contains(query.ToLower()))


let addWord (word: string) (definition: string): string =
    if dictionary.ContainsKey(word.ToLower()) then
        "Word already exists. Use the Update function."
    else
        dictionary <- dictionary.Add(word.ToLower(), definition)
        "Word added successfully."

        
let updateWord (word: string) (definition: string): string =
    if dictionary.ContainsKey(word.ToLower()) then
        dictionary <- dictionary.Add(word.ToLower(), definition)
        "Word updated successfully."
    else
        "Word not found. Use the Add function to create it."
