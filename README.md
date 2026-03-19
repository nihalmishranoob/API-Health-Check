A lightweight ASP.NET Core MVC web application that lets you monitor the health of any API endpoint in real time. Enter an endpoint URL, hit Run Health Check, and instantly get a dashboard showing:

✅ HTTP Status Code — checks if the response is in the 2xx success range
⚡ Response Latency — measures end-to-end response time with a 500ms threshold
🔍 JSON Validity — validates the response body if the endpoint returns application/json

All timestamps are displayed in IST (Indian Standard Time).

Tech Stack used are ASP.NET Core 8 MVC (Razor Views) and C# 12

Steps to run :
# 1. Clone the repository
  git clone https://github.com/your-username/ApiHealthDashboard.git

# 2. Navigate into the project
  cd ApiHealthDashboard

# 3. Run the app
  dotnet run

# 4. Open in your browser
  http://localhost:5000
