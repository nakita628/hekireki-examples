package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserSessionProgressTable : IdTable<String>("user_session_progress") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val enrollmentId =
        reference(
            "enrollmentId",
            UserProgramEnrollmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"user_session_progress_enrollmentId_fkey\"",
        )
    val sessionId =
        reference(
            "sessionId",
            ProgramSessionTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"user_session_progress_sessionId_fkey\"",
        )
    val startedAt = pgTimestamp("startedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val workoutSessionId =
        optReference(
            "workoutSessionId",
            WorkoutSessionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"user_session_progress_workoutSessionId_fkey\"",
        ).uniqueIndex("\"user_session_progress_workoutSessionId_key\"")
    override val primaryKey = PrimaryKey(id, name = "user_session_progress_pkey")

    init {
        uniqueIndex("\"user_session_progress_enrollmentId_sessionId_key\"", enrollmentId, sessionId)
    }
}
