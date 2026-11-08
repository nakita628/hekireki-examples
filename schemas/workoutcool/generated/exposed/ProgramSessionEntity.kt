package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProgramSessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProgramSessionEntity>(ProgramSessionTable)

    var weekId by ProgramSessionTable.weekId
    var sessionNumber by ProgramSessionTable.sessionNumber
    var title by ProgramSessionTable.title
    var titleEn by ProgramSessionTable.titleEn
    var titleEs by ProgramSessionTable.titleEs
    var titlePt by ProgramSessionTable.titlePt
    var titleRu by ProgramSessionTable.titleRu
    var titleZhCn by ProgramSessionTable.titleZhCn
    var slug by ProgramSessionTable.slug
    var slugEn by ProgramSessionTable.slugEn
    var slugEs by ProgramSessionTable.slugEs
    var slugPt by ProgramSessionTable.slugPt
    var slugRu by ProgramSessionTable.slugRu
    var slugZhCn by ProgramSessionTable.slugZhCn
    var description by ProgramSessionTable.description
    var descriptionEn by ProgramSessionTable.descriptionEn
    var descriptionEs by ProgramSessionTable.descriptionEs
    var descriptionPt by ProgramSessionTable.descriptionPt
    var descriptionRu by ProgramSessionTable.descriptionRu
    var descriptionZhCn by ProgramSessionTable.descriptionZhCn
    var equipment by ProgramSessionTable.equipment
    var estimatedMinutes by ProgramSessionTable.estimatedMinutes
    var isPremium by ProgramSessionTable.isPremium
    var week by ProgramWeekEntity referencedOn ProgramSessionTable.weekId
    val exercises by ProgramSessionExerciseEntity referrersOn ProgramSessionExerciseTable.sessionId
    val userProgress by UserSessionProgressEntity referrersOn UserSessionProgressTable.sessionId
}
