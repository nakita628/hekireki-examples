package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AvailabilityScheduleTable : IdTable<String>("\"AvailabilitySchedule\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val isDefault = bool("isDefault").default(false)
    val timezone = text("timezone").default("UTC")
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AvailabilitySchedule_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"AvailabilitySchedule_pkey\"")

    init {
        index("\"AvailabilitySchedule_emailAccountId_idx\"", false, emailAccountId)
        index("\"AvailabilitySchedule_emailAccountId_isDefault_idx\"", false, emailAccountId, isDefault)
        uniqueIndex("\"AvailabilitySchedule_id_emailAccountId_key\"", id, emailAccountId)
    }
}
