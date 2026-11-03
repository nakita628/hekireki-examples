package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmailTransportTable : IdTable<String>("\"EmailTransport\"") {
    override val id = text("id").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val type = pgEnum("type", "\"EmailTransportType\"", EmailTransportType.entries, EmailTransportType::dbName)
    val fromName = text("fromName")
    val fromAddress = text("fromAddress")
    val config = text("config")
    override val primaryKey = PrimaryKey(id, name = "\"EmailTransport_pkey\"")
}
