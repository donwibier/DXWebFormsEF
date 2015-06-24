<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DXWebFormsEF.Sample.Default" %>

<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>		  
    	 <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
				Width="1000px"
				DataSourceID="ObjectDataSource1" KeyFieldName="ArtistId" OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating">
			  <SettingsEditing Mode="Inline">
			  </SettingsEditing>
			  <SettingsSearchPanel Visible="True" />
			  <Columns>
					<dx:GridViewCommandColumn Width="150px"
						 ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
					</dx:GridViewCommandColumn>
					<dx:GridViewDataTextColumn FieldName="Name" Caption="Artist">

					</dx:GridViewDataTextColumn>
			  	 <dx:GridViewDataTextColumn Caption="ID" FieldName="ArtistId" ReadOnly="True" VisibleIndex="1" Width="100px">
					</dx:GridViewDataTextColumn>
			  </Columns>
		  </dx:ASPxGridView>
    </div>
		  <ef:EntityDataSource runat="server" ID="EntityDatasource1" ContextTypeName="DXWebFormsEF.Data.ChinookModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EnableViewState="False" EntitySetName="Artist" StoreOriginalValuesInViewState="False">
		  </ef:EntityDataSource>
    	 <dx:EntityServerModeDataSource ID="EntityServerModeDataSource1" runat="server" ContextTypeName="DXWebFormsEF.Data.ChinookModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" OnDeleting="EntityServerModeDataSource1_Deleting" OnSelecting="EntityServerModeDataSource1_Selecting" TableName="Artist" OnInserting="EntityServerModeDataSource1_Inserting" OnUpdating="EntityServerModeDataSource1_Updating" />
		  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
				DataObjectTypeName="DXWebFormsEF.Data.Artist" 
				DeleteMethod="Delete" InsertMethod="Insert" 
				OldValuesParameterFormatString="original_{0}" 
				SelectMethod="Select" TypeName="DXWebFormsEF.Data.ArtistDatasource" 
				UpdateMethod="Update">

		  </asp:ObjectDataSource>
    </form>
</body>
</html>
