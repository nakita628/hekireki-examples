package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ProgramSessionTable : IdTable<String>("program_sessions") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val weekId =
        reference(
            "weekId",
            ProgramWeekTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"program_sessions_weekId_fkey\"",
        )
    val sessionNumber = integer("sessionNumber")
    val title = text("title")
    val titleEn = text("titleEn")
    val titleEs = text("titleEs")
    val titlePt = text("titlePt")
    val titleRu = text("titleRu")
    val titleZhCn = text("titleZhCn")
    val slug = text("slug")
    val slugEn = text("slugEn")
    val slugEs = text("slugEs")
    val slugPt = text("slugPt")
    val slugRu = text("slugRu")
    val slugZhCn = text("slugZhCn")
    val description = text("description")
    val descriptionEn = text("descriptionEn")
    val descriptionEs = text("descriptionEs")
    val descriptionPt = text("descriptionPt")
    val descriptionRu = text("descriptionRu")
    val descriptionZhCn = text("descriptionZhCn")
    val equipment =
        array(
            "equipment",
            PgEnumColumnType("\"ExerciseAttributeValueEnum\"", ExerciseAttributeValueEnum.entries, ExerciseAttributeValueEnum::dbName),
        ).nullable().databaseDefault("'{}'") { emptyList() }
    val estimatedMinutes = integer("estimatedMinutes")
    val isPremium = bool("isPremium").default(true)
    override val primaryKey = PrimaryKey(id, name = "program_sessions_pkey")

    init {
        uniqueIndex("\"program_sessions_weekId_sessionNumber_key\"", weekId, sessionNumber)
        uniqueIndex("\"program_sessions_weekId_slug_key\"", weekId, slug)
        uniqueIndex("\"program_sessions_weekId_slugEn_key\"", weekId, slugEn)
        uniqueIndex("\"program_sessions_weekId_slugEs_key\"", weekId, slugEs)
        uniqueIndex("\"program_sessions_weekId_slugPt_key\"", weekId, slugPt)
        uniqueIndex("\"program_sessions_weekId_slugRu_key\"", weekId, slugRu)
        uniqueIndex("\"program_sessions_weekId_slugZhCn_key\"", weekId, slugZhCn)
    }
}
