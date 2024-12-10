# Hotel Room Availability and Reservation Preview

This application previews hotel room availability and manages reservations. It reads from specified hotel and booking data files, enabling users to check room availability for selected hotels, dates, and room types.

## Features

- Check room availability by hotel ID, date range, and room type.
- Load hotel and booking data from JSON files for quick, offline access.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Running the Application

1. **Navigate to the `HotelBooker` directory** (containing `HotelBooker.csproj`).
2. **Run the following command** to start the application:

   ```bash
   dotnet run --hotels <hotel_data_file> --bookings <booking_data_file>
	```
3. Once you see the message `"JSON files loaded successfully"`, you’re ready to check room availability.

### Checking Availability

Use the following commands to check room availability for a given hotel, date range, and room type:

-   **Single Night Check:**
    
    ```
    Availability(<hotelId>, <check_in_date_of_one_night_stay>, <roomType>)
    ```
-  **Date range check**
	```
	Availability(<hotelId>, <check_in_date>-<check_out_date>, <roomType>)
	```
The response will be an integer specifying the amount of such rooms available.
**Note:** Dates should be formatted as `yyyyMMdd`. For example, `20240101` for January 1, 2024.

### Example

To check availability for a hotel with ID `H1`, staying from January 1 to January 3, 2025, in a "DBL" room:

```
Availability(H1, 20250101-20250103, DBL)
```
 
 To check availability for a hotel with ID `H2`, staying from January 7 to January 8, 2025, in a "SGL" room:
     
```
Availability(H2, 20250107, SGL)
```
