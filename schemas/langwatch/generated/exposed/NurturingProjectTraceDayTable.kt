package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object NurturingProjectTraceDayTable : CompositeIdTable("\"NurturingProjectTraceDay\"") {
    val projectId = text("projectId").entityId()
    val day = text("day").entityId()
    val traceCount = integer("traceCount").default(0)
    val errorCount = integer("errorCount").default(0)
    val lastTraceAt = pgTimestamp("lastTraceAt", 3).nullable()
    val lastFailedTraceAt = pgTimestamp("lastFailedTraceAt", 3).nullable()
    override val primaryKey = PrimaryKey(projectId, day, name = "\"NurturingProjectTraceDay_pkey\"")
}
