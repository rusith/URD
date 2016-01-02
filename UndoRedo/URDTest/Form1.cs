using System;
using System.Windows.Forms;
using URD.BasicOperations;
using URD.ListOperations;

namespace URDTest
{
    public partial class Test_form : Form
    {
        public Test_form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void appendButton_Click(object sender, EventArgs e)
        {
            using (new PropertyChange(display, "Text", "display label Text Change"))
            {
                display.Text = display.Text + textbox.Text;
                textbox.Text = " ";
            }
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            URD.URD.UndoLastChnage();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            URD.URD.RedoLastUndo();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var newitem = "Item " + DateTime.Now.Millisecond;
            using (new ListChangeAddElement(list.Items, newitem, "new item add to list"))
            {
                list.Items.Add(newitem);
            }
              
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            using (new ListChangeRemoveElement(list.Items, list.SelectedItem, list.Items.IndexOf(list.SelectedItem), "removed an item from the list"))
            {
                list.Items.Remove(list.SelectedItem);
            }
        }
    }
}
