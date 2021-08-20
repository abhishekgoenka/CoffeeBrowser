using CoffeeBrowser.DataProvider;
using CoffeeBrowser.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CoffeeBrowser.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    private readonly ICoffeeDataProvider _coffeeDataProvider;
    private Coffee _selectedCoffee;

    public MainViewModel(ICoffeeDataProvider coffeeDataProvider)
    {
      _coffeeDataProvider = coffeeDataProvider;
    }

    public Coffee SelectedCoffee
    {
      get { return _selectedCoffee; }
      set
      {
        if (_selectedCoffee != value)
        {
          _selectedCoffee = value;
          RaisePropertyChanged();
        }
      }
    }

    public ObservableCollection<Coffee> Coffees { get; } = new ObservableCollection<Coffee>();

    public async Task Load()
    {
      Coffees.Clear();
      var coffees = await _coffeeDataProvider.LoadCoffees();
      foreach (var coffee in coffees)
      {
        Coffees.Add(coffee);
      }

      SelectedCoffee = Coffees.FirstOrDefault();
    }

    public void Next()
    {
      if (Coffees.Any())
      {
        if (SelectedCoffee == null)
        {
          SelectedCoffee = Coffees.First();
        }
        else
        {
          var index = Coffees.IndexOf(SelectedCoffee) + 1;

          if (index > Coffees.Count - 1)
          {
            index = 0;
          }

          SelectedCoffee = Coffees[index];
        }
      }
    }

    public void Previous()
    {
      if (Coffees.Any())
      {
        if (SelectedCoffee == null)
        {
          SelectedCoffee = Coffees.Last();
        }
        else
        {
          var index = Coffees.IndexOf(SelectedCoffee) - 1;

          if (index < 0)
          {
            index = Coffees.Count - 1;
          }

          SelectedCoffee = Coffees[index];
        }
      }
    }
  }
}
