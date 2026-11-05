package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimConnectionBindingTable : IdTable<String>("\"scimConnectionBinding\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val connectionId = text("connectionId")
    val connectionKey = text("connectionKey").uniqueIndex("\"scimConnectionBinding_connectionKey_key\"")
    val provisioningDomainId = text("provisioningDomainId")
    val createdAt = pgTimestamp("createdAt", 3)
    val decommissionedAt = pgTimestamp("decommissionedAt", 3).nullable()
    val decommissionStatus = text("decommissionStatus").default("active")
    val decommissionCursorUserId = text("decommissionCursorUserId").nullable()
    val decommissionReconciledUserCount = integer("decommissionReconciledUserCount").default(0)
    val decommissionBatchCount = integer("decommissionBatchCount").default(0)
    val decommissionRevision = integer("decommissionRevision").default(0)
    val decommissionCompletedAt = pgTimestamp("decommissionCompletedAt", 3).nullable()
    val decommissionLeaseId = text("decommissionLeaseId").nullable()
    val decommissionLeaseExpiresAt = pgTimestamp("decommissionLeaseExpiresAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"scimConnectionBinding_pkey\"")

    init {
        index("\"scimConnectionBinding_connectionId_idx\"", false, connectionId)
    }
}
