package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object AiProvidersTable : IdTable<Int>("\"aiProviders\"") {
    override val id = integer("id").autoIncrement().entityId()
    val title = varchar("title", 255)
    val provider = varchar("provider", 50)
    val baseURL = varchar("baseURL", 500).nullable()
    val apiKey = varchar("apiKey", 500).nullable()
    val config = json<String>("config", { it }, { it }).nullable()
    val sortOrder = integer("sortOrder").default(0)
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"aiProviders_pkey\"")
}
