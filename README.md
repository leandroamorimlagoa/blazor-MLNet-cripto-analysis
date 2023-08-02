# Blazor-MLNet-Crypto-analysis

Application in Blazor .NET 7 with ML.NET 7 used to analyze and invest cryptocurrencies on the foxbit.com.br platform

# DO NOT USE THIS APPLICATION TO INVEST IN CRYPTOCURRENCIES

## This is a proof of concept project
The objective is to integrate with the foxbit.com.br platform to get Crypto stocks values and create buy and sell orders.  
This project is a proof of concept and is not intended to be used for actual trading.  
The code is not optimized for speed or accuracy.  
The code is not optimized for memory usage.  
The code is not optimized for security.  
The code is not optimized for anything.  

# Current Status
Application is still in development, but you can already use it to analyze the price of cryptocurrencies and invest in them.  
The applied analysis is quite simple, but it is already possible to see some results.  
There is a project in python used to train models used to predict the price of cryptocurrencies.   
You can check the Neural Network using Python and TensorFlow used to train a model for each specific crypto. here:  
- https://github.com/leandroamorimlagoa/python-tensorflow-crypto-prediction


# Technologies used
- Blazor .NET 7  
- ML.NET 7  
- Bootstrap 5  
- Entity Framework Core 7  
- MySQL  
- Docker  

# Next steps
Today we have a very simple analysis method, but the objective is to integrate ML.NET with TensorFlow to create a more robust analysis model.  
The idea is to use the data from the foxbit.com.br platform to train the model and then use it to predict the price of cryptocurrencies.  
Based on the prediction, the application will DO THE INVESTMENTS to buy or sell the crypto.  

## For the immediate future
- Integrate with TensorFlow with ML.NET  
- Create a dashboard to show the results of the analysis and Results  
- Unit and integration tests should be add  

# Relevant information
When the user register a new account, some parameters are required to be filled in. These parameters are used to integrate with foxbit.com.br and also used to do the calculations to buy and sell the cryptos.  
There is also a background service that runs every 5 minutes to update the buy and sell orders status.  
Each buy and sell compose a cycle of investments. 
The application will only do a new investment when the previous cycle is finished.  


# How to use
1. Clone the repository  
1. Open the solution in Visual Studio 2022  
1. Run the project  
1. Access the application in the browser at the address: `https://localhost:5001`  
  
# Credits
Developer: Leandro Amorim Lagoa  
Email: leamorim@outlook.com  
Linkedin: https://www.linkedin.com/in/leandrolagoa/
