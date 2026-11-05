defmodule OpenPanel.Project do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          events_count: integer(),
          types: atom(),
          domain: String.t() | nil,
          cors: [String.t()],
          cross_domain: boolean(),
          allow_unsafe_revenue_tracking: boolean(),
          ai_store_content: boolean(),
          filters: map(),
          first_event_at: DateTime.t() | nil,
          no_data_notified_at: DateTime.t() | nil,
          data_stopped_notified_at: DateTime.t() | nil,
          delete_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: OpenPanel.Organization.t() | nil,
          share: OpenPanel.ShareOverview.t() | nil,
          gsc_connection: OpenPanel.GscConnection.t() | nil,
          clients: [OpenPanel.Client.t()],
          reports: [OpenPanel.Report.t()],
          dashboards: [OpenPanel.Dashboard.t()],
          share_dashboards: [OpenPanel.ShareDashboard.t()],
          share_reports: [OpenPanel.ShareReport.t()],
          share_widgets: [OpenPanel.ShareWidget.t()],
          meta: [OpenPanel.EventMeta.t()],
          custom_events: [OpenPanel.CustomEvent.t()],
          references: [OpenPanel.Reference.t()],
          member_projects: [OpenPanel.MemberProject.t()],
          notification_rules: [OpenPanel.NotificationRule.t()],
          notifications: [OpenPanel.Notification.t()],
          imports: [OpenPanel.Import.t()],
          cohorts: [OpenPanel.Cohort.t()],
          export_watermarks: [OpenPanel.ExportWatermark.t()],
          integrations: [OpenPanel.Integration.t()],
          conversations: [OpenPanel.Conversation.t()]
        }

  schema "projects" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    field(:events_count, :integer, default: 0, source: :eventsCount)
    field(:types, {:array, Ecto.Enum}, values: [:website, :app, :backend])
    field(:domain, :string)
    field(:cors, {:array, :string})
    field(:cross_domain, :boolean, default: false, source: :crossDomain)
    field(:allow_unsafe_revenue_tracking, :boolean, default: false, source: :allowUnsafeRevenueTracking)
    field(:ai_store_content, :boolean, default: true, source: :aiStoreContent)
    field(:filters, :map)
    field(:first_event_at, OpenPanel.PrismaDateTime, source: :firstEventAt)
    field(:no_data_notified_at, OpenPanel.PrismaDateTime, source: :noDataNotifiedAt)
    field(:data_stopped_notified_at, OpenPanel.PrismaDateTime, source: :dataStoppedNotifiedAt)
    field(:delete_at, OpenPanel.PrismaDateTime, source: :deleteAt)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, OpenPanel.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_one(:share, OpenPanel.ShareOverview, foreign_key: :project_id, references: :id)
    has_one(:gsc_connection, OpenPanel.GscConnection, foreign_key: :project_id, references: :id)
    has_many(:clients, OpenPanel.Client, foreign_key: :project_id, references: :id)
    has_many(:reports, OpenPanel.Report, foreign_key: :project_id, references: :id)
    has_many(:dashboards, OpenPanel.Dashboard, foreign_key: :project_id, references: :id)
    has_many(:share_dashboards, OpenPanel.ShareDashboard, foreign_key: :project_id, references: :id)
    has_many(:share_reports, OpenPanel.ShareReport, foreign_key: :project_id, references: :id)
    has_many(:share_widgets, OpenPanel.ShareWidget, foreign_key: :project_id, references: :id)
    has_many(:meta, OpenPanel.EventMeta, foreign_key: :project_id, references: :id)
    has_many(:custom_events, OpenPanel.CustomEvent, foreign_key: :project_id, references: :id)
    has_many(:references, OpenPanel.Reference, foreign_key: :project_id, references: :id)
    has_many(:member_projects, OpenPanel.MemberProject, foreign_key: :project_id, references: :id)
    has_many(:notification_rules, OpenPanel.NotificationRule, foreign_key: :project_id, references: :id)
    has_many(:notifications, OpenPanel.Notification, foreign_key: :project_id, references: :id)
    has_many(:imports, OpenPanel.Import, foreign_key: :project_id, references: :id)
    has_many(:cohorts, OpenPanel.Cohort, foreign_key: :project_id, references: :id)
    has_many(:export_watermarks, OpenPanel.ExportWatermark, foreign_key: :project_id, references: :id)
    has_many(:integrations, OpenPanel.Integration, foreign_key: :project_id, references: :id)
    has_many(:conversations, OpenPanel.Conversation, foreign_key: :project_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end