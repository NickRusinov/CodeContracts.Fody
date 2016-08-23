using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public interface InterfaceWithProperties
    {
        byte PropertyWithoutAll { get; set; }

        [Range(0, 500)]
        byte PropertyWithRange0And500 { get; set; }
        
        [Range(100uL, 200uL)]
        byte PropertyWithRange100And200GetOnly { get; }

        [Range(100u, 200u)]
        byte PropertyWithRange100And200SetOnly { set; }
    }

    public class InterfaceWithPropertiesImplementation : InterfaceWithProperties
    {
        public byte PropertyWithoutAll { get; set; }

        public byte PropertyWithRange0And500 { get; set; }

        public byte FieldWithRange100And200GetOnly;
        public byte PropertyWithRange100And200GetOnly
        {
            get { return FieldWithRange100And200GetOnly; }
        }

        public byte FieldWithRange100And200SetOnly;
        public byte PropertyWithRange100And200SetOnly
        {
            set { FieldWithRange100And200SetOnly = value; }
        }
    }
}
