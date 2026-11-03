package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TaskEventEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskEventEntity>(TaskEventTable)

    var message by TaskEventTable.message
    var traceId by TaskEventTable.traceId
    var spanId by TaskEventTable.spanId
    var parentId by TaskEventTable.parentId
    var tracestate by TaskEventTable.tracestate
    var isError by TaskEventTable.isError
    var isPartial by TaskEventTable.isPartial
    var isCancelled by TaskEventTable.isCancelled
    var isDebug by TaskEventTable.isDebug
    var serviceName by TaskEventTable.serviceName
    var serviceNamespace by TaskEventTable.serviceNamespace
    var level by TaskEventTable.level
    var kind by TaskEventTable.kind
    var status by TaskEventTable.status
    var links by TaskEventTable.links
    var events by TaskEventTable.events
    var startTime by TaskEventTable.startTime
    var duration by TaskEventTable.duration
    var attemptId by TaskEventTable.attemptId
    var attemptNumber by TaskEventTable.attemptNumber
    var environmentId by TaskEventTable.environmentId
    var environmentType by TaskEventTable.environmentType
    var organizationId by TaskEventTable.organizationId
    var projectId by TaskEventTable.projectId
    var projectRef by TaskEventTable.projectRef
    var runId by TaskEventTable.runId
    var runIsTest by TaskEventTable.runIsTest
    var idempotencyKey by TaskEventTable.idempotencyKey
    var taskSlug by TaskEventTable.taskSlug
    var taskPath by TaskEventTable.taskPath
    var taskExportName by TaskEventTable.taskExportName
    var workerId by TaskEventTable.workerId
    var workerVersion by TaskEventTable.workerVersion
    var queueId by TaskEventTable.queueId
    var queueName by TaskEventTable.queueName
    var batchId by TaskEventTable.batchId
    var properties by TaskEventTable.properties
    var metadata by TaskEventTable.metadata
    var style by TaskEventTable.style
    var output by TaskEventTable.output
    var outputType by TaskEventTable.outputType
    var payload by TaskEventTable.payload
    var payloadType by TaskEventTable.payloadType
    var createdAt by TaskEventTable.createdAt
    var usageDurationMs by TaskEventTable.usageDurationMs
    var usageCostInCents by TaskEventTable.usageCostInCents
    var machinePreset by TaskEventTable.machinePreset
    var machinePresetCpu by TaskEventTable.machinePresetCpu
    var machinePresetMemory by TaskEventTable.machinePresetMemory
    var machinePresetCentsPerMs by TaskEventTable.machinePresetCentsPerMs
}
