# efcore-npgsql-enum-reproducer

How to reproduce 
- Run `docker-compose up` in the root directory and wait for the db to initialize
- run the test in EfCoreNpgsqlPublicEnumReproducer.Tests. 

Expectation: test succeeds. actual behaviour: test fails with "Npgsql.PostgresException: 42704: type "my_enum" does not exist"
