using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
//using library;

namespace proWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                ClearForm();
            }

            // Registrar manejadores de eventos
            btnCreate.Click += btnCreate_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnRead.Click += btnRead_Click;
            btnReadFirst.Click += btnReadFirst_Click;
            btnReadPrev.Click += btnReadPrev_Click;
            btnReadNext.Click += btnReadNext_Click;
        }

        private void LoadCategories()
        {
            // Código para cargar categorías...
        }

        private void ClearForm()
        {
            // Código para limpiar formulario...
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

                // Verificar que exista un producto con ese código
                if (!product.Read())
                {
                    lblMessage.Text = "No product found with the specified code.";
                    return;
                }

                if (product.Delete())
                {
                    lblMessage.Text = "Product deleted successfully.";
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Error deleting product.";
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
            // Implementa la lógica de lectura
        }

        protected void btnReadFirst_Click(object sender, EventArgs e)
        {
            // Implementa la lógica de lectura del primero
        }

        protected void btnReadPrev_Click(object sender, EventArgs e)
        {
            // Implementa la lógica de lectura del anterior
        }

        protected void btnReadNext_Click(object sender, EventArgs e)
        {
            // Implementa la lógica de lectura del siguiente
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


    }
}