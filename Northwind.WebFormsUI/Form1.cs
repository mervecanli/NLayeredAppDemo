using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccesss.Concrete.EntityFramework;
using Northwind.DataAccesss.Concrete.NHibernate;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public Form1()
        {
            InitializeComponent();
            _productService = new ProductManager(new EfProductDal());
            _categoryService = new CategoryManager(new EfCategoryDal());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        private void LoadProducts()
        {
            dgvProducts.DataSource = _productService.GetAll();
        }
        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            cbxCategoryId.DataSource = _categoryService.GetAll(); 
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";

            cbxUpdateCategoryId.DataSource = _categoryService.GetAll(); 
            cbxUpdateCategoryId.DisplayMember = "CategoryName";
            cbxUpdateCategoryId.ValueMember = "CategoryId";
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvProducts.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch (Exception)
            {
            }
        }

        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbxProductName.Text))
                LoadProducts();
            else
                dgvProducts.DataSource = _productService.GetProductsByProductName(tbxProductName.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productService.Add( new Product
                {
                    CategoryId = Convert.ToInt32(cbxCategoryId.SelectedValue),
                    ProductName = txbProductName2.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStock.Text),
                    QuantityPerUnit = tbxQuantityPerUnit.Text
                }
            );
            MessageBox.Show("Ürün eklendi!");
            LoadProducts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productService.Update(new Product 
            {
                ProductId = Convert.ToInt32(dgvProducts.CurrentRow.Cells[0].Value),
                CategoryId = Convert.ToInt32(cbxUpdateCategoryId.SelectedValue),
                ProductName = tbxUpdateProductName.Text,
                UnitPrice = Convert.ToDecimal(tbxUpdateUnitPrice.Text),
                UnitsInStock = Convert.ToInt16(tbxUpdateUnitsInStock.Text),
                QuantityPerUnit = tbxUpdateQuantityPerUnit.Text
            });
            MessageBox.Show("Ürün güncellendi!");
            LoadProducts();
        }
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvProducts.CurrentRow;
            cbxUpdateCategoryId.SelectedValue = row.Cells[1].Value;
            tbxUpdateProductName.Text = row.Cells[2].Value.ToString();
            tbxUpdateUnitPrice.Text = row.Cells[3].Value.ToString();
            tbxUpdateQuantityPerUnit.Text = row.Cells[4].Value.ToString();
            tbxUpdateUnitsInStock.Text = row.Cells[5].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(dgvProducts.CurrentRow != null)
            {
                try
                {
                    _productService.Delete(new Product
                    {
                        ProductId = Convert.ToInt32(dgvProducts.CurrentRow.Cells[0].Value)
                    });
                    MessageBox.Show("Ürün silindi!");
                    LoadProducts();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Bir hata ile karşılaşıldı, lütfen kurum ile irtibata geçiniz.");
                }
            }
        }
    }
}
// S: Bir görev bir kere yapılır ve onu sadece bir yer yapar. Bir metodun sadece tek bir sorumluluğu vardır,
// ve don't repeat yourself prensibiyle de tekrarlanmamalıdır.