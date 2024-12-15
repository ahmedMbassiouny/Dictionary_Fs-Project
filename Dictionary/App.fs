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

    let btnAdd = new Button(Text = "Add", Top = 100, Left = 20, Width = 80)
    btnAdd.Click.Add(fun _ ->
        if txtWord.Text <> "" && txtDefinition.Text <> "" then
            let word = txtWord.Text.ToLower()
            if dictionary.ContainsKey(word) then
                MessageBox.Show("This word already exists! Use Update instead.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
            else
                dictionary <- dictionary.Add(word, txtDefinition.Text)
                updateListBox listBox
                txtWord.Clear()
                txtDefinition.Clear()
        else
            MessageBox.Show("Please fill in both fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
    )
    form.Controls.Add(btnAdd)

    
    let btnUpdate = new Button(Text = "Update", Top = 140, Left = 20, Width = 80)
    btnUpdate.Click.Add(fun _ ->
        if txtWord.Text = "" || txtDefinition.Text = "" then
            MessageBox.Show("Please enter both a word and a new definition.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
        else
            let message = updateWord txtWord.Text txtDefinition.Text
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
            updateListBox listBox
    )
    form.Controls.Add(btnUpdate)

    
    let btnDelete = new Button(Text = "Delete", Top = 180, Left = 20, Width = 80)
    btnDelete.Click.Add(fun _ ->
        if txtWord.Text = "" then
            MessageBox.Show("Please enter a word to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
        else
            let message = deleteWord txtWord.Text
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
            updateListBox listBox
    )
    form.Controls.Add(btnDelete)


    let btnSearch = new Button(Text = "Search", Top = 220, Left = 20, Width = 80)
    btnSearch.Click.Add(fun _ ->
        if txtWord.Text = "" then
            MessageBox.Show("Please enter a word to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
        else
            let results = searchWord txtWord.Text
            listBox.Items.Clear()
            results |> Map.iter (fun word definition -> listBox.Items.Add($"{word}: {definition}") |> ignore)
            if results.IsEmpty then
                MessageBox.Show("No matching words found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
    )
    form.Controls.Add(btnSearch)
    

    let btnSave = new Button(Text = "Save", Top = 220, Left = 20, Width = 80)
    btnSave.Click.Add(fun _ ->
        saveToFile "dictionary.json"
        MessageBox.Show("Dictionary saved!") |> ignore
    )
    form.Controls.Add(btnSave)
    

    let btnLoad = new Button(Text = "Load", Top = 260, Left = 20, Width = 80)
    btnLoad.Click.Add(fun _ ->
        loadFromFile "dictionary.json"
        updateListBox listBox
        MessageBox.Show("Dictionary loaded!") |> ignore
    )
    form.Controls.Add(btnLoad)

    
