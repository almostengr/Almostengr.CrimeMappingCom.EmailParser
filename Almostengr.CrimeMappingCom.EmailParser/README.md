# Crime Mapping Email Parser

## Purpose 

Read email alerts that have been received from the CrimeMapping.com website.
This library is designed to read plain text emails that are sent from the site. 




## How To Use

Use the example code below for reading emails. Each of the emails will be written 
to a separate file. 

```csharp
var reader = new ImapEmailReader();
var parser = new CrimeEmailParser();
var writer = new JsonCrimeWriter();
var settings = new CrimeMappingSettings();

var emails = await reader.GetUnreadAsync(
    settings.Hostname, settings.PortNumber, settings.Username, settings.Password);

foreach (var email in emails)
{
    var alert = parser.Parse(email.TextBody);

    foreach (var crime in alert.Incidents)
    {
        writer.Write(crime);
    }
}
```
