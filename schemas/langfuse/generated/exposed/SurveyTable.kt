package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SurveyTable : IdTable<String>("surveys") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val surveyName = pgEnum("survey_name", "\"SurveyName\"", SurveyName.entries, SurveyName::dbName)
    val response = jsonb<String>("response", { it }, { it })
    val userId =
        optReference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "surveys_user_id_fkey",
        )
    val userEmail = text("user_email").nullable()
    val orgId =
        optReference(
            "org_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "surveys_org_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "surveys_pkey")
}
