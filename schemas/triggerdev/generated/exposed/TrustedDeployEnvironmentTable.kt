package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TrustedDeployEnvironmentTable : IdTable<String>("\"TrustedDeployEnvironment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val trustedDeployRepositoryId =
        reference(
            "trustedDeployRepositoryId",
            TrustedDeployRepositoryTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TrustedDeployEnvironment_trustedDeployRepositoryId_fkey\"",
        )
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TrustedDeployEnvironment_runtimeEnvironmentId_fkey\"",
        ).uniqueIndex("\"TrustedDeployEnvironment_runtimeEnvironmentId_key\"")
    val enabled = bool("enabled").default(false)
    val scope = pgEnum("scope", "\"TrustedDeployScope\"", TrustedDeployScope.entries, TrustedDeployScope::dbName)
    val scopeValue = text("scopeValue").nullable()
    val additionalClaims = jsonb<String>("additionalClaims", { it }, { it }).default("{}")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TrustedDeployEnvironment_pkey\"")

    init {
        index("\"TrustedDeployEnvironment_trustedDeployRepositoryId_idx\"", false, trustedDeployRepositoryId)
    }
}
