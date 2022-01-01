using Microsoft.EntityFrameworkCore;
using NorthWind_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorthWind_EntityFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnShow_Click(object sender, EventArgs e)
        {
            lblLoading.Visible = true;
            using (var db = new NorthwindContext())
            {
                switch (comBoxQues.Text)
                {
                    case "Question 1":
                        var query1 = db.Products.Select(p => new { p.ProductName, p.QuantityPerUnit });
                        dataGridView1.DataSource = await Task.Run(() => query1.ToList());
                        lblLoading.Visible = false;
                        break;
                    case "Question 2":
                        var query2 = db.Products.Where(p => p.Discontinued == false).Select(p => new {p.ProductId , p.ProductName});
                        dataGridView1.DataSource = await Task.Run(() => query2.ToList());
                        lblLoading.Visible = false;
                        break;
                    case "Question 3":
                        var query3 = db.Products.Where(p => p.Discontinued == true).Select(p => new { p.ProductId, p.ProductName });
                        dataGridView1.DataSource = await Task.Run(() => query3.ToList());
                        lblLoading.Visible = false;
                        break;
                    case "Question 4":
                        var query4 = db.Products.Where(p => p.UnitPrice == db.Products.Max(p => p.UnitPrice) || p.UnitPrice == db.Products.Min(p => p.UnitPrice))
                            .Select(p => new { p.ProductName, p.UnitPrice });
                        dataGridView1.DataSource = await Task.Run(() => query4.ToList());
                        lblLoading.Visible = false;
                        break;
                    case "Question 5":
                        var query5 = db.Products.Where(p => p.UnitPrice < 20)
                            .Select(p => new { p.ProductId, p.ProductName, p.UnitPrice });
                        dataGridView1.DataSource = await Task.Run(() => query5.ToList());
                        lblLoading.Visible = false;
                        break;
                    case "Question 6":
                        var query6 = db.Products.Where(p => p.UnitPrice <= 25 && p.UnitPrice >= 15)
                            .Select(p => new { p.ProductId, p.ProductName, p.UnitPrice });
                        dataGridView1.DataSource = await Task.Run(() => query6.ToList());
                        lblLoading.Visible = false;
                        break;
                    case "Question 7":
                        var query7 = db.Products.Where(p => p.UnitPrice > db.Products.Average(p => p.UnitPrice))
                            .Select(p => new { p.ProductName, p.UnitPrice });
                        dataGridView1.DataSource = await Task.Run(() => query7.ToList());
                        lblLoading.Visible = false;
                        break;
                    case "Question 8":
                        var query8 = db.Products.Where(p => 10 >= (db.Products.Where(q => q.UnitPrice >= p.UnitPrice).Distinct().Count()) )
                            .Select(p => new { p.ProductName, p.UnitPrice }).OrderByDescending(p => p.UnitPrice);
                        dataGridView1.DataSource = await Task.Run(() => query8.ToList());
                        lblLoading.Visible = false;
                        break;
                    case "Question 9":
                        var query9 = from p in db.Products
                                     group p by p.Discontinued into pl
                                     select new { Discontinued = pl.Key, Count = pl.Count() };
                                                  
                        dataGridView1.DataSource = await Task.Run(() => query9.ToList());
                        lblLoading.Visible = false;
                        break;
                    case "Question 10":
                        var query10 = from p in db.Products
                                     where p.UnitsInStock < p.UnitsOnOrder
                                     select new {p.ProductName , p.UnitsOnOrder , p.UnitsInStock};

                        var lambdaQuery10 = db.Products.Where(p => p.UnitsInStock < p.UnitsOnOrder)
                            .Select(p => new { p.ProductName, p.UnitsOnOrder, p.UnitsInStock });

                        dataGridView1.DataSource = await Task.Run(() => lambdaQuery10.ToList());
                        lblLoading.Visible = false;
                        break;

                }
            }
        }
    }
}
