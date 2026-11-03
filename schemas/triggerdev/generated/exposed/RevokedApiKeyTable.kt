package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RevokedApiKeyTable : IdTable<String>("\"RevokedApiKey\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val apiKey = text("apiKey")
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RevokedApiKey_runtimeEnvironmentId_fkey\"",
        )
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"RevokedApiKey_pkey\"")

    init {
        index("\"RevokedApiKey_apiKey_idx\"", false, apiKey)
        index("\"RevokedApiKey_runtimeEnvironmentId_idx\"", false, runtimeEnvironmentId)
    }
}
