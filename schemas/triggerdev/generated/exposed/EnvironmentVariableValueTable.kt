package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EnvironmentVariableValueTable : IdTable<String>("\"EnvironmentVariableValue\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val valueReferenceId =
        optReference(
            "valueReferenceId",
            SecretReferenceTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvironmentVariableValue_valueReferenceId_fkey\"",
        )
    val variableId =
        reference(
            "variableId",
            EnvironmentVariableTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvironmentVariableValue_variableId_fkey\"",
        )
    val environmentId =
        reference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvironmentVariableValue_environmentId_fkey\"",
        )
    val isSecret = bool("isSecret").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val version = integer("version").default(1)
    val lastUpdatedBy = jsonb<String>("lastUpdatedBy", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"EnvironmentVariableValue_pkey\"")

    init {
        index("\"EnvironmentVariableValue_environmentId_idx\"", false, environmentId)
        index("\"EnvironmentVariableValue_valueReferenceId_idx\"", false, valueReferenceId)
        uniqueIndex("\"EnvironmentVariableValue_variableId_environmentId_key\"", variableId, environmentId)
    }
}
