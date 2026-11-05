package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class InsightEventEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, InsightEventEntity>(InsightEventTable)

    var insightId by InsightEventTable.insightId
    var insight by ProjectInsightEntity referencedOn InsightEventTable.insightId
    var eventKind by InsightEventTable.eventKind
    var changeFrom by InsightEventTable.changeFrom
    var changeTo by InsightEventTable.changeTo
    var createdAt by InsightEventTable.createdAt
}
