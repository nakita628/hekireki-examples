package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object MarketDataTable : IdTable<String>("\"MarketData\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val dataSource = pgEnum("dataSource", "\"DataSource\"", DataSource.entries, DataSource::dbName)
    val date = pgTimestamp("date", 3)
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val isCarriedForward = bool("isCarriedForward").default(false)
    val marketPrice = double("marketPrice")
    val state = pgEnum("state", "\"MarketDataState\"", MarketDataState.entries, MarketDataState::dbName).default(MarketDataState.CLOSE)
    val symbol = text("symbol")
    override val primaryKey = PrimaryKey(id, name = "\"MarketData_pkey\"")

    init {
        index("\"MarketData_date_idx\"", false, date)
        index("\"MarketData_state_idx\"", false, state)
        index("\"MarketData_symbol_idx\"", false, symbol)
        uniqueIndex("\"MarketData_dataSource_symbol_date_key\"", dataSource, symbol, date)
    }
}
