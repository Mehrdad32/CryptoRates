# 🚀 CryptoRates - Cryptocurrency Exchange Rate Viewer

CryptoRates is an ASP.NET Core MVC application that allows users to fetch real-time cryptocurrency exchange rates and convert them into different fiat currencies (USD, EUR, BRL, GBP, AUD). It integrates with **CoinMarketCap API** and **ExchangeRatesAPI**.

---

## 📋 Features
✅ Fetches real-time cryptocurrency exchange rates  
✅ Converts cryptocurrency prices to multiple currencies (EUR, USD, BRL, GBP, AUD)  
✅ Caches search history in the browser for quick access  
✅ Handles invalid inputs gracefully and suggests similar cryptocurrency names  
✅ Fully tested with **xUnit** and **Moq** for unit testing  

---

## 🛠️ Installation & Setup

### **1️⃣ Clone the Repository**
```bash
git clone https://github.com/Mehrdad32/CryptoRates.git
cd CryptoRates
```

### **2️⃣ Install Dependencies**
```bash
dotnet restore
```

### **3️⃣ Set Up API Keys**
Create a **`appsettings.json`** file in the root of the project (if not already created) and add your API keys:
```json
{
  "CoinMarketCapApiKey": "YOUR_COINMARKETCAP_API_KEY",
  "ExchangeRatesApiKey": "YOUR_EXCHANGERATES_API_KEY"
}
```

### **4️⃣ Run the Application**
```bash
dotnet run
```
Once the application is running, open your browser and navigate to:
```
http://localhost:5000/
```

---

## 🚦 Usage Guide
1️⃣ Enter a cryptocurrency code (e.g., `BTC`, `ETH`) in the input field.  
2️⃣ Click "Get Rates" to fetch exchange rates in multiple currencies.  
3️⃣ The system will store your searched items in **localStorage** for autocomplete.  
4️⃣ If an invalid cryptocurrency is entered, the system will suggest similar cryptocurrencies.

---

## 🧪 Running Tests
CryptoRates includes **unit tests** for `CryptoService` and `CryptoController`.

### **1️⃣ Run All Tests**
```bash
dotnet test
```

### **2️⃣ Test Coverage**
| Component       | Test Cases | Status |
|----------------|-----------|--------|
| CryptoService  | ✅ API Calls, Conversion Logic, Error Handling | ✅ Passed |
| CryptoController | ✅ Valid/Invalid Inputs, Error Responses | ✅ Passed |

---

## 🔍 API References
The project uses the following APIs:

1. **[CoinMarketCap API](https://coinmarketcap.com/api/)**
   - Fetches real-time cryptocurrency prices.
2. **[ExchangeRatesAPI.io](https://exchangeratesapi.io/)**
   - Converts cryptocurrency values to different fiat currencies.

---

## 👨‍💻 Contributing
If you'd like to contribute to **CryptoRates**, follow these steps:

1. **Fork** the repository.
2. **Create** a new branch (`feature-new-functionality`).
3. **Commit** your changes.
4. **Push** the changes to your branch.
5. **Create** a Pull Request.

---

## 📜 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 💡 Future Improvements
🚀 Add a dark mode for UI  
🚀 Implement a caching mechanism to reduce API calls  
🚀 Add historical cryptocurrency price trends  

---

## 📞 Contact
📧 **Mehrdad Mohajer** - [mohajer.mhr@example.com](mailto:mohajer.mhr@gmail.com)  
🔗 **GitHub** - [github.com/Mehrdad32](https://github.com/Mehrdad32)  
🔗 **LinkedIn** - [linkedin.com/in/Mehrdad32](https://linkedin.com/in/Mehrdad32)

---

### 🎉 **Thank You for Using CryptoRates! Happy Coding!** 🚀
