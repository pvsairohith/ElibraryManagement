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
    public partial class adminmembermanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        //Go Button
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from member_master_tbl WHERE member_id=@member_id", con);
                cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                SqlDataReader sr = cmd.ExecuteReader();
                if (sr.HasRows)
                {
                    while (sr.Read())
                    {
                        TextBox2.Text = sr.GetValue(0).ToString();
                        TextBox7.Text = sr.GetValue(10).ToString();
                        TextBox8.Text = sr.GetValue(1).ToString();
                        TextBox3.Text = sr.GetValue(2).ToString();
                        TextBox4.Text = sr.GetValue(3).ToString();
                        TextBox9.Text = sr.GetValue(4).ToString();
                        TextBox10.Text = sr.GetValue(5).ToString();
                        TextBox11.Text = sr.GetValue(6).ToString();
                        TextBox6.Text = sr.GetValue(6).ToString();
                    }
                }
                else
                {
                    Response.Write("<script> alert('invalid credintials');</script>");
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "');</script>");
            }

        }

        //active button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            updateMemberStatusById("active");
        }
        //pending button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            updateMemberStatusById("pending");
        }
        //deactive button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            updateMemberStatusById("deactive");
        }
        //delete button
        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteMemberById();
        }

        void updateMemberStatusById(string status)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(" UPDATE member_master_tbl SET account_status='" + status + "' WHERE member_id='" + TextBox1.Text.Trim() + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
                clearform();
                Response.Write("<script> alert('Member status updated');</script>");
               
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "');</script>");
            }
        }

        void deleteMemberById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE from member_master_tbl where member_id='" + TextBox1.Text.Trim() + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
                clearform();
                Response.Write("<script> alert('Member deleted successfully');</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "');</script>");
            }
        }
        void clearform()
        {
            TextBox2.Text = " ";
            TextBox7.Text = " ";
            TextBox8.Text = " ";
            TextBox3.Text = " ";
            TextBox4.Text = " ";
            TextBox9.Text = " ";
            TextBox10.Text =  " ";
            TextBox11.Text = " ";
            TextBox6.Text = " ";
        }

        
    }
}