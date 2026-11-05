package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MemberTable : IdTable<String>("\"Member\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Member_organizationId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Member_emailAccountId_fkey\"",
        ).uniqueIndex("\"Member_emailAccountId_key\"")
    val role = text("role").default("member")
    val allowOrgAdminAnalytics = bool("allowOrgAdminAnalytics").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Member_pkey\"")

    init {
        uniqueIndex("\"Member_organizationId_emailAccountId_key\"", organizationId, emailAccountId)
    }
}
