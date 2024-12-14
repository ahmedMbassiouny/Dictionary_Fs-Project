module UIHelpers

open System.Windows.Forms
open Program

let updateListBox (listBox: ListBox) =
    listBox.Items.Clear()
    dictionary
    |> Map.iter (fun word definition ->
        listBox.Items.Add($"{word}: {definition}") |> ignore
    )
