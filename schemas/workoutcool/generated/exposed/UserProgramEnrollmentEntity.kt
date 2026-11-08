package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserProgramEnrollmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserProgramEnrollmentEntity>(UserProgramEnrollmentTable)

    var userId by UserProgramEnrollmentTable.userId
    var programId by UserProgramEnrollmentTable.programId
    var enrolledAt by UserProgramEnrollmentTable.enrolledAt
    var currentWeek by UserProgramEnrollmentTable.currentWeek
    var currentSession by UserProgramEnrollmentTable.currentSession
    var completedSessions by UserProgramEnrollmentTable.completedSessions
    var isActive by UserProgramEnrollmentTable.isActive
    var completedAt by UserProgramEnrollmentTable.completedAt
    var user by UserEntity referencedOn UserProgramEnrollmentTable.userId
    var program by ProgramEntity referencedOn UserProgramEnrollmentTable.programId
    val sessionProgress by UserSessionProgressEntity referrersOn UserSessionProgressTable.enrollmentId
}
