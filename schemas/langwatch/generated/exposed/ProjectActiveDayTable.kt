package langwatch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectActiveDayTable : IdTable<String>("\"ProjectActiveDay\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId = text("projectId")
    val day = text("day")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ProjectActiveDay_pkey\"")

    init {
        uniqueIndex("\"ProjectActiveDay_projectId_day_key\"", projectId, day)
    }
}
