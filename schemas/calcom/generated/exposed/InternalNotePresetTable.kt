package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InternalNotePresetTable : IdTable<Int>("\"InternalNotePreset\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name")
    val cancellationReason = text("cancellationReason").nullable()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"InternalNotePreset_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"InternalNotePreset_pkey\"")

    init {
        index("\"InternalNotePreset_teamId_idx\"", false, teamId)
        uniqueIndex("\"InternalNotePreset_teamId_name_key\"", teamId, name)
    }
}
