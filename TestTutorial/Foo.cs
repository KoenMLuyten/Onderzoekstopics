using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTutorial
{
    class Foo
    {
        public Foo(){

        }

        public string Bar()
        {
            return "bar";
        }

        public string FooBar(string foo, string bar)
        {
            if (foo.Equals("") || bar.Equals(""))
            {
                return foo + foo + bar + bar;
            }
            else
            {
                return foo + bar;
            }
            
        }
    }
}
