using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinitionQuery
{
    internal class ApplyButton : Button
    {
        protected override void OnClick()
        {
            QueuedTask.Run(() =>
            {
                string layer;
                string attribute;
                string value;


                if (Module1.Current.selectLayer.SelectedItem == null || Module1.Current.selectLayer.Text == "")
                {
                    MessageBox.Show("Please Select a Layer");
                    return;
                }
                layer = Module1.Current.selectLayer.Text;

                if (Module1.Current.selectAttribute.SelectedItem == null || Module1.Current.selectAttribute.Text == "")
                {
                    MessageBox.Show("Please Select an attribute to set def query on");
                    return;
                }
                attribute = Module1.Current.selectAttribute.Text;


                if (Module1.Current.selectValue.SelectedItem == null || Module1.Current.selectValue.Text == "")
                {
                    MessageBox.Show("Please Select a value");
                    return;
                }
                value = Module1.Current.selectValue.Text;


                // Apply Definition Query
                var featureLayer = MapView.Active.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>().First(f => f.Name == layer);
                featureLayer.SetDefinitionQuery($"{attribute} = '{value}'");

                MessageBox.Show("Definition Query Applied");

            });
           

        }
    }
}
