package openpanel.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> =
        listOf(
            "CREATE TYPE \"ProjectType\" AS ENUM ('website', 'app', 'backend')",
            "CREATE TYPE \"ProjectScope\" AS ENUM ('all', 'selected')",
            "CREATE TYPE \"AccessLevel\" AS ENUM ('read', 'write', 'admin')",
            "CREATE TYPE \"ClientType\" AS ENUM ('read', 'write', 'root')",
            "CREATE TYPE \"Interval\" AS ENUM ('hour', 'day', 'month', 'minute', 'week')",
            "CREATE TYPE \"ChartType\" AS ENUM ('linear', 'bar', 'histogram', 'pie', 'metric', 'area', 'map', 'funnel', 'retention', 'conversion', 'sankey')",
            "CREATE TYPE \"Metric\" AS ENUM ('sum', 'average', 'min', 'max', 'count')",
            "CREATE TYPE \"IntegrationType\" AS ENUM ('app', 'mail', 'custom')",
            "CREATE TYPE \"ImportStatus\" AS ENUM ('pending', 'processing', 'completed', 'failed')",
            "CREATE TYPE \"InsightState\" AS ENUM ('active', 'suppressed', 'closed')",
        )
    val tables: List<Table> =
        listOf(
            CodeMigrationTable,
            ConversationTable,
            ChatMessageTable,
            OrganizationTable,
            UserTable,
            UserTotpTable,
            TwoFactorTable,
            TwoFactorChallengeTable,
            AccountTable,
            SessionTable,
            VerificationTable,
            MemberTable,
            MemberProjectTable,
            InviteTable,
            ProjectTable,
            CohortTable,
            SaltTable,
            ClientTable,
            DashboardTable,
            ReportTable,
            ReportLayoutTable,
            ShareOverviewTable,
            ShareDashboardTable,
            ShareReportTable,
            ShareWidgetTable,
            EventMetaTable,
            CustomEventTable,
            ReferenceTable,
            NotificationRuleTable,
            NotificationTable,
            IntegrationTable,
            ExportWatermarkTable,
            ResetPasswordTable,
            ImportTable,
            ProjectInsightTable,
            InsightEventTable,
            GscConnectionTable,
            EmailUnsubscribeTable,
            IntegrationToNotificationRuleTable,
        )
}
