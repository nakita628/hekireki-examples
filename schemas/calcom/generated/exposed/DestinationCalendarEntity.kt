package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DestinationCalendarEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, DestinationCalendarEntity>(DestinationCalendarTable)

    var integration by DestinationCalendarTable.integration
    var externalId by DestinationCalendarTable.externalId
    var primaryEmail by DestinationCalendarTable.primaryEmail
    var user by UserEntity optionalReferencedOn DestinationCalendarTable.userId
    var userId by DestinationCalendarTable.userId
    val booking by BookingEntity optionalReferrersOn BookingTable.destinationCalendarId
    var eventType by EventTypeEntity optionalReferencedOn DestinationCalendarTable.eventTypeId
    var eventTypeId by DestinationCalendarTable.eventTypeId
    var credentialId by DestinationCalendarTable.credentialId
    var credential by CredentialEntity optionalReferencedOn DestinationCalendarTable.credentialId
    var createdAt by DestinationCalendarTable.createdAt
    var updatedAt by DestinationCalendarTable.updatedAt
    var delegationCredential by DelegationCredentialEntity optionalReferencedOn DestinationCalendarTable.delegationCredentialId
    var delegationCredentialId by DestinationCalendarTable.delegationCredentialId
    var customCalendarReminder by DestinationCalendarTable.customCalendarReminder

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DestinationCalendarTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
