﻿using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Synapse.Core
{
    public partial class ParameterInfo : IParameterInfo, ICloneable<ParameterInfo>
    {
        public ParameterInfo() { }

        public string Name { get; set; }
        [YamlIgnore]
        public bool HasName { get { return !string.IsNullOrWhiteSpace( Name ); } }

        public SerializationType Type { get; set; }

        public List<ForEach> ForEach { get; set; }
        [YamlIgnore]
        public bool HasForEach { get { return ForEach != null && ForEach.Count > 0; } }

        public string InheritFrom { get; set; }
        [YamlIgnore]
        public bool HasInheritFrom { get { return !string.IsNullOrWhiteSpace( InheritFrom ); } }
        [YamlIgnore]
        public object InheritedValues { get; set; }
        [YamlIgnore]
        public bool HasInheritedValues { get { return InheritedValues != null; } }

        public string Uri { get; set; }
        [YamlIgnore]
        public bool HasUri { get { return !string.IsNullOrWhiteSpace( Uri ); } }

        public object Values { get; set; }
        [YamlIgnore]
        public bool HasValues { get { return Values != null; } }

        public List<DynamicValue> Dynamic { get; set; }
        [YamlIgnore]
        public bool HasDynamic { get { return Dynamic != null && Dynamic.Count > 0; } }

        [YamlIgnore]
        public object ResolvedValues { get; set; }
        [YamlIgnore]
        public string ResolvedValuesSerialized { get; set; }

        object ICloneable.Clone()
        {
            return Clone( true );
        }

        public ParameterInfo Clone(bool shallow = true)
        {
            return (ParameterInfo)MemberwiseClone();
        }
    }
}