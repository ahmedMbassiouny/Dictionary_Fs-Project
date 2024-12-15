
module App

open System.Drawing
open System.Windows.Forms
open Program
open UIHelpers

[<EntryPoint>]
let main argv =
    // Main Form with Dark Theme
    let form = new Form(Text = "F# Dictionary App", Width = 700, Height = 500, BackColor = Color.FromArgb(45, 45, 48), FormBorderStyle = FormBorderStyle.FixedSingle, MaximizeBox = false)

    // Title Label
    let lblTitle = new Label(Text = "Dictionary App", AutoSize = true, Font = new Font("Arial", 16.0F, FontStyle.Bold), Top = 10, Left = 20, ForeColor = Color.FromArgb(72, 133, 237))
    form.Controls.Add(lblTitle)

    // Word TextBox with Padding
    let pnlWord = new Panel(Width = 250, Height = 30, BackColor = Color.FromArgb(30, 30, 30), Top = 50, Left = 20, Padding = Padding(5))
    let txtWord = new TextBox(PlaceholderText = "Enter word", Font = new Font("Arial", 10.0F), BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White, BorderStyle = BorderStyle.None, Dock = DockStyle.Fill)
    pnlWord.Controls.Add(txtWord)
    form.Controls.Add(pnlWord)

    // Definition TextBox with Padding
    let pnlDefinition = new Panel(Width = 250, Height = 30, BackColor = Color.FromArgb(30, 30, 30), Top = 90, Left = 20, Padding = Padding(5))
    let txtDefinition = new TextBox(PlaceholderText = "Enter definition", Font = new Font("Arial", 10.0F), BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White, BorderStyle = BorderStyle.None, Dock = DockStyle.Fill)
    pnlDefinition.Controls.Add(txtDefinition)
    form.Controls.Add(pnlDefinition)

    
    // Title Label
    let listTitle = new Label(Text = "Dictionary List", AutoSize = true, Font = new Font("Arial", 10.0F, FontStyle.Bold), Top = 30, Left = 300, ForeColor = Color.White)
    form.Controls.Add(listTitle)


    // ListBox for Display (Margin adjusted)
    let listBox = new ListBox(Top = 50, Left = 300, Width = 350, Height = 350, Font = new Font("Arial", 10.0F), BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White, BorderStyle = BorderStyle.None)
    listBox.Margin <- Padding(10)
    form.Controls.Add(listBox)

    // Add Button
    let btnAdd = new Button(Text = "Add", Top = 140, Left = 20, Width = 100, Height = 30, Font = new Font("Arial", 10.0F), BackColor = Color.FromArgb(72, 133, 237), ForeColor = Color.White, FlatStyle = FlatStyle.Flat)
    btnAdd.FlatAppearance.BorderSize <- 0
    btnAdd.Padding <- Padding(5)
    btnAdd.Margin <- Padding(10)
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
    let btnUpdate = new Button(Text = "Update", Top = 180, Left = 20, Width = 100, Height = 30, Font = new Font("Arial", 10.0F), BackColor = Color.FromArgb(72, 133, 237), ForeColor = Color.White, FlatStyle = FlatStyle.Flat)
    btnUpdate.FlatAppearance.BorderSize <- 0
    btnUpdate.Padding <- Padding(5)
    btnUpdate.Margin <- Padding(10)
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
    let btnDelete = new Button(Text = "Delete", Top = 220, Left = 20, Width = 100, Height = 30, Font = new Font("Arial", 10.0F), BackColor = Color.FromArgb(72, 133, 237), ForeColor = Color.White, FlatStyle = FlatStyle.Flat)
    btnDelete.FlatAppearance.BorderSize <- 0
    btnDelete.Padding <- Padding(5)
    btnDelete.Margin <- Padding(10)
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
    let btnSearch = new Button(Text = "Search", Top = 260, Left = 20, Width = 100, Height = 30, Font = new Font("Arial", 10.0F), BackColor = Color.FromArgb(72, 133, 237), ForeColor = Color.White, FlatStyle = FlatStyle.Flat)
    btnSearch.FlatAppearance.BorderSize <- 0
    btnSearch.Padding <- Padding(5)
    btnSearch.Margin <- Padding(10)
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
    let btnSave = new Button(Text = "Save", Top = 300, Left = 20, Width = 100, Height = 30, Font = new Font("Arial", 10.0F), BackColor = Color.FromArgb(72, 133, 237), ForeColor = Color.White, FlatStyle = FlatStyle.Flat)
    btnSave.FlatAppearance.BorderSize <- 0
    btnSave.Padding <- Padding(5)
    btnSave.Margin <- Padding(10)
    btnSave.Click.Add(fun _ ->
        saveToFile "dictionary.json"
        showMessage "Dictionary saved!" "Info"
    )
    form.Controls.Add(btnSave)

    // Load Button
    let btnLoad = new Button(Text = "Load", Top = 340, Left = 20, Width = 100, Height = 30, Font = new Font("Arial", 10.0F), BackColor = Color.FromArgb(72, 133, 237), ForeColor = Color.White, FlatStyle = FlatStyle.Flat)
    btnLoad.FlatAppearance.BorderSize <- 0
    btnLoad.Padding <- Padding(5)
    btnLoad.Margin <- Padding(10)
    btnLoad.Click.Add(fun _ ->
        loadFromFile "dictionary.json"
        updateListBox listBox
        showMessage "Dictionary loaded!" "Info"
    )
    form.Controls.Add(btnLoad)

    // Run Application
    Application.Run(form)
    0
