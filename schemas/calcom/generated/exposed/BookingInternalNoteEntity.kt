package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class BookingInternalNoteEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, BookingInternalNoteEntity>(BookingInternalNoteTable)

    var notePreset by InternalNotePresetEntity optionalReferencedOn BookingInternalNoteTable.notePresetId
    var notePresetId by BookingInternalNoteTable.notePresetId
    var text by BookingInternalNoteTable.text
    var booking by BookingEntity referencedOn BookingInternalNoteTable.bookingId
    var bookingId by BookingInternalNoteTable.bookingId
    var createdBy by UserEntity referencedOn BookingInternalNoteTable.createdById
    var createdById by BookingInternalNoteTable.createdById
    var createdAt by BookingInternalNoteTable.createdAt
}
