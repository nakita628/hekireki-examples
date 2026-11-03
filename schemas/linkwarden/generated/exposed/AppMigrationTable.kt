package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AppMigrationTable : IdTable<Int>("\"AppMigration\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name").uniqueIndex("\"AppMigration_name_key\"")
    val status = pgEnum("status", "\"AppMigrationStatus\"", AppMigrationStatus.entries, AppMigrationStatus::dbName)
    val finishedAt = pgTimestamp("finishedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AppMigration_pkey\"")
}
