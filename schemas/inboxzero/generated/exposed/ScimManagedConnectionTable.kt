package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimManagedConnectionTable : IdTable<String>("\"scimManagedConnection\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val creationRequestId = text("creationRequestId").uniqueIndex("\"scimManagedConnection_creationRequestId_key\"")
    val connectionId = text("connectionId").uniqueIndex("\"scimManagedConnection_connectionId_key\"")
    val provisioningDomainId = text("provisioningDomainId")
    val status = text("status")
    val revision = integer("revision")
    val createdAt = pgTimestamp("createdAt", 3)
    val createdBy = text("createdBy")
    val decommissionStartedAt = pgTimestamp("decommissionStartedAt", 3).nullable()
    val decommissionStartedBy = text("decommissionStartedBy").nullable()
    val decommissionedAt = pgTimestamp("decommissionedAt", 3).nullable()
    val decommissionedBy = text("decommissionedBy").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"scimManagedConnection_pkey\"")

    init {
        index("\"scimManagedConnection_provisioningDomainId_idx\"", false, provisioningDomainId)
    }
}
