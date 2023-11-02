using System;
using System.IO;
using System.Windows.Forms;

namespace Costos_por_procesos_y_ordenes.Clases
{
    public class OdenProduccion
    {
        private StreamWriter TxtRecibo;
        public int i = 0;

        public void ClearArrayDecimal(decimal[,] Num)
        {
            for (int i = 0; i < Num.GetLength(0); i++)
            {
                for (int j = 0; j < Num.GetLength(1); j++)
                {
                    Num[i, j] = 0;
                }
            }
        }

        public void ClearArrayString(string[] Info)
        {
            for (int i = 0; i < Info.Length; i++)
            {
                Info[i] = "";
            }
        }

        public void SaveRecibo(string[] ReciboInfo, decimal[,] DimArrCostos)
        {
            FolderBrowserDialog OpenFolder = new FolderBrowserDialog();
            if (OpenFolder.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string DataTime = DateTime.Now.ToString("ddMMyyyy");
            string FolderDestiny = OpenFolder.SelectedPath;
            string PathFolderDestiny = Path.Combine(FolderDestiny, $"Recibo de pedido Folio{i + DataTime}.txt");
            using (TxtRecibo = new StreamWriter(PathFolderDestiny))
            {
                TxtRecibo.WriteLine($"\t\t\t\t\tALESCA, S.A.");
                TxtRecibo.WriteLine($"\t\t\t\tOrden de producción.\t\t\tNúmero: {ReciboInfo[0]}.\n");
                TxtRecibo.WriteLine($"Cliente: \t{ReciboInfo[1]}\t\t\tFecha de pedido: \t{ReciboInfo[6]}");
                TxtRecibo.WriteLine($"Producto: \t{ReciboInfo[2]}\t\t\t\tFecha de expedido: \t{ReciboInfo[7]}");
                TxtRecibo.WriteLine($"Especificaciones: \t{ReciboInfo[3]}\tFecha de iniciación: \t{ReciboInfo[8]}");
                TxtRecibo.WriteLine($"Cantidad: \t{ReciboInfo[4]}\t\t\t\t\tFecha deseada de entrega: \t{ReciboInfo[9]}");
                TxtRecibo.WriteLine($"Unidad: \t{ReciboInfo[5]}\t\t\t\t\tFecha de terminación: \t{ReciboInfo[10]}\n");
                TxtRecibo.WriteLine("\t\t\t\t\t\tCosto");
                TxtRecibo.WriteLine($"\tConcepto\t\t\tTotal\t\tUnitario");
                TxtRecibo.WriteLine($"Materia prima directa\t\t\t{string.Format("{0:C2}", DimArrCostos[0,0])}\t\t{string.Format("{0:C2}", DimArrCostos[0, 1])}");
                TxtRecibo.WriteLine($"Mano de obra directa\t\t\t{string.Format("{0:C2}", DimArrCostos[1, 0])}\t\t{string.Format("{0:C2}", DimArrCostos[1, 1])}");
                TxtRecibo.WriteLine($"Costos primos\t\t\t\t{string.Format("{0:C2}", DimArrCostos[2, 0])}\t\t{string.Format("{0:C2}", DimArrCostos[2, 1])}");
                TxtRecibo.WriteLine($"Cargos indirectos\t\t\t{string.Format("{0:C2}", DimArrCostos[3, 0])}\t\t{string.Format("{0:C2}", DimArrCostos[3, 1])}");
                TxtRecibo.WriteLine($"Costos de producción\t\t\t{string.Format("{0:C2}", DimArrCostos[4, 0])}\t\t{string.Format("{0:C2}", DimArrCostos[4, 1])}\n");
                TxtRecibo.WriteLine($"Expedido por: {ReciboInfo[11]}.\tCalculado por: {ReciboInfo[12]}.\tContabilizada por: {ReciboInfo[13]}.");
            }
            TxtRecibo.Close();
            MessageBox.Show("Archivo de texto creado y guardado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            i++;
        }
    }
}