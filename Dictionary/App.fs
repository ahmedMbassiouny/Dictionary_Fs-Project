
module App

open System.Windows.Forms
open Program
open UIHelpers

[<EntryPoint>]
let main argv =
    let form = new Form(Text = "F# Dictionary App", Width = 600, Height = 400)

    let txtWord = new TextBox(Top = 20, Left = 20, Width = 200, PlaceholderText = "Enter word")
    form.Controls.Add(txtWord)

    let txtDefinition = new TextBox(Top = 60, Left = 20, Width = 200, PlaceholderText = "Enter definition")
    form.Controls.Add(txtDefinition)

    let listBox = new ListBox(Top = 20, Left = 250, Width = 300, Height = 300)
    form.Controls.Add(listBox)

    // Add Button
    let btnAdd = new Button(Text = "Add", Top = 100, Left = 20, Width = 80)
    btnAdd.Click.Add(fun _ ->
        if txtWord.Text = "" || txtDefinition.Text = "" then
            showMessage "Please enter both a word and a definition." "Warning"
        else
            let message = addWord txtWord.Text txtDefinition.Text
            showMessage message "Info"
            updateListBox listBox
    )
    form.Controls.Add(btnAdd)

    // Update Button
    let btnUpdate = new Button(Text = "Update", Top = 140, Left = 20, Width = 80)
    btnUpdate.Click.Add(fun _ ->
        if txtWord.Text = "" || txtDefinition.Text = "" then
            showMessage "Please enter both a word and a new definition." "Warning"
        else
            let message = updateWord txtWord.Text txtDefinition.Text
            showMessage message "Info"
            updateListBox listBox
    )
    form.Controls.Add(btnUpdate)

    // Delete Button
    let btnDelete = new Button(Text = "Delete", Top = 180, Left = 20, Width = 80)
    btnDelete.Click.Add(fun _ ->
        if txtWord.Text = "" then
            showMessage "Please enter a word to delete." "Warning"
        else
            let message = deleteWord txtWord.Text
            showMessage message "Info"
            updateListBox listBox
    )
    form.Controls.Add(btnDelete)

    // Search Button
    let btnSearch = new Button(Text = "Search", Top = 220, Left = 20, Width = 80)
    btnSearch.Click.Add(fun _ ->
        if txtWord.Text = "" then
            showMessage "Please enter a word to search." "Warning"
        else
            let results = searchWord txtWord.Text
            listBox.Items.Clear()
            results |> Map.iter (fun word definition -> listBox.Items.Add($"{word}: {definition}") |> ignore)
            if results.IsEmpty then
                showMessage "No matching words found." "Info"
    )
    form.Controls.Add(btnSearch)

    // Save Button
    let btnSave = new Button(Text = "Save", Top = 260, Left = 20, Width = 80)
    btnSave.Click.Add(fun _ ->
        saveToFile "dictionary.json"
        showMessage "Dictionary saved!" "Info"
    )
    form.Controls.Add(btnSave)

    // Load Button
    let btnLoad = new Button(Text = "Load", Top = 300, Left = 20, Width = 80)
    btnLoad.Click.Add(fun _ ->
        loadFromFile "dictionary.json"
        updateListBox listBox
        showMessage "Dictionary loaded!" "Info"
    )
    form.Controls.Add(btnLoad)

    Application.Run(form)
    0
