using CoffeeBrowser.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoffeeBrowser.DataProvider
{
  public interface ICoffeeDataProvider
  {
    Task<IEnumerable<Coffee>> LoadCoffees();
  }

  public class CoffeeInMemoryDataProvider : ICoffeeDataProvider
  {
    public async Task<IEnumerable<Coffee>> LoadCoffees()
    {
      await Task.Delay(100); // Simulate a bit of server work

      return new[]
      {
        new Coffee{Name="Cappuccino",Description="Espresso with streamed milk and with milk foam"},
        new Coffee{Name="Doppio",Description="Double espresso"},
        new Coffee{Name="Espresso",Description="Pure coffee to keep you awake! :-)"},
        new Coffee{Name="Latte",Description="Cappuccino with more streamed milk"}
      };
    }
  }

  public class CoffeeWebApiDataProvider : ICoffeeDataProvider
  {
    private static readonly HttpClient _client = new HttpClient();

    public async Task<IEnumerable<Coffee>> LoadCoffees()
    {
      var json = await _client.GetStringAsync(
        "https://thomasclaudiushuber.com/pluralsight/coffees.json");
      return JsonConvert.DeserializeObject<IEnumerable<Coffee>>(json);
    }
  }
}
