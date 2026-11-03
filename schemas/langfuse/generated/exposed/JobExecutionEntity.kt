package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class JobExecutionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, JobExecutionEntity>(JobExecutionTable)

    var createdAt by JobExecutionTable.createdAt
    var updatedAt by JobExecutionTable.updatedAt
    var projectId by JobExecutionTable.projectId
    var project by ProjectEntity referencedOn JobExecutionTable.projectId
    var jobConfigurationId by JobExecutionTable.jobConfigurationId
    var jobTemplateId by JobExecutionTable.jobTemplateId
    var status by JobExecutionTable.status
    var startTime by JobExecutionTable.startTime
    var endTime by JobExecutionTable.endTime
    var error by JobExecutionTable.error
    var jobInputTraceId by JobExecutionTable.jobInputTraceId
    var jobInputTraceTimestamp by JobExecutionTable.jobInputTraceTimestamp
    var jobInputObservationId by JobExecutionTable.jobInputObservationId
    var jobInputDatasetItemId by JobExecutionTable.jobInputDatasetItemId
    var jobInputDatasetItemValidFrom by JobExecutionTable.jobInputDatasetItemValidFrom
    var jobOutputScoreId by JobExecutionTable.jobOutputScoreId
    var executionTraceId by JobExecutionTable.executionTraceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(JobExecutionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
