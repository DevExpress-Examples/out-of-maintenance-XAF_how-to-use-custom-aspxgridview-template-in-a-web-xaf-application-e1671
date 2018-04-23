Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web.Editors.ASPx
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.ExpressApp.Web
Imports DevExpress.Web.ASPxEditors
Imports System.Collections
Imports DevExpress.ExpressApp.Web.Editors

Namespace WebExample.Module.Web
	Public Class ChangeColumnTemplateController
		Inherits ViewController
		Public Sub New()
			TargetViewId = "DomainObject1_ListView"
		End Sub
		Protected Overrides Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			Dim listEditor As ASPxGridListEditor = TryCast((CType(View, ListView)).Editor, ASPxGridListEditor)
			If listEditor IsNot Nothing Then
				Dim gridView As ASPxGridView = CType(listEditor.Grid, ASPxGridView)
				gridView.ClientInstanceName = View.Id
				Dim detailsColums As GridViewDataColumn = TryCast(gridView.Columns("Index"), GridViewDataColumn)
				If detailsColums IsNot Nothing Then
					detailsColums.DataItemTemplate = New UpDownButtonsTemplate()
					gridView.ClearSort()
					gridView.SortBy(detailsColums, DevExpress.Data.ColumnSortOrder.Ascending)
					AddHandler gridView.CustomCallback, AddressOf gridView_CustomCallback
				End If
			End If
		End Sub
		Private Sub gridView_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
			Dim parameters() As String = e.Parameters.Split("|"c)

			If parameters.Length = 2 AndAlso (parameters(0) = "up" OrElse parameters(0) = "down") Then
				Dim gridView As ASPxGridView = CType(sender, ASPxGridView)
				Dim key As Object = TypeDescriptor.GetConverter(View.ObjectTypeInfo.KeyMember.MemberType).ConvertFromString(parameters(1))
				Dim rowIndex As Integer = gridView.FindVisibleIndexByKeyValue(key)
				If parameters(0) = "up" AndAlso rowIndex > 0 Then
					SwitchIndices(CType(gridView.GetRow(rowIndex), DomainObject1), CType(gridView.GetRow(rowIndex - 1), DomainObject1))
				End If
				If parameters(0) = "down" AndAlso rowIndex < gridView.VisibleRowCount - 1 Then
					SwitchIndices(CType(gridView.GetRow(rowIndex), DomainObject1), CType(gridView.GetRow(rowIndex + 1), DomainObject1))
				End If
			End If
		End Sub
		Private Sub SwitchIndices(ByVal currentObject As DomainObject1, ByVal targetObject As DomainObject1)
			Dim oldIndex As Integer = currentObject.Index
			currentObject.Index = targetObject.Index
			targetObject.Index = oldIndex
			ObjectSpace.CommitChanges()
		End Sub
	End Class
	Public Class UpDownButtonsTemplate
		Implements ITemplate
		Private Const ClickEventHandlerFormat As String = "function(s, e) {{ e.processOnServer = false; {0}.PerformCallback(""{1}|{2}""); }}"
		Private Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
			Dim gridContainer As GridViewDataItemTemplateContainer = CType(container, GridViewDataItemTemplateContainer)
			Dim downButton As ASPxButton = RenderHelper.CreateASPxButton()
			downButton.Text = "Down"
			downButton.ClientSideEvents.Click = String.Format(ClickEventHandlerFormat, gridContainer.Grid.ClientInstanceName, "down", gridContainer.KeyValue)
			downButton.ID = "downButton_" & container.ID
			Dim upButton As ASPxButton = RenderHelper.CreateASPxButton()
			upButton.Text = "Up"
			upButton.ClientSideEvents.Click = String.Format(ClickEventHandlerFormat, gridContainer.Grid.ClientInstanceName, "up", gridContainer.KeyValue)
			upButton.ID = "upButton_" & container.ID
			Dim table As New Table()
			table.Rows.Add(New TableRow())
			table.Rows(0).Cells.Add(New TableCell())
			table.Rows(0).Cells.Add(New TableCell())
			table.Rows(0).Cells(0).Controls.Add(downButton)
			table.Rows(0).Cells(1).Controls.Add(upButton)
			table.Attributes("onclick") = RenderHelper.EventCancelBubbleCommand
			container.Controls.Add(table)
		End Sub
	End Class
End Namespace