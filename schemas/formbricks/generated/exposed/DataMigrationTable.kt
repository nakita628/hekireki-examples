package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataMigrationTable : IdTable<String>("\"DataMigration\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val startedAt = pgTimestamp("started_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val finishedAt = pgTimestamp("finished_at", 3).nullable()
    val name = text("name").uniqueIndex("\"DataMigration_name_key\"")
    val status = pgEnum("status", "\"DataMigrationStatus\"", DataMigrationStatus.entries, DataMigrationStatus::dbName)
    override val primaryKey = PrimaryKey(id, name = "\"DataMigration_pkey\"")
}
