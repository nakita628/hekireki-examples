package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SelectedCalendarEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SelectedCalendarEntity>(SelectedCalendarTable)

    var user by UserEntity referencedOn SelectedCalendarTable.userId
    var userId by SelectedCalendarTable.userId
    var integration by SelectedCalendarTable.integration
    var externalId by SelectedCalendarTable.externalId
    var credential by CredentialEntity optionalReferencedOn SelectedCalendarTable.credentialId
    var credentialId by SelectedCalendarTable.credentialId
    var createdAt by SelectedCalendarTable.createdAt
    var updatedAt by SelectedCalendarTable.updatedAt
    var googleChannelId by SelectedCalendarTable.googleChannelId
    var googleChannelKind by SelectedCalendarTable.googleChannelKind
    var googleChannelResourceId by SelectedCalendarTable.googleChannelResourceId
    var googleChannelResourceUri by SelectedCalendarTable.googleChannelResourceUri
    var googleChannelExpiration by SelectedCalendarTable.googleChannelExpiration
    var channelId by SelectedCalendarTable.channelId
    var channelKind by SelectedCalendarTable.channelKind
    var channelResourceId by SelectedCalendarTable.channelResourceId
    var channelResourceUri by SelectedCalendarTable.channelResourceUri
    var channelExpiration by SelectedCalendarTable.channelExpiration
    var syncSubscribedAt by SelectedCalendarTable.syncSubscribedAt
    var syncSubscribedErrorAt by SelectedCalendarTable.syncSubscribedErrorAt
    var syncSubscribedErrorCount by SelectedCalendarTable.syncSubscribedErrorCount
    var syncToken by SelectedCalendarTable.syncToken
    var syncedAt by SelectedCalendarTable.syncedAt
    var syncErrorAt by SelectedCalendarTable.syncErrorAt
    var syncErrorCount by SelectedCalendarTable.syncErrorCount
    var delegationCredential by DelegationCredentialEntity optionalReferencedOn SelectedCalendarTable.delegationCredentialId
    var delegationCredentialId by SelectedCalendarTable.delegationCredentialId
    var error by SelectedCalendarTable.error
    var lastErrorAt by SelectedCalendarTable.lastErrorAt
    var watchAttempts by SelectedCalendarTable.watchAttempts
    var unwatchAttempts by SelectedCalendarTable.unwatchAttempts
    var maxAttempts by SelectedCalendarTable.maxAttempts
    var eventTypeId by SelectedCalendarTable.eventTypeId
    var eventType by EventTypeEntity optionalReferencedOn SelectedCalendarTable.eventTypeId
    val calendarCacheEvents by CalendarCacheEventEntity referrersOn CalendarCacheEventTable.selectedCalendarId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SelectedCalendarTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
