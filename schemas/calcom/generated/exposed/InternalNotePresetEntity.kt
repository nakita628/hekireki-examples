package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InternalNotePresetEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, InternalNotePresetEntity>(InternalNotePresetTable)

    var name by InternalNotePresetTable.name
    var cancellationReason by InternalNotePresetTable.cancellationReason
    var team by TeamEntity referencedOn InternalNotePresetTable.teamId
    var teamId by InternalNotePresetTable.teamId
    var createdAt by InternalNotePresetTable.createdAt
    val bookingInternalNote by BookingInternalNoteEntity optionalReferrersOn BookingInternalNoteTable.notePresetId
}
