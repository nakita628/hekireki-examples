package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RetentionNoticeTable : IdTable<String>("\"RetentionNotice\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionNotice_organizationId_fkey\"",
        )
    val entity = pgEnum("entity", "\"RetentionEntity\"", RetentionEntity.entries, RetentionEntity::dbName)
    val surveyId =
        optReference(
            "surveyId",
            SurveyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionNotice_surveyId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionNotice_userId_fkey\"",
        )
    val sentAt = pgTimestamp("sentAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deliveredAt = pgTimestamp("deliveredAt", 3).nullable()
    val emailSent = bool("emailSent").default(false)
    val claimToken = text("claimToken").nullable()
    val clockAt = pgTimestamp("clockAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"RetentionNotice_pkey\"")

    init {
        index("\"RetentionNotice_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"RetentionNotice_surveyId_entity_key\"", surveyId, entity)
        uniqueIndex("\"RetentionNotice_userId_organizationId_entity_key\"", userId, organizationId, entity)
    }
}
