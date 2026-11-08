package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProgramEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProgramEntity>(ProgramTable)

    var slug by ProgramTable.slug
    var slugEn by ProgramTable.slugEn
    var slugEs by ProgramTable.slugEs
    var slugPt by ProgramTable.slugPt
    var slugRu by ProgramTable.slugRu
    var slugZhCn by ProgramTable.slugZhCn
    var title by ProgramTable.title
    var titleEn by ProgramTable.titleEn
    var titleEs by ProgramTable.titleEs
    var titlePt by ProgramTable.titlePt
    var titleRu by ProgramTable.titleRu
    var titleZhCn by ProgramTable.titleZhCn
    var description by ProgramTable.description
    var descriptionEn by ProgramTable.descriptionEn
    var descriptionEs by ProgramTable.descriptionEs
    var descriptionPt by ProgramTable.descriptionPt
    var descriptionRu by ProgramTable.descriptionRu
    var descriptionZhCn by ProgramTable.descriptionZhCn
    var category by ProgramTable.category
    var image by ProgramTable.image
    var level by ProgramTable.level
    var type by ProgramTable.type
    var durationWeeks by ProgramTable.durationWeeks
    var sessionsPerWeek by ProgramTable.sessionsPerWeek
    var sessionDurationMin by ProgramTable.sessionDurationMin
    var equipment by ProgramTable.equipment
    var isPremium by ProgramTable.isPremium
    var isActive by ProgramTable.isActive
    var visibility by ProgramTable.visibility
    var participantCount by ProgramTable.participantCount
    var createdAt by ProgramTable.createdAt
    var updatedAt by ProgramTable.updatedAt
    val weeks by ProgramWeekEntity referrersOn ProgramWeekTable.programId
    val enrollments by UserProgramEnrollmentEntity referrersOn UserProgramEnrollmentTable.programId
    val coaches by ProgramCoachEntity referrersOn ProgramCoachTable.programId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProgramTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
