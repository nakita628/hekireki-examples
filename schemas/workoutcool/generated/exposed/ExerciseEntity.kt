package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ExerciseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExerciseEntity>(ExerciseTable)

    var name by ExerciseTable.name
    var nameEn by ExerciseTable.nameEn
    var description by ExerciseTable.description
    var descriptionEn by ExerciseTable.descriptionEn
    var fullVideoUrl by ExerciseTable.fullVideoUrl
    var fullVideoImageUrl by ExerciseTable.fullVideoImageUrl
    var introduction by ExerciseTable.introduction
    var introductionEn by ExerciseTable.introductionEn
    var slug by ExerciseTable.slug
    var slugEn by ExerciseTable.slugEn
    var createdAt by ExerciseTable.createdAt
    var updatedAt by ExerciseTable.updatedAt
    val attributes by ExerciseAttributeEntity referrersOn ExerciseAttributeTable.exerciseId
    val workoutSessionExercise by WorkoutSessionExerciseEntity referrersOn WorkoutSessionExerciseTable.exerciseId
    val programSessionExercise by ProgramSessionExerciseEntity referrersOn ProgramSessionExerciseTable.exerciseId
    val favoritesByUsers by UserFavoriteExerciseEntity referrersOn UserFavoriteExerciseTable.exerciseId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExerciseTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
