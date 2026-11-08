package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ExerciseTable : IdTable<String>("exercises") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val nameEn = text("nameEn").nullable()
    val description = text("description").nullable()
    val descriptionEn = text("descriptionEn").nullable()
    val fullVideoUrl = text("fullVideoUrl").nullable()
    val fullVideoImageUrl = text("fullVideoImageUrl").nullable()
    val introduction = text("introduction").nullable()
    val introductionEn = text("introductionEn").nullable()
    val slug = text("slug").nullable().uniqueIndex("exercises_slug_key")
    val slugEn = text("slugEn").nullable().uniqueIndex("\"exercises_slugEn_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "exercises_pkey")
}
