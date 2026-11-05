package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AvailabilityWindowTable : IdTable<String>("\"AvailabilityWindow\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val weekday = integer("weekday")
    val startMinutes = integer("startMinutes")
    val endMinutes = integer("endMinutes")
    val availabilityScheduleId =
        reference(
            "availabilityScheduleId",
            AvailabilityScheduleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AvailabilityWindow_availabilityScheduleId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"AvailabilityWindow_pkey\"")

    init {
        index("\"AvailabilityWindow_availabilityScheduleId_idx\"", false, availabilityScheduleId)
    }
}
