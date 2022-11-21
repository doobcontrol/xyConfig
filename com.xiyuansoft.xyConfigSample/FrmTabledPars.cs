using com.xiyuansoft.xyConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.xiyuansoft.xyConfigSample
{
    public partial class FrmTabledPars : Form
    {
        string parTableName = "pipeType";
        Dictionary<string, TextBox> pipeParsEditorMaps = new Dictionary<string, TextBox>();
        Dictionary<string, Dictionary<string, string>> TabledPars;

        DataGridViewRow selectedDgvrRow;
        Dictionary<string, string> selectedDicRow;

        public FrmTabledPars()
        {
            InitializeComponent(); 
            formateDatagridviewToReadonlySignleselect(dataGridView1);
            initParsMaps();
            intiColumns();
            loadPars();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        //新增
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            clearAllEditor();
            txtType.Enabled = true;
            txtType.Focus();
        }

        //删除
        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedDgvrRow != null)
            {
                xConfig.delTabledParsRow(parTableName, selectedDicRow["Type"]);
                TabledPars.Remove(selectedDicRow["Type"]);
                dataGridView1.Rows.Remove(selectedDgvrRow);
                //selectedDgvrRow = null;
                //selectedDicRow = null;

            }
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            //新增行
            //newParRow();
        }

        private void txtType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                //新增行
                newParRow();
            }
        }

        private void txtType_Leave(object sender, EventArgs e)
        {
            //新增行
            newParRow();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                selectedDgvrRow = null;
                selectedDicRow = null;
                clearAllEditor();
                return;
            }

            selectedDgvrRow = dataGridView1.SelectedRows[0];
            if (selectedDgvrRow.Tag == null)
            {
                selectedDgvrRow = null;
                selectedDicRow = null;
                clearAllEditor();
                return;
            }

            selectedDicRow = selectedDgvrRow.Tag as Dictionary<string, string>; 
            foreach (string key in pipeParsEditorMaps.Keys)
            {
                pipeParsEditorMaps[key].Text = selectedDicRow[key];
            }
        }
        
        #region init data and dataGridView1 and so on

        private void initParsMaps()
        {
            //Type D3  D5 D6  De P Plug
            pipeParsEditorMaps.Add("Type", txtType);
            pipeParsEditorMaps.Add("D3", txtD3);
            pipeParsEditorMaps.Add("D3MPEma", txtD3MPEma);
            pipeParsEditorMaps.Add("D3MPEmi", txtD3MPEmi);
            pipeParsEditorMaps.Add("D5", txtD5);
            pipeParsEditorMaps.Add("D5MPEma", txtD5MPEma);
            pipeParsEditorMaps.Add("D5MPEmi", txtD5MPEmi);
            pipeParsEditorMaps.Add("D6", txtD6);
            pipeParsEditorMaps.Add("D6MPEma", txtD6MPEma);
            pipeParsEditorMaps.Add("D6MPEmi", txtD6MPEmi);
            pipeParsEditorMaps.Add("De", txtDe);
            pipeParsEditorMaps.Add("DeMPEma", txtDeMPEma);
            pipeParsEditorMaps.Add("DeMPEmi", txtDeMPEmi);
            pipeParsEditorMaps.Add("P", txtP);
            pipeParsEditorMaps.Add("Plug", txtPlug); 

            foreach (string key in pipeParsEditorMaps.Keys)
            {
                TextBox tb = pipeParsEditorMaps[key];
                tb.Tag = key;
                if (tb != txtType){
                    tb.TextChanged += txtPar_TextChanged;
                }
            }
            txtType.Enabled = false;
        }
        private void intiColumns()
        {

            DataGridViewColumn dgvc;

            foreach(string pName in pipeParsEditorMaps.Keys)
            {
                dataGridView1.Columns.Add(
                pName,
                pName
                ); 
            }
        }
        private void loadPars()
        {
            TabledPars = xConfig.getTabledPars(parTableName);

            showPars();
        }
        private void showPars()
        {
            if (TabledPars != null)
            {
                dataGridView1.Rows.Clear();
                DataGridViewRow dgvr;
                foreach (Dictionary<string,string> rDic in TabledPars.Values)
                {
                    dgvr = dataGridView1.Rows[dataGridView1.Rows.Add()];
                    dgvr.Tag = rDic;
                    foreach (string key in pipeParsEditorMaps.Keys)
                    {
                        dgvr.Cells[key].Value = rDic[key];
                    }
                }
            }
        }
        private void clearAllEditor()
        {
            foreach(TextBox tb in pipeParsEditorMaps.Values)
            {
                tb.Clear();
            }
        }
        private void txtPar_TextChanged(object sender, EventArgs e)
        {
            if (selectedDgvrRow != null)
            {
                TextBox tb = sender as TextBox;
                string newValue = tb.Text.Trim();
                xConfig.editTabledPar(
                    parTableName, 
                    selectedDicRow["Type"], 
                    tb.Tag.ToString(), 
                    newValue);
                selectedDicRow[tb.Tag.ToString()] = newValue;
                selectedDgvrRow.Cells[tb.Tag.ToString()].Value= newValue;
            }
        }
        private void newParRow()
        {
            if (txtType.Enabled && txtType.Text.Trim()!="")
            {
                if (TabledPars == null)
                {
                    TabledPars = new Dictionary<string, Dictionary<string, string>>();
                }
                if (TabledPars.ContainsKey(pipeParsEditorMaps["Type"].Text.Trim()))
                {
                    pipeParsEditorMaps["Type"].Clear();
                    MessageBox.Show("该 type 已经存在");
                    return;
                }
                Dictionary<string, string> newParsRow = new Dictionary<string, string>();
                foreach(string key in pipeParsEditorMaps.Keys)
                {
                    string value = "";
                    if (key == "Type")
                    {
                        value = pipeParsEditorMaps[key].Text.Trim();
                    }
                    newParsRow.Add(key, value);
                }
                xConfig.newTabledParsRow(parTableName, txtType.Text.Trim(), newParsRow);
                TabledPars.Add(txtType.Text.Trim(), newParsRow);

                DataGridViewRow dgvr = dataGridView1.Rows[dataGridView1.Rows.Add()];
                dgvr.Tag = newParsRow;
                foreach (string key in pipeParsEditorMaps.Keys)
                {
                    dgvr.Cells[key].Value = newParsRow[key];
                }
                dataGridView1.ClearSelection();
                dgvr.Selected = true;
            }
            

            txtType.Enabled = false;

        }
        private void formateDatagridviewToReadonlySignleselect(System.Windows.Forms.DataGridView objDataGridView)
        {
            objDataGridView.AllowUserToAddRows = false;
            objDataGridView.AllowUserToDeleteRows = false;
            objDataGridView.AllowUserToResizeColumns = false;
            //objDataGridView.AllowUserToResizeRows = false;
            objDataGridView.ReadOnly = true;
            objDataGridView.RowHeadersVisible = false;
            objDataGridView.ColumnHeadersVisible = true;
            objDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            objDataGridView.MultiSelect = false;
        }

        #endregion
    }
}
