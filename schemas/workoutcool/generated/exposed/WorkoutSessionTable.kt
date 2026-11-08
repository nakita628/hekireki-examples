package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object WorkoutSessionTable : IdTable<String>("workout_sessions") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workout_sessions_userId_fkey\"",
        )
    val startedAt = pgTimestamp("startedAt", 3)
    val endedAt = pgTimestamp("endedAt", 3).nullable()
    val duration = integer("duration").nullable()
    val muscles =
        array(
            "muscles",
            PgEnumColumnType("\"ExerciseAttributeValueEnum\"", ExerciseAttributeValueEnum.entries, ExerciseAttributeValueEnum::dbName),
        ).nullable().databaseDefault("'{}'") { emptyList() }
    val rating = integer("rating").nullable()
    val ratingComment = text("ratingComment").nullable()
    override val primaryKey = PrimaryKey(id, name = "workout_sessions_pkey")
}
