package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RetentionPolicyTable : IdTable<String>("\"RetentionPolicy\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionPolicy_organizationId_fkey\"",
        )
    val entity = pgEnum("entity", "\"RetentionEntity\"", RetentionEntity.entries, RetentionEntity::dbName)
    val enabled = bool("enabled").default(false)
    val enabledAt = pgTimestamp("enabledAt", 3).nullable()
    val warnDays = integer("warnDays").default(60)
    val periodDays = integer("periodDays")
    val conditions =
        array(
            "conditions",
            PgEnumColumnType("\"RetentionSurveyCondition\"", RetentionSurveyCondition.entries, RetentionSurveyCondition::dbName),
        ).nullable().databaseDefault("'{}'") { emptyList() }
    val deferredAt = pgTimestamp("deferredAt", 3).nullable()
    val updatedById =
        optReference(
            "updatedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionPolicy_updatedById_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"RetentionPolicy_pkey\"")

    init {
        index("\"RetentionPolicy_updatedById_idx\"", false, updatedById)
        uniqueIndex("\"RetentionPolicy_organizationId_entity_key\"", organizationId, entity)
    }
}
