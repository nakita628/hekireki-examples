package teable.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class TableQueryObservationShardEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<TableQueryObservationShardEntity>(TableQueryObservationShardTable)

    var spaceId by TableQueryObservationShardTable.spaceId
    var baseId by TableQueryObservationShardTable.baseId
    val tableId by TableQueryObservationShardTable.tableId
    val queryKind by TableQueryObservationShardTable.queryKind
    val shapeHash by TableQueryObservationShardTable.shapeHash
    val windowStart by TableQueryObservationShardTable.windowStart
    val writerId by TableQueryObservationShardTable.writerId
    var windowSizeSeconds by TableQueryObservationShardTable.windowSizeSeconds
    var requestCount by TableQueryObservationShardTable.requestCount
    var slowCount by TableQueryObservationShardTable.slowCount
    var timeoutCount by TableQueryObservationShardTable.timeoutCount
    var dbErrorCount by TableQueryObservationShardTable.dbErrorCount
    var totalDurationMs by TableQueryObservationShardTable.totalDurationMs
    var maxDurationMs by TableQueryObservationShardTable.maxDurationMs
    var totalDbDurationMs by TableQueryObservationShardTable.totalDbDurationMs
    var maxDbDurationMs by TableQueryObservationShardTable.maxDbDurationMs
    var shape by TableQueryObservationShardTable.shape
    var sqlDiagnostics by TableQueryObservationShardTable.sqlDiagnostics
    var createdTime by TableQueryObservationShardTable.createdTime
    var lastModifiedTime by TableQueryObservationShardTable.lastModifiedTime
}
