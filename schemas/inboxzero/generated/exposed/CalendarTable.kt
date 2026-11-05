package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CalendarTable : IdTable<String>("\"Calendar\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val calendarId = text("calendarId")
    val name = text("name")
    val description = text("description").nullable()
    val primary = bool("primary").default(false)
    val isEnabled = bool("isEnabled").default(true)
    val timezone = text("timezone").nullable()
    val connectionId =
        reference(
            "connectionId",
            CalendarConnectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Calendar_connectionId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Calendar_pkey\"")

    init {
        uniqueIndex("\"Calendar_connectionId_calendarId_key\"", connectionId, calendarId)
    }
}
