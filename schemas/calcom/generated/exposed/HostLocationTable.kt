package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object HostLocationTable : IdTable<String>("\"HostLocation\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val userId = integer("userId")
    val eventTypeId = integer("eventTypeId")
    val type = text("type")
    val credentialId =
        optReference(
            "credentialId",
            CredentialTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"HostLocation_credentialId_fkey\"",
        )
    val link = text("link").nullable()
    val address = text("address").nullable()
    val phoneNumber = text("phoneNumber").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"HostLocation_pkey\"")

    init {
        index("\"HostLocation_credentialId_idx\"", false, credentialId)
        index("\"HostLocation_eventTypeId_idx\"", false, eventTypeId)
        uniqueIndex("\"HostLocation_userId_eventTypeId_key\"", userId, eventTypeId)
        foreignKey(
            userId to HostTable.userId,
            eventTypeId to HostTable.eventTypeId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"HostLocation_userId_eventTypeId_fkey\"",
        )
    }
}
