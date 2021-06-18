using System;
using System.Collections.Generic;
using System.Text;

namespace BigMath.ActivationFunctions
{
    public interface IActivationFunction
    {
        float Activate(float x);

        float Deactivate(float y);
    }
}
