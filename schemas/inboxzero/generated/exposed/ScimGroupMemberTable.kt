package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimGroupMemberTable : IdTable<String>("\"scimGroupMember\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val connectionId = text("connectionId")
    val groupId =
        reference(
            "groupId",
            ScimGroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"scimGroupMember_groupId_fkey\"",
        )
    val scimUserId =
        reference(
            "scimUserId",
            ScimUserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"scimGroupMember_scimUserId_fkey\"",
        )
    val membershipKey = text("membershipKey").uniqueIndex("\"scimGroupMember_membershipKey_key\"")
    val createdAt = pgTimestamp("createdAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"scimGroupMember_pkey\"")

    init {
        index("\"scimGroupMember_connectionId_idx\"", false, connectionId)
        index("\"scimGroupMember_groupId_idx\"", false, groupId)
        index("\"scimGroupMember_scimUserId_idx\"", false, scimUserId)
    }
}
