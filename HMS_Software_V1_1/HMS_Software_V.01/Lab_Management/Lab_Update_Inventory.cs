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
    public partial class Lab_Update_Inventory : Form
    {
        private Lab_Inventory Lab_InventoryInstance;
        bool labCollapse;
        SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString);
        public Lab_Update_Inventory(Lab_Inventory Lab_Inventory)
        {
            InitializeComponent();
            Lab_InventoryInstance = Lab_Inventory;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void OKButton_Click(object sender, EventArgs e)
        {

        }
        private int? ParseNullableInt(string value)
        {
            int parsedValue;
            if (int.TryParse(value, out parsedValue))
            {
                return parsedValue;
            }
            else if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {

                return null;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, int?> inventoryUpdates = new Dictionary<string, int?>();


            inventoryUpdates["syringes"] = ParseNullableInt(textBox1.Text);
            inventoryUpdates["Anticeptic Wipes"] = ParseNullableInt(textBox2.Text);
            inventoryUpdates["Plaster rolls"] = ParseNullableInt(textBox3.Text);
            inventoryUpdates["Gloves"] = ParseNullableInt(textBox4.Text);
            inventoryUpdates["BCT"] = ParseNullableInt(textBox5.Text);
            inventoryUpdates["Cotton Balls"] = ParseNullableInt(textBox6.Text);
            inventoryUpdates["Sterile Gauze"] = ParseNullableInt(textBox7.Text);
            inventoryUpdates["Tourniquets"] = ParseNullableInt(textBox8.Text);
            inventoryUpdates["Urine Cups"] = ParseNullableInt(textBox9.Text);
            inventoryUpdates["VBCT"] = ParseNullableInt(textBox10.Text);
            inventoryUpdates["Biohazard Bags"] = ParseNullableInt(textBox11.Text);
            inventoryUpdates["Sharps Containers"] = ParseNullableInt(textBox12.Text);
            inventoryUpdates["Hermostatic Agents"] = ParseNullableInt(textBox13.Text);



            foreach (var entry in inventoryUpdates)
            {

                if (entry.Value.HasValue)
                {
                    UpdateQuantity(entry.Key, entry.Value.Value);
                }
                else
                {

                }
            }

            MessageBox.Show("Quantities updated successfully.");
            await Task.Delay(500);
            Lab_InventoryInstance.UpdateDisplayedData();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void UpdateQuantity(string inventory, int quantityToAdd)
        {
            string updateQuery = $"UPDATE Inventory SET [quantity] = [quantity] + @QuantityToAdd WHERE [inventory] = @Inventory";

            try
            {
                using (SqlConnection connect = new SqlConnection(MyCommonConnecString.ConnectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(updateQuery, connect);
                    command.Parameters.AddWithValue("@Inventory", inventory);
                    command.Parameters.AddWithValue("@QuantityToAdd", quantityToAdd);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating quantity: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";


        }

        private void labtimer_Tick(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
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

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
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
