﻿/*
  Copyright (c) 2011+, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  
*/

using System;
using Hl7.Fhir.Introspection;
using System.Runtime.Serialization;
using Hl7.Fhir.Specification;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Primitive Type instant
    /// </summary>
    /// 
#if !NETSTANDARD1_1
    [Serializable]
#endif
    [System.Diagnostics.DebuggerDisplay(@"\{Value={Value}}")]
    [FhirType("instant")]
    [DataContract]
    public partial class Instant : Primitive<DateTimeOffset?>, INullableValue<DateTimeOffset>
    {
        [NotMapped]
        public override string TypeName { get { return "instant"; } }
        
        // Must conform to the pattern "([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])T([01][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9]|60)(\.[0-9]+)?(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))"
        public const string PATTERN = @"([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])T([01][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9]|60)(\.[0-9]+)?(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))";

		public Instant(DateTimeOffset? value)
		{
			Value = value;
		}

		public Instant(): this((DateTimeOffset?)null) {}

        /// <summary>
        /// Primitive value of the element
        /// </summary>
        [FhirElement("value", IsPrimitiveValue=true, XmlSerialization=XmlRepresentation.XmlAttr, InSummary=true, Order=30)]
        [DataMember]
        public DateTimeOffset? Value
        {
            get { return (DateTimeOffset?)ObjectValue; }
            set { ObjectValue = value; OnPropertyChanged("Value"); }
        }

        public static bool IsValidValue(string value)
        {
            return Regex.IsMatch(value as string, "^" + PATTERN + "$", RegexOptions.Singleline);

            //TODO: Additional checks not implementable by the regex
        }

        public static Instant FromLocalDateTime(int year, int month, int day,
                    int hour, int min, int sec, int millis = 0)
        {
            return new Instant(new DateTimeOffset(year, month, day, hour, min, sec, millis,
                            DateTimeOffset.Now.Offset));
        }


        public static Instant FromDateTimeUtc(int year, int month, int day,
                                            int hour, int min, int sec, int millis = 0)
        {
            return new Instant(new DateTimeOffset(year, month, day, hour, min, sec, millis,
                                   TimeSpan.Zero));
        }

        public static Instant Now()
        {
            return new Instant(DateTimeOffset.Now);
        }

        public Primitives.PartialDateTime? ToPartialDateTime() => 
            Value != null ? (Primitives.PartialDateTime?)Primitives.PartialDateTime.FromDateTimeOffset(Value.Value) : null;

    }
}
