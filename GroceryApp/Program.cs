using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Form menu = new Form { Width = 300, Height = 250, Text = "Main Menu" };

        Label welcomeLabel = new Label
        {
            Text = "Welcome, User!",
            Dock = DockStyle.Top,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            Height = 40
        };

        Button viewBtn = new Button { Text = "View List", Dock = DockStyle.Top, Height = 40 };
        Button addBtn = new Button { Text = "Add Items", Dock = DockStyle.Top, Height = 40 };
        Button exitBtn = new Button { Text = "Exit", Dock = DockStyle.Top, Height = 40 };

        viewBtn.Click += (s, e) => new DisplayForm().ShowDialog();
        addBtn.Click += (s, e) => new AddForm().ShowDialog();
        exitBtn.Click += (s, e) =>
        {
            var confirm = MessageBox.Show("Are you sure you want to exit?", "Exit App", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                Application.Exit();
            }
        };

        menu.Controls.Add(exitBtn);
        menu.Controls.Add(addBtn);
        menu.Controls.Add(viewBtn);
        menu.Controls.Add(welcomeLabel);

        Application.Run(menu);
    }
}
