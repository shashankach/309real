## Farmer's Mercato App
### CSC 309 Project #1
### Shashank Acharya, Annie Le Piere, Joseph Macfarlane, and Anushree Parmar

APP OVERVIEW:
The system aims to give local farmers a platform to promote and sell their products. This application provides farmers with the ability to showcase their current
inventory and place/dispatch orders. Users will be able to view this information and find markets/farms based on their current location.

OBJECTS & DATABASES (MongoDB):
- User: {“username”: string,
    “password”: string,
    “farmer”: boolean
    “name”: string,
    “email”: string,
    “image”: string}
- Product: {“name”: string,
    “price”: Double,
    “seller”: string,
    “image”: string}
- Cart: {“name”: string,
    “price”: Double,
    “seller”: string,
    “image”: string}
- Order: {“id”: string,
    “items”: List<Product>}

INSTALLED PACKAGES:
- BlazorGoogleMaps
- BlazorGoogleMapsV3
- FluentValidation
- Microsfoft.AspNetCOre.Components.WebView.Maui
- Microsoft.Maui.Dependencies
- Microsoft.Maui.Extensions
- Microsoft.Windows.SDK.BuildTools
- MudBlazor
- MudBlazor.ThemeManager
- Newtonsoft.Json
- System.Runtime.InteropServices.NFloat.Internal
- Microsoft.NET.Sdk.Functions
- MongoDB.Driver
- coverlet.collector
- Microsoft.NET.Test.Sdk
- MSTest.TestFramework
- NUnit
- NUnit.Analyzers
- NUnit3TestAdapater
