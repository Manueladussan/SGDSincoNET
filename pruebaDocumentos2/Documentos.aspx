<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Documentos.aspx.cs" Inherits="pruebaDocumentos2.Documentos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous">
    <script src="js/jquery-3.5.1.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SGD Sinco</title>
</head>
<body>
    <form id="form1" novalidate runat="server">
        <div>
            <h1>Documentos</h1>
            <button type="button" id="crear" name="crear" runat="server" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalAddDocument">Agregar Documento</button>
            <div class="form-grpup">
                <input type="text" class="form-control" placeholder="Buscar" runat="server" id="buscar" name="buscar"/>
                <button id="Button1" runat="server" class="btn btn-primary" onserverclick="Buscar">Filtrar</button>
            </div>
            <label id="lblRespuesta" runat="server"></label>
            <table class="table table-bordered">
                <tr>
                    <th style="text-align: center;">Id</th>
                    <th style="text-align: center;">Nombre personalizado</th>
                    <th style="text-align: center;">Nombre real</th>
                    <th style="text-align: center;">Extension</th>
                    <th style="text-align: center;">Mime Type</th>
                    <th style="text-align: center;">Archivo</th>
                    <th style="text-align: center;">Editar</th>
                    <th style="text-align: center;">Eliminar</th>
                </tr>
                <asp:Repeater ID="rptDocumentos" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;"><%# Eval("id") %></td>
                            <td style="text-align: center;"><%# Eval("nombre_personalizado") %></td>
                            <td style="text-align: center;"><%# Eval("nombre_real") %></td>
                            <td style="text-align: center;"><%# Eval("extension") %></td>
                            <td style="text-align: center;"><%# Eval("mime_type") %></td>
                            <td style="text-align: center;"><%# Eval("archivo") %></td>
                            <td style="text-align: center;" runat="server">
                                <button type="button" idDocumento=<%# Eval("id").ToString() %>  id="editar" runat="server" data-bs-toggle="modal" data-bs-target="#modalEditDocument"  class="btn btn-primary" onclick="obtenerId(this)"><i class="fa fa-edit"></i></button>                                
                            </td>
                            <td style="text-align: center;">
                                <button type="button" idEliminar=<%# Eval("id").ToString() %>  id="eliminar" runat="server" data-bs-toggle="modal" data-bs-target="#modalConfirmDelete" class="btn btn-primary" onclick="eliminarPorId(this)"><i class="fa fa-trash"></i></button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div>
            <input type="text" class="form-control" id="idDocumentDelete" name="idDocumentDelete" runat="server" />
        </div>
        <div class="modal fade" id="modalEditDocument">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Editar documento</h4>
                    </div>
                    <div class="modal-body">
                        <div class="box-body">
                            <label id="validateInput" runat="server"></label>
                            <div class="form-group">
                                <label class="control-label">Carga archivo</label>
                                <input type="file" class="form-control" id="archivo_edit" name="archivo" runat="server"/>
                            </div>
                            <div class="form-group">
                                <input type="text" class="form-control" id="nombre_actual_edit" name="nombre_actual_edit" placeholder="Nombre Actual" runat="server" hidden/>
                            </div>
                            <div class="form-group">
                                <label class="control-label">Nombre personalizado</label>
                                <input type="text" class="form-control" id="nombre_personalizado_edit" name="nombre_personalizado" placeholder="Nombre Personalizado" runat="server" required/>
                            </div>
                            <div class="form-group" hidden="hidden">
                                <input type="text" class="form-control" id="id_document" name="id_document" runat="server" />
                                <input type="text" class="form-control" id="name_document" name="name_document" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default pull-left" data-bs-dismiss="modal">Cancelar</button>                        
                        <button type="button" ID="actualizar" name="actualizar"  runat="server" class="btn btn-primary" onserverclick="ActualizarDocumentos">Editar documento</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modalAddDocument">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Agregar documento</h4>
                    </div>
                    <div class="modal-body">
                        <div class="box-body">
                            <div class="form-group">
                                <label class="control-label">Carga archivo</label>
                                <input type="file" class="form-control" id="archivo" name="archivo" runat="server"/>
                            </div>
                            <div class="form-group">
                                <label class="control-label">Nombre personalizado</label>
                                <input type="text" class="form-control" id="nombre_personalizado" name="nombre_personalizado" placeholder="Nombre Personalizado" runat="server"/>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default pull-left" data-bs-dismiss="modal">Cancelar</button>
                        <button type="button" id="primeraCarga" name="primeraCarga" runat="server" class="btn btn-primary" onclick="validarCampos()">Agregar Documento</button>                        
                        <button type="button" id="guardar" name="guardar" class="btn btn-primary" runat="server" style="visibility: hidden" onserverclick="CrearDocumentos">Agregar documento</button>
                    </div>
                </div>
            </div>
        </div>        
        <div class="modal fade" id="modalConfirmDelete">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Eliminar documento</h4>
                    </div>
                    <div class="modal-body">
                        <h2>¿Seguro de eliminar este registro?</h2>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default pull-left" data-bs-dismiss="modal">Cancelar</button>
                        <button type="submit" id="eliminar" name="eliminar" class="btn btn-primary" runat="server" onserverclick="EliminarDocumentos">Eliminar documento</button>
                    </div>
                </div>
            </div>
        </div>        
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
   <script type="text/javascript">
       function obtenerId(data) {
           document.getElementById('id_document').value = data.attributes.iddocumento.value;
       }
       function eliminarPorId(data) {
           document.getElementById('idDocumentDelete').value = data.attributes.ideliminar.value;
       }

       function validarCampos() {
           if (archivo.value == "") {
               alert('El archivo es obligtorio');
               $('#modalAddDocument').modal('show');

           } else if (nombre_personalizado.value == "") {
               alert('Nombre personalizado es obligtorio');
               $('#modalAddDocument').modal('show');
           }else {
               guardar.click();
           }

       }

   </script> 
</body>
</html>
