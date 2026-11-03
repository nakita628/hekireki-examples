package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class HeatmapEventEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, HeatmapEventEntity>(HeatmapEventTable)

    var websiteId by HeatmapEventTable.websiteId
    var sessionId by HeatmapEventTable.sessionId
    var visitId by HeatmapEventTable.visitId
    var urlPath by HeatmapEventTable.urlPath
    var eventType by HeatmapEventTable.eventType
    var x by HeatmapEventTable.x
    var y by HeatmapEventTable.y
    var pageX by HeatmapEventTable.pageX
    var pageY by HeatmapEventTable.pageY
    var pageW by HeatmapEventTable.pageW
    var viewportW by HeatmapEventTable.viewportW
    var viewportH by HeatmapEventTable.viewportH
    var pageH by HeatmapEventTable.pageH
    var scrollPct by HeatmapEventTable.scrollPct
    var createdAt by HeatmapEventTable.createdAt
}
