# 📌 Answers to Technical Questions

## 1️⃣ How long did you spend on the coding assignment?
> **Answer:**  
I spent about 4 hours on this task. If I had more time, I would improve the front-end by adding features like live autocomplete with suggestions, authentication and authorization for the backend, and database integration to store request details and manage currencies.

## 2️⃣ What was the most useful feature that was added to the latest version of your language of choice?
> **Answer:**  
In C# 12 and .NET 9.0, I used Primary Constructors, Collection Expressions, and Per-Request HttpClient Timeout Handling. 

### Code Snippet:
```csharp
// Primary Constructor (C# 12)
public class CryptoService(HttpClient httpClient) : ICryptoService
{
    private readonly HttpClient _httpClient = httpClient;
}

// Collection Expressions (C# 12)
private readonly List<string> _currencies = ["USD", "EUR", "BRL", "GBP", "AUD"];

// HttpClient Timeout Handling (New in .NET 9)
var request = new HttpRequestMessage(HttpMethod.Get, cryptoUrl);
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)); // Set timeout
var response = await _httpClient.SendAsync(request, cts.Token);
```

## 3️⃣ How would you track down a performance issue in production? Have you ever had to do this?
> **Answer:**  
To track performance issues, I would:
- Use profiling tools in Visual Studio to analyze memory and CPU usage.
- Implement logging in both the frontend and backend for better debugging.
- Monitor real-time performance using tools like Azure Application Insights or Elastic APM.
No, I haven't used .NET monitoring tools extensively, but I am familiar with them, and I know that Azure Application Insights and Elastic APM are excellent tools for tracking performance issues in .NET applications.nd Elastic APM to monitor performance.

## 4️⃣ What was the latest technical book you have read or tech conference you have been to? What did you learn?
> **Answer:**
These days, I don’t have much time to read books, but I actively read technical articles about:
- iOS programming
- Android KMP (Kotlin Multiplatform)
- Best practices for .NET Web APIs
These articles help me stay up to date with the latest trends in software development.

## 5️⃣ What do you think about this technical assessment?
> **Answer:**  
I find coding tasks very interesting, and I enjoy challenges.
Currently, I am not working as a developer, so I don't face many coding challenges in my daily work. However, as a Technical Leader, I deal with complex team problems where my colleagues face difficulties, and I help them find solutions.
 
## 6️⃣ Please, describe yourself using JSON.
```json
{
  "name": "Mehrdad Mohajer",
  "role": "Development Team Lead",
  "skills": ["C#", "ASP.NET Core", "Java", "React", "Kotlin", "SQL", "Android", "Team Leadership"],
  "experience": "15 years",
  "hobbies": ["Coding", "Traveling", "Gaming"],
  "location": "Tehran, Iran"
}
```

---
**🎯 Thank you for reviewing my answers!** 🚀
