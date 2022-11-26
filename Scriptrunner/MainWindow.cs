using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Scriptrunner
{
    public partial class MainWindow : Form
    {
        private const string RegexPattern = @"`([A-Za-z0-9\s]+)\|([A-Za-z0-9\s]+)´";
        private readonly IMessageShower _messageShower = new MessageShower();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            txtInput.Focus();

            var s = new StringBuilder();
            s.AppendLine("# This script will write your name (`Your name|string´) `Repeat times|int´ times.");
            s.AppendLine();
            s.AppendLine("for ($i = 0; $i < `Repeat times|int´; $i++) {");
            s.AppendLine(@"    Write-Output ""Hello, my name is `Your name|string´.""");
            s.AppendLine("}");

            txtInput.Text = s.ToString();

            grid.RowTemplate.Cells.Add(new DataGridViewButtonCell());
            grid.RowTemplate.Cells[0].Style.BackColor = BackColor;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    btnOpen.Enabled = true;
                    btnCopy.Enabled = false;
                    grid.EndEdit();
                    txtInput.Focus();
                    break;
                case 1:
                    btnOpen.Enabled = false;
                    btnCopy.Enabled = false;
                    grid.EndEdit();
                    grid.Focus();
                    SetUpInputFields();
                    break;
                case 2:
                    btnOpen.Enabled = false;
                    btnCopy.Enabled = true;
                    grid.EndEdit();
                    txtOutput.Focus();
                    ShowResult();
                    break;
            }
        }

        private void SetUpInputFields()
        {
            var existingInputFieldsInGrid = GetExistingInputFieldsInGrid();
            var fieldsInInputTemplate = GetFieldsInInputTemplate();

            foreach (var f in fieldsInInputTemplate)
                f.Value = existingInputFieldsInGrid.GetValue(f.Name);

            foreach (var f in existingInputFieldsInGrid)
                f.Value = fieldsInInputTemplate.GetDatatype(f.Name);

            grid.Rows.Clear();

            foreach (var f in fieldsInInputTemplate)
            {
                var r = grid.Rows.Add();
                grid.Rows[r].Cells[0].Value = f.Name;
                grid.Rows[r].Cells[1].Value = f.Value;
            }
        }

        private void ShowResult()
        {
            var existingInputFieldsInGrid = GetExistingInputFieldsInGrid();
            var fieldsInInputTemplate = GetFieldsInInputTemplate();

            foreach (var f in fieldsInInputTemplate)
                f.Value = existingInputFieldsInGrid.GetValue(f.Name);

            var s = txtInput.Text;

            foreach (var f in fieldsInInputTemplate)
                s = s.Replace($@"`{f.Name}|{f.Datatype}´", f.Value);

            txtOutput.Text = s;
        }

        private ParameterList GetExistingInputFieldsInGrid()
        {
            var result = new ParameterList();

            foreach (DataGridViewRow r in grid.Rows)
            {
                var name = r.Cells[0].Value as string ?? "";
                var value = r.Cells[1].Value as string ?? "";

                result.AddIfNotExits(new Parameter(name, "") { Value = value });
            }

            return result;
        }

        private ParameterList GetFieldsInInputTemplate()
        {
            var result = new ParameterList();
            var matches = Regex.Matches(txtInput.Text, RegexPattern);

            foreach (Match match in matches)
            {
                var name = match.Groups[1].Value;
                var type = match.Groups[2].Value;

                result.AddIfNotExits(new Parameter(name, type));
            }

            return result;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !_messageShower.Ask(this, @"Exit Scriptrunner?", Text))
                e.Cancel = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtOutput.Text);
            _messageShower.Tell(this, @"Output is copied to clipboard.", Text);
        }
    }
}