package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CalendarConnectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CalendarConnectionEntity>(CalendarConnectionTable)

    var userId by CalendarConnectionTable.userId
    var provider by CalendarConnectionTable.provider
    var integrationId by CalendarConnectionTable.integrationId
    var providerAccountId by CalendarConnectionTable.providerAccountId
    var email by CalendarConnectionTable.email
    var displayName by CalendarConnectionTable.displayName
    var createdAt by CalendarConnectionTable.createdAt
    var updatedAt by CalendarConnectionTable.updatedAt
    var credential by CredentialEntity referencedOn CalendarConnectionTable.credentialId
    var credentialId by CalendarConnectionTable.credentialId
    var user by UserEntity referencedOn CalendarConnectionTable.userId
    val providerCalendars by ProviderCalendarEntity referrersOn ProviderCalendarTable.calendarConnectionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CalendarConnectionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
