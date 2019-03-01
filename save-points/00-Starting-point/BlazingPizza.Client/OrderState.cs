using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client
{
    public class OrderState
    {

        public Pizza ConfiguringPizza { get; set; }
        public bool ShowingConfigureDialog { get; set; }
        public Order Order { get; set; } = new Order();

        public event EventHandler StateChanged;

        void StateHasChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>(),
            };

            ShowingConfigureDialog = true;
        }


        public void CancelConfigurePizzaDialog()
        {
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;

            StateHasChanged();
        }

        public void ConfirmConfigurePizzaDialog()
        {
            Order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;

            ShowingConfigureDialog = false;

            StateHasChanged();
        }

        public void ResetOrder()
        {
            Order = new Order();
        }

        public void AddTopping(Topping topping)
        {
            if (ConfiguringPizza.Toppings.Find(pt => pt.Topping == topping) == null)
            {
                ConfiguringPizza.Toppings.Add(new PizzaTopping() { Topping = topping });
            }
        }

        public void RemoveConfiguredPizza(Pizza pizza)
        {
            Order.Pizzas.Remove(pizza);

            StateHasChanged();
        }

        public void RemoveTopping(Topping topping)
        {
            ConfiguringPizza.Toppings.RemoveAll(pt => pt.Topping == topping);
        }
    }
}
