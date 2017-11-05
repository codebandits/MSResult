using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Result.Test
{
    public class TestHelper
    {
        public static void ShouldThrow<T>(Action func) where T : Exception
        {
            try
            {
                func.Invoke();
                throw new AssertFailedException(
                    String.Format("An exception of type {0} was expected, but not thrown", typeof(T))
                    );
            }
            catch (T) { }
            catch (AssertFailedException) {}
            catch(Exception e)
            { throw new AssertFailedException(
                String.Format("An exception of type {0} was expected, but throw {1}", typeof(T), e)
                    );
            }
        }
    }
}
