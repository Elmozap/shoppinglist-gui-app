using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

public class DisplayForm : Form
{
    private ListBox listBox;
    private Button deleteButton;
    private Button addNewButton;

    public DisplayForm()
    {
        Text = "View Shopping List";
        Width = 350;
        Height = 400;

        Label title = new Label
        {
            Text = "Here's your list:",
            Dock = DockStyle.Top,
            Height = 30,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        listBox = new ListBox { Dock = DockStyle.Fill };

        deleteButton = new Button
        {
            Text = "Delete List",
            Dock = DockStyle.Bottom,
            Height = 40
        };
        deleteButton.Click += DeleteList;

        addNewButton = new Button
        {
            Text = "Add Another List",
            Dock = DockStyle.Bottom,
            Height = 40,
            Visible = false
        };
        addNewButton.Click += (s, e) => {
            this.Close();
            new AddForm().ShowDialog();
        };

        Controls.Add(addNewButton);
        Controls.Add(deleteButton);
        Controls.Add(listBox);
        Controls.Add(title);

        Load += (s, e) => LoadShoppingList();
    }

    private void LoadShoppingList()
    {
        listBox.Items.Clear();

        if (File.Exists("shoppinglist.json"))
        {
            var json = File.ReadAllText("shoppinglist.json");
            var items = JsonConvert.DeserializeObject<List<GroceryItem>>(json);
            foreach (var item in items)
                listBox.Items.Add(item.Name);
            addNewButton.Visible = false;
            deleteButton.Visible = true;
        }
        else
        {
            listBox.Items.Add("No shopping list found.");
            addNewButton.Visible = true;
            deleteButton.Visible = false;
        }
    }

    private void DeleteList(object sender, EventArgs e)
    {
        if (!File.Exists("shoppinglist.json"))
        {
            MessageBox.Show("No file to delete.");
            return;
        }

        var confirm = MessageBox.Show("Are you sure you want to delete the shopping list?", "Confirm Delete", MessageBoxButtons.YesNo);
        if (confirm == DialogResult.Yes)
        {
            File.Delete("shoppinglist.json");
            listBox.Items.Clear();
            listBox.Items.Add("Shopping list deleted.");
            deleteButton.Visible = false;
            addNewButton.Visible = true;
            MessageBox.Show("The shopping list has been deleted.");
        }
    }
}
