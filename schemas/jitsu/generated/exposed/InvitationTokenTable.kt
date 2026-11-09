package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InvitationTokenTable : IdTable<String>("\"InvitationToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val workspaceId = text("workspaceId")
    val email = text("email")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val token = text("token")
    val usedBy = text("usedBy").nullable()
    val role = pgEnum("role", "\"WorkspaceRole\"", WorkspaceRole.entries, WorkspaceRole::dbName).default(WorkspaceRole.OWNER)
    override val primaryKey = PrimaryKey(id, name = "\"InvitationToken_pkey\"")
}
