package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MembershipTable : IdTable<Int>("\"Membership\"") {
    override val id = integer("id").autoIncrement().entityId()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Membership_teamId_fkey\"",
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
    val role = pgEnum("role", "\"MembershipRole\"", MembershipRole.entries, MembershipRole::dbName)
    val customRoleId =
        optReference(
            "customRoleId",
            RoleTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Membership_customRoleId_fkey\"",
        )
    val createdAt =
        pgTimestamp("createdAt", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Membership_pkey\"")

    init {
        index("\"Membership_teamId_idx\"", false, teamId)
        index("\"Membership_userId_idx\"", false, userId)
        index("\"Membership_accepted_idx\"", false, accepted)
        index("\"Membership_role_idx\"", false, role)
        index("\"Membership_customRoleId_idx\"", false, customRoleId)
        uniqueIndex("\"Membership_userId_teamId_key\"", userId, teamId)
    }
}
