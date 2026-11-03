package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationMembershipTable : IdTable<String>("organization_memberships") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val orgId =
        reference(
            "org_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "organization_memberships_org_id_fkey",
        )
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "organization_memberships_user_id_fkey",
        )
    val role = pgEnum("role", "\"Role\"", Role.entries, Role::dbName)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "organization_memberships_pkey")

    init {
        index("organization_memberships_user_id_idx", false, userId)
        uniqueIndex("organization_memberships_org_id_user_id_key", orgId, userId)
    }
}
