package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimManagedCredentialTable : IdTable<String>("\"scimManagedCredential\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val connectionRecordId =
        reference(
            "connectionRecordId",
            ScimManagedConnectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"scimManagedCredential_connectionRecordId_fkey\"",
        )
    val credentialId = text("credentialId").uniqueIndex("\"scimManagedCredential_credentialId_key\"")
    val tokenDigest = text("tokenDigest")
    val hashVersion = text("hashVersion")
    val activeSlotKey = text("activeSlotKey").uniqueIndex("\"scimManagedCredential_activeSlotKey_key\"")
    val status = text("status")
    val serializedScopes = text("serializedScopes")
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3)
    val createdBy = text("createdBy")
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val revokedBy = text("revokedBy").nullable()
    val decommissionedAt = pgTimestamp("decommissionedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"scimManagedCredential_pkey\"")

    init {
        index("\"scimManagedCredential_connectionRecordId_idx\"", false, connectionRecordId)
    }
}
