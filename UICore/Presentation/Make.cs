using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UICore.Presentation
{
    public static class Make
    {
        public static class UICommand<T>
        {
            /// <summary>
            /// Specify a condition under which the command can be executed. Controls bound to the
            /// command will only be enabled when this condition is true. The condition is a lambda
            /// taking no parameters and returning a boolean. The syntax looks like: () => SelectedThing != null
            /// </summary>
            /// <param name="condition">A lambda expression taking no parameters and returing a boolean: () => SelectedThing != null</param>
            /// <returns>An object that you can add .Do to.</returns>
            public static When<T> When(Predicate<T> condition)
            {
                return new When<T>((o) => condition(o));
            }

            /// <summary>
            /// Specify an action to execute when the command is invoked. The action is a lambda
            /// taking no parameters and performing a statement. The syntax looks like: () => { DoSomething(); }
            /// </summary>
            /// <param name="execute">A lambda expression taking no parameters and performing a statement. The
            /// syntax looks like: () => { DoSomething(); }</param>
            /// <returns>A command that does that.</returns>

            public static ICommand Do(Action<T> execute)
            {
                return new Command<T>((p) => true, execute);
            }

            public static void Refresh(ICommand command)
            {
                var cmd = command as Command<T>;
                if (cmd != null) cmd.Refresh();
            }
        }
        public static class UICommand
        {

            /// <summary>
            /// Specify a condition under which the command can be executed. Controls bound to the
            /// command will only be enabled when this condition is true. The condition is a lambda
            /// taking no parameters and returning a boolean. The syntax looks like: () => SelectedThing != null
            /// </summary>
            /// <param name="condition">A lambda expression taking no parameters and returing a boolean: () => SelectedThing != null</param>
            /// <returns>An object that you can add .Do to.</returns>

            public static When<object> When(Func<bool> condition)
            {
                return new When<object>((o) => condition());
            }
            /// <summary>
            /// Specify an action to execute when the command is invoked. The action is a lambda
            /// taking no parameters and performing a statement. The syntax looks like: () => { DoSomething(); }
            /// </summary>
            /// <param name="execute">A lambda expression taking no parameters and performing a statement. The
            /// syntax looks like: () => { DoSomething(); }</param>
            /// <returns>A command that does that.</returns>
            public static ICommand Do(Action execute)
            {
                return new Command<object>((p) => true, (p) => execute());
            }

            public static void Refresh(ICommand command)
            {
                var cmd = command as Command<object>;
                cmd?.Refresh();
            }

            public static ICommand Do<T>(Action<T> execute)
            {
                return new Command<T>((p) => true, (p) => execute(p));
            }
        }





        public class When<T>
        {
            private Predicate<T> _canExecute;

            public When(Predicate<T> canExecute)
            {
                _canExecute = canExecute;
            }


            /// <summary>
            /// Specify an action to execute when the command is invoked. The action is a lambda
            /// taking no parameters and performing a statement. The syntax looks like: () => { DoSomething(); }
            /// </summary>
            /// <param name="execute">A lambda expression taking no parameters and performing a statement. The
            /// syntax looks like: () => { DoSomething(); }</param>
            /// <returns>A command that does that.</returns>
            public ICommand Do(Action<T> execute)
            {
                return new Command<T>(_canExecute, execute);
            }

            public ICommand Do(Action execute)
            {
                return new Command<T>(_canExecute, (t) => execute());
            }
        }
        class Command<T> : ICommand
        {


            // The condition under which it can execute, and the action to execute.
            private Predicate<T> _canExecuteFunction;
            private Action<T> _execute;



            // A dependent flag, true when the command can be executed.
            //private bool _canExecute = false;


            public Command(Predicate<T> canExecute, Action<T> execute)
            {
                _canExecuteFunction = canExecute;
                _execute = execute;


            }

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                // Just returning the flag. The flag gets set elsewhere.
                return _canExecuteFunction == null ? true : _canExecuteFunction((T)parameter);
            }

            public void Execute(object parameter)
            {


                try
                {
                    // Execute the command.
                    _execute((T)parameter);
                }
                finally
                {

                }
            }



            public void Refresh()
            {


                // Now that it is up-to-date again, we need to notify anybody bound
                // to this command that the flag has changed.
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, new EventArgs());
            }
        }


        public static T? FindElementByName<T>(this FrameworkElement element, string sChildName) where T : FrameworkElement
        {
            

            T? childElement = null;

            if (element is ContentPresenter cp && (cp.Content as T)?.Name == sChildName) return cp.Content as T;
            

            
            //
            // Spin through immediate children of the starting element.
            //
            var nChildCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < nChildCount; i++)
            {
                // Get next child element.
                FrameworkElement? child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                

                // Do we have a child?
                if (child == null)
                    continue;

                // Is child of desired type and name?
                if (child is T && child.Name.Equals(sChildName))
                {
                    // Bingo! We found a match.
                    childElement = (T)child;
                   
                    break;
                } // if

                // Recurse and search through this child's descendants.
                childElement = FindElementByName<T>(child, sChildName);

                // Did we find a matching child?
                if (childElement != null)
                    break;
            } // for

            
            return childElement;
        }
    }
}
