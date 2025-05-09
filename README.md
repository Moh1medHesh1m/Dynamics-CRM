# Dynamics CRM Contact Sync 

This project is a customized nopCommerce application that integrates with **Microsoft Dynamics 365 CRM** to allow public users to submit their contact information via a form. It checks if the contact exists in Dynamics CRM before creating a new record.

---

## 📂 Project Structure

```
src/
├── Presentation/
│   └── Nop.Web/             --> Main web application
│       ├── Controllers/
│       │   └── CrmRegisterController.cs
│       ├── Services/
│       │   └── CrmIntegrationService.cs
│       ├── Views/
│       │   └── Common/
│       │       └── CrmRegister.cshtml
│       ├── appsettings.json
│       └── Startup.cs
└── ...
```

---

## 💻 Setup & Configuration

### 1️⃣ Add CRM connection string to `appsettings.json`:

```
"CrmSettings": {
    "DataverseConnectionString": "AuthType=ClientSecret;Url=<your-url>;ClientId=<your-client-id>;ClientSecret=<your-client-secret>;RequireNewInstance=true;"
}
```

⚠️ Replace `<your-url>`, `<your-client-id>`, `<your-client-secret>` with your Dynamics CRM tenant values.  
**Do NOT commit secrets to GitHub.**

---

### 2️⃣ Register Services in `Startup.cs`

```csharp
services.Configure<CrmSettings>(configuration.GetSection("CrmSettings"));
services.AddSingleton<ICrmIntegrationService, CrmIntegrationService>();
```

This uses **IOptions pattern** + singleton for cleaner architecture.

---

### 3️⃣ Run Database + Application

Standard nopCommerce setup:
```bash
dotnet build
dotnet run
```

or through Visual Studio ➡️ `Start Debugging`

---

## 📋 Features

- `/` (Home page) is replaced with the Dynamics CRM **public form**
- Submits First Name, Last Name, Email
- Checks if email exists in CRM before creation
- Success + Error toasts displayed to user
- Prevents form resubmission on page refresh
- Clean singleton pattern used for ServiceClient

---

## 📝 Example Flow

1. User lands on `/` → sees the customized Contact Form
2. Submits info → `_crmService.ContactExistsAsync(email)` checks CRM
3. If email exists → shows error  
   If not → creates contact in Dynamics CRM

---

## ✅ Technologies

- ASP.NET Core 8 + MVC
- nopCommerce base platform
- Microsoft Dataverse Client SDK
- Autofac + Dependency Injection
- Singleton `ServiceClient`

---

## 💡 Notes

You can use it for testing or demo purposes.  
Not for production without full security hardening.

---

## 🔗 License

MIT License (for assessment purposes)

