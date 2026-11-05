package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AvailabilityScheduleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AvailabilityScheduleEntity>(AvailabilityScheduleTable)

    var createdAt by AvailabilityScheduleTable.createdAt
    var updatedAt by AvailabilityScheduleTable.updatedAt
    var name by AvailabilityScheduleTable.name
    var isDefault by AvailabilityScheduleTable.isDefault
    var timezone by AvailabilityScheduleTable.timezone
    var emailAccountId by AvailabilityScheduleTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn AvailabilityScheduleTable.emailAccountId
    val windows by AvailabilityWindowEntity referrersOn AvailabilityWindowTable.availabilityScheduleId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AvailabilityScheduleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
