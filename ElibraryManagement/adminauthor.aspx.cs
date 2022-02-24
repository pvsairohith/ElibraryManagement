using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
    public partial class adminauthor : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
         
        //add author
        protected void Button2_Click(object sender, EventArgs e)
        {
            if(checkuser())
            {
                Response.Write("<script> alert('author exists you can't add another author with same id');</script>");
            }
            else
            {
                addauthor();
            }
        }
        void addauthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("insert into author_master_tbl(author_id,author_name) values(@author_id,@author_name)", con);

                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                clearform();
                Response.Write("<script> alert('author added successful');</script>");
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "');</script>");
            }
        }
        bool checkuser()
        {

            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from author_master_tbl where author_id='" + TextBox1.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        //update author details
        protected void Button3_Click(object sender, EventArgs e)
        {
            if(checkuser())
            {
                updateAuthor();
            }
            else
            {
                Response.Write("<script> alert('author id  doesn't exist');</script>");
            }
        }
        

        void updateAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("update author_master_tbl SET author_name = @author_name WHERE author_id='" +TextBox1.Text.Trim() +"'", con);

                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                clearform();
                Response.Write("<script> alert('author updated successful');</script>");
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "');</script>");
            }
        }
        //delete author

        void deleteauthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE from author_master_tbl WHERE author_id = @author_id", con);
                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script> alert('deleted successful');</script>");
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "');</script>");
            }
        }
        void clearform()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkuser())
            {
                deleteauthor();
            }
            else
            {
                Response.Write("<script> alert('author id  doesn't exist for deletion');</script>");
            }

        }
        
    }
}