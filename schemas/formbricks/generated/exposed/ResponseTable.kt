package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ResponseTable : IdTable<String>("\"Response\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val finished = bool("finished").default(false)
    val surveyId =
        reference(
            "surveyId",
            SurveyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Response_surveyId_fkey\"",
        )
    val contactId =
        optReference(
            "contactId",
            ContactTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Response_contactId_fkey\"",
        )
    val endingId = text("endingId").nullable()
    val data = jsonb<String>("data", { it }, { it }).default("{}")
    val variables = jsonb<String>("variables", { it }, { it }).default("{}")
    val ttc = jsonb<String>("ttc", { it }, { it }).default("{}")
    val meta = jsonb<String>("meta", { it }, { it }).default("{}")
    val ingestFlags = jsonb<String>("ingest_flags", { it }, { it }).nullable()
    val contactAttributes = jsonb<String>("contactAttributes", { it }, { it }).nullable()
    val singleUseId = text("singleUseId").nullable()
    val language = text("language").nullable()
    val displayId =
        optReference(
            "displayId",
            DisplayTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Response_displayId_fkey\"",
        ).uniqueIndex("\"Response_displayId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"Response_pkey\"")

    init {
        index("\"Response_created_at_idx\"", false, createdAt)
        index("\"Response_created_at_id_idx\"", false, createdAt, id)
        index("\"Response_surveyId_created_at_idx\"", false, surveyId, createdAt)
        index("\"Response_surveyId_created_at_id_idx\"", false, surveyId, createdAt, id)
        index("\"Response_contactId_created_at_idx\"", false, contactId, createdAt)
        index("\"Response_contactId_created_at_id_idx\"", false, contactId, createdAt, id)
        index("\"Response_surveyId_finished_idx\"", false, surveyId, finished)
        uniqueIndex("\"Response_surveyId_singleUseId_key\"", surveyId, singleUseId)
    }
}
