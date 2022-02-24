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
    public partial class adminbookissue : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        // go button
        protected void Button1_Click(object sender, EventArgs e)
        {
            getbookandmembernames();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if(checkifbookexist() && checkifmemberexist())
            {
                if(checkifissueentryexist())
                {
                    Response.Write("<script>alert('This member already has this book');</script>");
                }
                else
                {
                    issuebook();
                }
               
            }
            else
            {
                Response.Write("<script>alert('wrong book id or member id');</script>");
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkifbookexist() && checkifmemberexist())
            {
                if (checkifissueentryexist())
                {
                    returnbook();
                }
                else
                {
                    Response.Write("<script>alert('This entry doesnot exist');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('wrong book id or member id');</script>");
            }
        }

        //user defined functions

        void returnbook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("Delete from book_issue_tbl WHERE book_id='" + TextBox1.Text.Trim() + "' AND member_id='" + TextBox2.Text.Trim() + "'", con);
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {

                    cmd = new SqlCommand("update book_master_tbl set current_stock = current_stock+1 WHERE book_id='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Write("<script>alert('Book Returned Successfully');</script>");
                    GridView1.DataBind();

                    con.Close();

                }
                else
                {
                    Response.Write("<script>alert('Error - Invalid details');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void issuebook()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("INSERT INTO book_issue_tbl(member_id,member_name,book_id,book_name,issue_date,due_date) values(@member_id,@member_name,@book_id,@book_name,@issue_date,@due_date)", con);
            cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.ToString());
            cmd.Parameters.AddWithValue("@member_name", TextBox3.Text.ToString());
            cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@book_name", TextBox4.Text.ToString());
            cmd.Parameters.AddWithValue("@issue_date", TextBox5.Text.ToString());
            cmd.Parameters.AddWithValue("@due_date", TextBox6.Text.ToString());
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("update book_master_tbl set current_stock = current_stock - 1 WHERE book_id='" + TextBox1.Text.Trim() + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script> alert('book added successfully');</script>");
            GridView1.DataBind();

        }

        //checking book and whether the book has current stock or not
        bool checkifbookexist()
        {
            SqlConnection con = new SqlConnection(strcon);
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from book_master_tbl WHERE book_id='" + TextBox1.Text.Trim() + "' AND current_stock>0", con);
            SqlDataReader sq = cmd.ExecuteReader();
            if(sq.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool checkifmemberexist()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select fullname from member_master_tbl WHERE member_id = '" + TextBox2.Text.Trim() + "'", con);
            SqlDataReader sq = cmd.ExecuteReader();
            if (sq.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool checkifissueentryexist()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from book_issue_tbl WHERE member_id = '" + TextBox2.Text.Trim() + "' AND book_id='" + TextBox1.Text.Trim()+"'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void getbookandmembernames()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if(con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select book_name from book_master_tbl WHERE book_id='" + TextBox1.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count>=1)
                {
                    TextBox4.Text = dt.Rows[0]["book_name"].ToString();
                }
                else
                {
                    Response.Write("<script> alert('wrong id');</script>");
                }

                cmd = new SqlCommand("select fullname from member_master_tbl WHERE member_id='" + TextBox2.Text.Trim() + "'", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox3.Text = dt.Rows[0]["fullname"].ToString();
                }
                else
                {
                    Response.Write("<script> alert('wrong id');</script>");
                }

            }
            catch(Exception ex)
            {

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //checking whether it has values or not
                if(e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if(dt<today)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}