package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ViewerTable : IdTable<String>("\"Viewer\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val email = text("email")
    val verified = bool("verified").default(false)
    val invitedAt = pgTimestamp("invitedAt", 3).nullable()
    val notificationPreferences = jsonb<String>("notificationPreferences", { it }, { it }).nullable()
    val dataroomId =
        optReference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Viewer_dataroomId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Viewer_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Viewer_pkey\"")

    init {
        index("\"Viewer_teamId_idx\"", false, teamId)
        index("\"Viewer_dataroomId_idx\"", false, dataroomId)
        uniqueIndex("\"Viewer_teamId_email_key\"", teamId, email)
    }
}
