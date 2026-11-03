package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectAlertChannelTable : IdTable<String>("\"ProjectAlertChannel\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"ProjectAlertChannel_friendlyId_key\"")
    val deduplicationKey = text("deduplicationKey").clientDefault { CUID.randomCUID1().toString() }
    val userProvidedDeduplicationKey = bool("userProvidedDeduplicationKey").default(false)
    val integrationId =
        optReference(
            "integrationId",
            OrganizationIntegrationTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProjectAlertChannel_integrationId_fkey\"",
        )
    val enabled = bool("enabled").default(true)
    val type = pgEnum("type", "\"ProjectAlertChannelType\"", ProjectAlertChannelType.entries, ProjectAlertChannelType::dbName)
    val name = text("name")
    val properties = jsonb<String>("properties", { it }, { it })
    val alertTypes =
        array("alertTypes", PgEnumColumnType("\"ProjectAlertType\"", ProjectAlertType.entries, ProjectAlertType::dbName)).nullable()
    val environmentTypes =
        array(
            "environmentTypes",
            PgEnumColumnType("\"RuntimeEnvironmentType\"", RuntimeEnvironmentType.entries, RuntimeEnvironmentType::dbName),
        ).nullable()
            .databaseDefault("'{\"STAGING\",\"PRODUCTION\"}'") { listOf(RuntimeEnvironmentType.STAGING, RuntimeEnvironmentType.PRODUCTION) }
    val errorAlertConfig = jsonb<String>("errorAlertConfig", { it }, { it }).nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProjectAlertChannel_projectId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ProjectAlertChannel_pkey\"")

    init {
        uniqueIndex("\"ProjectAlertChannel_projectId_deduplicationKey_key\"", projectId, deduplicationKey)
    }
}
