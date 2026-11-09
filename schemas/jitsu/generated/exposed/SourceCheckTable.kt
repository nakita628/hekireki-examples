package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SourceCheckTable : IdTable<String>("source_check") {
    val `package` = text("package")
    val version = text("version")
    override val id = text("key").entityId()
    val status = text("status")
    val description = text("description").nullable()
    val timestamp = pgTimestamptz("timestamp", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "source_check_pkey")
}
