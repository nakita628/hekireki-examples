package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class JobConfigurationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, JobConfigurationEntity>(JobConfigurationTable)

    var createdAt by JobConfigurationTable.createdAt
    var updatedAt by JobConfigurationTable.updatedAt
    var projectId by JobConfigurationTable.projectId
    var project by ProjectEntity referencedOn JobConfigurationTable.projectId
    var jobType by JobConfigurationTable.jobType
    var status by JobConfigurationTable.status
    var blockedAt by JobConfigurationTable.blockedAt
    var blockReason by JobConfigurationTable.blockReason
    var blockMessage by JobConfigurationTable.blockMessage
    var evalTemplateId by JobConfigurationTable.evalTemplateId
    var evalTemplate by EvalTemplateEntity optionalReferencedOn JobConfigurationTable.evalTemplateId
    var scoreName by JobConfigurationTable.scoreName
    var filter by JobConfigurationTable.filter
    var targetObject by JobConfigurationTable.targetObject
    var variableMapping by JobConfigurationTable.variableMapping
    var sampling by JobConfigurationTable.sampling
    var delay by JobConfigurationTable.delay
    var timeScope by JobConfigurationTable.timeScope

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(JobConfigurationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
