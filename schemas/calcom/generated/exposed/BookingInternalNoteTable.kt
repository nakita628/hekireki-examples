package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BookingInternalNoteTable : IdTable<Int>("\"BookingInternalNote\"") {
    override val id = integer("id").autoIncrement().entityId()
    val notePresetId =
        optReference(
            "notePresetId",
            InternalNotePresetTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingInternalNote_notePresetId_fkey\"",
        )
    val text = text("text").nullable()
    val bookingId =
        reference(
            "bookingId",
            BookingTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingInternalNote_bookingId_fkey\"",
        )
    val createdById =
        reference(
            "createdById",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingInternalNote_createdById_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"BookingInternalNote_pkey\"")

    init {
        index("\"BookingInternalNote_bookingId_idx\"", false, bookingId)
        uniqueIndex("\"BookingInternalNote_bookingId_notePresetId_key\"", bookingId, notePresetId)
    }
}
