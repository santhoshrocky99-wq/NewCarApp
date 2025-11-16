# Car Cleanz - ASP.NET Core Starter

This is a starter ASP.NET Core MVC project for **Car Cleanz** (red & black theme) with:

- Booking form (POSTs to Supabase REST API)
- Admin page (displays raw JSON of bookings from Supabase)
- Animated SVG logo and theme assets

## What you must do after downloading

1. Install .NET 8 SDK (or change TargetFramework in the .csproj).
2. Open the project folder in VS Code or Visual Studio.
3. In `appsettings.json`, set your Supabase `Url` and `Key`.
   - Example Supabase REST endpoint base: `https://yourproj.supabase.co/rest/v1/`
   - Create a `Bookings` table in Supabase with columns:
     - `id` (serial, primary key)
     - `name` (text)
     - `phone` (text)
     - `car_type` (text)
     - `service_date` (date)
     - `address` (text)
     - `created_at` (timestamp with time zone)
4. Run:
   ```
   dotnet restore
   dotnet run
   ```
5. Deploy to Render or Azure:
   - Connect your GitHub repo to Render and select the project.
   - Set environment variables if you don't want to commit keys.

## Notes & Next steps

- This starter uses Supabase REST API via server-side HttpClient.
- Admin page currently returns raw JSON (easy to convert to a table).
- Add authentication for Admin before production.
- Replace `wwwroot/images/qr-placeholder.png` with your real payment QR.

If you want, I can:
- Convert Admin JSON into a nice table.
- Add authentication for Admin.
- Produce a Git-ready commit and instructions to deploy to Render (free).
