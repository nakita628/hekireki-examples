package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMTelemetryReportEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMTelemetryReportEntity>(LiteLLMTelemetryReportTable)

    var windowStart by LiteLLMTelemetryReportTable.windowStart
    var windowEnd by LiteLLMTelemetryReportTable.windowEnd
    var report by LiteLLMTelemetryReportTable.report
    var createdAt by LiteLLMTelemetryReportTable.createdAt
}
