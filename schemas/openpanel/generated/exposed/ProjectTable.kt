package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectTable : IdTable<String>("projects") {
    override val id = text("id").databaseDefault("gen_random_uuid()").entityId()
    val name = text("name")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"projects_organizationId_fkey\"",
        )
    val eventsCount = integer("eventsCount").default(0)
    val types =
        array("types", PgEnumColumnType("\"ProjectType\"", ProjectType.entries, ProjectType::dbName))
            .nullable()
            .databaseDefault("'{}'") { emptyList() }
    val domain = text("domain").nullable()
    val cors = array("cors", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val crossDomain = bool("crossDomain").default(false)
    val allowUnsafeRevenueTracking = bool("allowUnsafeRevenueTracking").default(false)
    val aiStoreContent = bool("aiStoreContent").default(true)
    val filters = jsonb<String>("filters", { it }, { it }).default("[]")
    val firstEventAt = pgTimestamp("firstEventAt", 3).nullable()
    val noDataNotifiedAt = pgTimestamp("noDataNotifiedAt", 3).nullable()
    val dataStoppedNotifiedAt = pgTimestamp("dataStoppedNotifiedAt", 3).nullable()
    val deleteAt = pgTimestamp("deleteAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "projects_pkey")
}
