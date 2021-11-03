using System.Reflection;

namespace Mary.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var dog = new Dog();
			var animal = new Animal();
			Animal animalMaybe = new Dog();

			dog.Death();
			animal.Death();
			animalMaybe.Death();
			/*
			 *Мета 
			 *dog => Dog
			 *animal => Animal
			 *animalMaybe => Animal
			 *
			 *Память
			 *dog => Dog
			 *animal => Animal
			 *animalMaybe => Dog
			 */
		}

	}

	public class Animal
	{
		public Animal()
		{
			Birthday = DateTime.Now;
			Birth();
		}

		public DateTime Birthday { get; }

		private void Birth()
		{
			Console.WriteLine($"Я {GetType().Name} родился");
		}

		public virtual void Death()
        {
			Console.WriteLine($"FUUUUUUUUUCCCCCCK");
        }
	}

	public class Dog : Animal
	{
		public Dog() : base()
		{

		}

		public void Eat()
        {
			Console.WriteLine($"Я нашёл на полу гавно и ем");
        }

		public void Eat(string food)
		{
			Console.WriteLine($"Мне дали {food}, но я ем гавно с пола");
		}

        public override void Death()
        {
			Console.WriteLine($"Ой то есть ГААААААААВ!");
        }
    }
}



