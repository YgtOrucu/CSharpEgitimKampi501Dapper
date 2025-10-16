using CSharpEgitimKampi501Dapper.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi501Dapper
{
    public partial class Form1 : Form
    {
        SqlConnect Connect = new SqlConnect();
        public Form1()
        {
            InitializeComponent();
        }

        private async void btn_Listele_Click(object sender, EventArgs e)
        {
            string query = "Select * From TBL_Product";
            var values = await Connect.Baglantı().QueryAsync<ResultProductDto>(query);
            dataGridView1.DataSource = values;
        }

        private async void btn_Ekle_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO TBL_Product (ProductName,ProductStock,ProductPrice,ProductCategory) VALUES (@ProductName,@ProductStock,@ProductPrice,@ProductCategory)";
            var parameters = new DynamicParameters();

            parameters.Add("@ProductName", txtName.Text);
            parameters.Add("@ProductStock", numStock.Value);
            parameters.Add("@ProductPrice", txtPrice.Text);
            parameters.Add("@ProductCategory", txtCategory.Text);
            await Connect.Baglantı().ExecuteAsync(query, parameters);
            MessageBox.Show("Ekleme Başarılı");
        }

        private async void btn_Sil_Click(object sender, EventArgs e)
        {
            string values = "Delete From TBL_Product where  ProductId = @ProductId";
            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", txtID.Text);
            await Connect.Baglantı().ExecuteAsync(values, parameters);
            MessageBox.Show("Silme Başarılı");
        }

        private async void btn_Güncelle_Click(object sender, EventArgs e)
        {
            var values = "UPDATE TBL_Product SET ProductName = @ProductName , ProductStock = @ProductStock , ProductPrice = @ProductPrice , ProductCatgory = @ProductCatgory WHERE ProductID = @ProductID ";
            var parameters = new DynamicParameters();

            parameters.Add("@ProductName", txtName.Text);
            parameters.Add("@ProductStock", numStock.Value);
            parameters.Add("@ProductPrice", txtPrice.Text);
            parameters.Add("@ProductCategory", txtCategory.Text);
            parameters.Add("@ProductId", txtID.Text);
            await Connect.Baglantı().ExecuteAsync(values, parameters);
            MessageBox.Show("Güncelleme Başarılı");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            #region Toplam Ürün Sayısı
            string values = "select COUNT(*) from TBL_Product";
            var productTotalCount = await Connect.Baglantı().QueryFirstOrDefaultAsync<int>(values);
            lbl_TotalBook.Text = productTotalCount.ToString();
            #endregion

            #region En Pahalı Ürün
            string values1 = "select top 1 ProductName from TBL_Product order by ProductPrice desc";
            var productname = await Connect.Baglantı().QueryFirstOrDefaultAsync<string>(values1);
            lbl_MaxPriceProductName.Text = productname;
            #endregion

            #region Topalm Kategori Sayısı
            string values2 = "select Count(DISTINCT (ProductCategory)) from TBL_Product";
            var TotalCategoryCount = await Connect.Baglantı().QueryFirstOrDefaultAsync<string>(values2);
            lbl_TotalCategoryCount.Text = TotalCategoryCount;
            #endregion
        }
    }
}
