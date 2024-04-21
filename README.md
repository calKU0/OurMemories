# OurMemories

OurMemories is an online platform for saving memories with your partner (espesially long distance one).
It has as a digital-analog clock and countdown for your upcoming meeting. Moreover, it stores your previous meetings and you can attach pictures to them.

## 🏃 Getting Started

1. Go into directory where you plan on keeping project and run.

```bash
  git fork https://github.com/calKU0/OurMemories.git
```

2. Create a local database. (If you are unsure how to do this, watch some YouTube videos)

3. Add connection string to app settings.json. It will look something like this:
```bash
  Data Source=DESKTOP-EI2TOGP\\SQLEXPRESS;Initial Catalog=OurMemories;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
```
4. Register for a [Cloudinary Account](https://cloudinary.com/users/register/free) (%100 free) and add Cloudname, ApiKey, and Api secret to appsettings.json.