package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class MastraDatasetItemsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MastraDatasetItemsEntity>(MastraDatasetItemsTable)

    val id1 by MastraDatasetItemsTable.id1
    var datasetId by MastraDatasetItemsTable.datasetId
    val datasetVersion by MastraDatasetItemsTable.datasetVersion
    var validTo by MastraDatasetItemsTable.validTo
    var isDeleted by MastraDatasetItemsTable.isDeleted
    var input by MastraDatasetItemsTable.input
    var groundTruth by MastraDatasetItemsTable.groundTruth
    var requestContext by MastraDatasetItemsTable.requestContext
    var metadata by MastraDatasetItemsTable.metadata
    var source by MastraDatasetItemsTable.source1
    var createdAt by MastraDatasetItemsTable.createdAt
    var updatedAt by MastraDatasetItemsTable.updatedAt
    var createdAtZ by MastraDatasetItemsTable.createdAtZ
    var updatedAtZ by MastraDatasetItemsTable.updatedAtZ
    var expectedTrajectory by MastraDatasetItemsTable.expectedTrajectory
    var organizationId by MastraDatasetItemsTable.organizationId
    var projectId by MastraDatasetItemsTable.projectId
    var toolMocks by MastraDatasetItemsTable.toolMocks
    var unmockedToolPolicy by MastraDatasetItemsTable.unmockedToolPolicy
    var scorerIds by MastraDatasetItemsTable.scorerIds
    var externalId by MastraDatasetItemsTable.externalId
}
