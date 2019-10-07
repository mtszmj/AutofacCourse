using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.Metadata;

namespace AutofacCourse.S07
{
    class L46_Adapters
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var b = new ContainerBuilder();
            b.RegisterType<SaveCommand>().As<ICommand>().WithMetadata("Name", "SAVE");
            b.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("Name", "OPEN");
            //b.RegisterType<Button>();
            //b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));
            b.RegisterAdapter<Meta<ICommand>, Button>(cmd => new Button(cmd.Value, (string) cmd.Metadata["Name"]));
            b.RegisterType<Editor>();

            using (var c = b.Build())
            {
                var editor = c.Resolve<Editor>();
                editor.ClickAll();
                foreach (var btn in editor.Buttons)
                {
                    btn.PrintMe();
                }

                //c.Resolve<Button>().PrintMe();
            }
        }
    }

    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand {
        public void Execute()
        {
            Console.WriteLine("Saving a file");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Opening a file");
        }
    }

    public class Button
    {
        private ICommand command;
        private string name;

        public Button(ICommand command, string name)
        {
            this.command = command ?? throw new ArgumentNullException(nameof(command));
            this.name = name;
        }

        public void Click()
        {
            command.Execute();
        }

        public void PrintMe()
        {
            Console.WriteLine($"I am the button with the name {name}");
        }
    }

    public class Editor
    {
        private readonly IEnumerable<Button> buttons;

        public IEnumerable<Button> Buttons => buttons;

        public Editor(IEnumerable<Button> buttons)
        {
            this.buttons = buttons ?? throw new ArgumentNullException(nameof(buttons));
        }

        public void ClickAll()
        {
            foreach (var button in buttons)
            {
                button.Click();
            }
        }
    }
}
