package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProviderCalendarEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProviderCalendarEntity>(ProviderCalendarTable)

    var calendarConnectionId by ProviderCalendarTable.calendarConnectionId
    var providerCalendarId by ProviderCalendarTable.providerCalendarId
    var name by ProviderCalendarTable.name
    var isPrimary by ProviderCalendarTable.isPrimary
    var timeZone by ProviderCalendarTable.timeZone
    var isSelected by ProviderCalendarTable.isSelected
    var isDeleted by ProviderCalendarTable.isDeleted
    var isWritable by ProviderCalendarTable.isWritable
    var providerData by ProviderCalendarTable.providerData
    var lastSyncedAt by ProviderCalendarTable.lastSyncedAt
    var createdAt by ProviderCalendarTable.createdAt
    var updatedAt by ProviderCalendarTable.updatedAt
    var calendarConnection by CalendarConnectionEntity referencedOn ProviderCalendarTable.calendarConnectionId
    val userDefaultDestination by UserEntity optionalReferrersOn UserTable.defaultDestinationCalendarId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProviderCalendarTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
