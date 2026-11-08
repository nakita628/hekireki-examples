package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ProgramSuggestedSetTable : IdTable<String>("program_suggested_sets") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val programSessionExerciseId =
        reference(
            "programSessionExerciseId",
            ProgramSessionExerciseTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"program_suggested_sets_programSessionExerciseId_fkey\"",
        )
    val setIndex = integer("setIndex")
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
    override val primaryKey = PrimaryKey(id, name = "program_suggested_sets_pkey")

    init {
        uniqueIndex("\"program_suggested_sets_programSessionExerciseId_setIndex_key\"", programSessionExerciseId, setIndex)
    }
}
