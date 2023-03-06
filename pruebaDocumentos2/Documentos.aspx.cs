using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace pruebaDocumentos2
{
    public partial class Documentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                consultarDocumentos("");
            }

        }

        public void consultarDocumentos(string sql)
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["documentos"].ToString());
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;

            cnn.Open();

            if(sql != "")
            {
                cmd.CommandText = "SELECT * FROM dbo.DocumentosSinco " + sql;
            }
            else
            {
                cmd.CommandText = "SELECT * FROM dbo.DocumentosSinco";
            }
            //cmd.CommandText = "SELECT * FROM dbo.DocumentosSinco";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);

            rptDocumentos.DataSource = dataTable;
            rptDocumentos.DataBind();

            cnn.Close();
        }

        public void CrearDocumentos(object sender, EventArgs e)
        {
            string nombrePersonalizado = nombre_personalizado.Value;
            string mimeType = MimeMapping.GetMimeMapping(archivo.PostedFile.FileName);
            string nombreReal = archivo.PostedFile.FileName;
            string extension = archivo.PostedFile.FileName.ToString().Split('.')[1];
            string archivoNombre = archivo.PostedFile.FileName;
            string folder;
            string path;
            folder = Server.MapPath("./uploads");

            if(nombreReal != "")
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                path = folder +'/'+ nombreReal;

                if (File.Exists(path))
                {
                    lblRespuesta.InnerText = nombreReal + " Ya existe el documento en el repositorio";
                    consultarDocumentos("");
                }
                else
                {
                    archivo.PostedFile.SaveAs(path);
                    lblRespuesta.InnerText = nombreReal + " Ha sido agregado al repositorio exitosamente";
                    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["documentos"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter sqlDA = new SqlDataAdapter();

                    cnn.Open();

                    cmd.CommandText = "INSERT INTO dbo.DocumentosSinco VALUES('" + nombrePersonalizado + "','" + nombreReal + "','" + extension + "','" + mimeType + "','" + archivoNombre + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cnn;
                    sqlDA.InsertCommand = new SqlCommand(cmd.CommandText, cnn);
                    sqlDA.InsertCommand.ExecuteNonQuery();


                    sqlDA = new SqlDataAdapter(cmd);
                    cnn.Close();

                    nombre_personalizado.Value = "";
                    consultarDocumentos("");

                }
            }
        }

        public void ActualizarDocumentos(object sender, EventArgs e)
        {
            string nombrePersonalizado = nombre_personalizado_edit.Value;
            string mimeType ="";
            string nombreReal = "";
            string extension ="";
            string archivoNombre ="";
            string SQLNombreReal = "";
            string nombreActualArchivo = "";

            string id = id_document.Value;
            string folder;
            string path;
            string path_delete;
            

            folder = Server.MapPath("./uploads");
            if (archivo_edit.Value != "")
            {
                nombrePersonalizado = nombre_personalizado_edit.Value;
                mimeType = MimeMapping.GetMimeMapping(archivo_edit.PostedFile.FileName);
                nombreReal = archivo_edit.PostedFile.FileName;
                extension = archivo_edit.PostedFile.FileName.ToString().Split('.')[1];
                archivoNombre = archivo_edit.PostedFile.FileName;
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                path = folder + '/' + nombreReal;

                if (File.Exists(path))
                {
                    lblRespuesta.InnerText = nombreReal + " Ya existe el documento en el repositorio";
                    consultarDocumentos("");
                }
                else
                {
                    archivo_edit.PostedFile.SaveAs(path);
                    lblRespuesta.InnerText = nombreReal + " Ha sido agregado al repositorio exitosamente";
                    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["documentos"].ToString());
                    cnn.Open();
                    SQLNombreReal = "SELECT nombre_real  FROM dbo.DocumentosSinco where id=" + id;
                    SqlCommand sqlCmd2 = new SqlCommand(SQLNombreReal, cnn);
                    nombreActualArchivo = (string)sqlCmd2.ExecuteScalar();

                    cnn.Close();
                    path_delete = folder + '/' + nombreActualArchivo;

                    File.Delete(path_delete);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter sqlDA = new SqlDataAdapter();


                    cnn.Open();

                    cmd.CommandText = "UPDATE dbo.DocumentosSinco SET nombre_personalizado = '" + nombrePersonalizado + "', nombre_real = '" + nombreReal + "', extension = '" + extension + "', mime_type ='" + mimeType + "', archivo ='" + archivoNombre + "' where id="+ id;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cnn;
                    sqlDA.InsertCommand = new SqlCommand(cmd.CommandText, cnn);
                    sqlDA.InsertCommand.ExecuteNonQuery();


                    sqlDA = new SqlDataAdapter(cmd);
                    cnn.Close();

                    nombre_personalizado_edit.Value = "";
                    consultarDocumentos("");
                }
            }
            else
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["documentos"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sqlDA = new SqlDataAdapter();

                cnn.Open();

                cmd.CommandText = "UPDATE dbo.DocumentosSinco SET nombre_personalizado = '" + nombrePersonalizado + "' where id=" + id;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                sqlDA.InsertCommand = new SqlCommand(cmd.CommandText, cnn);
                sqlDA.InsertCommand.ExecuteNonQuery();


                sqlDA = new SqlDataAdapter(cmd);
                cnn.Close();

                nombre_personalizado_edit.Value = "";
                consultarDocumentos("");

            }
        }

        public void EliminarDocumentos(object sender, EventArgs e)
        {
            string id = idDocumentDelete.Value;
            string folder;
            string path_delete;
            string SQLNombreReal = "";
            string nombreActualArchivo = "";


            folder = Server.MapPath("./uploads");

            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["documentos"].ToString());
            cnn.Open();
            SQLNombreReal = "SELECT nombre_real  FROM dbo.DocumentosSinco where id=" + id;
            SqlCommand sqlCmd2 = new SqlCommand(SQLNombreReal, cnn);
            nombreActualArchivo = (string)sqlCmd2.ExecuteScalar();

            cnn.Close();
            path_delete = folder + '/' + nombreActualArchivo;

            File.Delete(path_delete);
            SqlCommand cmd = new SqlCommand();

            cnn.Open();

            SQLNombreReal = "DELETE FROM dbo.DocumentosSinco where id=" + id;
            SqlCommand sqlCmd = new SqlCommand(SQLNombreReal, cnn);
            nombreActualArchivo = (string)sqlCmd.ExecuteScalar();
            cnn.Close();

            consultarDocumentos("");
        }

        public void Buscar(object sender, EventArgs e)
        {
            string sql = "";
            string filtro = "";
            if (buscar.Value != "")
            {
                filtro = buscar.Value;
                sql = "where nombre_personalizado like '%" + filtro + "%' or nombre_real like '%" + filtro + "%' or extension like '%" + filtro + "%' or mime_type like '%" + filtro + "%' or archivo like '%" + filtro + "%'";
                consultarDocumentos(sql);
            }
            else
            {
                consultarDocumentos("");
            }
            

        }
    }
}