using System;

namespace ChainedEvents
{
    // define the delegate for the event handler
    public delegate void myEventHandler(string value);

    class MyClass
    {
        private string theVal;
        // declare the event handler
        public event myEventHandler ValueChanged;
        public event EventHandler<ObjChangeEventArgs> ObjChanged;

        public string Val
        {
            set
            {
                this.theVal = value;
                // when the value changes, fire the event
                this.ValueChanged(theVal);
                this.ObjChanged(this, new ObjChangeEventArgs() { propChanged = "Val" });
            }
        }
    }

    class ObjChangeEventArgs : EventArgs
    {
        public string propChanged;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // create the test class
            MyClass obj = new MyClass();
            // Connect multiple event handlers
            obj.ValueChanged += ChangeListener1;
            obj.ValueChanged += ChangeListener2;

            // Use an anonymous delegate as the event handler
            obj.ValueChanged += delegate(string s) {
                Console.WriteLine("This came from the anonymous handler!");
            };

            obj.ObjChanged += delegate(object sender, ObjChangeEventArgs e) {
                Console.WriteLine("{0} had the '{1}' property changed", sender.GetType(), e.propChanged);
            };

            string str;
            do {
                Console.WriteLine("Enter a value: ");
                str = Console.ReadLine();
                if (!str.Equals("exit")) {
                    obj.Val = str;
                }
            } while (!str.Equals("exit"));
            Console.WriteLine("Goodbye!");
        }

        static void ChangeListener1(string value)
        {
            Console.WriteLine("The value changed to {0}", value);
        }
        static void ChangeListener2(string value)
        {
            Console.WriteLine("I also listen to the event, and got {0}", value);
        }
    }
}
