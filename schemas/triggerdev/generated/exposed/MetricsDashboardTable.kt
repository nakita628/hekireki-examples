package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MetricsDashboardTable : IdTable<String>("\"MetricsDashboard\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"MetricsDashboard_friendlyId_key\"")
    val title = text("title")
    val description = text("description").default("")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MetricsDashboard_organizationId_fkey\"",
        )
    val projectId =
        optReference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MetricsDashboard_projectId_fkey\"",
        )
    val ownerId =
        optReference(
            "ownerId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MetricsDashboard_ownerId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val layout = text("layout")
    override val primaryKey = PrimaryKey(id, name = "\"MetricsDashboard_pkey\"")

    init {
        index(
            "\"MetricsDashboard_projectId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"projectId\""), indexExpression("\"createdAt\" DESC")),
        )
    }
}
