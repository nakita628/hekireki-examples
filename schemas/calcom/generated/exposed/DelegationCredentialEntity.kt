package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DelegationCredentialEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DelegationCredentialEntity>(DelegationCredentialTable)

    var workspacePlatform by WorkspacePlatformEntity referencedOn DelegationCredentialTable.workspacePlatformId
    var workspacePlatformId by DelegationCredentialTable.workspacePlatformId
    var serviceAccountKey by DelegationCredentialTable.serviceAccountKey
    var enabled by DelegationCredentialTable.enabled
    var lastEnabledAt by DelegationCredentialTable.lastEnabledAt
    var lastDisabledAt by DelegationCredentialTable.lastDisabledAt
    var organizationId by DelegationCredentialTable.organizationId
    var organization by TeamEntity referencedOn DelegationCredentialTable.organizationId
    var domain by DelegationCredentialTable.domain
    val selectedCalendars by SelectedCalendarEntity optionalReferrersOn SelectedCalendarTable.delegationCredentialId
    val destinationCalendar by DestinationCalendarEntity optionalReferrersOn DestinationCalendarTable.delegationCredentialId
    val bookingReferences by BookingReferenceEntity optionalReferrersOn BookingReferenceTable.delegationCredentialId
    var createdAt by DelegationCredentialTable.createdAt
    var updatedAt by DelegationCredentialTable.updatedAt
    val credentials by CredentialEntity optionalReferrersOn CredentialTable.delegationCredentialId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DelegationCredentialTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
