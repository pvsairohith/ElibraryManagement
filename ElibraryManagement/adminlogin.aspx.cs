using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
    public partial class adminlogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sq = new SqlConnection(strcon);
                if (sq.State == System.Data.ConnectionState.Closed)
                {
                    sq.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from admin_login_tbl where username='" + TextBox1.Text.Trim() + "' AND password='" + TextBox2.Text.Trim() + "'", sq);
                SqlDataReader sw = cmd.ExecuteReader();
                if (sw.HasRows)
                {
                    while (sw.Read())
                    {
                        Response.Write("<script> alert('" + sw.GetValue(0).ToString() + "');</script>");
                        Session["username"] = sw.GetValue(0).ToString();
                        Session["fullname"] = sw.GetValue(2).ToString();
                        Session["role"] = "admin";
                    }
                    Response.Redirect("homepage.aspx");
                }
                else
                {
                    Response.Write("<script> alert('Invalid credintials');</script>");
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "');</script>");
            }
        }
    }
}