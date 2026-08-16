# DockIn

A warehouse management system (WMS) for a food distribution warehouse, built to cut the distance order pickers walk and to keep stock numbers honest.

**Status:** in development. See [Roadmap](#roadmap) for what is done and what is not.

---

## The problem

A distribution warehouse does not sell anything. It sells speed and accuracy, and it loses money in three specific places.

### Pickers spend 40 percent of their time walking

In a warehouse with twelve aisles, the pick list decides how far someone walks. When the list comes out in the order the customer orders arrived, a picker goes to aisle A, then K, then back to B, then J. Over a shift that adds up to roughly fourteen kilometres of walking per person.

Walking is not picking. With eight pickers on a shift, every ten percent of distance removed is close to a full picker's worth of capacity recovered, without hiring anyone. The distance is decided at planning time, by software, before anyone leaves the dock.

### Stock records drift away from reality

If the system says there are forty units and there are twelve, the customer order ships incomplete, the store shelf stays empty, and the warehouse pays a penalty. Drift comes from movements that were never recorded, recorded twice, or recorded against the wrong location. Once records and reality diverge, nobody can tell where the gap started, and an annual inventory count finds a number that is too large to explain.

### Product expires on the shelf

In food distribution, stock has a shelf life. A picker naturally takes what is in front, which is usually what arrived last. The batch behind it quietly ages out and gets thrown away. Preventing this is not a preference, it is margin: the system has to direct every pick to the batch that expires first.

### And the network does not reach the whole building

Handheld terminals lose Wi-Fi in freezer aisles and at the back of the building. A terminal that stops working when the signal drops means a picker standing still. A terminal that queues work offline and then resends it means the same movement can be applied twice, which is the first problem again, wearing a different hat.

---

## What DockIn does

| Area | What it handles |
|---|---|
| Inbound | Goods receipt against the supplier note, batch and expiry capture, put-away with system-suggested locations that respect weight, volume and temperature zone |
| Stock | Balances per item, batch and location, backed by an append-only movement ledger that is never edited or deleted, only reversed |
| Allocation | FEFO reservation, so the batch expiring first is always the batch that ships. Optimistic concurrency, so two supervisors cannot reserve the same last unit |
| Picking | Waves built within cart weight and volume limits, with the pick sequence ordered by a route calculated over a graph of the warehouse layout |
| Handheld | Offline-first Android terminal. Work is queued locally and synchronised in idempotent batches, so a retry after a timeout cannot duplicate a stock movement |
| Counting | Blind cycle counts, with a mandatory second count by a different operator when the discrepancy passes a threshold |
| Analysis | ABC classification by pick frequency and re-slotting suggestions, each with the estimated metres saved per year |

---

## Why the implementation is interesting

**The data structures are written from scratch.** The hash map, doubly linked list, LRU cache, binary min-heap and graph are implemented in this repository rather than taken from the standard library, and they carry real load: the in-memory warehouse map, the SKU-to-location index, offline batch deduplication, memoised distance matrices and Dijkstra's priority queue. Each one is unit tested, benchmarked against its `System.Collections.Generic` equivalent, and the results, including the cases where the standard library wins, are published in the documentation.

**The hot path does not touch the database.** Every barcode scan needs an answer in milliseconds, and there are eight operators scanning all day. The warehouse map is loaded once into memory and answers validations from there; only writes go back through EF Core. Drawing that boundary correctly, and protecting a non-thread-safe structure living in a singleton, is most of the architectural work.

**Offline synchronisation is idempotent by construction.** Each event gets a ULID generated on the device, which never changes no matter how many times the batch is retried. Duplicates are filtered within the batch, then against history in a single query, with a unique database index as the last line of defence. Sending the same batch three times leaves exactly the same number of rows in the ledger.

**Route optimisation is measured, not asserted.** Three strategies are implemented, and all three run over the same two hundred generated waves so the comparison is fair: the input order as a baseline, the S-shape traversal heuristic that real warehouses use, and nearest-neighbour with 2-opt improvement over the distance matrix. The output is a table of metres walked and computation time, converted into an annual figure.

---

## Architecture

```
Api  ->  Application  ->  Domain  <-  Structures
 |                           ^
Infrastructure  ------------ +
```

| Project | Responsibility |
|---|---|
| `DockIn.Structures` | Hand-written data structures, with no knowledge of the domain |
| `DockIn.Domain` | Entities, value objects, invariants and domain events. No external package references |
| `DockIn.Application` | Use cases, DTOs, validation and the interfaces the infrastructure implements |
| `DockIn.Infrastructure` | EF Core persistence, in-memory caches, route calculation, file storage |
| `DockIn.Api` | HTTP endpoints, authentication, error contracts, real-time hub |

The dependency rule is enforced by an automated test, not by convention.

---

## Technology

- .NET 10, ASP.NET Core, Entity Framework Core, PostgreSQL
- Angular with Bootstrap for the back office
- Kotlin with Jetpack Compose and Room for the handheld terminal
- xUnit, Testcontainers and BenchmarkDotNet for tests and measurement

---

## Roadmap

- [ ] Data structures, tests and benchmarks
- [ ] Domain model and business rules
- [ ] Persistence and database seed
- [ ] API: receiving, put-away, stock
- [ ] Pick waves and route optimisation
- [ ] Offline synchronisation
- [ ] Angular back office
- [ ] Android handheld terminal

---

## Documentation

| Document | Contents |
|---|---|
| `docs/arquitetura.md` | Context and container diagrams, and the reasoning behind them |
| `docs/modelo-dados.md` | Entity relationship diagram and the justification for each index |
| `docs/benchmark-estruturas.md` | Hand-written structures measured against the standard library |
| `docs/percursos.md` | The three routing strategies over the same waves, and the annual figure |
| `docs/carga.md` | Load test results for the scanning and synchronisation endpoints |
| `docs/decisoes.md` | Architecture decision records |

---

## Running it

Requires .NET 10 SDK, Docker and Node.

```
docker compose up -d
dotnet ef database update --project DockIn.Infrastructure --startup-project DockIn.Api
dotnet run --project DockIn.Api
```

The API serves its OpenAPI document at `/swagger`.

---

## License

MIT. See [LICENSE](LICENSE).
