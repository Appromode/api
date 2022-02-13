using marking_api.DataModel.Enums;
using marking_api.DataModel.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace marking_api.Data.Audit
{
    public class AuditHelper
    {
        public AuditHelper(EntityEntry entry, string userId)
        {
            Entry = entry;
            UserId = userId;
            SetChanges();
        }

        public EntityEntry Entry { get; set; }
        public AuditType AuditType { get; set; }
        public string UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<string> ChangedColumns { get; } = new List<string>();

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
