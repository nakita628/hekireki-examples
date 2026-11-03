package papermark.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InvitationTable : IdTable<String>("\"Invitation\"") {
    val email = text("email")
    val expires = pgTimestamp("expires", 3)
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Invitation_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val id = text("token").entityId()
    val role = pgEnum("role", "\"Role\"", Role.entries, Role::dbName).default(Role.MEMBER)
    val dataroomIds = array("dataroomIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }

    init {
        uniqueIndex("\"Invitation_token_key\"", id)
        uniqueIndex("\"Invitation_email_teamId_key\"", email, teamId)
    }
}
