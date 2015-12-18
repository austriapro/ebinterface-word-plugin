using System.Globalization;
using System.Windows.Data;
using System.Windows.Forms;
using Binding = System.Windows.Forms.Binding;

namespace WinFormsMvvm.ConverterExtension
{
    public static class ConverterExtensions
    {
        public static ConverterBinding Bind(this IValueConverter converter, Control control, Binding binding)
        {
            return new ConverterBinding(converter, control, binding);
        }

        public static ConverterBinding Bind(this IValueConverter converter, Control control, 
                                            string propertyName, object dataSource, string dataMember)
        {
            return new ConverterBinding(converter, control, new Binding(propertyName, dataSource, dataMember));
        }
    }

    public sealed class ConverterBinding
    {
        public IValueConverter ValueConverter { get; private set; }
        public Control Control { get; private set; }
        public Binding Binding { get; private set; }

        public ConverterBinding(IValueConverter valueConverter, Control control, Binding binding)
        {
            ValueConverter = valueConverter;
            Control = control;
            Binding = binding;

            Binding.Format += Convert;
            Binding.Parse += ConvertBack;
            Control.DataBindings.Add(Binding);
        }

        public void Unbind()
        {
            Binding.Format -= Convert;
            Binding.Parse -= ConvertBack;
            Control.DataBindings.Remove(Binding);
        }

        /// <summary>
        /// Used to convert the value produced by the binding source to a value assignable
        /// to the binding target.
        /// </summary>
        private void Convert(object sender, ConvertEventArgs args)
        {
            args.Value = ValueConverter.Convert(args.Value, args.DesiredType, null, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Used to convert the value that is produced by the binding target to a type
        /// assignable to the binding source.
        /// </summary>
        private void ConvertBack(object sender, ConvertEventArgs args)
        {
            args.Value = ValueConverter.ConvertBack(args.Value, args.DesiredType, null, CultureInfo.CurrentCulture);
        }
    }
}
