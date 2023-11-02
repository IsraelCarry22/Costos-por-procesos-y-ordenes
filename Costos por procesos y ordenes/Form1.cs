using Costos_por_procesos_y_ordenes.Clases;
using System;
using System.Windows.Forms;

namespace Costos_por_procesos_y_ordenes
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        public OdenProduccion ClassAuto;
        public string[] ReciboInformation;
        public decimal[,] DimArrDPPU, DimArrCCUP, DimArrVIF, DimArrVPT, DimArrCost;

        public Form1()
        {
            InitializeComponent();
            ClassAuto = new OdenProduccion();
            ReciboInformation = new string[14];
            DimArrDPPU = new decimal[3, 8];//Determinación de producción procesada por unidades
            DimArrCCUP = new decimal[4, 7];//Calculo de costos unitarios por promedio
            DimArrVIF = new decimal[4, 3]; //Valuación de inventario final
            DimArrVPT = new decimal[4, 3]; //Valuación de producción terminada
            DimArrCost = new decimal[5, 2];//Costos (tabla del recibo)
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            RowsPrintTable();
        }

        public void RowsPrintTable()
        {
            PrintTables(DimArrDPPU, DtgTabla1);
            DtgTabla1.Rows[0].Cells[0].Value = "Materiales: ";
            DtgTabla1.Rows[1].Cells[0].Value = "Mano de obra: ";
            DtgTabla1.Rows[2].Cells[0].Value = "G. Fabricacion: ";
        }

        public void RowsPrintTables(DataGridView Tabla)
        {
            Tabla.Rows[0].Cells[0].Value = "Materiales: ";
            Tabla.Rows[1].Cells[0].Value = "Mano de obra: ";
            Tabla.Rows[2].Cells[0].Value = "G. Fabricacion: ";
            Tabla.Rows[3].Cells[0].Value = "Suma de costos: ";
        }

        public void PrintTables(decimal[,] Data, DataGridView Table)
        {
            Table.Rows.Clear();
            for (int i = 0; i < Data.GetLength(0); i++)
            {
                Table.Rows.Add();
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    if (Table == DtgTablaCostos)
                    {
                        Table.Rows[i].Cells[j].Value = Data[i, j];
                    }
                    else
                    {
                        Table.Rows[i].Cells[j + 1].Value = Data[i, j];
                    }
                }
            }
            if (Table == DtgTabla2)
            {
                RowsPrintTables(Table);
                //Tabla1 G1
                Data[0, 3] = DimArrDPPU[0, 6];
                Data[1, 3] = DimArrDPPU[1, 6];
                Data[2, 3] = DimArrDPPU[2, 6];

                //Tabla1 H1
                Data[0, 4] = DimArrDPPU[0, 7];
                Data[1, 4] = DimArrDPPU[1, 7];
                Data[2, 4] = DimArrDPPU[2, 7];
            }
            else if (Table == DtgTabla3)
            {
                RowsPrintTables(Table);
                //Tabla1 D1
                Data[0, 0] = DimArrDPPU[0, 3];
                Data[1, 0] = DimArrDPPU[1, 3];
                Data[2, 0] = DimArrDPPU[2, 3];

                //Tabla1 G2
                Data[0, 1] = DimArrCCUP[0, 6];
                Data[1, 1] = DimArrCCUP[1, 6];
                Data[2, 1] = DimArrCCUP[2, 6];

                //A * B = C
                Data[0, 2] = DimArrVIF[0, 0] * DimArrVIF[0, 1];
                Data[1, 2] = DimArrVIF[1, 0] * DimArrVIF[1, 1];
                Data[2, 2] = DimArrVIF[1, 0] * DimArrVIF[2, 1];

                //Sumatoria de C
                Data[3, 2] = DimArrVIF[0, 2] + DimArrVIF[1, 2] + DimArrVIF[2, 2];
            }
            else if(Table == DtgTabla4)
            {
                RowsPrintTables(Table);
                //Tabla1 Datos de A1
                Data[0, 0] = DimArrDPPU[0, 0];
                Data[1, 0] = DimArrDPPU[1, 0];
                Data[2, 0] = DimArrDPPU[2, 0];

                //Tabla2 Datos de G1
                Data[0, 1] = DimArrCCUP[0, 6];
                Data[1, 1] = DimArrCCUP[1, 6];
                Data[2, 1] = DimArrCCUP[2, 6];

                //A * B = C
                Data[0, 2] = DimArrVPT[0, 0] * DimArrVPT[0, 1];
                Data[1, 2] = DimArrVPT[1, 0] * DimArrVPT[1, 1];
                Data[2, 2] = DimArrVPT[1, 0] * DimArrVPT[2, 1];

                //Sumatoria de C
                Data[3, 2] = DimArrVPT[0, 2] + DimArrVPT[1, 2] + DimArrVPT[2, 2];
            }
            else if (Table == DtgTablaCostos)
            {
                //Data5 Total
                Data[0, 0] = DimArrVPT[0, 2];
                Data[1, 0] = DimArrVPT[1, 2];
                Data[2, 0] = DimArrCost[0, 0] + DimArrCost[1, 0];
                Data[3, 0] = DimArrVPT[2, 2];
                Data[4, 0] = DimArrCost[2, 0] + DimArrCost[3, 0];

                //Data5 Unitario
                Data[0, 1] = DimArrVPT[0, 1];
                Data[1, 1] = DimArrVPT[1, 1];
                Data[2, 1] = DimArrCost[0, 1] + DimArrCost[1, 1];
                Data[3, 1] = DimArrVPT[2, 1];
                Data[4, 1] = DimArrCost[2, 1] + DimArrCost[3, 1];
            }
            //Label
            LblNumPedido.Text = DateTime.Now.ToShortDateString();

            //Texbox
            TxbCantidad.Text = DimArrDPPU[0, 0].ToString();
        }

        private void BtmCalcularTabla1_Click(object sender, EventArgs e)
        {
            //Calculo de celdas(D, G y F) Tabla 1
            //B * C = D
            DimArrDPPU[0, 3] = DimArrDPPU[0, 1] * DimArrDPPU[0, 2];
            DimArrDPPU[1, 3] = DimArrDPPU[1, 1] * DimArrDPPU[1, 2];
            DimArrDPPU[2, 3] = DimArrDPPU[2, 1] * DimArrDPPU[2, 2];

            //E * F = G
            DimArrDPPU[0, 6] = DimArrDPPU[0, 4] * DimArrDPPU[0, 5];
            DimArrDPPU[1, 6] = DimArrDPPU[1, 4] * DimArrDPPU[1, 5];
            DimArrDPPU[2, 6] = DimArrDPPU[2, 4] * DimArrDPPU[2, 5];

            //A + D - G = F
            DimArrDPPU[0, 7] = DimArrDPPU[0, 0] + DimArrDPPU[0, 3] - DimArrDPPU[0, 6];
            DimArrDPPU[1, 7] = DimArrDPPU[1, 0] + DimArrDPPU[1, 3] - DimArrDPPU[1, 6];
            DimArrDPPU[2, 7] = DimArrDPPU[2, 0] + DimArrDPPU[2, 3] - DimArrDPPU[2, 6];

            PrintTables(DimArrDPPU, DtgTabla1);
            PrintTables(DimArrCCUP, DtgTabla2);
            RowsPrintTable();

            MenuTabla2.Enabled = true;
        }

        private void BtmCalcularTabla2_Click(object sender, EventArgs e)
        {
            //Calculo de celdas(C, F y G) Tabla 2
            //A + B = C
            DimArrCCUP[0, 2] = DimArrCCUP[0, 0] + DimArrCCUP[0, 1];
            DimArrCCUP[1, 2] = DimArrCCUP[1, 0] + DimArrCCUP[1, 1];
            DimArrCCUP[2, 2] = DimArrCCUP[2, 0] + DimArrCCUP[2, 1];

            //Sumatoria de C
            DimArrCCUP[3, 2] = DimArrCCUP[0, 2] + DimArrCCUP[1, 2] + DimArrCCUP[2, 2];

            //D + E = F
            DimArrCCUP[0, 5] = DimArrCCUP[0, 3] + DimArrCCUP[0, 4];
            DimArrCCUP[1, 5] = DimArrCCUP[1, 3] + DimArrCCUP[1, 4];
            DimArrCCUP[2, 5] = DimArrCCUP[2, 3] + DimArrCCUP[2, 4];

            //C / F = G
            DimArrCCUP[0, 6] = DimArrCCUP[0, 2] / DimArrCCUP[0, 5];
            DimArrCCUP[1, 6] = DimArrCCUP[1, 2] / DimArrCCUP[1, 5];
            DimArrCCUP[2, 6] = DimArrCCUP[2, 2] / DimArrCCUP[2, 5];

            PrintTables(DimArrCCUP, DtgTabla2);
            PrintTables(DimArrVIF, DtgTabla3);
            PrintTables(DimArrVPT, DtgTabla4);
            PrintTables(DimArrCost, DtgTablaCostos);

            MenuTabla3y4.Enabled = true;
            MenuRecibo.Enabled = true;
        }

        private void Tabla1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (decimal.TryParse(DtgTabla1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out decimal NewValue))
                {
                    DimArrDPPU[e.RowIndex, e.ColumnIndex - 1] = NewValue;
                }
            }
        }

        private void Tabla2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (decimal.TryParse(DtgTabla2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out decimal NewValue))
                {
                    DimArrCCUP[e.RowIndex, e.ColumnIndex - 1] = NewValue;
                }
            }
        }

        private void Tabla1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlTabla2.Visible = false;
            PnlTabla3y4.Visible = false;
        }

        private void Tabla2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintTables(DimArrCCUP, DtgTabla2);
            PnlTabla2.Visible = true;
            PnlTabla3y4.Visible = false;
        }

        private void Tabla3y4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintTables(DimArrVIF, DtgTabla3);
            PrintTables(DimArrVPT, DtgTabla4);
            PnlTabla2.Visible = true;
            PnlTabla3y4.Visible = true;
            PnlRecibo.Visible = false;
        }

        private void MenuRecibo_Click(object sender, EventArgs e)
        {
            PrintTables(DimArrCost, DtgTablaCostos);
            PnlTabla2.Visible = true;
            PnlTabla3y4.Visible = true;
            PnlRecibo.Visible = true;
        }

        private void Btm_Nuevo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Se borraran todos los datos, guarde el recibo si no lo tiene guardado para empezar una nueva orden, ¿Desea continuar?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }
            //Limpiar Text Boxs
            TxbCliente.Clear();
            TxbProducto.Clear();
            TxbEspecificaciones.Clear();
            TxbCantidad.Clear();
            TxbUnidad.Clear();
            TxbExpedidaPor.Clear();
            TxbCalculadaPor.Clear();
            TxbContabilizadaPor.Clear();

            //Panels
            PnlTabla2.Visible = false;
            PnlTabla3y4.Visible = false;
            PnlRecibo.Visible = false;

            //Menu Buttoms
            MenuTabla2.Enabled = false;
            MenuTabla3y4.Enabled = false;
            MenuRecibo.Enabled = false;

            //Limpiar arreglos
            ClassAuto.ClearArrayDecimal(DimArrDPPU);
            ClassAuto.ClearArrayDecimal(DimArrCCUP);
            ClassAuto.ClearArrayDecimal(DimArrVIF);
            ClassAuto.ClearArrayDecimal(DimArrVPT);
            ClassAuto.ClearArrayDecimal(DimArrCost);
            ClassAuto.ClearArrayString(ReciboInformation);

            //Re imprimimos
            PrintTables(DimArrDPPU, DtgTabla1);
            PrintTables(DimArrCCUP, DtgTabla2);
            PrintTables(DimArrVIF, DtgTabla3);
            PrintTables(DimArrVPT, DtgTabla4);
            RowsPrintTable();
            MessageBox.Show("Datos Borrados por completo", "Operación", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void Btm_Guardar_Click(object sender, EventArgs e)
        {
            //Guardar informacion de TextBoxs
            ReciboInformation[0] = LblNumPedido.Text;
            ReciboInformation[1] = TxbCliente.Text;
            ReciboInformation[2] = TxbProducto.Text;
            ReciboInformation[3] = TxbEspecificaciones.Text;
            ReciboInformation[4] = TxbCantidad.Text;
            ReciboInformation[5] = TxbUnidad.Text;
            ReciboInformation[6] = DtpFechaPedido.Value.ToString();
            ReciboInformation[7] = DtpFechaExpedición.Value.ToString();
            ReciboInformation[8] = DtpFechaIniciación.Value.ToString();
            ReciboInformation[9] = DtpFechaEntrega.Value.ToString();
            ReciboInformation[10] = DtpFechaTerminación.Value.ToString();
            ReciboInformation[11] = TxbExpedidaPor.Text;
            ReciboInformation[12] = TxbCalculadaPor.Text;
            ReciboInformation[13] = TxbContabilizadaPor.Text;

            //Gurda y crea un recibo en txt
            ClassAuto.SaveRecibo(ReciboInformation, DimArrCost);
        }
    }
}