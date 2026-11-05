package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimUserTable : IdTable<String>("\"scimUser\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val connectionId = text("connectionId")
    val provisioningDomainId = text("provisioningDomainId")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"scimUser_userId_fkey\"",
        )
    val connectionUserKey = text("connectionUserKey").uniqueIndex("\"scimUser_connectionUserKey_key\"")
    val userName = text("userName")
    val userNameKey = text("userNameKey").uniqueIndex("\"scimUser_userNameKey_key\"")
    val primaryEmail = text("primaryEmail")
    val workEmailValueIndex = text("workEmailValueIndex")
    val emailValueIndex = text("emailValueIndex")
    val displayName = text("displayName")
    val formattedName = text("formattedName")
    val givenName = text("givenName").nullable()
    val familyName = text("familyName").nullable()
    val serializedEmails = text("serializedEmails")
    val serializedAttributes = text("serializedAttributes").nullable()
    val externalId = text("externalId").nullable()
    val externalIdKey = text("externalIdKey").nullable().uniqueIndex("\"scimUser_externalIdKey_key\"")
    val active = bool("active")
    val orderKey = text("orderKey").uniqueIndex("\"scimUser_orderKey_key\"")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"scimUser_pkey\"")

    init {
        index("\"scimUser_connectionId_idx\"", false, connectionId)
        index("\"scimUser_provisioningDomainId_idx\"", false, provisioningDomainId)
        index("\"scimUser_userId_idx\"", false, userId)
    }
}
