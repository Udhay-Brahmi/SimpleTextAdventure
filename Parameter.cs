using System;

namespace SimpleTextAdventure
{
    class Parameter
    {
        public ParameterType type;
        public string stringParameter;
        public Direction directionParameter;
        public Zone zoneParameter;
        public Item itemParameter;

        public Parameter(string stringParameter)
        {
            type = ParameterType.String;
            this.stringParameter = stringParameter;
        }

        public Parameter(Direction directionParameter)
        {
            type = ParameterType.Direction;
            this.directionParameter = directionParameter;
        }

        public Parameter(Zone zoneParameter)
        {
            type = ParameterType.Zone;
            this.zoneParameter = zoneParameter;
        }

        public Parameter(Item itemParameter)
        {
            type = ParameterType.Item;
            this.itemParameter = itemParameter;
        }
    }
}
