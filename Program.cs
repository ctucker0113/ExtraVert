// See https://aka.ms/new-console-template for more information
using System.Diagnostics.Metrics;
using ExtraVert;
using static System.Runtime.InteropServices.JavaScript.JSType;

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "Fern",
        LightNeeds = 1,
        AskingPrice = 10.00M,
        City = "New York",
        ZipCode = 10001,
        Sold = true,
        AvailableUntil = new DateTime (2024, 2, 17)
    },

    new Plant()
    {
        Species = "Succulent",
        LightNeeds = 5,
        AskingPrice = 5.00M,
        City = "Los Angeles",
        ZipCode =  90001,
        Sold = false,
        AvailableUntil = new DateTime(2023, 12, 5)
    },

    new Plant()
    {
        Species = "Orchid",
        LightNeeds = 3,
        AskingPrice = 7.00M,
        City = "Houston",
        ZipCode = 77001,
        Sold = false,
        AvailableUntil = new DateTime(2024, 3, 17)

    },

    new Plant()
    {
        Species = "Conifer",
        LightNeeds = 4,
        AskingPrice = 20.00M,
        City = "Chicago",
        ZipCode = 60601,
        Sold = false,
        AvailableUntil = new DateTime(2024, 6, 12)
    },

    new Plant()
    {
        Species = "Daffodil",
        LightNeeds = 3,
        AskingPrice = 35.00M,
        City = "Phoenix",
        ZipCode = 85001,
        Sold = true,
        AvailableUntil = new DateTime(2023, 11, 28)
    }
};

Plant plantOfTheDay = generatePlantOfTheDay(plants);

while (true)
{
    Console.WriteLine(@"Bonjour!
Please choose an option below:
a. Display all plants
b. Post a plant to be adopted
c. Adopt a Plant
d. Delist a Plant
e. See the Plant of the Day!
f. Search Plants
g. View App Statistics
h. Exit Program");

    string userMenuChoice = Console.ReadLine().Trim().ToLower();

    if (userMenuChoice == "a")
    {
        Console.WriteLine("You chose 'Display All Plants'");
        DisplayAllPlants(plants);
    }
    else if (userMenuChoice == "b")
    {
        Console.WriteLine("You selected 'Add a Plant'");
        AddAPlant(plants);
    }

    else if (userMenuChoice == "c")
    {
        Console.WriteLine("You selected 'Adopt a Plant'");
        AdoptAPlant(plants);
    }

    else if (userMenuChoice == "d")
    {
        Console.WriteLine("You selected 'Delist a Plant'");
        DelistAPlant(plants);

    }
    else if (userMenuChoice == "e")
    {
        Console.WriteLine($"Today's plant of the day is {plantOfTheDay.Species}. It is located in {plantOfTheDay.City}, has a light need of {plantOfTheDay.LightNeeds}, and costs {plantOfTheDay.AskingPrice} dollars.");
    }

    else if (userMenuChoice == "f")
    {
        Console.WriteLine("You have selected 'Search for a Plant'");
        SearchPlantsByLightNeeds(plants);
    }

    else if (userMenuChoice == "g")
    {
        Console.WriteLine("You have selected 'View App Statistics'");
        DisplayStatistics(plants);
    }

    else if (userMenuChoice == "h")
    {
        Console.WriteLine("You have chosen to leave this simulation, and re-enter the real world. Farewell!");
        break;
    }

    else
    {
        Console.Clear();
        Console.WriteLine("Invalid Option. Please try again.");
        // The loop will continue, asking for input again
    }
}

static void DisplayAllPlants(List<Plant> plants)
{
    for (int i = 0; i < plants.Count; i++)
    {
        string availability = plants[i].Sold ? "was sold" : "is available";
        Console.WriteLine($"{i + 1}. A {plants[i].Species} in {plants[i].City} {availability} for ${plants[i].AskingPrice}");
    }
}

static void AddAPlant(List<Plant> plants)
{
    Console.WriteLine("Please enter a Plant Name");
    string plantName = Console.ReadLine();

    Console.WriteLine("On a scale of 1 to 5, rate this plant's sunlight needs.");
    int lightNeeds;
    while (!int.TryParse(Console.ReadLine(), out lightNeeds) || lightNeeds < 1 || lightNeeds > 5)
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
    }

    Console.WriteLine("How much does this plant cost?");
    decimal price;
    while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
    {
        Console.WriteLine("Invalid input. Please enter a positive number.");
    }

    Console.WriteLine("What city is this plant located in?");
    string city = Console.ReadLine();

    Console.WriteLine("What is the city's zip code?");
    int zipcode;
    while (!int.TryParse(Console.ReadLine(), out zipcode))
    {
        Console.WriteLine("Invalid input. Please enter a valid zip code.");
    }

    //User Input for 'AvailableUntil'
    while (true)
    {
        int year;
        Console.WriteLine("Enter the plant's expiration year (4-digit)");
        // Prevent the user from entering a string for the year, store the numerical input in the variable "year."
        while (!int.TryParse(Console.ReadLine(), out year))
        {
            Console.WriteLine("Invalid output. Please enter a numerical year.");
        }
        Console.WriteLine("Enter the plant's expiration month (1 to 12)");
        int month;
        // Prevent the user from entering a string or a number greater than 12
        while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
        {
            Console.WriteLine("Invalid output. Please enter a month between 1 and 12.");
        }
        Console.WriteLine("Enter the plant's expiration day (1 to 31)");
        int day;
        // Prevent the user from entering a string or a number less than 1 or greater than 31.
        while (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > 31)
        {
            Console.WriteLine("Invalid output. Please enter a day between 1 and 31.");
        }


        DateTime expirationDate = new DateTime(year, month, day);

        // Check that the expiration date is in the future
        if (expirationDate.Date > DateTime.Now.Date)
        {
            Plant newPlant = new Plant
            {
                Species = plantName,
                LightNeeds = lightNeeds,
                AskingPrice = price,
                City = city,
                ZipCode = zipcode,
                Sold = false,
                AvailableUntil = expirationDate
            };


            plants.Add(newPlant);
            Console.WriteLine($@"You have entered a plant called {plantName} with a Light Need of {lightNeeds},
a cost of {price}, a city of {city}, with the zipcode of {zipcode} into the Plant database. The plant is available until {expirationDate}");
            break;
        }
        else
        {
            Console.WriteLine("Invalid input.The expiration date must be in the future.Please re-enter the date.");
        }
    }
}

static void AdoptAPlant(List<Plant> plants)
{
    Console.WriteLine("The following plants are available to adopt: ");
    int counter = 1;
    List<int> validIndices = new List<int>();

    DateTime currentDate = DateTime.Now;

    for (int i = 0; i < plants.Count; i++)
    {
        if (plants[i].Sold == false && plants[i].AvailableUntil >= currentDate)
        {
            Console.WriteLine($"{counter}. {plants[i].Species}. Press {i} to adopt.");
            validIndices.Add(i);
            counter++;
        }
    }

    int userAdoptionOption;
    while (true)
    {
        Console.WriteLine("Please enter the number of the plant you wish to adopt:");
        if (int.TryParse(Console.ReadLine(), out userAdoptionOption) && validIndices.Contains(userAdoptionOption))
        {
            plants[userAdoptionOption].Sold = true;
            // User has entered a valid number, break the loop
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number corresponding to the plant.");
        }
    }

}

static void DelistAPlant(List<Plant> plants)
{
    // First, check to see if there are any plants in the list.
    if (plants.Count == 0)
    {
        Console.WriteLine("There are no plants to delist.");
    }
    //Otherwise, display the list of all plants
    else
    {
        Console.WriteLine("The following plants are listed:");
        // Display all available plants by looping through the plants list.
        for (int i = 0; i < plants.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {plants[i].Species}");
        }

        Console.WriteLine("Please enter the number of the plant you wish to delist:");
        // Variable for storing user input
        int delistOption;
        while (true)
        {
            // The if statement first checks if the input is an integer. If true, it stores the integer in delistOption.
            // Then it checks if the integer is within the acceptable bounds of the length of the list.
            if (int.TryParse(Console.ReadLine(), out delistOption) && delistOption > 0 && delistOption <= plants.Count)
            {
                // The user has entered a valid number, break the loop
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number corresponding to the plant.");
            }
        }
        // Remove the selected plant. Subtract 1 from delistOption to convert from 1-based to 0-based index
        plants.RemoveAt(delistOption - 1);

        Console.WriteLine("The plant has been successfully delisted.");
    }
};

static Plant generatePlantOfTheDay(List<Plant> plants)
{
    Random randomInteger = new Random();
    while (true)
    {
        int plantOfTheDayIndex = randomInteger.Next(0, plants.Count);
        Plant potentialPlant = plants[plantOfTheDayIndex];
        if (potentialPlant.Sold == false)
        {
            return potentialPlant;
        }
    }
}

static void SearchPlantsByLightNeeds(List<Plant> plants)
{
    Console.WriteLine("Let's categorize based on light needs. On a scale of 1 to 5, what is the maximum light need you are searching for? ");
    int userInput;
    while (true)
    {
        // The if statement first checks if the input is an integer. If true, it stores the integer in delistOption.
        // Then it checks if the integer is within the acceptable bounds of the length of the list.
        if (int.TryParse(Console.ReadLine(), out userInput) && userInput > 0 && userInput <= 5)
        {
            // The user has entered a valid number, break the loop
            Console.WriteLine("Valid Input Received!");
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number corresponding to the plant.");
        }
    };
    List<Plant> filteredPlants = new List<Plant>();
    for (var i = 0; i < plants.Count; i++)
    {
        if (plants[i].LightNeeds <= userInput)
        {
            filteredPlants.Add(plants[i]);
        }
    }
    Console.WriteLine("Here are the plants that match your search criteria:");
    for (var i = 0; i < filteredPlants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {filteredPlants[i].Species}");
    }
}

static void DisplayStatistics(List<Plant> plants)
    {
        // LOWEST PRICE PLANT NAME
        // Initialize a variable with the value of the first price in the plants list so you have somewhere for the loop to start.
        decimal lowestPrice = plants[0].AskingPrice;
        int lowestPriceIndex = 0;
        for (var i = 1; i < plants.Count; i++)
        {
            if (plants[i].AskingPrice < lowestPrice)
            {
                lowestPrice = plants[i].AskingPrice;
                lowestPriceIndex = i;
            }
        }
        Console.WriteLine($"Lowest priced plant: {plants[lowestPriceIndex].Species} with a cost of {lowestPrice}");

        //NUMBER OF PLANTS AVAILABLE
        int availablePlantCount = 0;

        DateTime currentDate = DateTime.Now;
        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i].Sold == false && plants[i].AvailableUntil >= currentDate)
            {
                availablePlantCount++;
            }
        }
        Console.WriteLine($"Total Available Plants: {availablePlantCount}");

        // PLANTS WITH THE HIGHEST LIGHT NEEDS
        // Loop through the plants list to determine what the highest light need number is.
        int highestLightCount = 1;
        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i].LightNeeds == 5)
            {
                highestLightCount = 5;
                break;
            }
            else if (plants[i].LightNeeds > highestLightCount)
            {
                highestLightCount = plants[i].LightNeeds;
            }
        }
        // Loop through the plants list again and find all the plants with the matching highestLightCount value
        Console.WriteLine($"Plants with the highest light need in the database ({highestLightCount})");
        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i].LightNeeds == highestLightCount)
            {
                Console.WriteLine(plants[i].Species);
            }
        }

        // AVERAGE LIGHT NEEDS
        decimal lightNeedsTotal = 0;
        for (int i = 0; i < plants.Count; i++)
        {
            lightNeedsTotal += plants[i].LightNeeds;
        }

        Console.WriteLine($"Average Light Needs of Plants: {(lightNeedsTotal / plants.Count).ToString()}");

        // PERCENTAGE OF PLANTS ADOPTED
        decimal plantsAdopted = 0;
        for (var i = 0; i < plants.Count; i++)
        {
            if (plants[i].Sold)
            {
                plantsAdopted += 1;
            }
        }
        Console.WriteLine($"Percentage of plants adopted: {(plantsAdopted / plants.Count) * 100}%");
    }