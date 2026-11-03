package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object InstalledIntegrationTable : IdTable<String>("\"InstalledIntegration\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val credentials = jsonb<String>("credentials", { it }, { it }).nullable()
    val configuration = jsonb<String>("configuration", { it }, { it }).nullable()
    val enabled = bool("enabled").default(true)
    val integrationId =
        reference(
            "integrationId",
            IntegrationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"InstalledIntegration_integrationId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"InstalledIntegration_userId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"InstalledIntegration_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"InstalledIntegration_pkey\"")

    init {
        index("\"InstalledIntegration_teamId_idx\"", false, teamId)
        index("\"InstalledIntegration_integrationId_idx\"", false, integrationId)
        uniqueIndex("\"InstalledIntegration_teamId_integrationId_key\"", teamId, integrationId)
    }
}
