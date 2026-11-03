package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SelectedCalendarTable : IdTable<String>("\"SelectedCalendar\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SelectedCalendar_userId_fkey\"",
        )
    val integration = text("integration")
    val externalId = text("externalId")
    val credentialId =
        optReference(
            "credentialId",
            CredentialTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SelectedCalendar_credentialId_fkey\"",
        )
    val createdAt =
        pgTimestamp("createdAt", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val googleChannelId = text("googleChannelId").nullable()
    val googleChannelKind = text("googleChannelKind").nullable()
    val googleChannelResourceId = text("googleChannelResourceId").nullable()
    val googleChannelResourceUri = text("googleChannelResourceUri").nullable()
    val googleChannelExpiration = text("googleChannelExpiration").nullable()
    val channelId = text("channelId").nullable()
    val channelKind = text("channelKind").nullable()
    val channelResourceId = text("channelResourceId").nullable()
    val channelResourceUri = text("channelResourceUri").nullable()
    val channelExpiration = pgTimestamp("channelExpiration", 3).nullable()
    val syncSubscribedAt = pgTimestamp("syncSubscribedAt", 3).nullable()
    val syncSubscribedErrorAt = pgTimestamp("syncSubscribedErrorAt", 3).nullable()
    val syncSubscribedErrorCount = integer("syncSubscribedErrorCount").default(0)
    val syncToken = text("syncToken").nullable()
    val syncedAt = pgTimestamp("syncedAt", 3).nullable()
    val syncErrorAt = pgTimestamp("syncErrorAt", 3).nullable()
    val syncErrorCount = integer("syncErrorCount").nullable().default(0)
    val delegationCredentialId =
        optReference(
            "delegationCredentialId",
            DelegationCredentialTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SelectedCalendar_delegationCredentialId_fkey\"",
        )
    val error = text("error").nullable()
    val lastErrorAt = pgTimestamp("lastErrorAt", 3).nullable()
    val watchAttempts = integer("watchAttempts").default(0)
    val unwatchAttempts = integer("unwatchAttempts").default(0)
    val maxAttempts = integer("maxAttempts").default(3)
    val eventTypeId =
        optReference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SelectedCalendar_eventTypeId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"SelectedCalendar_pkey\"")

    init {
        index("\"SelectedCalendar_userId_idx\"", false, userId)
        index("\"SelectedCalendar_externalId_idx\"", false, externalId)
        index("\"SelectedCalendar_eventTypeId_idx\"", false, eventTypeId)
        index("\"SelectedCalendar_credentialId_idx\"", false, credentialId)
        index("\"SelectedCalendar_channelId_idx\"", false, channelId)
        index("\"SelectedCalendar_watch_idx\"", false, integration, googleChannelExpiration, error, watchAttempts, maxAttempts)
        index("\"SelectedCalendar_unwatch_idx\"", false, integration, googleChannelExpiration, error, unwatchAttempts, maxAttempts)
        uniqueIndex("SelectedCalendar_userId_integration_externalId_eventTypeId_key", userId, integration, externalId, eventTypeId)
        uniqueIndex("\"SelectedCalendar_googleChannelId_eventTypeId_key\"", googleChannelId, eventTypeId)
    }
}
