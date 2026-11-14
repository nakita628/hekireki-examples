package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object NurturingOrganizationMemberTable : CompositeIdTable("\"NurturingOrganizationMember\"") {
    val organizationId = text("organizationId").entityId()
    val userId = text("userId").entityId()
    val joinedAt = pgTimestamp("joinedAt", 3).nullable()
    val leftAt = pgTimestamp("leftAt", 3).nullable()
    val returnedAt = pgTimestamp("returnedAt", 3).nullable()
    override val primaryKey = PrimaryKey(organizationId, userId, name = "\"NurturingOrganizationMember_pkey\"")

    init {
        index("\"NurturingOrganizationMember_userId_idx\"", false, userId)
    }
}
