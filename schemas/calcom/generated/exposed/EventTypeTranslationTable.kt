package calcom.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EventTypeTranslationTable : IdTable<String>("\"EventTypeTranslation\"") {
    override val id = text("uid").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val eventTypeId =
        reference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventTypeTranslation_eventTypeId_fkey\"",
        )
    val field =
        pgEnum("field", "\"EventTypeAutoTranslatedField\"", EventTypeAutoTranslatedField.entries, EventTypeAutoTranslatedField::dbName)
    val sourceLocale = text("sourceLocale")
    val targetLocale = text("targetLocale")
    val translatedText = text("translatedText")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy =
        reference(
            "createdBy",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventTypeTranslation_createdBy_fkey\"",
        )
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy =
        optReference(
            "updatedBy",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventTypeTranslation_updatedBy_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"EventTypeTranslation_pkey\"")

    init {
        index("\"EventTypeTranslation_eventTypeId_field_targetLocale_idx\"", false, eventTypeId, field, targetLocale)
        uniqueIndex("\"EventTypeTranslation_eventTypeId_field_targetLocale_key\"", eventTypeId, field, targetLocale)
    }
}
