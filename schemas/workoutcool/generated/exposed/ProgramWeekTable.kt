package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ProgramWeekTable : IdTable<String>("program_weeks") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val programId =
        reference(
            "programId",
            ProgramTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"program_weeks_programId_fkey\"",
        )
    val weekNumber = integer("weekNumber")
    val title = text("title")
    val titleEn = text("titleEn")
    val titleEs = text("titleEs")
    val titlePt = text("titlePt")
    val titleRu = text("titleRu")
    val titleZhCn = text("titleZhCn")
    val description = text("description")
    val descriptionEn = text("descriptionEn")
    val descriptionEs = text("descriptionEs")
    val descriptionPt = text("descriptionPt")
    val descriptionRu = text("descriptionRu")
    val descriptionZhCn = text("descriptionZhCn")
    override val primaryKey = PrimaryKey(id, name = "program_weeks_pkey")

    init {
        uniqueIndex("\"program_weeks_programId_weekNumber_key\"", programId, weekNumber)
    }
}
