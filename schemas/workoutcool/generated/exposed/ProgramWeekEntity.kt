package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProgramWeekEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProgramWeekEntity>(ProgramWeekTable)

    var programId by ProgramWeekTable.programId
    var weekNumber by ProgramWeekTable.weekNumber
    var title by ProgramWeekTable.title
    var titleEn by ProgramWeekTable.titleEn
    var titleEs by ProgramWeekTable.titleEs
    var titlePt by ProgramWeekTable.titlePt
    var titleRu by ProgramWeekTable.titleRu
    var titleZhCn by ProgramWeekTable.titleZhCn
    var description by ProgramWeekTable.description
    var descriptionEn by ProgramWeekTable.descriptionEn
    var descriptionEs by ProgramWeekTable.descriptionEs
    var descriptionPt by ProgramWeekTable.descriptionPt
    var descriptionRu by ProgramWeekTable.descriptionRu
    var descriptionZhCn by ProgramWeekTable.descriptionZhCn
    var program by ProgramEntity referencedOn ProgramWeekTable.programId
    val sessions by ProgramSessionEntity referrersOn ProgramSessionTable.weekId
}
