package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimProjectionGrantTable : IdTable<String>("\"scimProjectionGrant\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val connectionId = text("connectionId")
    val provisioningDomainId = text("provisioningDomainId")
    val scimUserId =
        reference(
            "scimUserId",
            ScimUserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"scimProjectionGrant_scimUserId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"scimProjectionGrant_userId_fkey\"",
        )
    val sourceKind = text("sourceKind")
    val sourceId = text("sourceId")
    val sourceValue = text("sourceValue").nullable()
    val role = text("role")
    val grantKey = text("grantKey").uniqueIndex("\"scimProjectionGrant_grantKey_key\"")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"scimProjectionGrant_pkey\"")

    init {
        index("\"scimProjectionGrant_connectionId_idx\"", false, connectionId)
        index("\"scimProjectionGrant_provisioningDomainId_idx\"", false, provisioningDomainId)
        index("\"scimProjectionGrant_scimUserId_idx\"", false, scimUserId)
        index("\"scimProjectionGrant_userId_idx\"", false, userId)
    }
}
