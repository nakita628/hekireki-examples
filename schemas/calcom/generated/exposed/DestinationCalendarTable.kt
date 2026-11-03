package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DestinationCalendarTable : IdTable<Int>("\"DestinationCalendar\"") {
    override val id = integer("id").autoIncrement().entityId()
    val integration = text("integration")
    val externalId = text("externalId")
    val primaryEmail = text("primaryEmail").nullable()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DestinationCalendar_userId_fkey\"",
        ).uniqueIndex("\"DestinationCalendar_userId_key\"")
    val eventTypeId =
        optReference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DestinationCalendar_eventTypeId_fkey\"",
        ).uniqueIndex("\"DestinationCalendar_eventTypeId_key\"")
    val credentialId =
        optReference(
            "credentialId",
            CredentialTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DestinationCalendar_credentialId_fkey\"",
        )
    val createdAt =
        pgTimestamp("createdAt", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val delegationCredentialId =
        optReference(
            "delegationCredentialId",
            DelegationCredentialTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DestinationCalendar_delegationCredentialId_fkey\"",
        )
    val customCalendarReminder = integer("customCalendarReminder").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"DestinationCalendar_pkey\"")

    init {
        index("\"DestinationCalendar_userId_idx\"", false, userId)
        index("\"DestinationCalendar_eventTypeId_idx\"", false, eventTypeId)
        index("\"DestinationCalendar_credentialId_idx\"", false, credentialId)
    }
}
