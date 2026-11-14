package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ExperimentVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExperimentVersionEntity>(ExperimentVersionTable)

    var experimentId by ExperimentVersionTable.experimentId
    var projectId by ExperimentVersionTable.projectId
    var version by ExperimentVersionTable.version
    var counterVersion by ExperimentVersionTable.counterVersion
    var autoSaved by ExperimentVersionTable.autoSaved
    var commitMessage by ExperimentVersionTable.commitMessage
    var authorId by ExperimentVersionTable.authorId
    var authorLabel by ExperimentVersionTable.authorLabel
    var runId by ExperimentVersionTable.runId
    var state by ExperimentVersionTable.state
    var schemaVersion by ExperimentVersionTable.schemaVersion
    var createdAt by ExperimentVersionTable.createdAt
    var updatedAt by ExperimentVersionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExperimentVersionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
