using System;

namespace SimpleTextAdventure
{
    class Parameter
    {
        public ParameterType type;
        public string stringParameter;
        public Direction directionParameter;

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
    }
}
