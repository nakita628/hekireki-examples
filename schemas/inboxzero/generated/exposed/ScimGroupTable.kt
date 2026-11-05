package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimGroupTable : IdTable<String>("\"scimGroup\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val connectionId = text("connectionId")
    val provisioningDomainId = text("provisioningDomainId")
    val revision = integer("revision").default(0)
    val displayName = text("displayName")
    val displayNameKey = text("displayNameKey").uniqueIndex("\"scimGroup_displayNameKey_key\"")
    val externalId = text("externalId").nullable()
    val externalIdKey = text("externalIdKey").nullable().uniqueIndex("\"scimGroup_externalIdKey_key\"")
    val orderKey = text("orderKey").uniqueIndex("\"scimGroup_orderKey_key\"")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"scimGroup_pkey\"")

    init {
        index("\"scimGroup_connectionId_idx\"", false, connectionId)
        index("\"scimGroup_provisioningDomainId_idx\"", false, provisioningDomainId)
    }
}
