﻿using System;
using System.Collections.Generic;

namespace Synapse.Core
{
    public class ParentExitDataValue
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public SerializationType SourceType { get; set; }
        public string Destination { get; set; }
        public string Replace { get; set; }
        public string Encode { get; set; }
        public bool CastToForEachValues { get; set; }

        public override string ToString()
        {
            return $"[{Name}]::[{Source}]::[{SourceType}]::[{Destination}]::[{Replace}]::[{Encode}]::[{CastToForEachValues}]";
        }


        public static ParentExitDataValue CreateSample()
        {
            ParentExitDataValue dv = new ParentExitDataValue()
            {
                Name = "URI parameter name",
                Source = "Element:IndexedElement[0]:Element",
                SourceType = SerializationType.Yaml,
                Destination = "Element:IndexedElement[0]:Element",
                Replace = "Regex expression",
                Encode = "Base64"
            };

            return dv;
        }
    }
}