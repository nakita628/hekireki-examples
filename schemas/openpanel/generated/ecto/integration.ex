defmodule OpenPanel.Integration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          config: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: OpenPanel.Organization.t() | nil,
          project: OpenPanel.Project.t() | nil,
          notifications: [OpenPanel.Notification.t()],
          export_watermarks: [OpenPanel.ExportWatermark.t()],
          notification_rules: [OpenPanel.NotificationRule.t()]
        }

  schema "integrations" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    field(:config, :map)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:organization, OpenPanel.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:notifications, OpenPanel.Notification, foreign_key: :integration_id, references: :id)
    has_many(:export_watermarks, OpenPanel.ExportWatermark, foreign_key: :integration_id, references: :id)
    many_to_many(:notification_rules, OpenPanel.NotificationRule, join_through: "_IntegrationToNotificationRule", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end