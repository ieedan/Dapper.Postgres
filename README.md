# Dapper.Postgres
An extension of NpgsqlConnection and Dapper to allow for easier use of Postgres functions.

### Why?
If you aren't a fan of writing parameterized queries and would rather have your logic in functions in your database then this Dapper extension should benefit you.

### What does it do?
Dapper.Postgres writes the parameterized query on the function for you so you don't have to write the statements yourself. It simply extends the functionality of DynamicParameters with a slightly different implementation to allow for use with functions.

## Methods
- `QueryFunction<T>`
- `QueryFunctionFirst<T>`
- `ExecuteFunction<T>`
- `QueryFunctionAsync<T>`
- `QueryFunctionFirstAsync<T>`
- `ExecuteFunctionAsync<T>`

## Examples

```csharp
using Dapper;
using Dapper.Postgres;

string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=test;";

using NpgsqlConnection connection = new NpgsqlConnection(connectionString);

var parameters = new DynamicFunctionParameters(new User("ieedan"));

Guid? userId = await connection.ExecuteFunctionAsync<Guid>("add_user", parameters);

Console.WriteLine(userId?.ToString());

```
