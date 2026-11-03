package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object UserOrganizationTable : IdTable<String>("\"UserOrganization\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserOrganization_userId_fkey\"",
        )
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserOrganization_organizationId_fkey\"",
        )
    val disabled = bool("disabled").default(false)
    val role = pgEnum("role", "\"Role\"", Role.entries, Role::dbName).default(Role.USER)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"UserOrganization_pkey\"")

    init {
        index("\"UserOrganization_organizationId_idx\"", false, organizationId)
        index("\"UserOrganization_disabled_idx\"", false, disabled)
        uniqueIndex("\"UserOrganization_userId_organizationId_key\"", userId, organizationId)
    }
}
