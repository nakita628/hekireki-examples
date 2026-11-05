package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScheduleTable : IdTable<String>("\"Schedule\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val intervalDays = integer("intervalDays").nullable()
    val occurrences = integer("occurrences").nullable()
    val daysOfWeek = integer("daysOfWeek").nullable()
    val timeOfDay = pgTimestamp("timeOfDay", 3).nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Schedule_emailAccountId_fkey\"",
        ).uniqueIndex("\"Schedule_emailAccountId_key\"")
    val lastOccurrenceAt = pgTimestamp("lastOccurrenceAt", 3).nullable()
    val nextOccurrenceAt = pgTimestamp("nextOccurrenceAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Schedule_pkey\"")
}
