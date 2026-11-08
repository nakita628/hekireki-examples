package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserProgramEnrollmentTable : IdTable<String>("user_program_enrollments") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"user_program_enrollments_userId_fkey\"",
        )
    val programId =
        reference(
            "programId",
            ProgramTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"user_program_enrollments_programId_fkey\"",
        )
    val enrolledAt = pgTimestamp("enrolledAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val currentWeek = integer("currentWeek").default(1)
    val currentSession = integer("currentSession").default(1)
    val completedSessions = integer("completedSessions").default(0)
    val isActive = bool("isActive").default(true)
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "user_program_enrollments_pkey")

    init {
        uniqueIndex("\"user_program_enrollments_userId_programId_key\"", userId, programId)
    }
}
