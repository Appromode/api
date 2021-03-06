using marking_api.DataModel.Enums;
using marking_api.DataModel.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Data.Audit
{
    /// <summary>
    /// Convert entries to AuditDM format
    /// </summary>
    public class AuditDetails
    {
        /// <summary>
        /// Set database entry and userid as well as converting the entry to AuditDM
        /// </summary>
        /// <param name="entry">EntityEntry - Database entry</param>
        /// <param name="userId">String - UserId</param>
        public AuditDetails(EntityEntry entry, string userId)
        {
            Entry = entry;
            UserId = userId;
            SetChanges();
        }
        /// <summary>
        /// Database Entry
        /// </summary>
        public EntityEntry Entry { get; set; }
        /// <summary>
        ///Type of database entry. New, modified, deleted etc. 
        /// </summary>
        public AuditType AuditType { get; set; }
        /// <summary>
        /// UserId of the user that created the database entry
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Table the database entry is going to affect
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// List of key values. Primary or Foreign.
        /// </summary>
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        /// <summary>
        /// List of old values tha are going to be deleted or values that are going to be updated
        /// </summary>
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        /// <summary>
        /// List of new values that have been added or modified
        /// </summary>
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        /// <summary>
        /// List of columns that have been modified.
        /// </summary>
        public List<string> ChangedColumns { get; } = new List<string>();

        /// <summary>
        /// When a new instance of AuditHelper is initialised this method is called to break down the database entry into values that can be used to be converted into an AuditDM object
        /// </summary>
        private void SetChanges()
        {
            TableName = Entry.Metadata.GetTableName();
            foreach (PropertyEntry property in Entry.Properties)
            {
                string propertyName = property.Metadata.Name;

                if (property.Metadata.IsPrimaryKey())
                {
                    Int64 keyValue;
                    bool result = Int64.TryParse(property.CurrentValue?.ToString(), out keyValue);
                    if (result)
                        KeyValues[propertyName] = keyValue > 0 ? keyValue : 0;
                    else
                        KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (Entry.State)
                {
                    case EntityState.Added:
                        NewValues[propertyName] = property.CurrentValue;
                        AuditType = AuditType.Create;
                        break;

                    case EntityState.Deleted:
                        OldValues[propertyName] = property.OriginalValue;
                        AuditType = AuditType.Delete;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            ChangedColumns.Add(propertyName);

                            OldValues[propertyName] = property.OriginalValue;
                            NewValues[propertyName] = property.CurrentValue;
                            AuditType = AuditType.Update;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Convert changes ef core has detected to audit entries. Logs when, what and who data about the change.
        /// </summary>
        /// <returns>
        /// An AuditDM object containing info listed above.
        /// </returns>
        public AuditDM ToAudit()
        {
            var audit = new AuditDM();
            audit.AuditDate = DateTime.Now;
            audit.AuditType = AuditType.ToString();
            audit.UserId = UserId;
            audit.TableName = TableName;
            audit.KeyValues = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.ChangedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);

            return audit;
        }
    }
}
