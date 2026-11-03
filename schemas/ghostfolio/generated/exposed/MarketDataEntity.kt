package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MarketDataEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MarketDataEntity>(MarketDataTable)

    var createdAt by MarketDataTable.createdAt
    var dataSource by MarketDataTable.dataSource
    var date by MarketDataTable.date
    var isCarriedForward by MarketDataTable.isCarriedForward
    var marketPrice by MarketDataTable.marketPrice
    var state by MarketDataTable.state
    var symbol by MarketDataTable.symbol
}
