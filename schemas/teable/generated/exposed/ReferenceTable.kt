package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ReferenceTable : IdTable<String>("reference") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val fromFieldId = text("from_field_id")
    val toFieldId = text("to_field_id")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "reference_pkey")

    init {
        index("reference_from_field_id_idx", false, fromFieldId)
        index("reference_to_field_id_idx", false, toFieldId)
        uniqueIndex("reference_to_field_id_from_field_id_key", toFieldId, fromFieldId)
    }
}
