package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimIdentityTombstoneTable : IdTable<String>("\"scimIdentityTombstone\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val connectionId = text("connectionId")
    val provisioningDomainId = text("provisioningDomainId")
    val externalId = text("externalId")
    val externalIdKey = text("externalIdKey").uniqueIndex("\"scimIdentityTombstone_externalIdKey_key\"")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"scimIdentityTombstone_userId_fkey\"",
        )
    val profile = text("profile")
    val deletedAt = pgTimestamp("deletedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"scimIdentityTombstone_pkey\"")

    init {
        index("\"scimIdentityTombstone_connectionId_idx\"", false, connectionId)
        index("\"scimIdentityTombstone_provisioningDomainId_idx\"", false, provisioningDomainId)
        index("\"scimIdentityTombstone_userId_idx\"", false, userId)
    }
}
