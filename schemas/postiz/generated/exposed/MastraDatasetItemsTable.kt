package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraDatasetItemsTable : CompositeIdTable("mastra_dataset_items") {
    val id1 = text("id").entityId()
    val datasetId = text("datasetId")
    val datasetVersion = integer("datasetVersion").entityId()
    val validTo = integer("validTo").nullable()
    val isDeleted = bool("isDeleted")
    val input = jsonb<String>("input", { it }, { it })
    val groundTruth = jsonb<String>("groundTruth", { it }, { it }).nullable()
    val requestContext = jsonb<String>("requestContext", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val source1 = jsonb<String>("source", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expectedTrajectory = jsonb<String>("expectedTrajectory", { it }, { it }).nullable()
    val organizationId = text("organizationId").nullable()
    val projectId = text("projectId").nullable()
    val toolMocks = jsonb<String>("toolMocks", { it }, { it }).nullable()
    val unmockedToolPolicy = text("unmockedToolPolicy").nullable()
    val scorerIds = jsonb<String>("scorerIds", { it }, { it }).nullable()
    val externalId = text("externalId").nullable()
    override val primaryKey = PrimaryKey(id1, datasetVersion, name = "mastra_dataset_items_pkey")

    init {
        index("idx_dataset_items_dataset_validto", false, datasetId, validTo)
        index("idx_dataset_items_dataset_validto_deleted", false, datasetId, validTo, isDeleted)
        index("idx_dataset_items_dataset_version", false, datasetId, datasetVersion)
        index("idx_dataset_items_external_id_history", false, datasetId, externalId, datasetVersion)
        index("idx_dataset_items_org_project", false, organizationId, projectId)
    }
}
