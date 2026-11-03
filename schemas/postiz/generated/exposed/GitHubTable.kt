package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object GitHubTable : IdTable<String>("\"GitHub\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val login = text("login").nullable()
    val name = text("name").nullable()
    val token = text("token")
    val jobId = text("jobId").nullable()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"GitHub_organizationId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GitHub_pkey\"")

    init {
        index("\"GitHub_login_idx\"", false, login)
        index("\"GitHub_organizationId_idx\"", false, organizationId)
    }
}
