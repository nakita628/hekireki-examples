package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserFavoriteExerciseTable : IdTable<String>("user_favorite_exercises") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"user_favorite_exercises_userId_fkey\"",
        )
    val exerciseId =
        reference(
            "exerciseId",
            ExerciseTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"user_favorite_exercises_exerciseId_fkey\"",
        )
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "user_favorite_exercises_pkey")

    init {
        uniqueIndex("\"user_favorite_exercises_userId_exerciseId_key\"", userId, exerciseId)
    }
}
