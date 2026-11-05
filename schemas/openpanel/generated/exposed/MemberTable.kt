package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object MemberTable : IdTable<UUID>("members") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val role = text("role")
    val email = text("email")
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"members_userId_fkey\"",
        )
    val invitedById =
        optReference(
            "invitedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"members_invitedById_fkey\"",
        )
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"members_organizationId_fkey\"",
        )
    val projectScope = pgEnum("projectScope", "\"ProjectScope\"", ProjectScope.entries, ProjectScope::dbName).default(ProjectScope.ALL)
    val meta = jsonb<String>("meta", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "members_pkey")

    init {
        uniqueIndex("\"members_organizationId_userId_key\"", organizationId, userId)
    }
}
