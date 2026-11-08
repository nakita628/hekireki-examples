package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProgramCoachEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProgramCoachEntity>(ProgramCoachTable)

    var programId by ProgramCoachTable.programId
    var name by ProgramCoachTable.name
    var image by ProgramCoachTable.image
    var order by ProgramCoachTable.order
    var program by ProgramEntity referencedOn ProgramCoachTable.programId
}
