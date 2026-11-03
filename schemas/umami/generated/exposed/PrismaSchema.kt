package umami.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> = emptyList()
    val tables: List<Table> =
        listOf(
            UserTable,
            ApiKeyTable,
            SessionTable,
            SessionLinkTable,
            WebsiteTable,
            WebsiteEventTable,
            EventDataTable,
            SessionDataTable,
            TeamTable,
            TeamUserTable,
            ReportTable,
            AnnotationTable,
            SegmentTable,
            RevenueTable,
            LinkTable,
            PixelTable,
            BoardTable,
            ShareTable,
            SessionReplayTable,
            SessionReplaySavedTable,
            HeatmapEventTable,
            TwoFactorAuthTable,
            TwoFactorBackupCodeTable,
            TwoFactorOtpUsedTable,
            TwoFactorRateLimitTable,
            AppSettingTable,
        )
}
