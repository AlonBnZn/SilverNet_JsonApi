namespace SilverNetJsonApiAssigment.Tests
{
    public class Request<TAttributes>
    {
        private JsonApiData<TAttributes> _data = new();

        public JsonApiData<TAttributes> Data
        {
            get => _data;
            set
            {
                _data = value;
                RemoveRelationships();
            }
        }

        private void RemoveRelationships()
        {
            if (Data.Attributes != null)
            {
                var props = typeof(TAttributes).GetProperties();

                foreach (var prop in props)
                {
                    var value = prop.GetValue(Data.Attributes);

                    if (value is System.Collections.IList list && list.Count == 0)
                    {
                        prop.SetValue(Data.Attributes, null);
                    }
                }
            }

            if (Data.Relationships != null && Data.Relationships.Count == 0)
            {
                Data.Relationships = null;
            }
        }
    }

    public class JsonApiData<TAttributes>
    {
        public string Type { get; set; } = null!;

        public string? Id { get; set; }

        public TAttributes Attributes { get; set; } = default!;

        public Dictionary<string, RelationshipData>? Relationships { get; set; }
    }

    public class RelationshipData
    {
        public object Data { get; set; } = null!;
    }
}
