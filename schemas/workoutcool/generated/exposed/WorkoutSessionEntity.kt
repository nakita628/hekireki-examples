package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WorkoutSessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkoutSessionEntity>(WorkoutSessionTable)

    var userId by WorkoutSessionTable.userId
    var user by UserEntity referencedOn WorkoutSessionTable.userId
    var startedAt by WorkoutSessionTable.startedAt
    var endedAt by WorkoutSessionTable.endedAt
    var duration by WorkoutSessionTable.duration
    val exercises by WorkoutSessionExerciseEntity referrersOn WorkoutSessionExerciseTable.workoutSessionId
    var muscles by WorkoutSessionTable.muscles
    var rating by WorkoutSessionTable.rating
    var ratingComment by WorkoutSessionTable.ratingComment
    val userSessionProgress by UserSessionProgressEntity optionalBackReferencedOn UserSessionProgressTable.workoutSessionId
}
