package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ProgramCoachTable : IdTable<String>("program_coaches") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val programId =
        reference(
            "programId",
            ProgramTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"program_coaches_programId_fkey\"",
        )
    val name = text("name")
    val image = text("image")
    val order = integer("order").default(0)
    override val primaryKey = PrimaryKey(id, name = "program_coaches_pkey")
}
