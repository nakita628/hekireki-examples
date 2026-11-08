package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserSessionProgressEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserSessionProgressEntity>(UserSessionProgressTable)

    var enrollmentId by UserSessionProgressTable.enrollmentId
    var sessionId by UserSessionProgressTable.sessionId
    var startedAt by UserSessionProgressTable.startedAt
    var completedAt by UserSessionProgressTable.completedAt
    var workoutSessionId by UserSessionProgressTable.workoutSessionId
    var enrollment by UserProgramEnrollmentEntity referencedOn UserSessionProgressTable.enrollmentId
    var session by ProgramSessionEntity referencedOn UserSessionProgressTable.sessionId
    var workoutSession by WorkoutSessionEntity optionalReferencedOn UserSessionProgressTable.workoutSessionId
}
