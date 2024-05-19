using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V1._01.Lab_Management
{
    public partial class Lab_Inventory : Form
    {
        bool labCollapse;
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);

        public Lab_Inventory()
        {
            InitializeComponent();
            DisplayData();


        }


        private void Lab_Inventory_Load(object sender, EventArgs e)
        {

        }
        private Dictionary<string, int> inventoryThresholds = new Dictionary<string, int>();

        private void InitializeInventoryThresholds()
        {
            inventoryThresholds.Clear();

            inventoryThresholds.Add("syringes", 150);
            inventoryThresholds.Add("Anticeptic Wipes", 150);
            inventoryThresholds.Add("Plaster rolls", 30);
            inventoryThresholds.Add("Gloves", 150);
            inventoryThresholds.Add("BCT", 150);
            inventoryThresholds.Add("Cotton Balls", 30);
            inventoryThresholds.Add("Sterile Gauze", 30);
            inventoryThresholds.Add("Tourniquets", 5);
            inventoryThresholds.Add("Urine Cups", 150);
            inventoryThresholds.Add("VBCT", 40);
            inventoryThresholds.Add("Biohazard Bags", 30);
            inventoryThresholds.Add("Sharps Containers", 30);
            inventoryThresholds.Add("Hermostatic Agents", 30);

        }
        public void DisplayData()
        {

         
            string query = "SELECT [inventory], [quantity] FROM Inventory";

            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(query, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    int xColumn1 = 300;
                    int xColumn2 = 700;
                    int y = 100;
                    InitializeInventoryThresholds();

                    bool secondColumn = false;

                    while (reader.Read())
                    {
                        string inventory = reader["inventory"].ToString();
                        int quantity = Convert.ToInt32(reader["quantity"]);


                        System.Windows.Forms.Label label = new System.Windows.Forms.Label();
                        label.AutoSize = true;
                        label.Font = new Font(label.Font.FontFamily, 14);


                        int x = secondColumn ? xColumn2 : xColumn1; ;
                        if (y > 400 && !secondColumn)
                        {
                            secondColumn = true;
                            y = 100;
                            x = xColumn2;
                        }


                        label.Location = new Point(x, y);


                        label.Text = $"{inventory}: {quantity}";


                        if (inventoryThresholds.ContainsKey(inventory))
                        {
                            int threshold = inventoryThresholds[inventory];
                            if (quantity < threshold)
                            {
                                label.ForeColor = Color.Red;
                                MessageBox.Show($"The quantity of {inventory} is {quantity} refill the inventory");
                            }
                            else
                            {
                                label.ForeColor = Color.Green;
                            }
                        }
                        else
                        {

                            label.ForeColor = Color.Black;
                        }
                        Controls.Add(label);

                        Button incrementButton = new Button();
                        incrementButton.Text = "+";
                        incrementButton.Location = new Point(x + 200, y);
                        incrementButton.Tag = inventory;
                        incrementButton.Click += IncrementButton_Click;
                        Controls.Add(incrementButton);


                        Button decrementButton = new Button();
                        decrementButton.Text = "-";
                        decrementButton.Location = new Point(x + 250, y);
                        decrementButton.Tag = inventory;
                        decrementButton.Click += DecrementButton_Click;
                        Controls.Add(decrementButton);


                        y += 50;
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void IncrementButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string inventory = button.Tag.ToString();
            int change = 1;
            UpdateQuantity(inventory, change);
            MessageBox.Show($"Quantity of {inventory} incremented by {change}");

        }

        private void DecrementButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string inventory = button.Tag.ToString();
            int change = -1;
            UpdateQuantity(inventory, change);
            MessageBox.Show($"Quantity of {inventory} decremented by {Math.Abs(change)}");

        }

        private void UpdateQuantity(string inventory, int change)
        {
            string updateQuery = $"UPDATE Inventory SET [quantity] = [quantity] + {change} WHERE [inventory] = @Inventory";

            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(updateQuery, connect);
                    command.Parameters.AddWithValue("@Inventory", inventory);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        UpdateLabel(inventory, change);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating quantity: " + ex.Message);
            }
        }

        private void UpdateLabel(string inventory, int change)
        {

            foreach (Control control in Controls)
            {
                if (control is System.Windows.Forms.Label label && label.Text.StartsWith(inventory))
                {

                    string[] parts = label.Text.Split(':');
                    int currentQuantity = int.Parse(parts[1].Trim());


                    int newQuantity = currentQuantity + change;


                    label.Text = $"{inventory}: {newQuantity}";


                    if (inventoryThresholds.ContainsKey(inventory))
                    {
                        int threshold = inventoryThresholds[inventory];
                        if (newQuantity < threshold)
                        {
                            label.ForeColor = Color.Red;
                            MessageBox.Show($"The quantity of {inventory} is {newQuantity}. Refill the inventory.");
                        }
                        else
                        {
                            label.ForeColor = Color.Green;
                        }
                    }
                    else
                    {

                        label.ForeColor = Color.Black;
                    }

                    break;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form4 in Application.OpenForms)
            {
                if (Form4.Name == "Form4")
                {
                    Isopen = true;
                    Form4.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Update_Inventory form4 = new Lab_Update_Inventory(this);
                form4.Show();
            }
        }
        public void UpdateDisplayedData()
        {
            DisplayData();
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labtimer_Tick(object sender, EventArgs e)
        {

        }

        /*private void button19_Click(object sender, EventArgs e)
        {
            labtimer.Start();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form1 in Application.OpenForms)
            {
                if (Form1.Name == "Form1")
                {
                    Isopen = true;
                    Form1.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Dashboard form1 = new Lab_Dashboard();
                form1.Show();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_UpdateTestDetails in Application.OpenForms)
            {
                if (Lab_UpdateTestDetails.Name == "Lab_UpdateTestDetails")
                {
                    Isopen = true;
                    Lab_UpdateTestDetails.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_UpdateTestDetails Lab_UpdateTestDetails = new Lab_UpdateTestDetails();
                Lab_UpdateTestDetails.Show();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_EmployeeStatus in Application.OpenForms)
            {
                if (Lab_EmployeeStatus.Name == "Lab_EmployeeStatus")
                {
                    Isopen = true;
                    Lab_EmployeeStatus.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus Lab_EmployeeStatus = new Lab_EmployeeStatus();
                Lab_EmployeeStatus.Show();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_TestToDo in Application.OpenForms)
            {
                if (Lab_TestToDo.Name == "Lab_TestToDo")
                {
                    Isopen = true;
                    Lab_TestToDo.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_TestToDo Lab_TestToDo = new Lab_TestToDo();
                Lab_TestToDo.Show();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_Inventory in Application.OpenForms)
            {
                if (Lab_Inventory.Name == "Lab_Inventory")
                {
                    Isopen = true;
                    Lab_Inventory.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Inventory Lab_Inventory = new Lab_Inventory();
                Lab_Inventory.Show();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_CompleteTestDetails in Application.OpenForms)
            {
                if (Lab_CompleteTestDetails.Name == "Lab_CompleteTestDetails")
                {
                    Isopen = true;
                    Lab_CompleteTestDetails.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_CompleteTestDetails Lab_CompleteTestDetails = new Lab_CompleteTestDetails();
                Lab_CompleteTestDetails.Show();
            }
        }
        */
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form4 in Application.OpenForms)
            {
                if (Form4.Name == "Form4")
                {
                    Isopen = true;
                    Form4.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Update_Inventory form4 = new Lab_Update_Inventory(this);
                form4.Show();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            labtimer.Start();
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Form1 in Application.OpenForms)
            {
                if (Form1.Name == "Form1")
                {
                    Isopen = true;
                    Form1.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Dashboard form1 = new Lab_Dashboard();
                form1.Show();
            }
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_UpdateTestDetails in Application.OpenForms)
            {
                if (Lab_UpdateTestDetails.Name == "Lab_UpdateTestDetails")
                {
                    Isopen = true;
                    Lab_UpdateTestDetails.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_UpdateTestDetails Lab_UpdateTestDetails = new Lab_UpdateTestDetails();
                Lab_UpdateTestDetails.Show();
            }
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_EmployeeStatus in Application.OpenForms)
            {
                if (Lab_EmployeeStatus.Name == "Lab_EmployeeStatus")
                {
                    Isopen = true;
                    Lab_EmployeeStatus.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_EmployeeStatus Lab_EmployeeStatus = new Lab_EmployeeStatus();
                Lab_EmployeeStatus.Show();
            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_TestToDo in Application.OpenForms)
            {
                if (Lab_TestToDo.Name == "Lab_TestToDo")
                {
                    Isopen = true;
                    Lab_TestToDo.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_TestToDo Lab_TestToDo = new Lab_TestToDo();
                Lab_TestToDo.Show();
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_CompleteTestDetails in Application.OpenForms)
            {
                if (Lab_CompleteTestDetails.Name == "Lab_CompleteTestDetails")
                {
                    Isopen = true;
                    Lab_CompleteTestDetails.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_CompleteTestDetails Lab_CompleteTestDetails = new Lab_CompleteTestDetails();
                Lab_CompleteTestDetails.Show();
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form Lab_Inventory in Application.OpenForms)
            {
                if (Lab_Inventory.Name == "Lab_Inventory")
                {
                    Isopen = true;
                    Lab_Inventory.BringToFront();
                    break;
                }
            }
            if (Isopen == false)
            {
                Lab_Inventory Lab_Inventory = new Lab_Inventory();
                Lab_Inventory.Show();
            }
        }

        private void labtimer_Tick_1(object sender, EventArgs e)
        {
            if (labCollapse)
            {
                panel8.Height += 10;
                if (panel8.Height == panel8.MaximumSize.Height)
                {
                    labCollapse = false;
                    labtimer.Stop();
                }
            }
            else
            {
                panel8.Height -= 10;
                if (panel8.Height == panel8.MinimumSize.Height)
                {
                    labCollapse = true;
                    labtimer.Stop();
                }
            }
        }
    }
}
