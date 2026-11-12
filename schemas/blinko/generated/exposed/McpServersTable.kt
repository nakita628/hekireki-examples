package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object McpServersTable : IdTable<Int>("\"mcpServers\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = varchar("name", 255)
    val description = text("description").nullable()
    val type = varchar("type", 50)
    val command = text("command").nullable()
    val args = json<String>("args", { it }, { it }).nullable()
    val url = varchar("url", 500).nullable()
    val env = json<String>("env", { it }, { it }).nullable()
    val headers = json<String>("headers", { it }, { it }).nullable()
    val isEnabled = bool("isEnabled").default(true)
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"mcpServers_pkey\"")
}
