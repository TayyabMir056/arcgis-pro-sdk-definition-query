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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinitionQuery
{
    /// <summary>
    /// Represents the ComboBox
    /// </summary>
    internal class SelectValue : ComboBox
    {

        private bool _isInitialized;

        /// <summary>
        /// Combo Box constructor
        /// </summary>
        public SelectValue()
        {

            Module1.Current.selectValue = this;
        }


        protected override void OnSelectionChange(ComboBoxItem item)
        {

            if (item == null)
                return;

            if (string.IsNullOrEmpty(item.Text))
                return;

            // TODO  Code behavior when selection changes.    
        }

        public void UpdateValues(string layer,string field)
        {
            List<string> values = new List<string>();
            QueuedTask.Run(() =>
            {
                Clear();
                var featureLayer = MapView.Active.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>().First(f => f.Name == layer);
                var rows = featureLayer.GetFeatureClass().Search();
                using(RowCursor rowCursor = featureLayer.GetFeatureClass().Search())
                {
                    while (rowCursor.MoveNext())
                    {
                        using (Row row = rowCursor.Current)
                        {
                            string value = Convert.ToString(row[field]);
                            if (!values.Contains(value))
                            {
                                values.Add(value);
                                if (values.Count > 100)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                foreach(string val in values)
                {
                    Add(new ComboBoxItem(val));
                }

            });
        }

        public void ClearValue()
        {
            this.Text = "";
        }

    }
}
