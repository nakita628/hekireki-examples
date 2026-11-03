package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AssetProfileResolutionTable : IdTable<String>("\"AssetProfileResolution\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val currency = text("currency")
    val dataSourceOrigin = pgEnum("dataSourceOrigin", "\"DataSource\"", DataSource.entries, DataSource::dbName)
    val dataSourceTarget = pgEnum("dataSourceTarget", "\"DataSource\"", DataSource.entries, DataSource::dbName)
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val requestCount = integer("requestCount").default(1)
    val symbolOrigin = text("symbolOrigin")
    val symbolTarget = text("symbolTarget")
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AssetProfileResolution_pkey\"")

    init {
        uniqueIndex("\"AssetProfileResolution_dataSourceOrigin_symbolOrigin_key\"", dataSourceOrigin, symbolOrigin)
    }
}
