package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object MembershipTable : CompositeIdTable("\"Membership\"") {
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Membership_organizationId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Membership_userId_fkey\"",
        )
    val accepted = bool("accepted").default(false)
    val role = pgEnum("role", "\"OrganizationRole\"", OrganizationRole.entries, OrganizationRole::dbName).default(OrganizationRole.MEMBER)
    override val primaryKey = PrimaryKey(userId, organizationId, name = "\"Membership_pkey\"")

    init {
        addIdColumn(userId)
        addIdColumn(organizationId)
        index("\"Membership_organizationId_idx\"", false, organizationId)
    }
}
