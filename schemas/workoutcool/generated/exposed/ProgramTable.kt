package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProgramTable : IdTable<String>("programs") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val slug = text("slug").uniqueIndex("programs_slug_key")
    val slugEn = text("slugEn").uniqueIndex("\"programs_slugEn_key\"")
    val slugEs = text("slugEs").uniqueIndex("\"programs_slugEs_key\"")
    val slugPt = text("slugPt").uniqueIndex("\"programs_slugPt_key\"")
    val slugRu = text("slugRu").uniqueIndex("\"programs_slugRu_key\"")
    val slugZhCn = text("slugZhCn").uniqueIndex("\"programs_slugZhCn_key\"")
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
    val category = text("category")
    val image = text("image")
    val level = pgEnum("level", "\"ProgramLevel\"", ProgramLevel.entries, ProgramLevel::dbName)
    val type = pgEnum("type", "\"ExerciseAttributeValueEnum\"", ExerciseAttributeValueEnum.entries, ExerciseAttributeValueEnum::dbName)
    val durationWeeks = integer("durationWeeks").default(4)
    val sessionsPerWeek = integer("sessionsPerWeek").default(3)
    val sessionDurationMin = integer("sessionDurationMin").default(30)
    val equipment =
        array(
            "equipment",
            PgEnumColumnType("\"ExerciseAttributeValueEnum\"", ExerciseAttributeValueEnum.entries, ExerciseAttributeValueEnum::dbName),
        ).nullable().databaseDefault("'{}'") { emptyList() }
    val isPremium = bool("isPremium").default(true)
    val isActive = bool("isActive").default(true)
    val visibility =
        pgEnum("visibility", "\"ProgramVisibility\"", ProgramVisibility.entries, ProgramVisibility::dbName).default(ProgramVisibility.DRAFT)
    val participantCount = integer("participantCount").default(0)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "programs_pkey")
}
