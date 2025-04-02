using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using library;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace proWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                ClearForm();
            }
        }

        private void LoadCategories()
        {
            try
            {
                ENCategory enCategory = new ENCategory();
                List<ENCategory> categories = enCategory.ReadAll();

                ddlCategory.DataSource = categories;
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading categories: " + ex.Message;
            }
        }

        private void ClearForm()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtAmount.Text = "0";
            txtPrice.Text = "0.00";
            txtCreationDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            if (ddlCategory.Items.Count > 0)
            {
                ddlCategory.SelectedIndex = 0;
            }
            lblMessage.Text = "";
        }

        private bool ValidateForm()
        {
            lblMessage.Text = "";

            // Validar Code
            if (string.IsNullOrEmpty(txtCode.Text) || txtCode.Text.Length > 16)
            {
                lblMessage.Text = "Code must be between 1 and 16 characters.";
                return false;
            }

            // Validar Name
            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text.Length > 32)
            {
                lblMessage.Text = "Name must be between 1 and 32 characters.";
                return false;
            }

            // Validar Amount
            int amount;
            if (!int.TryParse(txtAmount.Text, out amount) || amount < 0 || amount > 9999)
            {
                lblMessage.Text = "Amount must be a positive integer between 0 and 9999.";
                return false;
            }

            // Validar Price
            float price;
            if (!float.TryParse(txtPrice.Text, out price) || price < 0 || price > 9999.99f)
            {
                lblMessage.Text = "Price must be a positive number between 0 and 9999.99.";
                return false;
            }

            // Validar Creation Date
            DateTime creationDate;
            if (!DateTime.TryParse(txtCreationDate.Text, out creationDate))
            {
                lblMessage.Text = "Creation Date must be in format dd/MM/yyyy HH:mm:ss.";
                return false;
            }

            return true;
        }

        private ENProduct GetProductFromForm()
        {
            ENProduct product = new ENProduct();
            product.Code = txtCode.Text;
            product.Name = txtName.Text;
            product.Amount = Convert.ToInt32(txtAmount.Text);
            product.Price = Convert.ToSingle(txtPrice.Text);
            product.Category = Convert.ToInt32(ddlCategory.SelectedValue);
            product.CreationDate = Convert.ToDateTime(txtCreationDate.Text);
            return product;
        }

        private void SetFormFromProduct(ENProduct product)
        {
            txtCode.Text = product.Code;
            txtName.Text = product.Name;
            txtAmount.Text = product.Amount.ToString();
            txtPrice.Text = product.Price.ToString("0.00");
            ddlCategory.SelectedValue = product.Category.ToString();
            txtCreationDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                ENProduct product = GetProductFromForm();

                // Verificar que no exista un producto con el mismo código
                ENProduct existingProduct = new ENProduct();
                existingProduct.Code = product.Code;
                if (existingProduct.Read())
                {
                    lblMessage.Text = "A product with the same code already exists.";
                    return;
                }

                if (product.Create())
                {
                    lblMessage.Text = "Product created successfully.";
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Error creating product.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                ENProduct product = GetProductFromForm();

                // Verificar que exista un producto con ese código
                ENProduct existingProduct = new ENProduct();
                existingProduct.Code = product.Code;
                if (!existingProduct.Read())
                {
                    lblMessage.Text = "No product found with the specified code.";
                    return;
                }

                if (product.Update())
                {
                    lblMessage.Text = "Product updated successfully.";
                }
                else
                {
                    lblMessage.Text = "Error updating product.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblMessage.Text = "Please enter a product code to delete.";
                return;
            }

            try
            {
                ENProduct product = new ENProduct();
                product.Code = txtCode.Text;

                if (product.Delete())
                {
                    lblMessage.Text = "Product deleted successfully.";
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Error deleting product. Product might not exist.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblMessage.Text = "Please enter a product code to read.";
                return;
            }

            try
            {
                ENProduct product = new ENProduct();
                product.Code = txtCode.Text;

                if (product.Read())
                {
                    SetFormFromProduct(product);
                    lblMessage.Text = "Product read successfully.";
                }
                else
                {
                    lblMessage.Text = "Product not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }

        protected void btnReadFirst_Click(object sender, EventArgs e)
        {
            try
            {
                ENProduct product = new ENProduct();

                if (product.ReadFirst())
                {
                    SetFormFromProduct(product);
                    lblMessage.Text = "First product read successfully.";
                }
                else
                {
                    lblMessage.Text = "No products found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }

        protected void btnReadPrev_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblMessage.Text = "Please read a product first.";
                return;
            }

            try
            {
                ENProduct product = new ENProduct();
                product.Code = txtCode.Text;

                if (product.ReadPrev())
                {
                    SetFormFromProduct(product);
                    lblMessage.Text = "Previous product read successfully.";
                }
                else
                {
                    lblMessage.Text = "No previous product found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }

        protected void btnReadNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblMessage.Text = "Please read a product first.";
                return;
            }

            try
            {
                ENProduct product = new ENProduct();
                product.Code = txtCode.Text;

                if (product.ReadNext())
                {
                    SetFormFromProduct(product);
                    lblMessage.Text = "Next product read successfully.";
                }
                else
                {
                    lblMessage.Text = "No next product found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }
    }
}