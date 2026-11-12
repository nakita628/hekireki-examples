package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SessionTable : IdTable<String>("session") {
    override val id = text("id").entityId()
    val sid = text("sid").uniqueIndex("session_sid_key")
    val data = text("data")
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "session_pkey")

    init {
        index("\"session_expiresAt_idx\"", false, expiresAt)
    }
}
