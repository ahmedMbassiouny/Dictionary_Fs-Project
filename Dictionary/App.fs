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

    
