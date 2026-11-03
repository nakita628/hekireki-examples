package typebot.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InvitationTable : CompositeIdTable("\"Invitation\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = text("email").entityId()
    val typebotId =
        reference(
            "typebotId",
            TypebotTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Invitation_typebotId_fkey\"",
        )
    val type = pgEnum("type", "\"CollaborationType\"", CollaborationType.entries, CollaborationType::dbName)

    init {
        addIdColumn(typebotId)
        uniqueIndex("\"Invitation_email_typebotId_key\"", email, typebotId)
    }
}
