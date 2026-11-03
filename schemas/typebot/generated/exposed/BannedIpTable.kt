package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BannedIpTable : IdTable<String>("\"BannedIp\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val ip = text("ip").uniqueIndex("\"BannedIp_ip_key\"")
    val responsibleTypebotId =
        reference(
            "responsibleTypebotId",
            TypebotTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BannedIp_responsibleTypebotId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BannedIp_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"BannedIp_pkey\"")
}
