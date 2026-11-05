package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimManagedConnectionEventTable : IdTable<String>("\"scimManagedConnectionEvent\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val connectionRecordId =
        reference(
            "connectionRecordId",
            ScimManagedConnectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"scimManagedConnectionEvent_connectionRecordId_fkey\"",
        )
    val eventKey = text("eventKey").uniqueIndex("\"scimManagedConnectionEvent_eventKey_key\"")
    val sequence = integer("sequence")
    val type = text("type")
    val actorId = text("actorId")
    val credentialId = text("credentialId").nullable()
    val createdAt = pgTimestamp("createdAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"scimManagedConnectionEvent_pkey\"")

    init {
        index("\"scimManagedConnectionEvent_connectionRecordId_idx\"", false, connectionRecordId)
    }
}
