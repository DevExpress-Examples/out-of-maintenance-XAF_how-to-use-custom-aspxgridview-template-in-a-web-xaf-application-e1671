using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using DevExpress.ExpressApp.Web;
using System.Collections;
using DevExpress.ExpressApp.Web.Editors;

namespace WebExample.Module.Web {
    public class ChangeColumnTemplateController : ViewController {
        public ChangeColumnTemplateController() {
            TargetViewId = "DomainObject1_ListView";
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            ASPxGridListEditor listEditor = ((ListView)View).Editor as ASPxGridListEditor;
            if (listEditor != null) {
                ASPxGridView gridView = (ASPxGridView)listEditor.Grid;
                gridView.ClientInstanceName = View.Id;
                GridViewDataColumn detailsColums = gridView.Columns["Index"] as GridViewDataColumn;
                if (detailsColums != null) {
                    detailsColums.DataItemTemplate = new UpDownButtonsTemplate();
                    gridView.ClearSort();
                    gridView.SortBy(detailsColums, DevExpress.Data.ColumnSortOrder.Ascending);
                    gridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                }
            }
        }
        void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
            string[] parameters = e.Parameters.Split('|');

            if (parameters.Length == 2 && (parameters[0] == "up" || parameters[0] == "down")) {
                ASPxGridView gridView = (ASPxGridView)sender;
                Object key = TypeDescriptor.GetConverter(View.ObjectTypeInfo.KeyMember.MemberType).ConvertFromString(parameters[1]);
                int rowIndex = gridView.FindVisibleIndexByKeyValue(key);
                if (parameters[0] == "up" && rowIndex > 0) {
                    SwitchIndices((DomainObject1)gridView.GetRow(rowIndex), (DomainObject1)gridView.GetRow(rowIndex - 1));
                }
                if (parameters[0] == "down" && rowIndex < gridView.VisibleRowCount - 1) {
                    SwitchIndices((DomainObject1)gridView.GetRow(rowIndex), (DomainObject1)gridView.GetRow(rowIndex + 1));
                }
            }
        }
        private void SwitchIndices(DomainObject1 currentObject, DomainObject1 targetObject) {
            int oldIndex = currentObject.Index;
            currentObject.Index = targetObject.Index;
            targetObject.Index = oldIndex;
            ObjectSpace.CommitChanges();
        }
    }
    public class UpDownButtonsTemplate : ITemplate {
        const string ClickEventHandlerFormat = "function(s, e) {{ e.processOnServer = false; {0}.PerformCallback(\"{1}|{2}\"); }}";
        void ITemplate.InstantiateIn(Control container) {
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
            ASPxButton downButton = RenderHelper.CreateASPxButton();
            downButton.Text = "Down";
            downButton.ClientSideEvents.Click = String.Format(ClickEventHandlerFormat, gridContainer.Grid.ClientInstanceName, "down", gridContainer.KeyValue);
            downButton.ID = "downButton_" + container.ID;
            ASPxButton upButton = RenderHelper.CreateASPxButton();
            upButton.Text = "Up";
            upButton.ClientSideEvents.Click = String.Format(ClickEventHandlerFormat, gridContainer.Grid.ClientInstanceName, "up", gridContainer.KeyValue);
            upButton.ID = "upButton_" + container.ID;
            Table table = new Table();
            table.Rows.Add(new TableRow());
            table.Rows[0].Cells.Add(new TableCell());
            table.Rows[0].Cells.Add(new TableCell());
            table.Rows[0].Cells[0].Controls.Add(downButton);
            table.Rows[0].Cells[1].Controls.Add(upButton);
            table.Attributes["onclick"] = RenderHelper.EventCancelBubbleCommand;
            container.Controls.Add(table);
        }
    }
}