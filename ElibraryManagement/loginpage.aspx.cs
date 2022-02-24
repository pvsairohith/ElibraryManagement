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
    public partial class loginpage : System.Web.UI.Page
    {
        string stcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stcon);
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Select * from member_master_tbl where member_id='" + TextBox1.Text.Trim() + "' AND password='" + TextBox2.Text.Trim() + "'",con);
            SqlDataReader sw = cmd.ExecuteReader();
            if(sw.HasRows)
            {
                while(sw.Read())
                {
                    Response.Write("<script> alert('" + sw.GetValue(8).ToString() + "');</script>");
                    Session["username"] = sw.GetValue(8).ToString();
                    Session["fullname"] = sw.GetValue(0).ToString();
                    Session["role"] = "user";
                    Session["status"] = sw.GetValue(10).ToString();
                }
                Response.Redirect("homepage.aspx");
            }
            else
            {
                Response.Write("<script> alert('Invalid credintials');</script>");
            }


        }
    }
}