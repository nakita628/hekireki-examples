package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object WorkoutSetTable : IdTable<String>("workout_sets") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val workoutSessionExerciseId =
        reference(
            "workoutSessionExerciseId",
            WorkoutSessionExerciseTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workout_sets_workoutSessionExerciseId_fkey\"",
        )
    val setIndex = integer("setIndex")
    val type = pgEnum("type", "\"WorkoutSetType\"", WorkoutSetType.entries, WorkoutSetType::dbName)
    val types =
        array("types", PgEnumColumnType("\"WorkoutSetType\"", WorkoutSetType.entries, WorkoutSetType::dbName))
            .nullable()
            .databaseDefault("'{}'") { emptyList() }
    val valuesInt = array("valuesInt", IntegerColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val valuesSec = array("valuesSec", IntegerColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val units =
        array("units", PgEnumColumnType("\"WorkoutSetUnit\"", WorkoutSetUnit.entries, WorkoutSetUnit::dbName))
            .nullable()
            .databaseDefault("'{}'") { emptyList() }
    val completed = bool("completed").default(false)
    override val primaryKey = PrimaryKey(id, name = "workout_sets_pkey")
}
