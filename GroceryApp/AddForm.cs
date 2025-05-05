using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

public class AddForm : Form
{
    private TextBox bulkInputBox = new TextBox();
    private Button saveButton = new Button();
    private Button backButton = new Button();

    public AddForm()
    {
        Text = "Add Groceries";
        Width = 400;
        Height = 320;

        Label instructions = new Label
        {
            Text = "Enter up to 5 items (one per line):",
            Dock = DockStyle.Top,
            Height = 30
        };

        bulkInputBox.Multiline = true;
        bulkInputBox.Dock = DockStyle.Top;
        bulkInputBox.Height = 120;
        bulkInputBox.ScrollBars = ScrollBars.None;

        saveButton.Text = "Save to JSON";
        saveButton.Dock = DockStyle.Top;
        saveButton.Height = 40;
        saveButton.Click += SaveJson;

        backButton.Text = "Back";
        backButton.Dock = DockStyle.Top;
        backButton.Height = 40;
        backButton.Click += (s, e) => this.Close();

        Controls.Add(saveButton);
        Controls.Add(backButton);
        Controls.Add(bulkInputBox);
        Controls.Add(instructions);
    }

    private void SaveJson(object sender, EventArgs e)
    {
        string[] lines = bulkInputBox.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length > 5)
        {
            MessageBox.Show("You can only add up to 5 items.");
            return;
        }

        var items = lines.Select(line => new GroceryItem { Name = line.Trim() }).ToList();

        var json = JsonConvert.SerializeObject(items, Formatting.Indented);
        File.WriteAllText("shoppinglist.json", json);

        MessageBox.Show("Saved to shoppinglist.json!");
        this.Close();
    }
}
