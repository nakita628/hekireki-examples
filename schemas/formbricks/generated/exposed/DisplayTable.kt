package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DisplayTable : IdTable<String>("\"Display\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val surveyId =
        reference(
            "surveyId",
            SurveyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Display_surveyId_fkey\"",
        )
    val contactId =
        optReference(
            "contactId",
            ContactTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Display_contactId_fkey\"",
        )
    val creativeId = text("creativeId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Display_pkey\"")

    init {
        index("\"Display_surveyId_idx\"", false, surveyId)
        index("\"Display_contactId_created_at_idx\"", false, contactId, createdAt)
    }
}
