using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace MentorSpeedDatingApp.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ShowNoGoDatesViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ShowNoGoDatesViewModel NoGoVM => ServiceLocator.Current.GetInstance<ShowNoGoDatesViewModel>();

        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Unregister<ShowNoGoDatesViewModel>();
        }
    }
}