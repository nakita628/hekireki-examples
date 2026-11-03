package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object CustomerQueryTable : IdTable<String>("\"CustomerQuery\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val query = text("query")
    val scope = pgEnum("scope", "\"CustomerQueryScope\"", CustomerQueryScope.entries, CustomerQueryScope::dbName)
    val stats = jsonb<String>("stats", { it }, { it })
    val title = text("title").nullable()
    val source1 =
        pgEnum("source", "\"CustomerQuerySource\"", CustomerQuerySource.entries, CustomerQuerySource::dbName).default(
            CustomerQuerySource.DASHBOARD,
        )
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CustomerQuery_organizationId_fkey\"",
        )
    val projectId =
        optReference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CustomerQuery_projectId_fkey\"",
        )
    val environmentId =
        optReference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CustomerQuery_environmentId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CustomerQuery_userId_fkey\"",
        )
    val filterPeriod = text("filterPeriod").nullable()
    val filterFrom = pgTimestamp("filterFrom", 3).nullable()
    val filterTo = pgTimestamp("filterTo", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"CustomerQuery_pkey\"")

    init {
        index(
            "\"CustomerQuery_organizationId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"organizationId\""), indexExpression("\"createdAt\" DESC")),
        )
        index("\"CustomerQuery_createdAt_idx\"", false, createdAt)
    }
}
